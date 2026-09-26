using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class PrecioYSubtotal_DetAsigTintas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DtAsigTinta_Precio",
                table: "DetalleAsignaciones_Tintas",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true, 
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DtAsigTinta_Subtotal",
                table: "DetalleAsignaciones_Tintas",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtAsigTinta_Precio",
                table: "DetalleAsignaciones_Tintas");

            migrationBuilder.DropColumn(
                name: "DtAsigTinta_Subtotal",
                table: "DetalleAsignaciones_Tintas");
        }
    }
}
