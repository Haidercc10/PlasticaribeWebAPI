using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class PesoBruto_Unidades_EnExistenciasProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExProd_PesoBruto",
                table: "Existencias_Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true, 
                defaultValue : 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExProd_Unidades",
                table: "Existencias_Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExProd_PesoBruto",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "ExProd_Unidades",
                table: "Existencias_Productos");
        }
    }
}
