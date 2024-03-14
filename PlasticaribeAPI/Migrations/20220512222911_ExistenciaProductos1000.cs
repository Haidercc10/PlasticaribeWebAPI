using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class ExistenciaProductos1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Existencias_Productos",
                columns: table => new
                {
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    ExProd_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false),
                    ExProd_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExProd_Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExProd_PrecioExistencia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExProd_PrecioSinInflacion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExProd_PrecioTotalFinal = table.Column<string>(type: "nvarchar(max)", precision: 18, scale: 2, nullable: true),
                    TpMoneda_Id = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Existencias_Productos", x => x.ExProd_Id);
                    table.ForeignKey(
                        name: "FK_Existencias_Productos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Existencias_Productos_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Existencias_Productos_Tipos_Monedas_TpMoneda_Id",
                        column: x => x.TpMoneda_Id,
                        principalTable: "Tipos_Monedas",
                        principalColumn: "TpMoneda_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Existencias_Productos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_Productos_Prod_Id",
                table: "Existencias_Productos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_Productos_TpBod_Id",
                table: "Existencias_Productos",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_Productos_TpMoneda_Id",
                table: "Existencias_Productos",
                column: "TpMoneda_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_Productos_UndMed_Id",
                table: "Existencias_Productos",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Existencias_Productos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
