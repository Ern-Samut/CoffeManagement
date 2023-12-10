using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeApi.Migrations
{
    public partial class InitCoffes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Category = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Code", "CreatedOn", "Name", "Price" },
                values: new object[] { "841cbc9e-cb95-4b4c-b2c0-cb7b3d64441d", (byte)1, "CffD001", new DateTime(2023, 12, 10, 19, 6, 2, 561, DateTimeKind.Local).AddTicks(8408), "Moca", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Code", "CreatedOn", "Name", "Price" },
                values: new object[] { "a42df353-757f-4611-9261-eeab6e2e45ac", (byte)2, "CFFD002", new DateTime(2023, 12, 10, 19, 6, 2, 561, DateTimeKind.Local).AddTicks(8431), "Capucino", null });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
