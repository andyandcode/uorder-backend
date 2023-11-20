using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-191123-293484");

            migrationBuilder.DropColumn(
                name: "ForcedPasswordReset",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 18, 8, 25, 29, DateTimeKind.Local).AddTicks(9774),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 17, 9, 40, 627, DateTimeKind.Local).AddTicks(8241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 18, 8, 25, 29, DateTimeKind.Local).AddTicks(7681),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 17, 9, 40, 627, DateTimeKind.Local).AddTicks(2002));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 18, 8, 25, 27, DateTimeKind.Local).AddTicks(3414),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 17, 9, 40, 624, DateTimeKind.Local).AddTicks(2952));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "Username" },
                values: new object[] { "110116-191123-315243", new DateTime(2023, 11, 19, 18, 8, 25, 314, DateTimeKind.Local).AddTicks(4076), true, "$2a$11$zWOQyMwzXyadTmeWWURdMuOdaHEw4ZGbWzrT3eXga/Dk/2bw2ivXa", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-191123-315243");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 17, 9, 40, 627, DateTimeKind.Local).AddTicks(8241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 18, 8, 25, 29, DateTimeKind.Local).AddTicks(9774));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 17, 9, 40, 627, DateTimeKind.Local).AddTicks(2002),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 18, 8, 25, 29, DateTimeKind.Local).AddTicks(7681));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 17, 9, 40, 624, DateTimeKind.Local).AddTicks(2952),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 18, 8, 25, 27, DateTimeKind.Local).AddTicks(3414));

            migrationBuilder.AddColumn<bool>(
                name: "ForcedPasswordReset",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "ForcedPasswordReset", "IsActive", "Password", "Username" },
                values: new object[] { "110116-191123-293484", new DateTime(2023, 11, 19, 17, 9, 40, 629, DateTimeKind.Local).AddTicks(3623), true, true, "admin", "admin" });
        }
    }
}
