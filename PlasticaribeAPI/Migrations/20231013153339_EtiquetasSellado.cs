using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class EtiquetasSellado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelladoCorte_Etiqueta_Ancho",
                table: "OT_Sellado_Corte",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "0");

            migrationBuilder.AddColumn<string>(
                name: "SelladoCorte_Etiqueta_Fuelle",
                table: "OT_Sellado_Corte",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "0");

            migrationBuilder.AddColumn<string>(
                name: "SelladoCorte_Etiqueta_Largo",
                table: "OT_Sellado_Corte",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelladoCorte_Etiqueta_Ancho",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_Etiqueta_Fuelle",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_Etiqueta_Largo",
                table: "OT_Sellado_Corte");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
