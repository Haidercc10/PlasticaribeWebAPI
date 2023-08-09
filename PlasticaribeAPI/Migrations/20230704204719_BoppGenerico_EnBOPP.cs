using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class BoppGenerico_EnBOPP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BoppGen_Id",
                table: "BOPP",
                type: "bigint",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_BoppGen_Id",
                table: "BOPP",
                column: "BoppGen_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BOPP_Bopp_Generico_BoppGen_Id",
                table: "BOPP",
                column: "BoppGen_Id",
                principalTable: "Bopp_Generico",
                principalColumn: "BoppGen_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOPP_Bopp_Generico_BoppGen_Id",
                table: "BOPP");

            migrationBuilder.DropIndex(
                name: "IX_BOPP_BoppGen_Id",
                table: "BOPP");

            migrationBuilder.DropColumn(
                name: "BoppGen_Id",
                table: "BOPP");
        }
    }
}
