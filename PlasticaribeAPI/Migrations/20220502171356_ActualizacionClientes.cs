using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class ActualizacionClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "Usuarios",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Clientes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Usua_Id",
                table: "Clientes",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_Usua_Id",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_Usua_Id",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "Usuarios",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
