using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class DeleteIDAreaUsuarioDeAsignacionMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_MatPrima_Areas_Area_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_MatPrima_Usuarios_Usua_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_MatPrima_Area_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_MatPrima_Usua_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "Area_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Asignaciones_MatPrima");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Area_Id",
                table: "Asignaciones_MatPrima",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Asignaciones_MatPrima",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_Area_Id",
                table: "Asignaciones_MatPrima",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_Usua_Id",
                table: "Asignaciones_MatPrima",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_MatPrima_Areas_Area_Id",
                table: "Asignaciones_MatPrima",
                column: "Area_Id",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_MatPrima_Usuarios_Usua_Id",
                table: "Asignaciones_MatPrima",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
