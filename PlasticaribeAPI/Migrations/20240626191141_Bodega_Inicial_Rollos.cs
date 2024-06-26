using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Bodega_Inicial_Rollos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BgRollo_BodegaInicial",
                table: "Detalles_BodegasRollos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_BodegasRollos_BgRollo_BodegaInicial",
                table: "Detalles_BodegasRollos",
                column: "BgRollo_BodegaInicial");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_BodegasRollos_Procesos_BgRollo_BodegaInicial",
                table: "Detalles_BodegasRollos",
                column: "BgRollo_BodegaInicial",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_BodegasRollos_Procesos_BgRollo_BodegaInicial",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropIndex(
                name: "IX_Detalles_BodegasRollos_BgRollo_BodegaInicial",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropColumn(
                name: "BgRollo_BodegaInicial",
                table: "Detalles_BodegasRollos");
        }
    }
}
