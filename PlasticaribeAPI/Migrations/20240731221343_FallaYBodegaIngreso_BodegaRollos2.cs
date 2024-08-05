using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FallaYBodegaIngreso_BodegaRollos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Falla_Id",
                table: "Detalles_SolicitudRollos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BgRollo_BodegaIngreso",
                table: "Detalles_BodegasRollos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudRollos_Falla_Id",
                table: "Detalles_SolicitudRollos",
                column: "Falla_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_BodegasRollos_BgRollo_BodegaIngreso",
                table: "Detalles_BodegasRollos",
                column: "BgRollo_BodegaIngreso");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_BodegasRollos_Procesos_BgRollo_BodegaIngreso",
                table: "Detalles_BodegasRollos",
                column: "BgRollo_BodegaIngreso",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_SolicitudRollos_Fallas_Tecnicas_Falla_Id",
                table: "Detalles_SolicitudRollos",
                column: "Falla_Id",
                principalTable: "Fallas_Tecnicas",
                principalColumn: "Falla_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_BodegasRollos_Procesos_BgRollo_BodegaIngreso",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_SolicitudRollos_Fallas_Tecnicas_Falla_Id",
                table: "Detalles_SolicitudRollos");

            migrationBuilder.DropIndex(
                name: "IX_Detalles_SolicitudRollos_Falla_Id",
                table: "Detalles_SolicitudRollos");

            migrationBuilder.DropIndex(
                name: "IX_Detalles_BodegasRollos_BgRollo_BodegaIngreso",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropColumn(
                name: "Falla_Id",
                table: "Detalles_SolicitudRollos");

            migrationBuilder.DropColumn(
                name: "BgRollo_BodegaIngreso",
                table: "Detalles_BodegasRollos");
        }
    }
}
