using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IoTMon.Models
{
    public class Device
    {
        private ICollection<DeviceSensor> deviceSensors;
        public Device()
        {
            this.deviceSensors = new HashSet<DeviceSensor>();
        }

        [Key]
        public Guid Id { get; set; }
        public string DeviceName { get; set; }

        public ICollection<DeviceSensor> DeviceSensors
        {
            get => this.deviceSensors;
            set => this.deviceSensors = value;
        }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        public int IntervalInSeconds { get; set; }
        public bool IsPublic { get; set; }
    }
}
