using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FieldInDevolutionProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Falla_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados",
                column: "Id_OrdenFact");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_Falla_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "Falla_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_ProductosFacturados_Fallas_Tecnicas_Falla_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "Falla_Id",
                principalTable: "Fallas_Tecnicas",
                principalColumn: "Falla_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_OrdenFacturacion_Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados",
                column: "Id_OrdenFact",
                principalTable: "OrdenFacturacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_ProductosFacturados_Fallas_Tecnicas_Falla_Id",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_OrdenFacturacion_Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_Falla_Id",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Falla_Id",
                table: "DetallesDevoluciones_ProductosFacturados");
        }
    }
}
