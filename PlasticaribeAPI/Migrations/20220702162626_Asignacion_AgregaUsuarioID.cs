using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Asignacion_AgregaUsuarioID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Asignaciones_MatPrima",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_Usua_Id",
                table: "Asignaciones_MatPrima",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_MatPrima_Usuarios_Usua_Id",
                table: "Asignaciones_MatPrima",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_MatPrima_Usuarios_Usua_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_MatPrima_Usua_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Asignaciones_MatPrima");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
