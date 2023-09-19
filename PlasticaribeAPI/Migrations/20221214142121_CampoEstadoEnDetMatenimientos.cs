using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CampoEstadoEnDetMatenimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "Detalles_Mantenimientos",
                type: "int",
                nullable: false,
                defaultValue: 11);

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Mantenimientos_Estado_Id",
                table: "Detalles_Mantenimientos",
                column: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_Mantenimientos_Estados_Estado_Id",
                table: "Detalles_Mantenimientos",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_Mantenimientos_Estados_Estado_Id",
                table: "Detalles_Mantenimientos");

            migrationBuilder.DropIndex(
                name: "IX_Detalles_Mantenimientos_Estado_Id",
                table: "Detalles_Mantenimientos");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "Detalles_Mantenimientos");


        }
    }
}
