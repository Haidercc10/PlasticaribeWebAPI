using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFields_BoppTinta_InvAreas5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BoppGen_Id",
                table: "Inventarios_Areas",
                type: "bigint",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<long>(
                name: "Tinta_Id",
                table: "Inventarios_Areas",
                type: "bigint",
                nullable: false,
                defaultValue: 2001);

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Areas_BoppGen_Id",
                table: "Inventarios_Areas",
                column: "BoppGen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Areas_Tinta_Id",
                table: "Inventarios_Areas",
                column: "Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Areas_Bopp_Generico_BoppGen_Id",
                table: "Inventarios_Areas",
                column: "BoppGen_Id",
                principalTable: "Bopp_Generico",
                principalColumn: "BoppGen_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Areas_Tintas_Tinta_Id",
                table: "Inventarios_Areas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Areas_Bopp_Generico_BoppGen_Id",
                table: "Inventarios_Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Areas_Tintas_Tinta_Id",
                table: "Inventarios_Areas");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_Areas_BoppGen_Id",
                table: "Inventarios_Areas");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_Areas_Tinta_Id",
                table: "Inventarios_Areas");

            migrationBuilder.DropColumn(
                name: "BoppGen_Id",
                table: "Inventarios_Areas");

            migrationBuilder.DropColumn(
                name: "Tinta_Id",
                table: "Inventarios_Areas");
        }
    }
}
