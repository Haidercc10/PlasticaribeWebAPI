using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Usuario_Autoriza_Pesos_Teoricos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Autoriza_Id",
                table: "Trazabilidad_Produccion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Autoriza_Id",
                table: "Produccion_Procesos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Autoriza_Id",
                table: "Trazabilidad_Produccion",
                column: "Autoriza_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Autoriza_Id",
                table: "Produccion_Procesos",
                column: "Autoriza_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produccion_Procesos_Usuarios_Autoriza_Id",
                table: "Produccion_Procesos",
                column: "Autoriza_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidad_Produccion_Usuarios_Autoriza_Id",
                table: "Trazabilidad_Produccion",
                column: "Autoriza_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produccion_Procesos_Usuarios_Autoriza_Id",
                table: "Produccion_Procesos");

            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Produccion_Usuarios_Autoriza_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Trazabilidad_Produccion_Autoriza_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Produccion_Procesos_Autoriza_Id",
                table: "Produccion_Procesos");

            migrationBuilder.DropColumn(
                name: "Autoriza_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropColumn(
                name: "Autoriza_Id",
                table: "Produccion_Procesos");
        }
    }
}
