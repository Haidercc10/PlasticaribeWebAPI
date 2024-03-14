using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class CantUndxPaqRollos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Prod_CantBolsasBulto",
                table: "DetallesEntradasRollos_Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_CantBolsasFacturadas",
                table: "DetallesEntradasRollos_Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesEntradasRollos_Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_CantBolsasRestates",
                table: "DetallesEntradasRollos_Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_CantPaquetesRestantes",
                table: "DetallesEntradasRollos_Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_CantidadUnidades",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasBulto",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasFacturadas",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasPaquete",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasRestates",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantPaquetesRestantes",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantidadUnidades",
                table: "DetallesAsignacionesProductos_FacturasVentas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
