using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class SedeCli_IdEnPedidoExterno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Sedes_Clientes_SedeCliente",
                table: "Pedidos_Externos");

            migrationBuilder.RenameColumn(
                name: "SedeCliente",
                table: "Pedidos_Externos",
                newName: "SedeCli_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_Externos_SedeCliente",
                table: "Pedidos_Externos",
                newName: "IX_Pedidos_Externos_SedeCli_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Sedes_Clientes_SedeCli_Id",
                table: "Pedidos_Externos",
                column: "SedeCli_Id",
                principalTable: "Sedes_Clientes",
                principalColumn: "SedeCli_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Sedes_Clientes_SedeCli_Id",
                table: "Pedidos_Externos");

            migrationBuilder.RenameColumn(
                name: "SedeCli_Id",
                table: "Pedidos_Externos",
                newName: "SedeCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_Externos_SedeCli_Id",
                table: "Pedidos_Externos",
                newName: "IX_Pedidos_Externos_SedeCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Sedes_Clientes_SedeCliente",
                table: "Pedidos_Externos",
                column: "SedeCliente",
                principalTable: "Sedes_Clientes",
                principalColumn: "SedeCli_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
