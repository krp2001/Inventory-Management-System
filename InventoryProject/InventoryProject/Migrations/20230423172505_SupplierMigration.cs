using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryProject.Migrations
{
    public partial class SupplierMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    SupplierEmailId = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    SupplierPhoneNo = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
