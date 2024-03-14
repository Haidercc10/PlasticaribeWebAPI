using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_DetalleRecuperadoMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallesRecuperados_MateriasPrimas",
                columns: table => new
                {
                    RecMp_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    RecMatPri_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    TpRecu_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesRecuperados_MateriasPrimas", x => new { x.RecMp_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_DetallesRecuperados_MateriasPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesRecuperados_MateriasPrimas_Recuperados_MatPrima_RecMp_Id",
                        column: x => x.RecMp_Id,
                        principalTable: "Recuperados_MatPrima",
                        principalColumn: "RecMp_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesRecuperados_MateriasPrimas_Tipos_Recuperados_TpRecu_Id",
                        column: x => x.TpRecu_Id,
                        principalTable: "Tipos_Recuperados",
                        principalColumn: "TpRecu_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesRecuperados_MateriasPrimas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRecuperados_MateriasPrimas_MatPri_Id",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRecuperados_MateriasPrimas_TpRecu_Id",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "TpRecu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesRecuperados_MateriasPrimas_UndMed_Id",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesRecuperados_MateriasPrimas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
