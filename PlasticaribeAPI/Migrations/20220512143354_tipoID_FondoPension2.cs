using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class tipoID_FondoPension2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "FondosPensiones",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FondosPensiones_TipoIdentificacion_Id",
                table: "FondosPensiones",
                column: "TipoIdentificacion_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FondosPensiones_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "FondosPensiones",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FondosPensiones_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "FondosPensiones");

            migrationBuilder.DropIndex(
                name: "IX_FondosPensiones_TipoIdentificacion_Id",
                table: "FondosPensiones");

            migrationBuilder.DropColumn(
                name: "TipoIdentificacion_Id",
                table: "FondosPensiones");
        }
    }
}
