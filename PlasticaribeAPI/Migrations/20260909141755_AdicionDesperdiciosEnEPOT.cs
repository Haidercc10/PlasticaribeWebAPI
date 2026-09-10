using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionDesperdiciosEnEPOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespCorteKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespDobladoKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespEmpaqueKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespExtrusionKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespImpresionKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespLaminadoKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespPerforadoKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespRotograbadoKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespSelladoKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_DespWiketiadoKg",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstProcOT_DespCorteKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespDobladoKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespEmpaqueKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespExtrusionKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespImpresionKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespLaminadoKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespPerforadoKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespRotograbadoKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespSelladoKg",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_DespWiketiadoKg",
                table: "Estados_ProcesosOT");
        }
    }
}
