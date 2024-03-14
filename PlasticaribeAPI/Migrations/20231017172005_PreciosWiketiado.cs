using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class PreciosWiketiado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_PrecioDia_Wik_Mq50",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_PrecioDia_Wik_Mq9",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_PrecioNoche_Wik_Mq50",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_PrecioNoche_Wik_Mq9",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelladoCorte_PrecioDia_Wik_Mq50",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_PrecioDia_Wik_Mq9",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_PrecioNoche_Wik_Mq50",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_PrecioNoche_Wik_Mq9",
                table: "OT_Sellado_Corte");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
