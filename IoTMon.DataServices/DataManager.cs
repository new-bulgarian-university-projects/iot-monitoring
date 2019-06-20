using IoTMon.DataServices.Contracts;
using IoTMon.Models.SignalR;
using IoTMon.Models.TimeSeries;
using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.DataServices
{
    public class DataManager : IDataManager
    {
        private readonly ITimeSeriesProvider influxDbClient;
        private readonly IDataParser dataParser;
        public DateTime LastSent { get; set; }

        public DataManager(ITimeSeriesProvider influxDbClient, IDataParser dataParser)
        {
            this.influxDbClient = influxDbClient ?? throw new ArgumentNullException(nameof(influxDbClient));
            this.dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        }

        public async Task<IEnumerable<ChartData>> Get(SignalRFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var result = await influxDbClient.QueryAsync(filter.DeviceId, filter.Sensor, DateTime.Now.AddSeconds(-2));

            var processed = this.dataParser.ParseMessages(filter.Sensor, result);
            return processed;
        }
    }
}
