using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryProject.Migrations
{
    public partial class cartAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CartProductId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartUserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.ProductId, x.UserId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartProductId_CartUserId",
                table: "Products",
                columns: new[] { "CartProductId", "CartUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartProductId_CartUserId",
                table: "Products",
                columns: new[] { "CartProductId", "CartUserId" },
                principalTable: "Carts",
                principalColumns: new[] { "ProductId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartProductId_CartUserId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartProductId_CartUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartUserId",
                table: "Products");
        }
    }
}
