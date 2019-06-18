using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.SignalR
{
    public class SignalRFilter
    {
        public Guid DeviceId { get; set; }
        public string Sensor { get; set; }
        public DateTime From { get; set; }
    }
}
