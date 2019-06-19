using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTMon.Data.Migrations
{
    public partial class add_user_navprop_to_devices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"),
                column: "UserId",
                value: new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d"));

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"),
                column: "UserId",
                value: new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d"));

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"),
                column: "UserId",
                value: new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("77f2aa97-f5a0-40e0-a811-5aaf59626356"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("b5e6b650-011f-42ea-8d14-153fa819cf98"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("c06c5c27-eddc-499a-815a-44dc4b866735"),
                column: "UserId",
                value: null);
        }
    }
}
