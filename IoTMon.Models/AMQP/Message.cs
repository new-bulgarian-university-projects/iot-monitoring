using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.AMQP
{
    public class Message
    {
        public DateTime Timestamp { get; set; }
        public Guid DeviceId { get; set; }
        public string SensorLabel { get; set; }
        public object Value { get; set; }
        public string ValueType { get; set; }

        public override string ToString()
        {
            return $"{Timestamp.ToUniversalTime()} | {DeviceId} | {SensorLabel} | {Value} | {ValueType}";
        }
    }
}
