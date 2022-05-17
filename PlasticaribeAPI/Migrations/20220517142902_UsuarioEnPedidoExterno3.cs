using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class UsuarioEnPedidoExterno3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Pedidos_Externos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Empresa_Id",
                table: "Pedidos_Externos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Pedidos_Externos",
                type: "bigint",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Externos_Usua_Id",
                table: "Pedidos_Externos",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Externos_Usuarios_Usua_Id",
                table: "Pedidos_Externos",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Externos_Usuarios_Usua_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_Externos_Usua_Id",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Pedidos_Externos");

            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Pedidos_Externos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Empresa_Id",
                table: "Pedidos_Externos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
