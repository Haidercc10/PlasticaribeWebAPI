using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddRolloDetallesAsignacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Rollo_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                type: "bigint",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rollo_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas");
        }
    }
}
