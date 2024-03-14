using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
