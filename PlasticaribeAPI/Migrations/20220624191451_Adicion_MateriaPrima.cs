using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_MateriaPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materias_Primas",
                columns: table => new
                {
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatPri_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    MatPri_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    MatPri_Stock = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    CatMP_Id = table.Column<int>(type: "int", nullable: false),
                    MatPri_Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias_Primas", x => x.MatPri_Id);
                    table.ForeignKey(
                        name: "FK_Materias_Primas_Categorias_MatPrima_CatMP_Id",
                        column: x => x.CatMP_Id,
                        principalTable: "Categorias_MatPrima",
                        principalColumn: "CatMP_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materias_Primas_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materias_Primas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materias_Primas_CatMP_Id",
                table: "Materias_Primas",
                column: "CatMP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_Primas_TpBod_Id",
                table: "Materias_Primas",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_Primas_UndMed_Id",
                table: "Materias_Primas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materias_Primas");
        }
    }
}
