using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class MaquinaSinFKEnDesperdicios2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desperdicios_Activos_Actv_Id",
                table: "Desperdicios");

            migrationBuilder.DropIndex(
                name: "IX_Desperdicios_Actv_Id",
                table: "Desperdicios");

            migrationBuilder.RenameColumn(
                name: "Actv_Id",
                table: "Desperdicios",
                newName: "Maquina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Maquina",
                table: "Desperdicios",
                newName: "Actv_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Actv_Id",
                table: "Desperdicios",
                column: "Actv_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Desperdicios_Activos_Actv_Id",
                table: "Desperdicios",
                column: "Actv_Id",
                principalTable: "Activos",
                principalColumn: "Actv_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
