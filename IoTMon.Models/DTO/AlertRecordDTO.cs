using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.DTO
{
    public class AlertRecordDTO
    {
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string SensorName { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }   

        public IEnumerable<AlertHistoryDTO> AlertHistory { get; set; }
    }
}
