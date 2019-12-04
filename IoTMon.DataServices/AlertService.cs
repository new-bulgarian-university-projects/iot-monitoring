using IoTMon.Data;
using IoTMon.DataServices.Contracts;
using IoTMon.Models;
using IoTMon.Models.AMQP;
using IoTMon.Models.DTO;
using IoTMon.Models.Enums;
using IoTMon.Models.Notifiers;
using IoTMon.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.DataServices
{
    public class AlertService : IAlertService, IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IAlertNotifier alertNotifier;

        public AlertService(ApplicationDbContext dbContext, IAlertNotifier alertNotifier)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.alertNotifier = alertNotifier ?? throw new ArgumentNullException(nameof(alertNotifier));
        }
        public class SensorDeviceTemp
        {
            public Guid SensorId { get; set; }
            public Guid DeviceId { get; set; }
            public string DeviceName { get; set; }
            public string SensorName { get; set; }

            public double? MinValue { get; set; }
            public double? MaxValue { get; set; }
        }
        public async Task<AlertRecordDTO> GetAlerts(Guid deviceId, string sensorName)
        {
            SensorDeviceTemp sdInfo = new SensorDeviceTemp();
            var deviceIdParam = new SqlParameter("@deviceId", deviceId);
            var sensorIdParam = new SqlParameter("@sensorName", sensorName);
            var sql = @"
DECLARE @sensorId nvarchar(100);
SET @sensorId = (SELECT Id FROM Sensors WHERE Label = @sensorName);

SELECT s.Id as SensorId, d.Id as DeviceId, d.DeviceName, s.FriendlyLabel as SensorName, 
		ds.MinValue as MinValue, ds.MaxValue as MaxValue
FROM DeviceSensor ds
INNER JOIN Devices d
ON ds.DeviceId = d.Id
INNER JOIN Sensors s
ON ds.SensorId = s.Id
WHERE (ds.DeviceId = @deviceId AND ds.SensorId = @sensorId)

SELECT a.TriggeringValue as Value, a.AlertType, a.AlertStarted as Started, a.AlertClosed as Closed
FROM Alerts a
WHERE (a.DeviceId = @deviceId AND a.SensorId = @sensorId)
ORDER BY a.AlertStarted DESC
";

            var alertsHistory = new List<AlertHistoryDTO>();

            using (var command = this.dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(new SqlParameter[] { deviceIdParam, sensorIdParam });

                await this.dbContext.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {

                        while (await reader.ReadAsync())
                        {
                            sdInfo.DeviceId = (Guid)reader["DeviceId"];
                            sdInfo.SensorId = (Guid)reader["SensorId"];
                            sdInfo.DeviceName = reader["DeviceName"] as string;
                            sdInfo.SensorName = reader["SensorName"] as string;
                            sdInfo.MinValue = reader["MinValue"] as double?;
                            sdInfo.MaxValue = reader["MaxValue"] as double?;
                        }
                    }

                    await reader.NextResultAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var history = new AlertHistoryDTO()
                            {
                                AlertType = Enum.Parse<AlertTypeEnum>(reader["AlertType"] as string),
                                Value = reader["Value"] as string,
                                Started = (DateTime)reader["Started"],
                                Closed = (DateTime?)reader["Closed"]
                            };
                            alertsHistory.Add(history);
                        }
                    }
                }
            }

            var res = new AlertRecordDTO
            {
                DeviceId = sdInfo.DeviceId,
                DeviceName = sdInfo.DeviceName,
                SensorName = sdInfo.SensorName,
                AlertHistory = alertsHistory,
                MinValue = sdInfo.MinValue,
                MaxValue = sdInfo.MaxValue
            };

            return res;
        }

        public async Task CheckAlerts(Message message)
        {
            var sensorId = this.dbContext.Sensors.First(s => s.Label == message.Sensor).Id;

            var deviceSensor = this.dbContext.Devices
                .Include(d => d.DeviceSensors)
                .Include(d => d.User)
                .First(d => d.Id == new Guid(message.DeviceId))
                .DeviceSensors
                .FirstOrDefault(ds => ds.SensorId == sensorId &&
                                ds.DeviceId == new Guid(message.DeviceId));

            if (deviceSensor.IsNotificationOn == false)
            {
                return;
            }

            double? min = deviceSensor?.MinValue;
            double? max = deviceSensor?.MaxValue;

            if (!min.HasValue && !max.HasValue)
            {
                //Console.WriteLine($"No alerts for {message.DeviceId} / {message.Sensor}");
                return;
            }

            if (max.HasValue)
            {
                var lastAlert = this.dbContext.Alerts
                    .OrderByDescending(a => a.AlertStarted)
                    .FirstOrDefault(a => a.DeviceId == new Guid(message.DeviceId) &&
                                a.SensorId == sensorId &&
                                a.AlertClosed == null);

                if (max.Value < Convert.ToDouble(message.Value))
                {
                    if (lastAlert == null)
                    {
                        var newAlert = new Alert()
                        {
                            Id = Guid.NewGuid(),
                            AlertStarted = message.Time,
                            AlertType = AlertTypeEnum.Above_Max_Value,
                            DeviceId = new Guid(message.DeviceId),
                            SensorId = this.dbContext.Sensors.First(s => s.Label == message.Sensor).Id,
                            TriggeringValue = message.Value.ToString(),
                            ValueType = Enum.Parse<ValueTypeEnum>(message.ValueType)
                        };

                        this.dbContext.Alerts.Add(newAlert);
                        this.dbContext.SaveChanges();
                        Console.WriteLine($"     - !!! Alert {newAlert.AlertType} / {newAlert.TriggeringValue} !!!");

                        // send email
                        var emailProps = new EmailAlert()
                        {
                            ReceiverEmail = deviceSensor.Device.User.Email,
                            ReceiverName = deviceSensor.Device.User.FirstName,
                            Subject = $"[Alert] {message.Sensor} {newAlert.AlertType}",
                            HtmlContent = $"<strong>[{message.Time}]:</strong> <br>Hello {deviceSensor.Device.User.FirstName},<br><br>" +
                            $" <strong>DeivceId :</strong>{message.DeviceId} <br> <strong>Sensor: </strong> {message.Sensor} <br>" +
                            $" Sensor's value <strong>{message.Value}</strong> triggered <i>{newAlert.AlertType} - {(newAlert.AlertType == AlertTypeEnum.Above_Max_Value ? max : min)}</i>"
                        };

                        await this.alertNotifier.Execute(emailProps);
                        Console.WriteLine($" == Email sent to {emailProps.ReceiverEmail}");
                    }
                }
                else
                {
                    if (lastAlert != null)
                    {

                        lastAlert.AlertClosed = message.Time;
                        this.dbContext.Alerts.Update(lastAlert);
                        this.dbContext.SaveChanges();
                        Console.WriteLine($"     - Alert {lastAlert.AlertType} closed w/ value {message.Value}");
                    }
                }
            }
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}
