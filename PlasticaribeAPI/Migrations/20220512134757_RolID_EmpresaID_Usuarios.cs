using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class RolID_EmpresaID_Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empresas_Empresa_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empresas_Empresa_Id",
                table: "Usuarios",
                column: "Empresa_Id",
                principalTable: "Empresas",
                principalColumn: "Empresa_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id",
                table: "Usuarios",
                column: "RolUsu_Id",
                principalTable: "Roles_Usuarios",
                principalColumn: "RolUsu_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empresas_Empresa_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empresas_Empresa_Id",
                table: "Usuarios",
                column: "Empresa_Id",
                principalTable: "Empresas",
                principalColumn: "Empresa_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_Usuarios_RolUsu_Id",
                table: "Usuarios",
                column: "RolUsu_Id",
                principalTable: "Roles_Usuarios",
                principalColumn: "RolUsu_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
