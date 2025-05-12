using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Adicion_Usuario_Elimina_Rollos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UsuaAutoriza_Id",
                table: "Rollos_Desechos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuaElimina_Id",
                table: "Rollos_Desechos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_UsuaAutoriza_Id",
                table: "Rollos_Desechos",
                column: "UsuaAutoriza_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_UsuaElimina_Id",
                table: "Rollos_Desechos",
                column: "UsuaElimina_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rollos_Desechos_Usuarios_UsuaAutoriza_Id",
                table: "Rollos_Desechos",
                column: "UsuaAutoriza_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rollos_Desechos_Usuarios_UsuaElimina_Id",
                table: "Rollos_Desechos",
                column: "UsuaElimina_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rollos_Desechos_Usuarios_UsuaAutoriza_Id",
                table: "Rollos_Desechos");

            migrationBuilder.DropForeignKey(
                name: "FK_Rollos_Desechos_Usuarios_UsuaElimina_Id",
                table: "Rollos_Desechos");

            migrationBuilder.DropIndex(
                name: "IX_Rollos_Desechos_UsuaAutoriza_Id",
                table: "Rollos_Desechos");

            migrationBuilder.DropIndex(
                name: "IX_Rollos_Desechos_UsuaElimina_Id",
                table: "Rollos_Desechos");

            migrationBuilder.DropColumn(
                name: "UsuaAutoriza_Id",
                table: "Rollos_Desechos");

            migrationBuilder.DropColumn(
                name: "UsuaElimina_Id",
                table: "Rollos_Desechos");
        }
    }
}
