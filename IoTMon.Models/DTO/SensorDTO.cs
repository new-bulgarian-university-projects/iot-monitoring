using IoTMon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTMon.Models.DTO
{
    public class SensorDTO
    {
        public SensorDTO()
        {

        }
        public SensorDTO(Sensor sensor)
        {
            if (sensor == null)
            {
                return;
            }

            this.Id = sensor.Id;
            this.Label = sensor.Label;
            this.FriendlyLabel = sensor.FriendlyLabel;
            this.Description = sensor.Description;
            this.MeasurementUnit = sensor.MeasurementUnit;
            this.ValueType = sensor.ValueType;

        }
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string FriendlyLabel { get; set; }

        public string Description { get; set; }

        public string MeasurementUnit { get; set; }

        public ValueTypeEnum ValueType { get; set; }

        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
    }
}
