using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionPreciosEnAsigMatPrima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DtAsigMp_Precio",
                table: "DetallesAsignaciones_MateriasPrimas",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DtAsigMp_Subtotal",
                table: "DetallesAsignaciones_MateriasPrimas",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtAsigMp_Precio",
                table: "DetallesAsignaciones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "DtAsigMp_Subtotal",
                table: "DetallesAsignaciones_MateriasPrimas");
        }
    }
}
