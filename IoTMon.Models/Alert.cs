using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoTMon.Models
{
    public class Alert
    {
        [Key]
        public Guid Id { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public Guid SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }

        public DateTime AlertStarted { get; set; }
        public DateTime? AlertClosed { get; set; }

        public ValueTypeEnum ValueType { get; set; }
        public string TriggeringValue { get; set; }
    }
}
