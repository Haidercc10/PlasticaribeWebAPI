using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Toma_Fisica_Inventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Toma_Fisica_Inventario",
                columns: table => new
                {
                    Tfi_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tfi_NumeroRollo = table.Column<long>(type: "bigint", nullable: false),
                    Tfi_Etiqueta = table.Column<long>(type: "bigint", nullable: false),
                    Tfi_OT = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tfi_CantidadReal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tfi_PesoBruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Presentacion = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Rollo = table.Column<int>(type: "int", nullable: false),
                    Tfi_PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tfi_EnvioZeus = table.Column<bool>(type: "bit", nullable: false),
                    Tfi_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Tfi_Hora = table.Column<string>(type: "varchar(20)", nullable: false),
                    UsuaRegistro_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tfi_Ubicacion = table.Column<string>(type: "varchar(100)", nullable: false),
                    Tfi_Observacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toma_Fisica_Inventario", x => x.Tfi_Id);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Inventario_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Inventario_Estados_Estado_Rollo",
                        column: x => x.Estado_Rollo,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Inventario_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Inventario_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Inventario_Unidades_Medidas_Presentacion",
                        column: x => x.Presentacion,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Inventario_Usuarios_UsuaRegistro_Id",
                        column: x => x.UsuaRegistro_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Inventario_Cli_Id",
                table: "Toma_Fisica_Inventario",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Inventario_Estado_Rollo",
                table: "Toma_Fisica_Inventario",
                column: "Estado_Rollo");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Inventario_Presentacion",
                table: "Toma_Fisica_Inventario",
                column: "Presentacion");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Inventario_Proceso_Id",
                table: "Toma_Fisica_Inventario",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Inventario_Prod_Id",
                table: "Toma_Fisica_Inventario",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Inventario_UsuaRegistro_Id",
                table: "Toma_Fisica_Inventario",
                column: "UsuaRegistro_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Toma_Fisica_Inventario");
        }
    }
}
