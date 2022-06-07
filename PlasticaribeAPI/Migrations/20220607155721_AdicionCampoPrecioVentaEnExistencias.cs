using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionCampoPrecioVentaEnExistencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropColumn(
                name: "cajComp_Codigo",
                table: "Cajas_Compensaciones"); */

            migrationBuilder.AddColumn<decimal>(
                name: "ExProd_PrecioVenta",
                table: "Existencias_Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExProd_PrecioVenta",
                table: "Existencias_Productos");

           /* migrationBuilder.AddColumn<int>(
                name: "cajComp_Codigo",
                table: "Cajas_Compensaciones",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); */
        }
    }
}
