using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposEnSelladoOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_Ancho1",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_Ancho2",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_Ancho3",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelladoCorte_Ancho1",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_Ancho2",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_Ancho3",
                table: "OT_Sellado_Corte");
        }
    }
}
