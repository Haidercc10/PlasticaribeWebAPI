using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class CambioNombreCamposCantidad_CantidadKg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BOPP_CantidadKg",
                table: "BOPP",
                newName: "BOPP_Stock");

            migrationBuilder.RenameColumn(
                name: "BOPP_Cantidad",
                table: "BOPP",
                newName: "BOPP_CantidadMicras");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BOPP_Stock",
                table: "BOPP",
                newName: "BOPP_CantidadKg");

            migrationBuilder.RenameColumn(
                name: "BOPP_CantidadMicras",
                table: "BOPP",
                newName: "BOPP_Cantidad");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
