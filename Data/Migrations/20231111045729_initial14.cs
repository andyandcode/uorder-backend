using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 67, DateTimeKind.Local).AddTicks(381),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 10, 21, 46, 17, 255, DateTimeKind.Local).AddTicks(1006));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 66, DateTimeKind.Local).AddTicks(8255),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 10, 21, 46, 17, 254, DateTimeKind.Local).AddTicks(9394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 65, DateTimeKind.Local).AddTicks(4720),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 10, 21, 46, 17, 253, DateTimeKind.Local).AddTicks(8803));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 10, 21, 46, 17, 255, DateTimeKind.Local).AddTicks(1006),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 67, DateTimeKind.Local).AddTicks(381));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 10, 21, 46, 17, 254, DateTimeKind.Local).AddTicks(9394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 66, DateTimeKind.Local).AddTicks(8255));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 10, 21, 46, 17, 253, DateTimeKind.Local).AddTicks(8803),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 65, DateTimeKind.Local).AddTicks(4720));
        }
    }
}
