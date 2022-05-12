using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class tipoID_CajaCompensacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "Cajas_Compensaciones",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_Compensaciones_TipoIdentificacion_Id",
                table: "Cajas_Compensaciones",
                column: "TipoIdentificacion_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cajas_Compensaciones_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Cajas_Compensaciones",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cajas_Compensaciones_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Cajas_Compensaciones");

            migrationBuilder.DropIndex(
                name: "IX_Cajas_Compensaciones_TipoIdentificacion_Id",
                table: "Cajas_Compensaciones");

            migrationBuilder.DropColumn(
                name: "TipoIdentificacion_Id",
                table: "Cajas_Compensaciones");
        }
    }
}
