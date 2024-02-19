using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFields_BoppTinta_InvAreas4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "Categorias_Insumos");

            /**/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /**/

            migrationBuilder.CreateTable(
                name: "Categorias_Insumos",
                columns: table => new
                {
                    CatInsu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatInsu_Descripcion = table.Column<string>(type: "varchar(max)", nullable: false),
                    CatInsu_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    CatInsu_UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias_Insumos", x => x.CatInsu_Id);
                });

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    Insu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatInsu_Id = table.Column<int>(type: "int", nullable: false),
                    Insu_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Insu_Stock = table.Column<string>(type: "varchar(100)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.Insu_Id);
                    table.ForeignKey(
                        name: "FK_Insumos_Categorias_Insumos_CatInsu_Id",
                        column: x => x.CatInsu_Id,
                        principalTable: "Categorias_Insumos",
                        principalColumn: "CatInsu_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insumos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insumos_CatInsu_Id",
                table: "Insumos",
                column: "CatInsu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Insumos_UndMed_Id",
                table: "Insumos",
                column: "UndMed_Id");
        }
    }
}
