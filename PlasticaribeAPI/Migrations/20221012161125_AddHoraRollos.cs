using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddHoraRollos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreEntRollo_Hora",
                table: "PreEntrega_RollosDespacho",
                type: "varchar(10)",
                nullable: false,
                defaultValue: DateTime.Now.ToString("hh:mm:ss tt"));

            migrationBuilder.AddColumn<string>(
                name: "EntRolloProd_Hora",
                table: "EntradasRollos_Productos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: DateTime.Now.ToString("hh:mm:ss tt"));

            migrationBuilder.AddColumn<string>(
                name: "DevProd_Hora",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: false,
                defaultValue: DateTime.Now.ToString("hh:mm:ss tt"));

            migrationBuilder.AddColumn<string>(
                name: "AsigProdFV_FechaHora",
                table: "AsignacionesProductos_FacturasVentas",
                type: "varchar(10)",
                nullable: false,
                defaultValue: DateTime.Now.ToString("hh:mm:ss tt"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreEntRollo_Hora",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "EntRolloProd_Hora",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "DevProd_Hora",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "AsigProdFV_FechaHora",
                table: "AsignacionesProductos_FacturasVentas");
        }
    }
}
