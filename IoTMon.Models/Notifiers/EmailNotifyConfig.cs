using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.Notifiers
{
    public class EmailNotifyConfig
    {
        public string EnvApiKey { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
    }
}
