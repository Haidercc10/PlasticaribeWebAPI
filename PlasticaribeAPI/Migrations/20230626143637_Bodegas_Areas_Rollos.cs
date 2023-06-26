using System;
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
                    BgRollo_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    BgRollo_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    BgRollo_BodegaActual = table.Column<string>(type: "varchar(10)", nullable: false),
                    BgRollo_Extrusion = table.Column<bool>(type: "bit", nullable: false),
                    BgRollo_ProdIntermedio = table.Column<bool>(type: "bit", nullable: false),
                    BgRollo_Impresion = table.Column<bool>(type: "bit", nullable: false),
                    BgRollo_Rotograbado = table.Column<bool>(type: "bit", nullable: false),
                    BgRollo_Sellado = table.Column<bool>(type: "bit", nullable: false),
                    BgRollo_Corte = table.Column<bool>(type: "bit", nullable: false),
                    BgRollo_Despacho = table.Column<bool>(type: "bit", nullable: false),
                    BgRollo_Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas_Rollos", x => x.BgRollo_Id);
                    table.ForeignKey(
                        name: "FK_Bodegas_Rollos_Procesos_BgRollo_BodegaActual",
                        column: x => x.BgRollo_BodegaActual,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bodegas_Rollos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Solicitud_Rollos_Areas",
                columns: table => new
                {
                    SolRollo_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    SolRollo_FechaSolicitud = table.Column<DateTime>(type: "date", nullable: false),
                    SolRollo_HoraSolicitud = table.Column<string>(type: "varchar(10)", nullable: false),
                    SolRollo_FechaRespues = table.Column<DateTime>(type: "date", nullable: false),
                    SolRollo_HoraRespuesta = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    SolRollo_OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    SolRollo_Maquina = table.Column<long>(type: "bigint", nullable: false),
                    SolRollo_BodegaSolicitante = table.Column<string>(type: "varchar(10)", nullable: false),
                    SolRollo_BodegaSolicitada = table.Column<string>(type: "varchar(10)", nullable: false),
                    SolRollo_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    SolRollo_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
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
                        name: "FK_Solicitud_Rollos_Areas_Procesos_SolRollo_BodegaSolicitada",
                        column: x => x.SolRollo_BodegaSolicitada,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Procesos_SolRollo_BodegaSolicitante",
                        column: x => x.SolRollo_BodegaSolicitante,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Tipo_Solicitud_Rollos_Areas_TpSol_Id",
                        column: x => x.TpSol_Id,
                        principalTable: "Tipo_Solicitud_Rollos_Areas",
                        principalColumn: "TpSol_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_Rollos_Areas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_Rollos_BgRollo_BodegaActual",
                table: "Bodegas_Rollos",
                column: "BgRollo_BodegaActual");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_Rollos_UndMed_Id",
                table: "Bodegas_Rollos",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_Estado_Id",
                table: "Solicitud_Rollos_Areas",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_SolRollo_BodegaSolicitada",
                table: "Solicitud_Rollos_Areas",
                column: "SolRollo_BodegaSolicitada");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_SolRollo_BodegaSolicitante",
                table: "Solicitud_Rollos_Areas",
                column: "SolRollo_BodegaSolicitante");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_TpSol_Id",
                table: "Solicitud_Rollos_Areas",
                column: "TpSol_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_UndMed_Id",
                table: "Solicitud_Rollos_Areas",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_Rollos_Areas_Usua_Id",
                table: "Solicitud_Rollos_Areas",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bodegas_Rollos");

            migrationBuilder.DropTable(
                name: "Solicitud_Rollos_Areas");

            migrationBuilder.DropTable(
                name: "Tipo_Solicitud_Rollos_Areas");
        }
    }
}
