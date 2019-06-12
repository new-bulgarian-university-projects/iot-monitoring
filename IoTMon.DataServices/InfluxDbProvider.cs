﻿using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using IoTMon.Models.TimeSeries;
using InfluxData.Net.InfluxDb.Models.Responses;
using IoTMon.DataServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfluxData.Net.InfluxDb.Helpers;

namespace IoTMon.DataServices
{
    public class InfluxDbProvider : ITimeSeriesProvider
    {
        private readonly InfluxDbClient influxDbClient;
        private readonly string dbName;

        public InfluxDbProvider(InfluxDbConfig influxDbConfig)
        {
            if (influxDbConfig == null)
            {
                throw new ArgumentNullException("influxDbConfig");
            }

            this.dbName = influxDbConfig.DbName;
            this.influxDbClient = new InfluxDbClient(influxDbConfig.Host,
                influxDbConfig.UserName,
                influxDbConfig.Password,
                InfluxDbVersion.Latest);


        }
        public async Task<IEnumerable<Models.AMQP.Message>> QueryAsync(Guid deviceId, string sensor)
        {
            // query by: 
            // deviceId, sensor, startDate, endDate, limit

            string query = "SELECT * FROM sensors WHERE \"sensor\"= '@SensorName' AND \"deviceId\"='@DeviceId'";
            IEnumerable<Serie> response = await this.influxDbClient.Client.QueryAsync(
                query,
                parameters: new
                {
                    @SensorName = sensor,
                    @DeviceId = deviceId.ToString()
                },
                this.dbName);
            var collection = response.As<Models.AMQP.Message>().ToList();
            return collection;
        }
        public async Task WriteAsync(PointMeasure data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var point = data.ToPoint();
            await this.influxDbClient.Client.WriteAsync(point, this.dbName);
        }

        public async Task WriteAsync(IEnumerable<PointMeasure> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var points = data.Select(d => d.ToPoint());
            await this.influxDbClient.Client.WriteAsync(points, this.dbName);
        }
    }
    internal static class CustomExtensions
    {
        public static Point ToPoint(this PointMeasure data)
        {
            return new Point()
            {
                Name = data.MeasurementName,
                Timestamp = data.Timestamp,
                Fields = data.Fields,
                Tags = data.Tags
            };
        }
    }
}
