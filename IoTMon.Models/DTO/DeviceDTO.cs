using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTMon.Models.DTO
{
    public class DeviceDTO
    {
        public DeviceDTO(Device device)
        {
            this.Id = device.Id;
            this.DeviceName = device.DeviceName;
            this.IntervalInSeconds = device.IntervalInSeconds;
            this.IsActivated = device.IsActivated;
            this.IsPublic = device.IsPublic;

            this.Sensors = device.DeviceSensors
                                 .Select(ds => new SensorDTO(ds.Sensor))
                                 .ToList();
        }
        public Guid Id { get; set; }

        public string DeviceName { get; set; }

        public int IntervalInSeconds { get; set; }

        public bool IsActivated { get; set; }

        public bool IsPublic { get; set; }

        public List<SensorDTO> Sensors { get; set; }
        public List<string> SensorIds { get; set; }
    }
}
