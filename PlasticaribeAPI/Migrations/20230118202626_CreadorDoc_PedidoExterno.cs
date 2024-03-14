using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CreadorDocPedidoExterno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PedExt_Archivo",
                table: "Pedidos_Externos",
                type: "binary(4)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(MAX)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<byte[]>(
                name: "PedExt_Archivo",
                table: "Pedidos_Externos",
                type: "binary(MAX)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(4)");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
