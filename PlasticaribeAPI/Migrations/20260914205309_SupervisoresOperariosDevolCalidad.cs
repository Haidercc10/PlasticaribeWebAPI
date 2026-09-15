using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SupervisoresOperariosDevolCalidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Operario",
                table: "Devoluciones_Calidad",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Supervisor",
                table: "Devoluciones_Calidad",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_Calidad_Usua_Operario",
                table: "Devoluciones_Calidad",
                column: "Usua_Operario");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_Calidad_Usua_Supervisor",
                table: "Devoluciones_Calidad",
                column: "Usua_Supervisor");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_Calidad_Usuarios_Usua_Operario",
                table: "Devoluciones_Calidad",
                column: "Usua_Operario",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_Calidad_Usuarios_Usua_Supervisor",
                table: "Devoluciones_Calidad",
                column: "Usua_Supervisor",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_Calidad_Usuarios_Usua_Operario",
                table: "Devoluciones_Calidad");

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_Calidad_Usuarios_Usua_Supervisor",
                table: "Devoluciones_Calidad");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_Calidad_Usua_Operario",
                table: "Devoluciones_Calidad");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_Calidad_Usua_Supervisor",
                table: "Devoluciones_Calidad");

            migrationBuilder.DropColumn(
                name: "Usua_Operario",
                table: "Devoluciones_Calidad");

            migrationBuilder.DropColumn(
                name: "Usua_Supervisor",
                table: "Devoluciones_Calidad");
        }
    }
}
