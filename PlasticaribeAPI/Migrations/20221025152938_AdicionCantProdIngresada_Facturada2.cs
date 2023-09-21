using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionCantProdIngresada_Facturada2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_CantProdIngresada",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EstProcOT_CantProdFacturada",
                table: "Estados_ProcesosOT",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstProcOT_CantProdIngresada",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "EstProcOT_CantProdFacturada",
                table: "Estados_ProcesosOT");

        }
    }
}
