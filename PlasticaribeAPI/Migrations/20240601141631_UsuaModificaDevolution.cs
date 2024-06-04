using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UsuaModificaDevolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "UsuaModifica_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "UsuaModifica_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados");
        }
    }
}
