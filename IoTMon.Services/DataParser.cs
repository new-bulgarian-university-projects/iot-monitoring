using IoTMon.Models.AMQP;
using IoTMon.Models.TimeSeries;
using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTMon.Services
{
    public class DataParser : IDataParser
    {
        public IEnumerable<ChartData> ParseMessages(string sensor, IEnumerable<Message> incoming)
        {
            if (string.IsNullOrEmpty(sensor))
            {
                throw new ArgumentException("message", nameof(sensor));
            }

            if (incoming == null)
            {
                throw new ArgumentNullException(nameof(incoming));
            }

            List<ChartData> processed;
            if (sensor == "openclose")
            {
                processed = incoming.Select(r => new ChartData(r.Time, r.Value.ToString() == "True" ? 1 : 0)).ToList();
            }
            else
            {
                processed = incoming.Select(r => new ChartData(r.Time, Convert.ToDouble(r.Value))).ToList();

            }
            return processed;
        }
    }
}
