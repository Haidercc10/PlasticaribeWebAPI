using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTabla_Produccion_Procesos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produccion_Procesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Operario1_Id = table.Column<long>(type: "bigint", nullable: true),
                    Operario2_Id = table.Column<long>(type: "bigint", nullable: true),
                    Operario3_Id = table.Column<long>(type: "bigint", nullable: true),
                    Operario4_Id = table.Column<long>(type: "bigint", nullable: true),
                    Pesado_Entre = table.Column<int>(type: "int", nullable: false),
                    Maquina = table.Column<int>(type: "int", nullable: false),
                    Cono_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Ancho_Cono = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tara_Cono = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Peso_Bruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Peso_Neto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Peso_Teorico = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Desviacion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Presentacion = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Turno_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Envio_Zeus = table.Column<bool>(type: "bit", nullable: false),
                    Datos_Etiqueta = table.Column<string>(type: "varchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<string>(type: "varchar(20)", nullable: false),
                    Creador_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produccion_Procesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Conos_Cono_Id",
                        column: x => x.Cono_Id,
                        principalTable: "Conos",
                        principalColumn: "Cono_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Turnos_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turnos",
                        principalColumn: "Turno_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Unidades_Medidas_Presentacion",
                        column: x => x.Presentacion,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Usuarios_Creador_Id",
                        column: x => x.Creador_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Usuarios_Operario1_Id",
                        column: x => x.Operario1_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Usuarios_Operario2_Id",
                        column: x => x.Operario2_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Usuarios_Operario3_Id",
                        column: x => x.Operario3_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Procesos_Usuarios_Operario4_Id",
                        column: x => x.Operario4_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Cli_Id",
                table: "Produccion_Procesos",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Cono_Id",
                table: "Produccion_Procesos",
                column: "Cono_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Creador_Id",
                table: "Produccion_Procesos",
                column: "Creador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Operario1_Id",
                table: "Produccion_Procesos",
                column: "Operario1_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Operario2_Id",
                table: "Produccion_Procesos",
                column: "Operario2_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Operario3_Id",
                table: "Produccion_Procesos",
                column: "Operario3_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Operario4_Id",
                table: "Produccion_Procesos",
                column: "Operario4_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Presentacion",
                table: "Produccion_Procesos",
                column: "Presentacion");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Proceso_Id",
                table: "Produccion_Procesos",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Prod_Id",
                table: "Produccion_Procesos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Turno_Id",
                table: "Produccion_Procesos",
                column: "Turno_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produccion_Procesos");
        }
    }
}
