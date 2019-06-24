using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTMon.Data.Migrations
{
    public partial class Add_Min_Max_Value_For_alerts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxValue",
                table: "DeviceSensor",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinValue",
                table: "DeviceSensor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "DeviceSensor");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "DeviceSensor");
        }
    }
}
