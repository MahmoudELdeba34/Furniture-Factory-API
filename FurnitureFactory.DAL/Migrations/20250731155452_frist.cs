using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureFactory.DAL.Migrations
{
    /// <inheritdoc />
    public partial class frist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcomponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    DetailLength = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    DetailWidth = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    DetailThickness = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    CuttingLength = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    CuttingWidth = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    CuttingThickness = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    FinalLength = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    FinalWidth = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    FinalThickness = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    VeneerInner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VeneerOuter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcomponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcomponents_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ProductId",
                table: "Components",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcomponents_ComponentId",
                table: "Subcomponents",
                column: "ComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subcomponents");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
