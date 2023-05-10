using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace product_api.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Created_at", "Description", "Image_url", "Price", "State", "Title", "Updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9250), "Description", "https...", 200f, "Draft", "Milk", new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9301) },
                    { 2, new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9303), "Description", "https...", 500f, "Draft", "Flower", new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9304) },
                    { 3, new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9307), "Description", "https...", 100f, "Draft", "Fish", new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9308) },
                    { 4, new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9309), "Description", "https...", 270f, "Draft", "Wood", new DateTime(2023, 5, 6, 18, 6, 0, 940, DateTimeKind.Local).AddTicks(9310) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { "elyse.customer@email.com", "elyse_customer", "MyPass_w0rd", "Customer" },
                    { "elyse.seller@email.com", "elyse_manager", "MyPass_w0rd", "Manager" },
                    { "jason.admin@email.com", "jason_admin", "MyPass_w0rd", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
