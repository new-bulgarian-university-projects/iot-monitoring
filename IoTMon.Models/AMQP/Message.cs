using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.AMQP
{
    public class Message
    {
        public DateTime Time { get; set; }
        public string DeviceId { get; set; }
        public string Sensor { get; set; }
        public object Value { get; set; }
        public string ValueType { get; set; }

        public override string ToString()
        {
            return $"{Time.ToUniversalTime()} | {DeviceId} | {Sensor} | {Value} | {ValueType}";
        }
    }
}
