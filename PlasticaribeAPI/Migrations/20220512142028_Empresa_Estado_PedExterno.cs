using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Empresa_Estado_PedExterno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Empresas_Empresa_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Estados_Estado_Id",
                table: "Pedidos_Externos");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Empresas_Empresa_Id",
                table: "Pedidos_Externos",
                column: "Empresa_Id",
                principalTable: "Empresas",
                principalColumn: "Empresa_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Estados_Estado_Id",
                table: "Pedidos_Externos",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Empresas_Empresa_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Estados_Estado_Id",
                table: "Pedidos_Externos");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Empresas_Empresa_Id",
                table: "Pedidos_Externos",
                column: "Empresa_Id",
                principalTable: "Empresas",
                principalColumn: "Empresa_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Estados_Estado_Id",
                table: "Pedidos_Externos",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
