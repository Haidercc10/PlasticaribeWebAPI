using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class ActualizacionUsuario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_FondosPensiones_fondosPensionfPen_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RolesRol_Id",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "fondosPensionfPen_Id",
                table: "Usuarios",
                newName: "fPen_Id");

            migrationBuilder.RenameColumn(
                name: "RolesRol_Id",
                table: "Usuarios",
                newName: "Rol_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_RolesRol_Id",
                table: "Usuarios",
                newName: "IX_Usuarios_Rol_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_fondosPensionfPen_Id",
                table: "Usuarios",
                newName: "IX_Usuarios_fPen_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_FondosPensiones_fPen_Id",
                table: "Usuarios",
                column: "fPen_Id",
                principalTable: "FondosPensiones",
                principalColumn: "fPen_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_Rol_Id",
                table: "Usuarios",
                column: "Rol_Id",
                principalTable: "Roles",
                principalColumn: "Rol_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_FondosPensiones_fPen_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_Rol_Id",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "fPen_Id",
                table: "Usuarios",
                newName: "fondosPensionfPen_Id");

            migrationBuilder.RenameColumn(
                name: "Rol_Id",
                table: "Usuarios",
                newName: "RolesRol_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Rol_Id",
                table: "Usuarios",
                newName: "IX_Usuarios_RolesRol_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_fPen_Id",
                table: "Usuarios",
                newName: "IX_Usuarios_fondosPensionfPen_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_FondosPensiones_fondosPensionfPen_Id",
                table: "Usuarios",
                column: "fondosPensionfPen_Id",
                principalTable: "FondosPensiones",
                principalColumn: "fPen_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RolesRol_Id",
                table: "Usuarios",
                column: "RolesRol_Id",
                principalTable: "Roles",
                principalColumn: "Rol_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
