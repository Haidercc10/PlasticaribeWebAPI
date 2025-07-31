using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SubcategoriasMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subcategorias_MatPrima",
                columns: table => new
                {
                    SubCatMP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCatMP_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    SubCatMP_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    CatMP_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategorias_MatPrima", x => x.SubCatMP_Id);
                    table.ForeignKey(
                        name: "FK_Subcategorias_MatPrima_Categorias_MatPrima_CatMP_Id",
                        column: x => x.CatMP_Id,
                        principalTable: "Categorias_MatPrima",
                        principalColumn: "CatMP_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subcategorias_MatPrima_CatMP_Id",
                table: "Subcategorias_MatPrima",
                column: "CatMP_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subcategorias_MatPrima");
        }
    }
}
