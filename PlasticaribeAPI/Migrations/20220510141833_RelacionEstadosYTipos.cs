using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class RelacionEstadosYTipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TpEstado_Id",
                table: "Estados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_TpEstado_Id",
                table: "Estados",
                column: "TpEstado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                table: "Estados",
                column: "TpEstado_Id",
                principalTable: "Tipos_Estados",
                principalColumn: "TpEstado_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_TpEstado_Id",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "TpEstado_Id",
                table: "Estados");
        }
    }
}
