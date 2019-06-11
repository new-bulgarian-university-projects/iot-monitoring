using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.TimeSeries
{
    public class InfluxDbConfig
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }
    }
}
