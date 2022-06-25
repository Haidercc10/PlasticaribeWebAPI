using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_FacturasComprasMateriasPrimas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturasCompras_MateriaPrimas",
                columns: table => new
                {
                    Facco_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    FaccoMatPri_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    FaccoMatPri_ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasCompras_MateriaPrimas", x => new { x.Facco_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_FacturasCompras_MateriaPrimas_Facturas_Compras_Facco_Id",
                        column: x => x.Facco_Id,
                        principalTable: "Facturas_Compras",
                        principalColumn: "Facco_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturasCompras_MateriaPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturasCompras_MateriaPrimas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturasCompras_MateriaPrimas_MatPri_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasCompras_MateriaPrimas_UndMed_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturasCompras_MateriaPrimas");
        }
    }
}
