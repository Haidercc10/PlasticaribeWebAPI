using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_FacturasCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas_Compras",
                columns: table => new
                {
                    Facco_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Facco_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Facco_FechaFactura = table.Column<DateTime>(type: "Date", nullable: false),
                    Facco_FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prov_Id = table.Column<long>(type: "bigint", nullable: false),
                    Facco_ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Facco_Observacion = table.Column<string>(type: "text", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas_Compras", x => x.Facco_Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Compras_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Compras_Proveedores_Prov_Id",
                        column: x => x.Prov_Id,
                        principalTable: "Proveedores",
                        principalColumn: "Prov_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Compras_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Compras_Estado_Id",
                table: "Facturas_Compras",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Compras_Prov_Id",
                table: "Facturas_Compras",
                column: "Prov_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Compras_Usua_Id",
                table: "Facturas_Compras",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas_Compras");
        }
    }
}
