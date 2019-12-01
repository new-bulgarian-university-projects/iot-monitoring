using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.DTO
{
    public class AlertHistoryDTO
    {
        public DateTime Started { get; set; }
        public DateTime? Closed { get; set; }
        public string Value { get; set; }
        public AlertTypeEnum AlertType { get; set; }
    }
}
