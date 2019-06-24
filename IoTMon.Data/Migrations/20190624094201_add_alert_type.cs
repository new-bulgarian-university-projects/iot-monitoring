using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTMon.Data.Migrations
{
    public partial class add_alert_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlertType",
                table: "Alerts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertType",
                table: "Alerts");
        }
    }
}
