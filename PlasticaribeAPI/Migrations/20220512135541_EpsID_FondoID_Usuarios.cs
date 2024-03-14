using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class EpsID_FondoID_Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_EPS_eps_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_FondosPensiones_fPen_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_EPS_eps_Id",
                table: "Usuarios",
                column: "eps_Id",
                principalTable: "EPS",
                principalColumn: "eps_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_FondosPensiones_fPen_Id",
                table: "Usuarios",
                column: "fPen_Id",
                principalTable: "FondosPensiones",
                principalColumn: "fPen_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_EPS_eps_Id",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_FondosPensiones_fPen_Id",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_EPS_eps_Id",
                table: "Usuarios",
                column: "eps_Id",
                principalTable: "EPS",
                principalColumn: "eps_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_FondosPensiones_fPen_Id",
                table: "Usuarios",
                column: "fPen_Id",
                principalTable: "FondosPensiones",
                principalColumn: "fPen_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
