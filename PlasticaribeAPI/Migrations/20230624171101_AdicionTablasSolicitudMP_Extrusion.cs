using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class AdicionTablasSolicitudMP_Extrusion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitud_MatPrimaExtrusion",
                columns: table => new
                {
                    SolMpExt_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolMpExt_OT = table.Column<long>(type: "bigint", nullable: false),
                    SolMpExt_Maquina = table.Column<int>(type: "int", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    SolMpExt_Fecha = table.Column<DateTime>(type: "Date", nullable: false),
                    SolMpExt_Hora = table.Column<string>(type: "varchar(10)", nullable: true),
                    SolMpExt_Observacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud_MatPrimaExtrusion", x => x.SolMpExt_Id);
                    table.ForeignKey(
                        name: "FK_Solicitud_MatPrimaExtrusion_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_MatPrimaExtrusion_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_MatPrimaExtrusion_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetSolicitud_MatPrimaExtrusion",
                columns: table => new
                {
                    Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolMpExt_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtSolMpExt_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetSolicitud_MatPrimaExtrusion", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_DetSolicitud_MatPrimaExtrusion_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetSolicitud_MatPrimaExtrusion_Solicitud_MatPrimaExtrusion_SolMpExt_Id",
                        column: x => x.SolMpExt_Id,
                        principalTable: "Solicitud_MatPrimaExtrusion",
                        principalColumn: "SolMpExt_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetSolicitud_MatPrimaExtrusion_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetSolicitud_MatPrimaExtrusion_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_MatPri_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_SolMpExt_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "SolMpExt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_Tinta_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_UndMed_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_MatPrimaExtrusion_Estado_Id",
                table: "Solicitud_MatPrimaExtrusion",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_MatPrimaExtrusion_Proceso_Id",
                table: "Solicitud_MatPrimaExtrusion",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_MatPrimaExtrusion_Usua_Id",
                table: "Solicitud_MatPrimaExtrusion",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropTable(
                name: "Solicitud_MatPrimaExtrusion");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
