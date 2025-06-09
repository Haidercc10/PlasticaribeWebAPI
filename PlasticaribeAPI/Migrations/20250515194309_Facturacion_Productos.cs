using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Facturacion_Productos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Of_Directa",
                table: "OrdenFacturacion",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Facturacion_Productos",
                columns: table => new
                {
                    FactPro_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactPro_Pedido = table.Column<string>(type: "varchar(50)", nullable: false),
                    Of_Id = table.Column<int>(type: "int", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    FactPro_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    FactPro_Unidades = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Peso_Bruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Peso_Neto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturacion_Productos", x => x.FactPro_Codigo);
                    table.ForeignKey(
                        name: "FK_Facturacion_Productos_OrdenFacturacion_Of_Id",
                        column: x => x.Of_Id,
                        principalTable: "OrdenFacturacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturacion_Productos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturacion_Productos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturacion_Productos_Of_Id",
                table: "Facturacion_Productos",
                column: "Of_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facturacion_Productos_Prod_Id",
                table: "Facturacion_Productos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Facturacion_Productos_UndMed_Id",
                table: "Facturacion_Productos",
                column: "UndMed_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturacion_Productos");

            migrationBuilder.DropColumn(
                name: "Of_Directa",
                table: "OrdenFacturacion");
        }
    }
}
