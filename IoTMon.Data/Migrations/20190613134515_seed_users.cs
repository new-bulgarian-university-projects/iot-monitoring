using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTMon.Data.Migrations
{
    public partial class seed_users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHashed", "PasswordSalted" },
                values: new object[] { new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d"), "doe@mail.com", "John", "Doe", new byte[] { 54, 7, 163, 253, 40, 189, 226, 20, 245, 209, 159, 19, 50, 239, 18, 79, 77, 69, 127, 132, 188, 129, 50, 128, 31, 159, 211, 207, 126, 207, 255, 151, 33, 66, 165, 203, 171, 22, 64, 105, 88, 171, 204, 253, 189, 177, 13, 188, 152, 198, 147, 167, 68, 180, 92, 178, 94, 33, 180, 27, 27, 206, 125, 243 }, new byte[] { 71, 80, 83, 81, 90, 82, 72, 57, 69, 84, 48, 72, 83, 90, 79, 69, 74, 50, 55, 85, 86, 71, 85, 69, 65, 48, 71, 83, 90, 85, 76, 56, 50, 78, 68, 78, 53, 85, 82, 89, 82, 88, 80, 49, 87, 89, 48, 48, 52, 69, 80, 84, 65, 51, 75, 56, 68, 74, 90, 70, 86, 50, 69, 70, 86, 51, 65, 56, 86, 68, 65, 70, 56, 88, 88, 65, 76, 85, 69, 86, 89, 49, 65, 50, 71, 73, 53, 50, 48, 65, 55, 79, 75, 73, 83, 83, 79, 55, 80, 66, 65, 72, 79, 83, 57, 66, 69, 51, 74, 90, 52, 80, 81, 80, 70, 55, 57, 84, 82, 90, 49, 87, 70, 86, 86, 86, 53, 76 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHashed", "PasswordSalted" },
                values: new object[] { new Guid("bf669250-dec1-4779-92fc-3c7e8032be7b"), "xuan@mail.com", "Alex", "Xuan", new byte[] { 54, 7, 163, 253, 40, 189, 226, 20, 245, 209, 159, 19, 50, 239, 18, 79, 77, 69, 127, 132, 188, 129, 50, 128, 31, 159, 211, 207, 126, 207, 255, 151, 33, 66, 165, 203, 171, 22, 64, 105, 88, 171, 204, 253, 189, 177, 13, 188, 152, 198, 147, 167, 68, 180, 92, 178, 94, 33, 180, 27, 27, 206, 125, 243 }, new byte[] { 71, 80, 83, 81, 90, 82, 72, 57, 69, 84, 48, 72, 83, 90, 79, 69, 74, 50, 55, 85, 86, 71, 85, 69, 65, 48, 71, 83, 90, 85, 76, 56, 50, 78, 68, 78, 53, 85, 82, 89, 82, 88, 80, 49, 87, 89, 48, 48, 52, 69, 80, 84, 65, 51, 75, 56, 68, 74, 90, 70, 86, 50, 69, 70, 86, 51, 65, 56, 86, 68, 65, 70, 56, 88, 88, 65, 76, 85, 69, 86, 89, 49, 65, 50, 71, 73, 53, 50, 48, 65, 55, 79, 75, 73, 83, 83, 79, 55, 80, 66, 65, 72, 79, 83, 57, 66, 69, 51, 74, 90, 52, 80, 81, 80, 70, 55, 57, 84, 82, 90, 49, 87, 70, 86, 86, 86, 53, 76 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bf669250-dec1-4779-92fc-3c7e8032be7b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c0961b4f-466c-4844-bd95-9f7bfa62cc7d"));
        }
    }
}
