using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class empresaActualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Empresas");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Empresas",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Empresas");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Empresas",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
