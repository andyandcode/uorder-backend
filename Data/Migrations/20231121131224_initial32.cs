using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-191123-119936");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-191123-908710");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-191123-908819");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-191123-119776");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 21, 20, 12, 23, 827, DateTimeKind.Local).AddTicks(7704),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 19, 36, 52, 110, DateTimeKind.Local).AddTicks(977));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 21, 20, 12, 23, 827, DateTimeKind.Local).AddTicks(3319),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 19, 36, 52, 109, DateTimeKind.Local).AddTicks(7832));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 21, 20, 12, 23, 824, DateTimeKind.Local).AddTicks(4868),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 19, 19, 36, 52, 108, DateTimeKind.Local).AddTicks(1425));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-211123-309547", 1, "admin" },
                    { "108101-211123-765717", 2, "creator" },
                    { "108101-211123-765819", 3, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-211123-309816", new DateTime(2023, 11, 21, 20, 12, 24, 76, DateTimeKind.Local).AddTicks(4793), true, "$2a$11$u5Rxh6/luyczjC949k9j7.2XzTbj.dYc13YrxVTH5N.m.Yby64eaW", "108101-211123-309547", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-211123-309816");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-211123-765717");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-211123-765819");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-211123-309547");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 19, 36, 52, 110, DateTimeKind.Local).AddTicks(977),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 21, 20, 12, 23, 827, DateTimeKind.Local).AddTicks(7704));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 19, 36, 52, 109, DateTimeKind.Local).AddTicks(7832),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 21, 20, 12, 23, 827, DateTimeKind.Local).AddTicks(3319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 19, 19, 36, 52, 108, DateTimeKind.Local).AddTicks(1425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 21, 20, 12, 23, 824, DateTimeKind.Local).AddTicks(4868));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-191123-119776", 1, "admin" },
                    { "108101-191123-908710", 2, "creator" },
                    { "108101-191123-908819", 3, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-191123-119936", new DateTime(2023, 11, 19, 19, 36, 52, 390, DateTimeKind.Local).AddTicks(7831), true, "$2a$11$/1dsri0vTQWM2VAY80fYEu01x7OOMNS6dq2tDl7rTQ4vFfSbWfo1m", "108101-191123-119776", "admin" });
        }
    }
}
