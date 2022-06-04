using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CajaCompensacionSinCodigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cajComp_Codigo",
                table: "Cajas_Compensaciones");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
