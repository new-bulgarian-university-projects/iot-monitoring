using IoTMon.Models;
using IoTMon.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDevices(this ModelBuilder modelBuilder)
        {
            // create sensors
            var sensors = new Sensor[]
            {
                new Sensor()
                {
                    Id = new Guid(""),
                    Label = "",
                    FriendlyLabel= "",
                    MeasurementUnit = "",
                    ValueType = ValueTypeEnum.Number
                }
            };
            
            // create devices


            // has data for each Entry
        }

        public static void SeedAlerts(this ModelBuilder modelBuilder)
        {

        }
    }
}
