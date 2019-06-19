using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.TimeSeries
{
    public class InfluxQueryFilter
    {
        public string SensorName { get; set; }
        public string DeviceId { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
