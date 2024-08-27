using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AsignacionRollos_OT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asignacion_RollosOT",
                columns: table => new
                {
                    AsgRll_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsgRll_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    AsgRll_Hora = table.Column<string>(type: "varchar(50)", nullable: false),
                    AsgRll_Observacion = table.Column<string>(type: "varchar(max)", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignacion_RollosOT", x => x.AsgRll_Id);
                    table.ForeignKey(
                        name: "FK_Asignacion_RollosOT_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_AsignacionRollosOT",
                columns: table => new
                {
                    DtlAsgRll_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsgRll_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtAsgRll_OT = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Rollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtAsgRll_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_AsignacionRollosOT", x => x.DtlAsgRll_Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_AsignacionRollosOT_Asignacion_RollosOT_AsgRll_Id",
                        column: x => x.AsgRll_Id,
                        principalTable: "Asignacion_RollosOT",
                        principalColumn: "AsgRll_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_AsignacionRollosOT_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_AsignacionRollosOT_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_AsignacionRollosOT_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignacion_RollosOT_Usua_Id",
                table: "Asignacion_RollosOT",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_AsignacionRollosOT_AsgRll_Id",
                table: "Detalles_AsignacionRollosOT",
                column: "AsgRll_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_AsignacionRollosOT_Proceso_Id",
                table: "Detalles_AsignacionRollosOT",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_AsignacionRollosOT_Prod_Id",
                table: "Detalles_AsignacionRollosOT",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_AsignacionRollosOT_UndMed_Id",
                table: "Detalles_AsignacionRollosOT",
                column: "UndMed_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_AsignacionRollosOT");

            migrationBuilder.DropTable(
                name: "Asignacion_RollosOT");
        }
    }
}
