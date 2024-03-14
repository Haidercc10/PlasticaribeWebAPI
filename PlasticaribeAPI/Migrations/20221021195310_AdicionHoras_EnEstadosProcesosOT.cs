using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AdicionHoras_EnEstadosProcesosOT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstProcOT_HoraFinal",
                table: "Estados_ProcesosOT",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstProcOT_HoraInicio",
                table: "Estados_ProcesosOT",
                type: "varchar(20)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstProcOT_HoraFinal",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_HoraInicio",
                table: "Estados_ProcesosOT");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
