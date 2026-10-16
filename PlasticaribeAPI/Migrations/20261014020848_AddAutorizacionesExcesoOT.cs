using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAutorizacionesExcesoOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autorizaciones_ExcesoOT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    CantidadExcesoKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Motivo = table.Column<string>(type: "varchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    UsuarioSolicitaId = table.Column<long>(type: "bigint", nullable: false),
                    FechaSolicitudUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioAutorizaId = table.Column<long>(type: "bigint", nullable: true),
                    MotivoRespuesta = table.Column<string>(type: "varchar(max)", nullable: true),
                    FechaRespuestaUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AsignacionMatPrimaId = table.Column<long>(type: "bigint", nullable: true),
                    FechaConsumoUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorizaciones_ExcesoOT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autorizaciones_ExcesoOT_Asignaciones_MatPrima_AsignacionMatPrimaId",
                        column: x => x.AsignacionMatPrimaId,
                        principalTable: "Asignaciones_MatPrima",
                        principalColumn: "AsigMp_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Autorizaciones_ExcesoOT_Usuarios_UsuarioAutorizaId",
                        column: x => x.UsuarioAutorizaId,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Autorizaciones_ExcesoOT_Usuarios_UsuarioSolicitaId",
                        column: x => x.UsuarioSolicitaId,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_ExcesoOT_AsignacionMatPrimaId",
                table: "Autorizaciones_ExcesoOT",
                column: "AsignacionMatPrimaId");

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_ExcesoOT_OrdenTrabajo_Estado",
                table: "Autorizaciones_ExcesoOT",
                columns: new[] { "OrdenTrabajo", "Estado" });

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_ExcesoOT_UsuarioAutorizaId",
                table: "Autorizaciones_ExcesoOT",
                column: "UsuarioAutorizaId");

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_ExcesoOT_UsuarioSolicitaId",
                table: "Autorizaciones_ExcesoOT",
                column: "UsuarioSolicitaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autorizaciones_ExcesoOT");
        }
    }
}
