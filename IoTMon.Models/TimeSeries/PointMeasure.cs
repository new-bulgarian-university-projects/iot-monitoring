using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.TimeSeries
{
    public class PointMeasure
    {
        public string MeasurementName { get; set; }
        public IDictionary<string, object> Tags { get; set; }
        public IDictionary<string, object> Fields { get; set; }
        public DateTime? Timestamp { get; set; }

    }
}
