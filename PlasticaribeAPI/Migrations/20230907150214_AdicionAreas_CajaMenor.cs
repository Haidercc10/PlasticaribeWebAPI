using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionAreas_CajaMenor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Area_Id",
                table: "CajaMenor_Plasticaribe",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CajaMenor_Plasticaribe_Area_Id",
                table: "CajaMenor_Plasticaribe",
                column: "Area_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CajaMenor_Plasticaribe_Areas_Area_Id",
                table: "CajaMenor_Plasticaribe",
                column: "Area_Id",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CajaMenor_Plasticaribe_Areas_Area_Id",
                table: "CajaMenor_Plasticaribe");

            migrationBuilder.DropIndex(
                name: "IX_CajaMenor_Plasticaribe_Area_Id",
                table: "CajaMenor_Plasticaribe");

            migrationBuilder.DropColumn(
                name: "Area_Id",
                table: "CajaMenor_Plasticaribe");
        }
    }
}
