using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class PrecioEstandarMateriales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Precio_Unitario",
                table: "Movimientros_Entradas_MP",
                newName: "Precio_RealUnitario");

            migrationBuilder.AddColumn<decimal>(
                name: "Tinta_PrecioEstandar",
                table: "Tintas",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio_EstandarUnitario",
                table: "Movimientros_Entradas_MP",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MatPri_PrecioEstandar",
                table: "Materias_Primas",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BoppGen_PrecioEstandar",
                table: "Bopp_Generico",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tinta_PrecioEstandar",
                table: "Tintas");

            migrationBuilder.DropColumn(
                name: "Precio_EstandarUnitario",
                table: "Movimientros_Entradas_MP");

            migrationBuilder.DropColumn(
                name: "MatPri_PrecioEstandar",
                table: "Materias_Primas");

            migrationBuilder.DropColumn(
                name: "BoppGen_PrecioEstandar",
                table: "Bopp_Generico");

            migrationBuilder.RenameColumn(
                name: "Precio_RealUnitario",
                table: "Movimientros_Entradas_MP",
                newName: "Precio_Unitario");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
