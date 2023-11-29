using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishOfMedia");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "110116-231123-747169");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-231123-321898");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-231123-322069");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "108101-231123-746990");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(9283),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 23, 20, 28, 39, 372, DateTimeKind.Local).AddTicks(8054));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(2712),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 23, 20, 28, 39, 371, DateTimeKind.Local).AddTicks(5701));

            migrationBuilder.AddColumn<byte[]>(
                name: "Cover",
                table: "Dishes",
                type: "varbinary(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Dishes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 23, 20, 28, 39, 372, DateTimeKind.Local).AddTicks(8054),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(9283));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 23, 20, 28, 39, 371, DateTimeKind.Local).AddTicks(5701),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 11, 29, 15, 52, 5, 614, DateTimeKind.Local).AddTicks(2712));

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 23, 20, 28, 39, 372, DateTimeKind.Local).AddTicks(6525)),
                    Desc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishOfMedia",
                columns: table => new
                {
                    MediaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DishId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishOfMedia", x => new { x.MediaId, x.DishId });
                    table.ForeignKey(
                        name: "FK_DishOfMedia_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishOfMedia_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Level", "Name" },
                values: new object[,]
                {
                    { "108101-231123-321898", 2, "creator" },
                    { "108101-231123-322069", 3, "staff" },
                    { "108101-231123-746990", 1, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Password", "RoleId", "Username" },
                values: new object[] { "110116-231123-747169", new DateTime(2023, 11, 23, 20, 28, 39, 632, DateTimeKind.Local).AddTicks(1235), true, "$2a$11$xe0euElPoyzjinVIsMCgIOITghnZY2ZUrb0Lg0.EKGFpisVSCznni", "108101-231123-746990", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DishOfMedia_DishId",
                table: "DishOfMedia",
                column: "DishId");
        }
    }
}
