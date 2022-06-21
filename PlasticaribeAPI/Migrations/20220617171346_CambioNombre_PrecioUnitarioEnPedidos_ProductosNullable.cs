using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CambioNombre_PrecioUnitarioEnPedidos_ProductosNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PedExt_PrecioUnitario",
                table: "PedidosExternos_Productos",
                newName: "PedExtProd_PrecioUnitario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PedExtProd_PrecioUnitario",
                table: "PedidosExternos_Productos",
                newName: "PedExt_PrecioUnitario");
        }
    }
}
