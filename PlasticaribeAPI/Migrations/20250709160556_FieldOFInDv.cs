using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FieldOFInDv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Of_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_Of_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "Of_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_ProductosFacturados_OrdenFacturacion_Of_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "Of_Id",
                principalTable: "OrdenFacturacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_ProductosFacturados_OrdenFacturacion_Of_Id",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_Of_Id",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Of_Id",
                table: "DetallesDevoluciones_ProductosFacturados");
        }
    }
}
