using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cambios_BodegaRollos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "Detalles_BodegasRollos",
                type: "int",
                nullable: true,
                defaultValue: 19);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Bodegas_Rollos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_BodegasRollos_Estado_Id",
                table: "Detalles_BodegasRollos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_Rollos_Usua_Id",
                table: "Bodegas_Rollos",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bodegas_Rollos_Usuarios_Usua_Id",
                table: "Bodegas_Rollos",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_BodegasRollos_Estados_Estado_Id",
                table: "Detalles_BodegasRollos",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bodegas_Rollos_Usuarios_Usua_Id",
                table: "Bodegas_Rollos");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_BodegasRollos_Estados_Estado_Id",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropIndex(
                name: "IX_Detalles_BodegasRollos_Estado_Id",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropIndex(
                name: "IX_Bodegas_Rollos_Usua_Id",
                table: "Bodegas_Rollos");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Bodegas_Rollos");
        }
    }
}
