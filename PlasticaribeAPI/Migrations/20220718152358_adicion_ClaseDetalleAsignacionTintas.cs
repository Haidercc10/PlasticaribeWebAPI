using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class adicion_ClaseDetalleAsignacionTintas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalleAsignaciones_Tintas",
                columns: table => new
                {
                    AsigMp_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtAsigTinta_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleAsignaciones_Tintas", x => new { x.AsigMp_Id, x.Tinta_Id });
                    table.ForeignKey(
                        name: "FK_DetalleAsignaciones_Tintas_Asignaciones_MatPrima_AsigMp_Id",
                        column: x => x.AsigMp_Id,
                        principalTable: "Asignaciones_MatPrima",
                        principalColumn: "AsigMp_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleAsignaciones_Tintas_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleAsignaciones_Tintas_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleAsignaciones_Tintas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleAsignaciones_Tintas_Proceso_Id",
                table: "DetalleAsignaciones_Tintas",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleAsignaciones_Tintas_Tinta_Id",
                table: "DetalleAsignaciones_Tintas",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleAsignaciones_Tintas_UndMed_Id",
                table: "DetalleAsignaciones_Tintas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleAsignaciones_Tintas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
