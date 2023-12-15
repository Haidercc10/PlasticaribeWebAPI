using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Estado_Rollos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_Rollo",
                table: "Produccion_Procesos",
                type: "int",
                nullable: false,
                defaultValue: 19);

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Estado_Rollo",
                table: "Produccion_Procesos",
                column: "Estado_Rollo");

            migrationBuilder.AddForeignKey(
                name: "FK_Produccion_Procesos_Estados_Estado_Rollo",
                table: "Produccion_Procesos",
                column: "Estado_Rollo",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produccion_Procesos_Estados_Estado_Rollo",
                table: "Produccion_Procesos");

            migrationBuilder.DropIndex(
                name: "IX_Produccion_Procesos_Estado_Rollo",
                table: "Produccion_Procesos");

            migrationBuilder.DropColumn(
                name: "Estado_Rollo",
                table: "Produccion_Procesos");
        }
    }
}
