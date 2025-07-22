using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_PerforadoKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DtBgRollo_Perforado",
                table: "Detalles_BodegasRollos",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstProcOT_PerforadoKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "DtBgRollo_Perforado",
                table: "Detalles_BodegasRollos");
        }
    }
}
