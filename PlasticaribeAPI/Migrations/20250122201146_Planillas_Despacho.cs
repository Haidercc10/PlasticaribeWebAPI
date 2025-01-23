using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Planillas_Despacho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planillas_Despacho",
                columns: table => new
                {
                    Pla_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Conductor = table.Column<long>(type: "bigint", nullable: false),
                    Pla_Placa = table.Column<string>(type: "varchar(50)", nullable: false),
                    Pla_Fecha = table.Column<DateTime>(type: "date", nullable: true),
                    Pla_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Pla_ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Pla_ValorContado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: true),
                    Pla_FechaRecepcion = table.Column<DateTime>(type: "date", nullable: true),
                    Pla_HoraRecepcion = table.Column<string>(type: "varchar(10)", nullable: false),
                    Pla_ValorRecibido = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Pla_Observacion = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planillas_Despacho", x => x.Pla_Id);
                    table.ForeignKey(
                        name: "FK_Planillas_Despacho_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planillas_Despacho_Usuarios_Usua_Conductor",
                        column: x => x.Usua_Conductor,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planillas_Despacho_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_PlanillaDespacho",
                columns: table => new
                {
                    DtPla_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pla_Id = table.Column<int>(type: "int", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtPla_Factura = table.Column<string>(type: "varchar(50)", nullable: false),
                    Pla_ValorFactura = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DtPla_FormaPago = table.Column<string>(type: "varchar(50)", nullable: false),
                    DtPla_UnidadesProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_PlanillaDespacho", x => x.DtPla_Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_PlanillaDespacho_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_PlanillaDespacho_Planillas_Despacho_Pla_Id",
                        column: x => x.Pla_Id,
                        principalTable: "Planillas_Despacho",
                        principalColumn: "Pla_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_PlanillaDespacho_Cli_Id",
                table: "Detalles_PlanillaDespacho",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_PlanillaDespacho_Pla_Id",
                table: "Detalles_PlanillaDespacho",
                column: "Pla_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_Despacho_Estado_Id",
                table: "Planillas_Despacho",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_Despacho_Usua_Conductor",
                table: "Planillas_Despacho",
                column: "Usua_Conductor");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_Despacho_Usua_Id",
                table: "Planillas_Despacho",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_PlanillaDespacho");

            migrationBuilder.DropTable(
                name: "Planillas_Despacho");
        }
    }
}
