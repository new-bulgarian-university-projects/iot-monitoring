using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoTMon.Models
{
    public class Sensor
    {
        private ICollection<DeviceSensor> deviceSensors;
        public Sensor()
        {
            this.deviceSensors = new HashSet<DeviceSensor>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string FriendlyLabel { get; set; }

        public string Description { get; set; }

        public string MeasurementUnit { get; set; }

        public ValueTypeEnum ValueType { get; set; }

        public ICollection<DeviceSensor> DeviceSensors
        {
            get => this.deviceSensors;
            set => this.deviceSensors = value;
        }
    }
}
