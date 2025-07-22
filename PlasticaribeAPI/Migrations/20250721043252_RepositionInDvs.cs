using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class RepositionInDvs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Reposicion_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Reposicion_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Reposicion_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Reposiciones_Reposicion_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Reposicion_Id",
                principalTable: "Reposiciones",
                principalColumn: "Rep_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Reposiciones_Reposicion_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_Reposicion_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Reposicion_Id",
                table: "Devoluciones_ProductosFacturados");
        }
    }
}
