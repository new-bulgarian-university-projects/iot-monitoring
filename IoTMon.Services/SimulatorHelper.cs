using IoTMon.Models.Enums;
using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Services
{
    public class SimulatorHelper : ISimulatorHelpers
    {
        static readonly Random random = new Random();
        private static readonly Dictionary<string, SensorRange> sensorRules = new Dictionary<string, SensorRange>()
        {
            {"no2", new SensorRange(0, 60) },
            {"co2", new SensorRange(3000, 9000) },
            {"o3", new SensorRange(0, 100) },
            {"so2", new SensorRange(0, 40) },
            {"temp", new SensorRange(-40, 80) },
            {"hum", new SensorRange(0,130) },
            {"sound", new SensorRange(0,150) },
            {"openclose", new SensorRange(0,1) },
        };

        public DateTime GetDatetimeUTC()
        {
            return DateTime.UtcNow;
        }

        public object GetRandomNumber(string sensor)
        {
            if (string.IsNullOrWhiteSpace(sensor))
            {
                throw new ArgumentException("sensor", nameof(sensor));
            }

            if (!sensorRules.ContainsKey(sensor))
            {
                throw new ArgumentNullException("no defined ranges for " + sensor);
            }

            SensorRange ranges = sensorRules[sensor];
            var generated = Math.Round(random.NextDouble() * (ranges.Max - ranges.Min) + ranges.Min, 4);
            if (sensor == "openclose")
            {
                return generated >= 0.5;
            }
            return generated;
        }
    }

    public class SensorRange
    {
        public SensorRange(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}
