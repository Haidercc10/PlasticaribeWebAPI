using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Bodegas_Areas_Rollos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bodegas_Rollos",
                columns: table => new
                {
                    BgRollo_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BgRollo_FechaEntrada = table.Column<DateTime>(type: "date", nullable: false),
                    BgRollo_HoraEntrada = table.Column<string>(type: "varchar(10)", nullable: false),
                    BgRollo_FechaModifica = table.Column<DateTime>(type: "date", nullable: false),
                    BgRollo_HoraModifica = table.Column<string>(type: "varchar(10)", nullable: false),
                    BgRollo_Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas_Rollos", x => x.BgRollo_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Solicitud_Rollos_Areas",
                columns: table => new
                {
                    TpSol_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpSol_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TpSol_Descricion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Solicitud_Rollos_Areas", x => x.TpSol_Id);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_BodegasRollos",
                columns: table => new
                {
                    Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BgRollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    BgRollo_OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    DtBgRollo_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    DtBgRollo_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    BgRollo_BodegaActual = table.Column<string>(type: "varchar(10)", nullable: false),
                    DtBgRollo_Extrusion = table.Column<bool>(type: "bit", nullable: false),
                    DtBgRollo_ProdIntermedio = table.Column<bool>(type: "bit", nullable: false),
                    DtBgRollo_Impresion = table.Column<bool>(type: "bit", nullable: false),
                    DtBgRollo_Rotograbado = table.Column<bool>(type: "bit", nullable: false),
                    DtBgRollo_Sellado = table.Column<bool>(type: "bit", nullable: false),
                    DtBgRollo_Corte = table.Column<bool>(type: "bit", nullable: false),
                    DtBgRollo_Despacho = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_BodegasRollos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_BodegasRollos_Bodegas_Rollos_BgRollo_Id",
                        column: x => x.BgRollo_Id,
                        principalTable: "Bodegas_Rollos",
                        principalColumn: "BgRollo_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_BodegasRollos_Procesos_BgRollo_BodegaActual",
                        column: x => x.BgRollo_BodegaActual,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_BodegasRollos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_BodegasRollos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Solicitud_Rollos_Areas",
                columns: table => new
                {
                    SolRollo_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    SolRollo_FechaSolicitud = table.Column<DateTime>(type: "date", nullable: false),
                    SolRollo_HoraSolicitud = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Respuesta = table.Column<long>(type: "bigint", nullable: false),
                    SolRollo_FechaRespuesta = table.Column<DateTime>(type: "date", nullable: true),
                    SolRollo_HoraRespuesta = table.Column<string>(type: "varchar(10)", nullable: true),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    TpSol_Id = table.Column<int>(type: "int", nullable: false),
                    SolRollo_Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud_Rollos_Areas", x => x.SolRollo_Id);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Tipo_Solicitud_Rollos_Areas_TpSol_Id",
                        column: x => x.TpSol_Id,
                        principalTable: "Tipo_Solicitud_Rollos_Areas",
                        principalColumn: "TpSol_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Usuarios_Usua_Respuesta",
                        column: x => x.Usua_Respuesta,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_SolicitudRollos",
                columns: table => new
                {
                    Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolRollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtSolRollo_OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    DtSolRollo_Maquina = table.Column<long>(type: "bigint", nullable: false),
                    DtSolRollo_BodegaSolicitante = table.Column<string>(type: "varchar(10)", nullable: false),
                    DtSolRollo_BodegaSolicitada = table.Column<string>(type: "varchar(10)", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    DtSolRollo_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    DtSolRollo_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_SolicitudRollos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudRollos_Procesos_DtSolRollo_BodegaSolicitada",
                        column: x => x.DtSolRollo_BodegaSolicitada,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudRollos_Procesos_DtSolRollo_BodegaSolicitante",
                        column: x => x.DtSolRollo_BodegaSolicitante,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudRollos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudRollos_Solicitud_Rollos_Areas_SolRollo_Id",
                        column: x => x.SolRollo_Id,
                        principalTable: "Solicitud_Rollos_Areas",
                        principalColumn: "SolRollo_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudRollos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_BodegasRollos_BgRollo_BodegaActual",
                table: "Detalles_BodegasRollos",
                column: "BgRollo_BodegaActual");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_BodegasRollos_BgRollo_Id",
                table: "Detalles_BodegasRollos",
                column: "BgRollo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_BodegasRollos_Prod_Id",
                table: "Detalles_BodegasRollos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_BodegasRollos_UndMed_Id",
                table: "Detalles_BodegasRollos",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudRollos_DtSolRollo_BodegaSolicitada",
                table: "Detalles_SolicitudRollos",
                column: "DtSolRollo_BodegaSolicitada");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudRollos_DtSolRollo_BodegaSolicitante",
                table: "Detalles_SolicitudRollos",
                column: "DtSolRollo_BodegaSolicitante");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudRollos_Prod_Id",
                table: "Detalles_SolicitudRollos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudRollos_SolRollo_Id",
                table: "Detalles_SolicitudRollos",
                column: "SolRollo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudRollos_UndMed_Id",
                table: "Detalles_SolicitudRollos",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_Estado_Id",
                table: "Solicitud_Rollos_Areas",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_TpSol_Id",
                table: "Solicitud_Rollos_Areas",
                column: "TpSol_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_Usua_Id",
                table: "Solicitud_Rollos_Areas",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_Usua_Respuesta",
                table: "Solicitud_Rollos_Areas",
                column: "Usua_Respuesta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_BodegasRollos");

            migrationBuilder.DropTable(
                name: "Detalles_SolicitudRollos");

            migrationBuilder.DropTable(
                name: "Bodegas_Rollos");

            migrationBuilder.DropTable(
                name: "Solicitud_Rollos_Areas");

            migrationBuilder.DropTable(
                name: "Tipo_Solicitud_Rollos_Areas");
        }
    }
}
