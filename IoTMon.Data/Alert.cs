using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IoTMon.Data
{
    public class Alert
    {
        [Key]
        public Guid Id { get; set; }

        public string DeviceId { get; set; }
        public string SensorId { get; set; }

        public DateTime AlertStarted { get; set; }
        public DateTime? AlertClosed { get; set; }

        public ValueType ValueType { get; set; }
        public string TriggeringValue { get; set; }
    }
}
