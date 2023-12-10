using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-071223-876223");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-071223-709344");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-071223-709614");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-071223-875370");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "DiscountCodes",
                newName: "StartDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 0, 28, 15, 121, DateTimeKind.Local).AddTicks(6267),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 7, 20, 25, 35, 380, DateTimeKind.Local).AddTicks(2201));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 0, 28, 15, 120, DateTimeKind.Local).AddTicks(9467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 7, 20, 25, 35, 378, DateTimeKind.Local).AddTicks(4884));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "DiscountCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-081223-250915", 1, "admin" },
                    { "108101-081223-791623", 2, "creator" },
                    { "108101-081223-791717", 3, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-081223-251056", new DateTime(2023, 12, 8, 0, 28, 15, 379, DateTimeKind.Local).AddTicks(1082), true, "$2a$11$mWmrqJsdIaG9r1Hpd0aHSunySfubSq6paRDFvZW14fRta0j9gcS8G", "108101-081223-250915", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-081223-251056");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-791623");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-791717");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-250915");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "DiscountCodes");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "DiscountCodes",
                newName: "ExpiryDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 7, 20, 25, 35, 380, DateTimeKind.Local).AddTicks(2201),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 0, 28, 15, 121, DateTimeKind.Local).AddTicks(6267));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 7, 20, 25, 35, 378, DateTimeKind.Local).AddTicks(4884),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 0, 28, 15, 120, DateTimeKind.Local).AddTicks(9467));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-071223-709344", 2, "creator" },
                    { "108101-071223-709614", 3, "staff" },
                    { "108101-071223-875370", 1, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-071223-876223", new DateTime(2023, 12, 7, 20, 25, 35, 570, DateTimeKind.Local).AddTicks(7789), true, "$2a$11$pyaZWw5738i0eazftRL/8u02oaFjXBBv1EyzfUIAB/P/UO3003hB6", "108101-071223-875370", "admin" });
        }
    }
}
