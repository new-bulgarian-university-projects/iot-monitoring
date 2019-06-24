﻿// <auto-generated />
using System;
using IoTMon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IoTMon.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190624094201_add_alert_type")]
    partial class add_alert_type
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IoTMon.Models.Alert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AlertClosed");

                    b.Property<DateTime>("AlertStarted");

                    b.Property<string>("AlertType")
                        .IsRequired();

                    b.Property<Guid>("DeviceId");

                    b.Property<Guid>("SensorId");

                    b.Property<string>("TriggeringValue");

                    b.Property<string>("ValueType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("SensorId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("IoTMon.Models.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceName");

                    b.Property<int>("IntervalInSeconds");

                    b.Property<bool>("IsActivated");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPublic");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"),
                            DeviceName = "device001",
                            IntervalInSeconds = 3,
                            IsActivated = true,
                            IsDeleted = false,
                            IsPublic = false,
                            UserId = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d")
                        },
                        new
                        {
                            Id = new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"),
                            DeviceName = "device002",
                            IntervalInSeconds = 2,
                            IsActivated = true,
                            IsDeleted = false,
                            IsPublic = true,
                            UserId = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d")
                        },
                        new
                        {
                            Id = new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"),
                            DeviceName = "device003",
                            IntervalInSeconds = 5,
                            IsActivated = true,
                            IsDeleted = false,
                            IsPublic = false,
                            UserId = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d")
                        });
                });

            modelBuilder.Entity("IoTMon.Models.DeviceSensor", b =>
                {
                    b.Property<Guid>("DeviceId");

                    b.Property<Guid>("SensorId");

                    b.Property<double?>("MaxValue");

                    b.Property<double?>("MinValue");

                    b.HasKey("DeviceId", "SensorId");

                    b.HasIndex("SensorId");

                    b.ToTable("DeviceSensor");

                    b.HasData(
                        new
                        {
                            DeviceId = new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"),
                            SensorId = new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a")
                        },
                        new
                        {
                            DeviceId = new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"),
                            SensorId = new Guid("a652d3d5-7467-4577-902d-c10a9b36760a")
                        },
                        new
                        {
                            DeviceId = new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"),
                            SensorId = new Guid("99234723-7492-4eb3-8e44-83468263080b")
                        },
                        new
                        {
                            DeviceId = new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"),
                            SensorId = new Guid("7529a87a-3bd2-44ad-9624-2d0ee3f40519")
                        },
                        new
                        {
                            DeviceId = new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"),
                            SensorId = new Guid("b8f4a465-6e36-4eaa-b0cd-ef6826f1b42f")
                        },
                        new
                        {
                            DeviceId = new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"),
                            SensorId = new Guid("e2fd8491-0b3a-41f1-bf14-e29cbab7ada4")
                        });
                });

            modelBuilder.Entity("IoTMon.Models.Sensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("FriendlyLabel");

                    b.Property<string>("Label");

                    b.Property<string>("MeasurementUnit");

                    b.Property<string>("ValueType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Label")
                        .IsUnique()
                        .HasFilter("[Label] IS NOT NULL");

                    b.ToTable("Sensors");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a"),
                            Description = "Nitrogen dioxide (NO2) at high concentrations causes inflammation of the airways. Breathing in high levels of NO2 can increase the likelihood of respiratory problems: wheezing, coughing, colds, flu and bronchitis.",
                            FriendlyLabel = "Nitrogen dioxide",
                            Label = "no2",
                            MeasurementUnit = "µg/m³",
                            ValueType = "Float"
                        },
                        new
                        {
                            Id = new Guid("a652d3d5-7467-4577-902d-c10a9b36760a"),
                            Description = "Carbon dioxide is a natural gas found in our atmosphere. It is colorless, odorless, and tasteless - indistinguishable by individuals.",
                            FriendlyLabel = "Carbon Dioxide",
                            Label = "co2",
                            MeasurementUnit = "µg/m³",
                            ValueType = "Float"
                        },
                        new
                        {
                            Id = new Guid("0aa2301c-3cd1-4d51-be90-ae772b72936c"),
                            Description = "Ozone is unstable and highly reactive. Ozone is used as a bleach, a deodorizing agent, and a sterilization agent for air and drinking water. At low concentrations, it is toxic.",
                            FriendlyLabel = "Ozone",
                            Label = "o3",
                            MeasurementUnit = "µg/m³",
                            ValueType = "Float"
                        },
                        new
                        {
                            Id = new Guid("c8d95eef-3891-40ff-8397-750156fdc448"),
                            Description = "Sulfur dioxide is a colourless gas with a sharp, irritating odour. It is produced by burning fossil fuels and by the smelting of mineral ores that contain sulfur.Erupting volcanoes can be a significant natural source of sulfur dioxide emissions.",
                            FriendlyLabel = "Sulfur dioxide",
                            Label = "so2",
                            MeasurementUnit = "µg/m³",
                            ValueType = "Float"
                        },
                        new
                        {
                            Id = new Guid("e2fd8491-0b3a-41f1-bf14-e29cbab7ada4"),
                            Description = "Temperature is a physical quantity expressing hot and cold.",
                            FriendlyLabel = "Temperature",
                            Label = "temp",
                            MeasurementUnit = "°C",
                            ValueType = "Float"
                        },
                        new
                        {
                            Id = new Guid("99234723-7492-4eb3-8e44-83468263080b"),
                            Description = "It can be attached to any door and window.",
                            FriendlyLabel = "Open/Close Sensor",
                            Label = "openclose",
                            MeasurementUnit = "",
                            ValueType = "Boolean"
                        },
                        new
                        {
                            Id = new Guid("7529a87a-3bd2-44ad-9624-2d0ee3f40519"),
                            Description = "Humidity is the amount of water vapour present in air.",
                            FriendlyLabel = "Humidity",
                            Label = "hum",
                            MeasurementUnit = "%",
                            ValueType = "Float"
                        },
                        new
                        {
                            Id = new Guid("b8f4a465-6e36-4eaa-b0cd-ef6826f1b42f"),
                            Description = "The Sound Sensor records noise levels, due to its integrated microphone.",
                            FriendlyLabel = "Sound Sensor",
                            Label = "sound",
                            MeasurementUnit = "dBA",
                            ValueType = "Float"
                        });
                });

            modelBuilder.Entity("IoTMon.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHashed")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalted")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d"),
                            Email = "doe@mail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PasswordHashed = new byte[] { 54, 7, 163, 253, 40, 189, 226, 20, 245, 209, 159, 19, 50, 239, 18, 79, 77, 69, 127, 132, 188, 129, 50, 128, 31, 159, 211, 207, 126, 207, 255, 151, 33, 66, 165, 203, 171, 22, 64, 105, 88, 171, 204, 253, 189, 177, 13, 188, 152, 198, 147, 167, 68, 180, 92, 178, 94, 33, 180, 27, 27, 206, 125, 243 },
                            PasswordSalted = new byte[] { 71, 80, 83, 81, 90, 82, 72, 57, 69, 84, 48, 72, 83, 90, 79, 69, 74, 50, 55, 85, 86, 71, 85, 69, 65, 48, 71, 83, 90, 85, 76, 56, 50, 78, 68, 78, 53, 85, 82, 89, 82, 88, 80, 49, 87, 89, 48, 48, 52, 69, 80, 84, 65, 51, 75, 56, 68, 74, 90, 70, 86, 50, 69, 70, 86, 51, 65, 56, 86, 68, 65, 70, 56, 88, 88, 65, 76, 85, 69, 86, 89, 49, 65, 50, 71, 73, 53, 50, 48, 65, 55, 79, 75, 73, 83, 83, 79, 55, 80, 66, 65, 72, 79, 83, 57, 66, 69, 51, 74, 90, 52, 80, 81, 80, 70, 55, 57, 84, 82, 90, 49, 87, 70, 86, 86, 86, 53, 76 }
                        },
                        new
                        {
                            Id = new Guid("bf669250-dec1-4779-92fc-3c7e8032be7b"),
                            Email = "xuan@mail.com",
                            FirstName = "Alex",
                            LastName = "Xuan",
                            PasswordHashed = new byte[] { 54, 7, 163, 253, 40, 189, 226, 20, 245, 209, 159, 19, 50, 239, 18, 79, 77, 69, 127, 132, 188, 129, 50, 128, 31, 159, 211, 207, 126, 207, 255, 151, 33, 66, 165, 203, 171, 22, 64, 105, 88, 171, 204, 253, 189, 177, 13, 188, 152, 198, 147, 167, 68, 180, 92, 178, 94, 33, 180, 27, 27, 206, 125, 243 },
                            PasswordSalted = new byte[] { 71, 80, 83, 81, 90, 82, 72, 57, 69, 84, 48, 72, 83, 90, 79, 69, 74, 50, 55, 85, 86, 71, 85, 69, 65, 48, 71, 83, 90, 85, 76, 56, 50, 78, 68, 78, 53, 85, 82, 89, 82, 88, 80, 49, 87, 89, 48, 48, 52, 69, 80, 84, 65, 51, 75, 56, 68, 74, 90, 70, 86, 50, 69, 70, 86, 51, 65, 56, 86, 68, 65, 70, 56, 88, 88, 65, 76, 85, 69, 86, 89, 49, 65, 50, 71, 73, 53, 50, 48, 65, 55, 79, 75, 73, 83, 83, 79, 55, 80, 66, 65, 72, 79, 83, 57, 66, 69, 51, 74, 90, 52, 80, 81, 80, 70, 55, 57, 84, 82, 90, 49, 87, 70, 86, 86, 86, 53, 76 }
                        });
                });

            modelBuilder.Entity("IoTMon.Models.Alert", b =>
                {
                    b.HasOne("IoTMon.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IoTMon.Models.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IoTMon.Models.Device", b =>
                {
                    b.HasOne("IoTMon.Models.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IoTMon.Models.DeviceSensor", b =>
                {
                    b.HasOne("IoTMon.Models.Device", "Device")
                        .WithMany("DeviceSensors")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IoTMon.Models.Sensor", "Sensor")
                        .WithMany("DeviceSensors")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
