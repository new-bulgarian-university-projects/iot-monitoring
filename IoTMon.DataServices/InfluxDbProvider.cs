using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using IoTMon.DataServices.Contracts;
using IoTMon.Models.TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task WriteAsync(Measurement data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var point = data.ToPoint();
            await this.influxDbClient.Client.WriteAsync(point, this.dbName);
        }

        public async Task WriteAsync(IEnumerable<Measurement> data)
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
        public static Point ToPoint(this Measurement data)
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
