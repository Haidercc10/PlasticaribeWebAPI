using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class ClientesActualizacionRelaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                table: "Clientes");

            /*migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_Usua_Id",
                table: "Clientes");*/

            migrationBuilder.RenameColumn(
                name: "Usua_Id",
                table: "Clientes",
                newName: "usua_Id");

            /*migrationBuilder.RenameIndex(
                name: "IX_Clientes_Usua_Id",
                table: "Clientes",
                newName: "IX_Clientes_usua_Id");*/

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Clientes",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                table: "Clientes",
                column: "TPCli_Id",
                principalTable: "Tipos_Clientes",
                principalColumn: "TPCli_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_usua_Id",
                table: "Clientes",
                column: "usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                table: "Clientes");

            /*migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_usua_Id",
                table: "Clientes");*/

            migrationBuilder.RenameColumn(
                name: "usua_Id",
                table: "Clientes",
                newName: "Usua_Id");

            /*migrationBuilder.RenameIndex(
                name: "IX_Clientes_usua_Id",
                table: "Clientes",
                newName: "IX_Clientes_Usua_Id");
            */
            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Clientes",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                table: "Clientes",
                column: "TPCli_Id",
                principalTable: "Tipos_Clientes",
                principalColumn: "TPCli_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_Usua_Id",
                table: "Clientes",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
