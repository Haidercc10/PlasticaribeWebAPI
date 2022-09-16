using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddUsuarioBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "BOPP",
                type: "bigint",
                nullable: false,
                defaultValue: 123456789);

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_Usua_Id",
                table: "BOPP",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BOPP_Usuarios_Usua_Id",
                table: "BOPP",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOPP_Usuarios_Usua_Id",
                table: "BOPP");

            migrationBuilder.DropIndex(
                name: "IX_BOPP_Usua_Id",
                table: "BOPP");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "BOPP");
        }
    }
}
