using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTMon.Data.Migrations
{
    public partial class seed_mix_max_for_one_sensor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"), new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a") },
                columns: new[] { "MaxValue", "MinValue" },
                values: new object[] { 50.0, 5.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DeviceSensor",
                keyColumns: new[] { "DeviceId", "SensorId" },
                keyValues: new object[] { new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"), new Guid("0e4309da-f578-4f0e-b2e1-181c5273ca5a") },
                columns: new[] { "MaxValue", "MinValue" },
                values: new object[] { null, null });
        }
    }
}
