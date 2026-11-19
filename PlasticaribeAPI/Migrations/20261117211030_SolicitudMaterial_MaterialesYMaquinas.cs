using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SolicitudMaterial_MaterialesYMaquinas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetSolicitud_Materiales",
                columns: table => new
                {
                    DtSolMat_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<long>(type: "bigint", nullable: false),
                    SolMpExt_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtSolMat_TipoMaterial = table.Column<int>(type: "int", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: true),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: true),
                    DtSolMat_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetSolicitud_Materiales", x => x.DtSolMat_Id);
                    table.CheckConstraint("CK_DetSolicitud_Materiales_Tipo", "([DtSolMat_TipoMaterial] = 1 AND [MatPri_Id] IS NOT NULL AND [Tinta_Id] IS NULL) OR ([DtSolMat_TipoMaterial] = 2 AND [Tinta_Id] IS NOT NULL AND [MatPri_Id] IS NULL)");
                    table.ForeignKey(
                        name: "FK_DetSolicitud_Materiales_DetSolicitud_MatPrimaExtrusion_Codigo",
                        column: x => x.Codigo,
                        principalTable: "DetSolicitud_MatPrimaExtrusion",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetSolicitud_Materiales_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetSolicitud_Materiales_Solicitud_MatPrimaExtrusion_SolMpExt_Id",
                        column: x => x.SolMpExt_Id,
                        principalTable: "Solicitud_MatPrimaExtrusion",
                        principalColumn: "SolMpExt_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetSolicitud_Materiales_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Solicitud_Maquinas",
                columns: table => new
                {
                    SolMaq_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolMpExt_Id = table.Column<long>(type: "bigint", nullable: false),
                    SolMaq_Maquina = table.Column<int>(type: "int", nullable: false),
                    SolMaq_Porcentaje = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud_Maquinas", x => x.SolMaq_Id);
                    table.ForeignKey(
                        name: "FK_Solicitud_Maquinas_Solicitud_MatPrimaExtrusion_SolMpExt_Id",
                        column: x => x.SolMpExt_Id,
                        principalTable: "Solicitud_MatPrimaExtrusion",
                        principalColumn: "SolMpExt_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_Materiales_Codigo",
                table: "DetSolicitud_Materiales",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_Materiales_MatPri_Id",
                table: "DetSolicitud_Materiales",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_Materiales_SolMpExt_Id",
                table: "DetSolicitud_Materiales",
                column: "SolMpExt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_Materiales_Tinta_Id",
                table: "DetSolicitud_Materiales",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Maquinas_SolMpExt_Id_SolMaq_Maquina",
                table: "Solicitud_Maquinas",
                columns: new[] { "SolMpExt_Id", "SolMaq_Maquina" },
                unique: true);

            // Las solicitudes existentes tenían una sola máquina: queda al 100%. Se omite la solicitud genérica (1),
            // que agrupa las asignaciones hechas sin solicitud, y las que no tienen máquina.
            migrationBuilder.Sql(@"
                INSERT INTO Solicitud_Maquinas (SolMpExt_Id, SolMaq_Maquina, SolMaq_Porcentaje)
                SELECT SolMpExt_Id, SolMpExt_Maquina, 100
                FROM Solicitud_MatPrimaExtrusion
                WHERE SolMpExt_Id <> 1 AND SolMpExt_Maquina > 0;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetSolicitud_Materiales");

            migrationBuilder.DropTable(
                name: "Solicitud_Maquinas");
        }
    }
}
