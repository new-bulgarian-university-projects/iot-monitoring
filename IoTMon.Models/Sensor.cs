using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoTMon.Models
{
    public class Sensor
    {
        [Key]
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string FriendlyLabel { get; set; }

        public string MeasurementUnit { get; set; }
        
        public ValueType ValueType { get; set; }
    }
}
