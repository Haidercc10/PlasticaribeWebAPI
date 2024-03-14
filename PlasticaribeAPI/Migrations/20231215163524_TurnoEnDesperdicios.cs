using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class TurnoEnDesperdicios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Turno_Id",
                table: "Desperdicios",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "NE");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Turno_Id",
                table: "Desperdicios",
                column: "Turno_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Desperdicios_Turnos_Turno_Id",
                table: "Desperdicios",
                column: "Turno_Id",
                principalTable: "Turnos",
                principalColumn: "Turno_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desperdicios_Turnos_Turno_Id",
                table: "Desperdicios");

            migrationBuilder.DropIndex(
                name: "IX_Desperdicios_Turno_Id",
                table: "Desperdicios");

            migrationBuilder.DropColumn(
                name: "Turno_Id",
                table: "Desperdicios");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
