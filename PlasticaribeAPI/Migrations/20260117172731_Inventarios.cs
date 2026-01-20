using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inventarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo_Inventario",
                table: "Toma_Fisica_Inventario",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TpBod_Id",
                table: "Toma_Fisica_Inventario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Inv_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inv_NumeroRollo = table.Column<long>(type: "bigint", nullable: false),
                    Inv_Etiqueta = table.Column<long>(type: "bigint", nullable: false),
                    Inv_OT = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Inv_Existencias = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Inv_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Inv_PesoBruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Presentacion = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Inv_PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Inv_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Inv_Hora = table.Column<string>(type: "varchar(20)", nullable: false),
                    UsuaRegistro_Id = table.Column<long>(type: "bigint", nullable: false),
                    Inv_Ubicacion = table.Column<string>(type: "varchar(100)", nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false),
                    Tipo_Inventario = table.Column<string>(type: "varchar(100)", nullable: false),
                    Inv_Observacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Inv_Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Unidades_Medidas_Presentacion",
                        column: x => x.Presentacion,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Usuarios_UsuaRegistro_Id",
                        column: x => x.UsuaRegistro_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Inventario_TpBod_Id",
                table: "Toma_Fisica_Inventario",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Cli_Id",
                table: "Inventarios",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Presentacion",
                table: "Inventarios",
                column: "Presentacion");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Proceso_Id",
                table: "Inventarios",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Prod_Id",
                table: "Inventarios",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_TpBod_Id",
                table: "Inventarios",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_UsuaRegistro_Id",
                table: "Inventarios",
                column: "UsuaRegistro_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Toma_Fisica_Inventario_Tipos_Bodegas_TpBod_Id",
                table: "Toma_Fisica_Inventario",
                column: "TpBod_Id",
                principalTable: "Tipos_Bodegas",
                principalColumn: "TpBod_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toma_Fisica_Inventario_Tipos_Bodegas_TpBod_Id",
                table: "Toma_Fisica_Inventario");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Toma_Fisica_Inventario_TpBod_Id",
                table: "Toma_Fisica_Inventario");

            migrationBuilder.DropColumn(
                name: "Tipo_Inventario",
                table: "Toma_Fisica_Inventario");

            migrationBuilder.DropColumn(
                name: "TpBod_Id",
                table: "Toma_Fisica_Inventario");
        }
    }
}
