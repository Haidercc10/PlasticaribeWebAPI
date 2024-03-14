using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_CambioNombreObservacionEnAsignacionMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Facco_Observacion",
                table: "Asignaciones_MatPrima",
                newName: "AsigMp_Observacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AsigMp_Observacion",
                table: "Asignaciones_MatPrima",
                newName: "Facco_Observacion");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
