using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Devoluciones_Calidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requerimientos_Calidad",
                columns: table => new
                {
                    Req_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Req_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Req_Descripcion = table.Column<string>(type: "varchar(max)", nullable: true),
                    Req_FechaCreacion = table.Column<DateTime>(type: "date", nullable: false),
                    Req_HoraCreacion = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimientos_Calidad", x => x.Req_Id);
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones_Calidad",
                columns: table => new
                {
                    Dvc_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dvc_Fecha = table.Column<DateTime>(type: "date", nullable: true),
                    Dvc_Ano = table.Column<int>(type: "int", nullable: false),
                    Dvc_Mes = table.Column<string>(type: "varchar(20)", nullable: false),
                    Dvc_OT = table.Column<int>(type: "int", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Falla_Id = table.Column<int>(type: "int", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Req_Id = table.Column<int>(type: "int", nullable: false),
                    Dvc_TipoRechazo = table.Column<string>(type: "varchar(50)", nullable: false),
                    Dvc_PesoBruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dvc_PesoNeto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dvc_Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dvc_Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dvc_FechaProduccion = table.Column<DateTime>(type: "date", nullable: true),
                    Dvc_Observacion = table.Column<string>(type: "varchar(max)", nullable: true),
                    Dvc_FechaRegistro = table.Column<DateTime>(type: "date", nullable: false),
                    Dvc_Hora = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoluciones_Calidad", x => x.Dvc_Id);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Calidad_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Calidad_Fallas_Tecnicas_Falla_Id",
                        column: x => x.Falla_Id,
                        principalTable: "Fallas_Tecnicas",
                        principalColumn: "Falla_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Calidad_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Calidad_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Calidad_Requerimientos_Calidad_Req_Id",
                        column: x => x.Req_Id,
                        principalTable: "Requerimientos_Calidad",
                        principalColumn: "Req_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_Calidad_Cli_Id",
                table: "Devoluciones_Calidad",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_Calidad_Falla_Id",
                table: "Devoluciones_Calidad",
                column: "Falla_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_Calidad_Proceso_Id",
                table: "Devoluciones_Calidad",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_Calidad_Prod_Id",
                table: "Devoluciones_Calidad",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_Calidad_Req_Id",
                table: "Devoluciones_Calidad",
                column: "Req_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devoluciones_Calidad");

            migrationBuilder.DropTable(
                name: "Requerimientos_Calidad");
        }
    }
}
