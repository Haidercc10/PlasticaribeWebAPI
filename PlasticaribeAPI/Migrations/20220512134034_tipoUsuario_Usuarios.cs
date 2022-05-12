using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class tipoUsuario_Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id",
                table: "Usuarios",
                column: "tpUsu_Id",
                principalTable: "Tipos_Usuarios",
                principalColumn: "tpUsu_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Tipos_Usuarios_tpUsu_Id",
                table: "Usuarios",
                column: "tpUsu_Id",
                principalTable: "Tipos_Usuarios",
                principalColumn: "tpUsu_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
