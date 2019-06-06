using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTMon.Data.Migrations
{
    public partial class Seed_Device_and_Sensors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceName", "IntervalInSeconds", "IsActivated", "IsDeleted", "IsPublic", "UserId" },
                values: new object[,]
                {
                    { new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"), "device001", 3, true, false, false, null },
                    { new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"), "device002", 2, true, false, true, null },
                    { new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"), "device003", 5, true, false, false, null }
                });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "Id", "Description", "FriendlyLabel", "Label", "MeasurementUnit", "ValueType" },
                values: new object[,]
                {
                    { new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a"), "Nitrogen dioxide (NO2) at high concentrations causes inflammation of the airways. Breathing in high levels of NO2 can increase the likelihood of respiratory problems: wheezing, coughing, colds, flu and bronchitis.", "Nitrogen dioxide", "no2", "µg/m³", "Float" },
                    { new Guid("a652d3d5-7467-4577-902d-c10a9b36760a"), "Carbon dioxide is a natural gas found in our atmosphere. It is colorless, odorless, and tasteless - indistinguishable by individuals.", "Carbon Dioxide", "co2", "µg/m³", "Float" },
                    { new Guid("0aa2301c-3cd1-4d51-be90-ae772b72936c"), "Ozone is unstable and highly reactive. Ozone is used as a bleach, a deodorizing agent, and a sterilization agent for air and drinking water. At low concentrations, it is toxic.", "Ozone", "o3", "µg/m³", "Float" },
                    { new Guid("c8d95eef-3891-40ff-8397-750156fdc448"), "Sulfur dioxide is a colourless gas with a sharp, irritating odour. It is produced by burning fossil fuels and by the smelting of mineral ores that contain sulfur.Erupting volcanoes can be a significant natural source of sulfur dioxide emissions.", "Sulfur dioxide", "so2", "µg/m³", "Float" },
                    { new Guid("e2fd8491-0b3a-41f1-bf14-e29cbab7ada4"), "Temperature is a physical quantity expressing hot and cold.", "Temperature", "temp", "°C", "Float" },
                    { new Guid("99234723-7492-4eb3-8e44-83468263080b"), "It can be attached to any door and window.", "Open/Close Sensor", "openclose", "", "Boolean" },
                    { new Guid("7529a87a-3bd2-44ad-9624-2d0ee3f40519"), "Humidity is the amount of water vapour present in air.", "Humidity", "hum", "%", "Float" },
                    { new Guid("b8f4a465-6e36-4eaa-b0cd-ef6826f1b42f"), "The Sound Sensor records noise levels, due to its integrated microphone.", "Sound Sensor", "sound", "dBA", "Float" }
                });

            migrationBuilder.InsertData(
                table: "DeviceSensor",
                columns: new[] { "DeviceId", "SensorId" },
                values: new object[,]
                {
                    { new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"), new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a") },
                    { new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"), new Guid("a652d3d5-7467-4577-902d-c10a9b36760a") },
                    { new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"), new Guid("e2fd8491-0b3a-41f1-bf14-e29cbab7ada4") },
                    { new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"), new Guid("99234723-7492-4eb3-8e44-83468263080b") },
                    { new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"), new Guid("7529a87a-3bd2-44ad-9624-2d0ee3f40519") },
                    { new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"), new Guid("b8f4a465-6e36-4eaa-b0cd-ef6826f1b42f") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"), new Guid("e2fd8491-0b3a-41f1-bf14-e29cbab7ada4") });

            migrationBuilder.DeleteData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"), new Guid("7529a87a-3bd2-44ad-9624-2d0ee3f40519") });

            migrationBuilder.DeleteData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"), new Guid("99234723-7492-4eb3-8e44-83468263080b") });

            migrationBuilder.DeleteData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"), new Guid("b8f4a465-6e36-4eaa-b0cd-ef6826f1b42f") });

            migrationBuilder.DeleteData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"), new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a") });

            migrationBuilder.DeleteData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"), new Guid("a652d3d5-7467-4577-902d-c10a9b36760a") });

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("0aa2301c-3cd1-4d51-be90-ae772b72936c"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("c8d95eef-3891-40ff-8397-750156fdc448"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("7529a87a-3bd2-44ad-9624-2d0ee3f40519"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("99234723-7492-4eb3-8e44-83468263080b"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("a652d3d5-7467-4577-902d-c10a9b36760a"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("b8f4a465-6e36-4eaa-b0cd-ef6826f1b42f"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: new Guid("e2fd8491-0b3a-41f1-bf14-e29cbab7ada4"));
        }
    }
}
