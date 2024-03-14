using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Falla_EnRollosDesechos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Falla_Id",
                table: "Rollos_Desechos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_Falla_Id",
                table: "Rollos_Desechos",
                column: "Falla_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rollos_Desechos_Fallas_Tecnicas_Falla_Id",
                table: "Rollos_Desechos",
                column: "Falla_Id",
                principalTable: "Fallas_Tecnicas",
                principalColumn: "Falla_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rollos_Desechos_Fallas_Tecnicas_Falla_Id",
                table: "Rollos_Desechos");

            migrationBuilder.DropIndex(
                name: "IX_Rollos_Desechos_Falla_Id",
                table: "Rollos_Desechos");

            migrationBuilder.DropColumn(
                name: "Falla_Id",
                table: "Rollos_Desechos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
