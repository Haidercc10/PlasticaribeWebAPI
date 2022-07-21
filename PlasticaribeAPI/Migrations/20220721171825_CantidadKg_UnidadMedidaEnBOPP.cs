using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CantidadKg_UnidadMedidaEnBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BOPP_CantidadKg",
                table: "BOPP",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Kg",
                table: "BOPP",
                type: "varchar(10)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_UndMed_Kg",
                table: "BOPP",
                column: "UndMed_Kg");

            migrationBuilder.AddForeignKey(
                name: "FK_BOPP_Unidades_Medidas_UndMed_Kg",
                table: "BOPP",
                column: "UndMed_Kg",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOPP_Unidades_Medidas_UndMed_Kg",
                table: "BOPP");

            migrationBuilder.DropIndex(
                name: "IX_BOPP_UndMed_Kg",
                table: "BOPP");

            migrationBuilder.DropColumn(
                name: "BOPP_CantidadKg",
                table: "BOPP");

            migrationBuilder.DropColumn(
                name: "UndMed_Kg",
                table: "BOPP");
        }
    }
}
