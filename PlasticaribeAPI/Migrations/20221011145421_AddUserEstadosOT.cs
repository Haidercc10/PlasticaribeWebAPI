using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddUserEstadosOT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Estados_ProcesosOT",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ProcesosOT_Usua_Id",
                table: "Estados_ProcesosOT",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_ProcesosOT_Usuarios_Usua_Id",
                table: "Estados_ProcesosOT",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_ProcesosOT_Usuarios_Usua_Id",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ProcesosOT_Usua_Id",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Estados_ProcesosOT");
        }
    }
}
