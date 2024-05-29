using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class EstadoEnDevolucionesFacturas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DevProdFact_Reposicion",
                table: "Devoluciones_ProductosFacturados",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Estado_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Estados_Estado_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Estados_Estado_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_Estado_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_Reposicion",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "Devoluciones_ProductosFacturados");
        }
    }
}
