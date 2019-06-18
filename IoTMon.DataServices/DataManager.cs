using IoTMon.DataServices.Contracts;
using IoTMon.Models.SignalR;
using IoTMon.Models.TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.DataServices
{
    public class DataManager
    {
        private ITimeSeriesProvider influxDbClient;
        public DateTime LastSent { get; set; }

        public DataManager(ITimeSeriesProvider influxDbClient)
        {
            this.influxDbClient = influxDbClient;
        }

        public async Task<IEnumerable<ChartData>> Get(object objectState)
        {
            var filter = (SignalRFilter)objectState;
            this.LastSent = DateTime.Now;
            // get data from now for that sensor
            var result = await influxDbClient.QueryAsync(filter.DeviceId, filter.Sensor, this.LastSent);
            var processed = result.Select(r => new ChartData(r.Time, Convert.ToDouble(r.Value))).ToList();
            return processed;
        }
    }
}
