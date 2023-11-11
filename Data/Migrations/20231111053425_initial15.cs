using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TableId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(9484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 67, DateTimeKind.Local).AddTicks(381));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(7757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 66, DateTimeKind.Local).AddTicks(8255));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 97, DateTimeKind.Local).AddTicks(5899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 65, DateTimeKind.Local).AddTicks(4720));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TableId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 67, DateTimeKind.Local).AddTicks(381),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(9484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 66, DateTimeKind.Local).AddTicks(8255),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 98, DateTimeKind.Local).AddTicks(7757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 11, 11, 57, 29, 65, DateTimeKind.Local).AddTicks(4720),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 11, 12, 34, 25, 97, DateTimeKind.Local).AddTicks(5899));
        }
    }
}
