using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddCantPaquete_Bulto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExProd_CantBolsasBulto",
                table: "Existencias_Productos",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExProd_CantBolsasPaquete",
                table: "Existencias_Productos",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasBulto",
                table: "DetallesPreEntrega_RollosDespacho",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesPreEntrega_RollosDespacho",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasBulto",
                table: "DetallesEntradasRollos_Productos",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesEntradasRollos_Productos",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasBulto",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExProd_CantBolsasBulto",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "ExProd_CantBolsasPaquete",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasBulto",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasBulto",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasBulto",
                table: "DetallesAsignacionesProductos_FacturasVentas");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesAsignacionesProductos_FacturasVentas");
        }
    }
}
