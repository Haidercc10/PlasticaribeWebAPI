using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class addUsuarioDevolucionesProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                defaultValue: 123456789);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Usua_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_Usua_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_Usua_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_Usua_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Devoluciones_ProductosFacturados");
        }
    }
}
