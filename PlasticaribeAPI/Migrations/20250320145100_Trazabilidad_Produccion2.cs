using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Trazabilidad_Produccion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trazabilidad_Produccion",
                columns: table => new
                {
                    Trz_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trz_Etiqueta = table.Column<long>(type: "bigint", nullable: false),
                    Trz_Ot = table.Column<int>(type: "int", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Trz_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Trz_Hora = table.Column<string>(type: "varchar(10)", nullable: true),
                    Trz_PesoNeto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Trz_PesoBruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Trz_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Trz_Maquina = table.Column<int>(type: "int", nullable: false),
                    Operario_1 = table.Column<long>(type: "bigint", nullable: false),
                    Operario_2 = table.Column<long>(type: "bigint", nullable: false),
                    Operario_3 = table.Column<long>(type: "bigint", nullable: false),
                    Operario_4 = table.Column<long>(type: "bigint", nullable: false),
                    Trz_EtiquetaAnterior = table.Column<long>(type: "bigint", nullable: false),
                    Trz_OtAnterior = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Anterior = table.Column<int>(type: "int", nullable: false),
                    Proceso_Anterior = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trazabilidad_Produccion", x => x.Trz_Id);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Procesos_Proceso_Anterior",
                        column: x => x.Proceso_Anterior,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Productos_Prod_Anterior",
                        column: x => x.Prod_Anterior,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Usuarios_Operario_1",
                        column: x => x.Operario_1,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Usuarios_Operario_2",
                        column: x => x.Operario_2,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Usuarios_Operario_3",
                        column: x => x.Operario_3,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trazabilidad_Produccion_Usuarios_Operario_4",
                        column: x => x.Operario_4,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Cli_Id",
                table: "Trazabilidad_Produccion",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Operario_1",
                table: "Trazabilidad_Produccion",
                column: "Operario_1");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Operario_2",
                table: "Trazabilidad_Produccion",
                column: "Operario_2");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Operario_3",
                table: "Trazabilidad_Produccion",
                column: "Operario_3");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Operario_4",
                table: "Trazabilidad_Produccion",
                column: "Operario_4");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Proceso_Anterior",
                table: "Trazabilidad_Produccion",
                column: "Proceso_Anterior");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Proceso_Id",
                table: "Trazabilidad_Produccion",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Prod_Anterior",
                table: "Trazabilidad_Produccion",
                column: "Prod_Anterior");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Prod_Id",
                table: "Trazabilidad_Produccion",
                column: "Prod_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trazabilidad_Produccion");
        }
    }
}
