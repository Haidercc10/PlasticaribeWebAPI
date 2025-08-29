using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cambios_Reposiciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Falla_Id",
                table: "Reposiciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Autoriza",
                table: "Reposiciones",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reposiciones_Falla_Id",
                table: "Reposiciones",
                column: "Falla_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reposiciones_Usua_Autoriza",
                table: "Reposiciones",
                column: "Usua_Autoriza");

            migrationBuilder.AddForeignKey(
                name: "FK_Reposiciones_Fallas_Tecnicas_Falla_Id",
                table: "Reposiciones",
                column: "Falla_Id",
                principalTable: "Fallas_Tecnicas",
                principalColumn: "Falla_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reposiciones_Usuarios_Usua_Autoriza",
                table: "Reposiciones",
                column: "Usua_Autoriza",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reposiciones_Fallas_Tecnicas_Falla_Id",
                table: "Reposiciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Reposiciones_Usuarios_Usua_Autoriza",
                table: "Reposiciones");

            migrationBuilder.DropIndex(
                name: "IX_Reposiciones_Falla_Id",
                table: "Reposiciones");

            migrationBuilder.DropIndex(
                name: "IX_Reposiciones_Usua_Autoriza",
                table: "Reposiciones");

            migrationBuilder.DropColumn(
                name: "Falla_Id",
                table: "Reposiciones");

            migrationBuilder.DropColumn(
                name: "Usua_Autoriza",
                table: "Reposiciones");
        }
    }
}
