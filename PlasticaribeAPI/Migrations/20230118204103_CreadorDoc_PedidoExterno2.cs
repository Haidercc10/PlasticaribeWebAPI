using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreadorDocPedidoExterno2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Creador_Id",
                table: "Pedidos_Externos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Externos_Creador_Id",
                table: "Pedidos_Externos",
                column: "Creador_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Usuarios_Creador_Id",
                table: "Pedidos_Externos",
                column: "Creador_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Usuarios_Creador_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_Externos_Creador_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "Creador_Id",
                table: "Pedidos_Externos");
        }
    }
}
