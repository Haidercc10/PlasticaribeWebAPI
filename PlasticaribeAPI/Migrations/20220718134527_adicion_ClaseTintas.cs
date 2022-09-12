using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class adicion_ClaseTintas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tintas",
                columns: table => new
                {
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Tinta_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Tinta_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Tinta_CodigoHexadecimal = table.Column<string>(type: "varchar(50)", nullable: true),
                    Tinta_Stock = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Tinta_Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CatMP_Id = table.Column<int>(type: "int", nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tintas", x => x.Tinta_Id);
                    table.ForeignKey(
                        name: "FK_Tintas_Categorias_MatPrima_CatMP_Id",
                        column: x => x.CatMP_Id,
                        principalTable: "Categorias_MatPrima",
                        principalColumn: "CatMP_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tintas_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tintas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tintas_CatMP_Id",
                table: "Tintas",
                column: "CatMP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tintas_TpBod_Id",
                table: "Tintas",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tintas_UndMed_Id",
                table: "Tintas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tintas");
        }
    }
}
