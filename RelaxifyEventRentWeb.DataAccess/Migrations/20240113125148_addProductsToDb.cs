using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RelaxifyEventRentWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProductsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, "Czarny namiot o ymiarach 3x6 idealny na wydarzenia plenerowe.", 450, "Namiot 3x6" },
                    { 2, "Czarny namiot o ymiarach 2x2 idealny na wydarzenia plenerowe.", 250, "Namiot 2x2" },
                    { 3, "Podesty sceniczne allustage idealne na Twoje wydarzenia", 30, "Podesty sceniczne" },
                    { 4, "Namiot w kształcie gwiazdy, wyglada swietnie na każdym uroczystm wydarzeniu, ale nie tylko", 2760, "Namiot gwiazda" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
