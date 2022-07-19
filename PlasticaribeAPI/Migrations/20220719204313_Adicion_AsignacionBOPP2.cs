using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_AsignacionBOPP2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallesAsignaciones_BOPP",
                columns: table => new
                {
                    AsigBOPP_Id = table.Column<long>(type: "bigint", nullable: false),
                    BOPP_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtAsigBOPP_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAsignaciones_BOPP", x => new { x.AsigBOPP_Id, x.BOPP_Id });
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_BOPP_Asignaciones_BOPP_AsigBOPP_Id",
                        column: x => x.AsigBOPP_Id,
                        principalTable: "Asignaciones_BOPP",
                        principalColumn: "AsigBOPP_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_BOPP_BOPP_BOPP_Id",
                        column: x => x.BOPP_Id,
                        principalTable: "BOPP",
                        principalColumn: "BOPP_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_BOPP_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesAsignaciones_BOPP_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_BOPP_BOPP_Id",
                table: "DetallesAsignaciones_BOPP",
                column: "BOPP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_BOPP_Proceso_Id",
                table: "DetallesAsignaciones_BOPP",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_BOPP_UndMed_Id",
                table: "DetallesAsignaciones_BOPP",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAsignaciones_BOPP");
        }
    }
}
