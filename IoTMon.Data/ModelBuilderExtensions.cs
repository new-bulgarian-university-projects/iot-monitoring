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
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            // customer password
            byte[] userPasswordHash = null;
            byte[] userPasswordSalt = null;
            HashPassword("asd123", out userPasswordHash, out userPasswordSalt);

            var users = new User[]
            {
                new User()
                {
                    Id = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "doe@mail.com",
                    PasswordSalted = userPasswordSalt,
                    PasswordHashed = userPasswordHash,
                },

                new User()
                {
                    Id = new Guid("bf669250-dec1-4779-92fc-3c7e8032be7b"),
                    FirstName = "Alex",
                    LastName = "Xuan",
                    Email = "xuan@mail.com",
                    PasswordSalted = userPasswordSalt,
                    PasswordHashed = userPasswordHash
                },

            };

            modelBuilder.Entity<User>()
                .HasData(users);
        }

        private static void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes("GPSQZRH9ET0HSZOEJ27UVGUEA0GSZUL82NDN5URYRXP1WY004EPTA3K8DJZFV2EFV3A8VDAF8XXALUEVY1A2GI520A7OKISSO7PBAHOS9BE3JZ4PQPF79TRZ1WFVVV5L")))
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static void SeedDevicesAndSensors(this ModelBuilder modelBuilder)
        {
            // create sensors
            var sensors = new Sensor[]
            {
                //0. no2 - µg/m³
                new Sensor()
                {
                    Id = new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a"),
                    Label = "no2",
                    FriendlyLabel= "Nitrogen dioxide",
                    MeasurementUnit = "µg/m³",
                    Description = "Nitrogen dioxide (NO2) at high concentrations causes inflammation of the airways. Breathing in high levels of NO2 can increase the likelihood of respiratory problems: wheezing, coughing, colds, flu and bronchitis.",
                    ValueType = ValueTypeEnum.Float
                },

                //1. co2 - µg/m³
                new Sensor()
                {
                    Id = new Guid("a652d3d5-7467-4577-902d-c10a9b36760a"),
                    Label = "co2",
                    FriendlyLabel= "Carbon Dioxide",
                    MeasurementUnit = "µg/m³",
                    Description = "Carbon dioxide is a natural gas found in our atmosphere. It is colorless, odorless, and tasteless - indistinguishable by individuals.",
                    ValueType = ValueTypeEnum.Float
                },

                //2. o3 - µg/m³
                new Sensor()
                {
                    Id = new Guid("0aa2301c-3cd1-4d51-be90-ae772b72936c"),
                    Label = "o3",
                    FriendlyLabel= "Ozone",
                    MeasurementUnit = "µg/m³",
                    Description = "Ozone is unstable and highly reactive. Ozone is used as a bleach, a deodorizing agent, and a sterilization agent for air and drinking water. At low concentrations, it is toxic.",
                    ValueType = ValueTypeEnum.Float
                },

                //3. so2 - µg/m³
                new Sensor()
                {
                    Id = new Guid("c8d95eef-3891-40ff-8397-750156fdc448"),
                    Label = "so2",
                    FriendlyLabel= "Sulfur dioxide",
                    MeasurementUnit = "µg/m³",
                    Description = "Sulfur dioxide is a colourless gas with a sharp, irritating odour. It is produced by burning fossil fuels and by the smelting of mineral ores that contain sulfur.Erupting volcanoes can be a significant natural source of sulfur dioxide emissions.",
                    ValueType = ValueTypeEnum.Float
                },

                //4. temp - °C
                new Sensor()
                {
                    Id = new Guid("e2fd8491-0b3a-41f1-bf14-e29cbab7ada4"),
                    Label = "temp",
                    FriendlyLabel= "Temperature",
                    MeasurementUnit = "°C",
                    Description = "Temperature is a physical quantity expressing hot and cold.",
                    ValueType = ValueTypeEnum.Float
                },

                //5. open/close - open / close
                new Sensor()
                {
                    Id = new Guid("99234723-7492-4eb3-8e44-83468263080b"),
                    Label = "openclose",
                    FriendlyLabel= "Open/Close Sensor",
                    MeasurementUnit = "",
                    Description = "It can be attached to any door and window.",
                    ValueType = ValueTypeEnum.Boolean
                },

                //6. humidity - %
                new Sensor()
                {
                    Id = new Guid("7529a87a-3bd2-44ad-9624-2d0ee3f40519"),
                    Label = "hum",
                    FriendlyLabel= "Humidity",
                    MeasurementUnit = "%",
                    Description = "Humidity is the amount of water vapour present in air.",
                    ValueType = ValueTypeEnum.Float
                },

                //7. sound - dBA
                new Sensor()
                {
                    Id = new Guid("b8f4a465-6e36-4eaa-b0cd-ef6826f1b42f"),
                    Label = "sound",
                    FriendlyLabel= "Sound Sensor",
                    MeasurementUnit = "dBA",
                    Description = "The Sound Sensor records noise levels, due to its integrated microphone.",
                    ValueType = ValueTypeEnum.Float
                },
            };

            // create devices

            var devices = new Device[]
            {
                new Device()
                {
                    Id = new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"),
                    DeviceName = "device001",
                    IntervalInSeconds = 3,
                    IsActivated = true,
                    IsPublic = false,
                    IsDeleted = false,
                    UserId = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d")
                },

                new Device()
                {
                    Id = new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"),
                    DeviceName = "device002",
                    IntervalInSeconds = 2,
                    IsActivated = true,
                    IsPublic = true,
                    IsDeleted = false,
                    UserId = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d")
                },

                new Device()
                {
                    Id = new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"),
                    DeviceName = "device003",
                    IntervalInSeconds = 5,
                    IsActivated = true,
                    IsPublic = false,
                    IsDeleted = false,
                    UserId = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d")
                },
            };

            var deviceSensors = new DeviceSensor[]
            {
                new DeviceSensor() {DeviceId = devices[0].Id, SensorId = sensors[0].Id},
                new DeviceSensor() {DeviceId = devices[0].Id, SensorId = sensors[1].Id},

                new DeviceSensor() {DeviceId = devices[1].Id, SensorId = sensors[5].Id},
                new DeviceSensor() {DeviceId = devices[1].Id, SensorId = sensors[6].Id},
                new DeviceSensor() {DeviceId = devices[1].Id, SensorId = sensors[7].Id},

                new DeviceSensor() {DeviceId = devices[2].Id, SensorId = sensors[4].Id},
            };

            modelBuilder.Entity<Device>()
               .HasData(devices);

            modelBuilder.Entity<Sensor>()
               .HasData(sensors);

            modelBuilder.Entity<DeviceSensor>()
                .HasData(deviceSensors);
        }
    }
}
