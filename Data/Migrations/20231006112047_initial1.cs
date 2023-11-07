using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Dishes_DishId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 20, 47, 813, DateTimeKind.Local).AddTicks(215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 15, 33, 82, DateTimeKind.Local).AddTicks(3914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 20, 47, 812, DateTimeKind.Local).AddTicks(8930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 15, 33, 82, DateTimeKind.Local).AddTicks(560));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 20, 47, 811, DateTimeKind.Local).AddTicks(9369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 15, 33, 80, DateTimeKind.Local).AddTicks(3512));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Dishes_DishId",
                table: "OrderDetails",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Dishes_DishId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 15, 33, 82, DateTimeKind.Local).AddTicks(3914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 20, 47, 813, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Medias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 15, 33, 82, DateTimeKind.Local).AddTicks(560),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 20, 47, 812, DateTimeKind.Local).AddTicks(8930));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 6, 18, 15, 33, 80, DateTimeKind.Local).AddTicks(3512),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 10, 6, 18, 20, 47, 811, DateTimeKind.Local).AddTicks(9369));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Dishes_DishId",
                table: "OrderDetails",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
