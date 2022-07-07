using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_InventarioInicialXDia_MatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventarioInicialXDias_MatPrima",
                columns: table => new
                {
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    InvInicial_Stock = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioInicialXDias_MatPrima", x => x.MatPri_Id);
                    table.ForeignKey(
                        name: "FK_InventarioInicialXDias_MatPrima_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventarioInicialXDias_MatPrima_UndMed_Id",
                table: "InventarioInicialXDias_MatPrima",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventarioInicialXDias_MatPrima");
        }
    }
}
