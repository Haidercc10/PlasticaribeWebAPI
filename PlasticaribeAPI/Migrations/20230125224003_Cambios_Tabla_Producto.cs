using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CambiosTablaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Prod_Peso_Bulto",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_Peso_Paquete",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_PrecioDia_Sellado",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_PrecioNoche_Sellado",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prod_Peso_Bulto",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Peso_Paquete",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_PrecioDia_Sellado",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_PrecioNoche_Sellado",
                table: "Productos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
