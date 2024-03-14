using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Create_OrdenFacturacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdenFacturacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Factura = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: true),
                    Hora = table.Column<string>(type: "varchar(20)", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(max)", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenFacturacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenFacturacion_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenFacturacion_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_OrdenFacturacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_OrdenFacturacion = table.Column<int>(type: "int", nullable: false),
                    Numero_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Presentacion = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_OrdenFacturacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenFacturacion_OrdenFacturacion_Id_OrdenFacturacion",
                        column: x => x.Id_OrdenFacturacion,
                        principalTable: "OrdenFacturacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenFacturacion_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenFacturacion_Unidades_Medidas_Presentacion",
                        column: x => x.Presentacion,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenFacturacion_Id_OrdenFacturacion",
                table: "Detalles_OrdenFacturacion",
                column: "Id_OrdenFacturacion");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenFacturacion_Presentacion",
                table: "Detalles_OrdenFacturacion",
                column: "Presentacion");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenFacturacion_Prod_Id",
                table: "Detalles_OrdenFacturacion",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenFacturacion_Cli_Id",
                table: "OrdenFacturacion",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenFacturacion_Usua_Id",
                table: "OrdenFacturacion",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_OrdenFacturacion");

            migrationBuilder.DropTable(
                name: "OrdenFacturacion");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
