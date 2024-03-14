using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Producto7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Prod_Cod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Prod_Descripcion = table.Column<string>(type: "text", nullable: true),
                    TpProd_Id = table.Column<int>(type: "int", nullable: false),
                    Prod_Peso_Bruto = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Prod_Peso_Neto = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMedPeso = table.Column<string>(type: "varchar(10)", nullable: false),
                    Prod_Fuelle = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    Prod_Ancho = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    Prod_Calibre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    UndMedACF = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Prod_Id);
                    table.ForeignKey(
                        name: "FK_Productos_Tipos_Productos_TpProd_Id",
                        column: x => x.TpProd_Id,
                        principalTable: "Tipos_Productos",
                        principalColumn: "TpProd_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Unidades_Medidas_UndMedACF",
                        column: x => x.UndMedACF,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Unidades_Medidas_UndMedPeso",
                        column: x => x.UndMedPeso,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TpProd_Id",
                table: "Productos",
                column: "TpProd_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UndMedACF",
                table: "Productos",
                column: "UndMedACF");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UndMedPeso",
                table: "Productos",
                column: "UndMedPeso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
