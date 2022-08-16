using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionUsuarioArchivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Archivos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_Usua_Id",
                table: "Archivos",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Usuarios_Usua_Id",
                table: "Archivos",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Usuarios_Usua_Id",
                table: "Archivos");

            migrationBuilder.DropIndex(
                name: "IX_Archivos_Usua_Id",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Archivos");
        }
    }
}
