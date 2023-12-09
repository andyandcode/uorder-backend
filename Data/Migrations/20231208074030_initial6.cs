using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountForDish_DiscountCodes_DishId",
                table: "DiscountForDish");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountForDish_Dishes_DiscountCodeId",
                table: "DiscountForDish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountForDish",
                table: "DiscountForDish");

            migrationBuilder.DropIndex(
                name: "IX_DiscountForDish_DishId",
                table: "DiscountForDish");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-081223-035810");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-670438");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-670532");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-081223-035670");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 597, DateTimeKind.Local).AddTicks(3690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 14, 10, 48, 800, DateTimeKind.Local).AddTicks(3121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 596, DateTimeKind.Local).AddTicks(5271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 14, 10, 48, 799, DateTimeKind.Local).AddTicks(6638));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountForDish",
                table: "DiscountForDish",
                columns: new[] { "DishId", "DiscountCodeId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_DiscountForDish_DiscountCodeId",
                table: "DiscountForDish",
                column: "DiscountCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountForDish_DiscountCodes_DiscountCodeId",
                table: "DiscountForDish",
                column: "DiscountCodeId",
                principalTable: "DiscountCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountForDish_Dishes_DishId",
                table: "DiscountForDish",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountForDish_DiscountCodes_DiscountCodeId",
                table: "DiscountForDish");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountForDish_Dishes_DishId",
                table: "DiscountForDish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountForDish",
                table: "DiscountForDish");

            migrationBuilder.DropIndex(
                name: "IX_DiscountForDish_DiscountCodeId",
                table: "DiscountForDish");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 14, 10, 48, 800, DateTimeKind.Local).AddTicks(3121),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 597, DateTimeKind.Local).AddTicks(3690));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 8, 14, 10, 48, 799, DateTimeKind.Local).AddTicks(6638),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 8, 14, 40, 30, 596, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountForDish",
                table: "DiscountForDish",
                columns: new[] { "DiscountCodeId", "DishId" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-081223-035670", 1, "admin" },
                    { "108101-081223-670438", 2, "creator" },
                    { "108101-081223-670532", 3, "staff" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-081223-035810", new DateTime(2023, 12, 8, 14, 10, 49, 66, DateTimeKind.Local).AddTicks(9824), true, "$2a$11$EM6Uk0X.5skG6a4pOhq/p.b6r6BDKRqtKhl/QbB3GRPOOeWCcthHO", "108101-081223-035670", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountForDish_DishId",
                table: "DiscountForDish",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountForDish_DiscountCodes_DishId",
                table: "DiscountForDish",
                column: "DishId",
                principalTable: "DiscountCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountForDish_Dishes_DiscountCodeId",
                table: "DiscountForDish",
                column: "DiscountCodeId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
