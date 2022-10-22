using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class DiferenciaDiasOTInicioFin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstProcOT_DiffDiasInicio_Fin",
                table: "Estados_ProcesosOT",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstProcOT_DiffDiasInicio_Fin",
                table: "Estados_ProcesosOT");
        }
    }
}
