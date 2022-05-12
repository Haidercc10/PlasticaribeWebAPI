using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class tipoID_EPS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "EPS",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EPS_TipoIdentificacion_Id",
                table: "EPS",
                column: "TipoIdentificacion_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EPS_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "EPS",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EPS_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "EPS");

            migrationBuilder.DropIndex(
                name: "IX_EPS_TipoIdentificacion_Id",
                table: "EPS");

            migrationBuilder.DropColumn(
                name: "TipoIdentificacion_Id",
                table: "EPS");
        }
    }
}
