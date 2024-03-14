using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class adicion_ClaseDetalleAsignacion_MatPrimaXTinta2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallesAsignaciones_MatPrimasXTintas",
                columns: table => new
                {
                    AsigMPxTinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    DetAsigMPxTinta_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAsignaciones_MatPrimasXTintas", x => new { x.AsigMPxTinta_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MatPrimasXTintas_Asignaciones_MatPrimasXTintas_AsigMPxTinta_Id",
                        column: x => x.AsigMPxTinta_Id,
                        principalTable: "Asignaciones_MatPrimasXTintas",
                        principalColumn: "AsigMPxTinta_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MatPrimasXTintas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MatPrimasXTintas_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_MatPrimasXTintas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MatPrimasXTintas_MatPri_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MatPrimasXTintas_Proceso_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MatPrimasXTintas_UndMed_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAsignaciones_MatPrimasXTintas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
