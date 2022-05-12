using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CajaID_EstadoID_Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Estados_Estado_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                table: "Usuarios",
                column: "cajComp_Id",
                principalTable: "Cajas_Compensaciones",
                principalColumn: "cajComp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Estados_Estado_Id",
                table: "Usuarios",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Estados_Estado_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                table: "Usuarios",
                column: "cajComp_Id",
                principalTable: "Cajas_Compensaciones",
                principalColumn: "cajComp_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Estados_Estado_Id",
                table: "Usuarios",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
