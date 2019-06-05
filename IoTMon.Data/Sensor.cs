using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IoTMon.Data
{
    public class Sensor
    {
        [Key]
        public Guid Id { get; set; }

        public string ShortName { get; set; }

        public string Label { get; set; }

        public ValueType ValueType { get; set; }

    }
}
