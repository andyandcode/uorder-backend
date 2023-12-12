using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-081223-002549");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-554857");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-554963");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-002387");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 12, 20, 0, 2, 427, DateTimeKind.Local).AddTicks(4025),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 597, DateTimeKind.Local).AddTicks(3690));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Menus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 12, 20, 0, 2, 426, DateTimeKind.Local).AddTicks(497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 596, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DiscountCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-121223-125990", 2, "creator" },
                    { "108101-121223-126125", 3, "staff" },
                    { "108101-121223-316689", 1, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "IsDeleted", "Password", "RoleId", "Username" },
                values: new object[] { "110116-121223-316983", new DateTime(2023, 12, 12, 20, 0, 2, 612, DateTimeKind.Local).AddTicks(4913), true, false, "$2a$11$4Yi1XfmNa2LYFnNaRbottOcmLm7ruFRGiy/x/relLiDh0IFbqW8kq", "108101-121223-316689", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-121223-316983");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-121223-125990");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-121223-126125");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-121223-316689");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 597, DateTimeKind.Local).AddTicks(3690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 12, 20, 0, 2, 427, DateTimeKind.Local).AddTicks(4025));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 596, DateTimeKind.Local).AddTicks(5271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 12, 20, 0, 2, 426, DateTimeKind.Local).AddTicks(497));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-081223-002387", 1, "admin" },
                    { "108101-081223-554857", 2, "creator" },
                    { "108101-081223-554963", 3, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-081223-002549", new DateTime(2023, 12, 8, 14, 40, 30, 755, DateTimeKind.Local).AddTicks(3765), true, "$2a$11$xP90YM0.MKPPgOerGNTl2.FuDm4vZKhmeU38hWQZzlwglqwzrZRVW", "108101-081223-002387", "admin" });
        }
    }
}
