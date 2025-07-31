using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SubcategoriesInMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCatMP_Id",
                table: "Tintas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCatMP_Id",
                table: "Materias_Primas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCatMP_Id",
                table: "BOPP",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tintas_SubCatMP_Id",
                table: "Tintas",
                column: "SubCatMP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_Primas_SubCatMP_Id",
                table: "Materias_Primas",
                column: "SubCatMP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_SubCatMP_Id",
                table: "BOPP",
                column: "SubCatMP_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BOPP_Subcategorias_MatPrima_SubCatMP_Id",
                table: "BOPP",
                column: "SubCatMP_Id",
                principalTable: "Subcategorias_MatPrima",
                principalColumn: "SubCatMP_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Primas_Subcategorias_MatPrima_SubCatMP_Id",
                table: "Materias_Primas",
                column: "SubCatMP_Id",
                principalTable: "Subcategorias_MatPrima",
                principalColumn: "SubCatMP_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tintas_Subcategorias_MatPrima_SubCatMP_Id",
                table: "Tintas",
                column: "SubCatMP_Id",
                principalTable: "Subcategorias_MatPrima",
                principalColumn: "SubCatMP_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOPP_Subcategorias_MatPrima_SubCatMP_Id",
                table: "BOPP");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Primas_Subcategorias_MatPrima_SubCatMP_Id",
                table: "Materias_Primas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tintas_Subcategorias_MatPrima_SubCatMP_Id",
                table: "Tintas");

            migrationBuilder.DropIndex(
                name: "IX_Tintas_SubCatMP_Id",
                table: "Tintas");

            migrationBuilder.DropIndex(
                name: "IX_Materias_Primas_SubCatMP_Id",
                table: "Materias_Primas");

            migrationBuilder.DropIndex(
                name: "IX_BOPP_SubCatMP_Id",
                table: "BOPP");

            migrationBuilder.DropColumn(
                name: "SubCatMP_Id",
                table: "Tintas");

            migrationBuilder.DropColumn(
                name: "SubCatMP_Id",
                table: "Materias_Primas");

            migrationBuilder.DropColumn(
                name: "SubCatMP_Id",
                table: "BOPP");
        }
    }
}
