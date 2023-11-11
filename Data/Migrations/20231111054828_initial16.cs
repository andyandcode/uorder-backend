using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 48, 27, 879, DateTimeKind.Local).AddTicks(7489),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(9484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 48, 27, 879, DateTimeKind.Local).AddTicks(5675),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(7757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 48, 27, 878, DateTimeKind.Local).AddTicks(3693),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 97, DateTimeKind.Local).AddTicks(5899));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(9484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 48, 27, 879, DateTimeKind.Local).AddTicks(7489));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(7757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 48, 27, 879, DateTimeKind.Local).AddTicks(5675));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 97, DateTimeKind.Local).AddTicks(5899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 48, 27, 878, DateTimeKind.Local).AddTicks(3693));
        }
    }
}
