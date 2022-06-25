using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_DetallesAsignaciones_MateriasPrimas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallesAsignaciones_MateriasPrimas",
                columns: table => new
                {
                    AsigMp_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtAsigMp_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAsignaciones_MateriasPrimas", x => new { x.AsigMp_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MateriasPrimas_Asignaciones_MatPrima_AsigMp_Id",
                        column: x => x.AsigMp_Id,
                        principalTable: "Asignaciones_MatPrima",
                        principalColumn: "AsigMp_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MateriasPrimas_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MateriasPrimas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MateriasPrimas_MatPri_Id",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MateriasPrimas_Proceso_Id",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MateriasPrimas_UndMed_Id",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAsignaciones_MateriasPrimas");
        }
    }
}
