using IoTMon.Data;
using IoTMon.DataServices.Contracts;
using IoTMon.Models;
using IoTMon.Models.AMQP;
using IoTMon.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTMon.DataServices
{
    public class AlertService : IAlertService
    {
        private readonly ApplicationDbContext dbContext;

        public AlertService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void CheckAlerts(Message message)
        {
            var sensorId = this.dbContext.Sensors.First(s => s.Label == message.Sensor).Id;

            var deviceSensor = this.dbContext.Devices
                .Include(d => d.DeviceSensors)
                .First(d => d.Id == new Guid(message.DeviceId))
                .DeviceSensors
                .FirstOrDefault(ds => ds.SensorId == sensorId &&
                                ds.DeviceId == new Guid(message.DeviceId));

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
                        // send sms
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
    }
}
