using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class PedidoSinArchivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Usuarios_CreadorUsua_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_Externos_CreadorUsua_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "CreadorUsua_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "Creador_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "PedExt_Archivo",
                table: "Pedidos_Externos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreadorUsua_Id",
                table: "Pedidos_Externos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Creador_Id",
                table: "Pedidos_Externos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PedExt_Archivo",
                table: "Pedidos_Externos",
                type: "binary(MAX)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Externos_CreadorUsua_Id",
                table: "Pedidos_Externos",
                column: "CreadorUsua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Usuarios_CreadorUsua_Id",
                table: "Pedidos_Externos",
                column: "CreadorUsua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id");
        }
    }
}
