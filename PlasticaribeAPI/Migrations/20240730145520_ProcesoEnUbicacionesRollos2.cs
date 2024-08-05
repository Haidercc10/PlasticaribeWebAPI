using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProcesoEnUbicacionesRollos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Proceso_Id",
                table: "Ubicaciones_BodegaRollos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "BGPI");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_BodegaRollos_Proceso_Id",
                table: "Ubicaciones_BodegaRollos",
                column: "Proceso_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ubicaciones_BodegaRollos_Procesos_Proceso_Id",
                table: "Ubicaciones_BodegaRollos",
                column: "Proceso_Id",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ubicaciones_BodegaRollos_Procesos_Proceso_Id",
                table: "Ubicaciones_BodegaRollos");

            migrationBuilder.DropIndex(
                name: "IX_Ubicaciones_BodegaRollos_Proceso_Id",
                table: "Ubicaciones_BodegaRollos");

            migrationBuilder.DropColumn(
                name: "Proceso_Id",
                table: "Ubicaciones_BodegaRollos");
        }
    }
}
