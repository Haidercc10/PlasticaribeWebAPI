using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Table_FinishedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto_Terminado",
                columns: table => new
                {
                    Pt_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedExtProd_Id = table.Column<long>(type: "bigint", nullable: false),
                    Pt_Margen = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Pt_PesoMillar = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Pt_PesoRollo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    Pt_PesoUnd = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    Pt_CantBolsasBulto = table.Column<int>(type: "int", nullable: false),
                    Pt_CantBolsasPaquete = table.Column<int>(type: "int", nullable: false),
                    TpSellado_Id = table.Column<int>(type: "int", nullable: false),
                    TpImpresion_Id = table.Column<int>(type: "int", nullable: false),
                    TpProd_Id = table.Column<int>(type: "int", nullable: false),
                    Pt_Ancho = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Pt_Largo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Pt_Fuelle = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto_Terminado", x => x.Pt_Id);
                    table.ForeignKey(
                        name: "FK_Producto_Terminado_PedidosExternos_Productos_PedExtProd_Id",
                        column: x => x.PedExtProd_Id,
                        principalTable: "PedidosExternos_Productos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Producto_Terminado_Tipos_Impresion_TpSellado_Id",
                        column: x => x.TpSellado_Id,
                        principalTable: "Tipos_Impresion",
                        principalColumn: "TpImpresion_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Producto_Terminado_Tipos_Productos_TpProd_Id",
                        column: x => x.TpProd_Id,
                        principalTable: "Tipos_Productos",
                        principalColumn: "TpProd_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Producto_Terminado_Tipos_Sellados_TpSellado_Id",
                        column: x => x.TpSellado_Id,
                        principalTable: "Tipos_Sellados",
                        principalColumn: "TpSellado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_PedExtProd_Id",
                table: "Producto_Terminado",
                column: "PedExtProd_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_TpProd_Id",
                table: "Producto_Terminado",
                column: "TpProd_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_TpSellado_Id",
                table: "Producto_Terminado",
                column: "TpSellado_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto_Terminado");
        }
    }
}
