using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial35 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-291123-161420");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-291123-097460");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-291123-097576");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-291123-161286");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 29, 20, 56, 0, 416, DateTimeKind.Local).AddTicks(9672),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(9283));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 29, 20, 56, 0, 416, DateTimeKind.Local).AddTicks(1646),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(2712));

            migrationBuilder.AlterColumn<string>(
                name: "Cover",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-291123-182839", 1, "admin" },
                    { "108101-291123-965501", 2, "creator" },
                    { "108101-291123-965630", 3, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-291123-182992", new DateTime(2023, 11, 29, 20, 56, 0, 696, DateTimeKind.Local).AddTicks(4619), true, "$2a$11$1b9B.fXTOFuwir5fg2I0GedsFZh.zxUD2xzFjTT6YwuUbP2ONQQDC", "108101-291123-182839", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-291123-182992");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-291123-965501");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-291123-965630");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-291123-182839");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(9283),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 29, 20, 56, 0, 416, DateTimeKind.Local).AddTicks(9672));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(2712),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 29, 20, 56, 0, 416, DateTimeKind.Local).AddTicks(1646));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Cover",
                table: "Dishes",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-291123-097460", 2, "creator" },
                    { "108101-291123-097576", 3, "staff" },
                    { "108101-291123-161286", 1, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-291123-161420", new DateTime(2023, 11, 29, 15, 52, 5, 909, DateTimeKind.Local).AddTicks(6503), true, "$2a$11$pVSu813fWDlj5FKDzs0Eg./33lUgojdtTwcvAu5d1f7HBI5edjhwC", "108101-291123-161286", "admin" });
        }
    }
}
