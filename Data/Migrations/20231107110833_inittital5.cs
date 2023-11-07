using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class inittital5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtyPerDate",
                table: "Dishes",
                newName: "QtyPerDay");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 7, 18, 8, 33, 11, DateTimeKind.Local).AddTicks(3492),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 59, 10, 494, DateTimeKind.Local).AddTicks(4114));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 7, 18, 8, 33, 11, DateTimeKind.Local).AddTicks(1453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 59, 10, 494, DateTimeKind.Local).AddTicks(2545));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 7, 18, 8, 33, 9, DateTimeKind.Local).AddTicks(6746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 59, 10, 493, DateTimeKind.Local).AddTicks(2010));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtyPerDay",
                table: "Dishes",
                newName: "QtyPerDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 59, 10, 494, DateTimeKind.Local).AddTicks(4114),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 7, 18, 8, 33, 11, DateTimeKind.Local).AddTicks(3492));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 59, 10, 494, DateTimeKind.Local).AddTicks(2545),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 7, 18, 8, 33, 11, DateTimeKind.Local).AddTicks(1453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 59, 10, 493, DateTimeKind.Local).AddTicks(2010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 7, 18, 8, 33, 9, DateTimeKind.Local).AddTicks(6746));
        }
    }
}
