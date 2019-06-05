using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IoTMon.Models
{
    public class Device
    {

        private ICollection<Sensor> sensors;
        public Device()
        {
            this.sensors = new HashSet<Sensor>();
        }

        [Key]
        public Guid Id { get; set; }
        public string DeviceId { get; set; }

        public ICollection<Sensor> Sensors
        {
            get => this.sensors;
            set => this.sensors = value;
        }

        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
        public int IntervalInSeconds { get; set; }
        public bool IsPublic { get; set; }
    }
}
