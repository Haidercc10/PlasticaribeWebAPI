using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_ClaseRemisionMateriaPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remisiones_MateriasPrimas",
                columns: table => new
                {
                    Rem_Id = table.Column<int>(type: "int", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    RemiMatPri_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    RemiMatPri_ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remisiones_MateriasPrimas", x => new { x.Rem_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_Remisiones_MateriasPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remisiones_MateriasPrimas_Remisiones_Rem_Id",
                        column: x => x.Rem_Id,
                        principalTable: "Remisiones",
                        principalColumn: "Rem_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remisiones_MateriasPrimas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_MateriasPrimas_MatPri_Id",
                table: "Remisiones_MateriasPrimas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_MateriasPrimas_UndMed_Id",
                table: "Remisiones_MateriasPrimas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remisiones_MateriasPrimas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
