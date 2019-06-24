using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models
{
    public class DeviceSensor
    {
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }

        public Guid SensorId { get; set; }
        public Sensor Sensor { get; set; }

        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
    }
}
