using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class BoppAsignadoEnEPOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_CantBoppAsignado",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true, 
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstProcOT_CantBoppAsignado",
                table: "Estados_ProcesosOT");
        }
    }
}
