using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Insumos3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Categorias_Insumos_CatInsu_Id",
                table: "Insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Unidades_Medidas_UndMed_Id",
                table: "Insumo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insumo",
                table: "Insumo");

            migrationBuilder.RenameTable(
                name: "Insumo",
                newName: "Insumos");

            migrationBuilder.RenameIndex(
                name: "IX_Insumo_UndMed_Id",
                table: "Insumos",
                newName: "IX_Insumos_UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Insumo_CatInsu_Id",
                table: "Insumos",
                newName: "IX_Insumos_CatInsu_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insumos",
                table: "Insumos",
                column: "Insu_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Categorias_Insumos_CatInsu_Id",
                table: "Insumos",
                column: "CatInsu_Id",
                principalTable: "Categorias_Insumos",
                principalColumn: "CatInsu_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Unidades_Medidas_UndMed_Id",
                table: "Insumos",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Categorias_Insumos_CatInsu_Id",
                table: "Insumos");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Unidades_Medidas_UndMed_Id",
                table: "Insumos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insumos",
                table: "Insumos");

            migrationBuilder.RenameTable(
                name: "Insumos",
                newName: "Insumo");

            migrationBuilder.RenameIndex(
                name: "IX_Insumos_UndMed_Id",
                table: "Insumo",
                newName: "IX_Insumo_UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Insumos_CatInsu_Id",
                table: "Insumo",
                newName: "IX_Insumo_CatInsu_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insumo",
                table: "Insumo",
                column: "Insu_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Categorias_Insumos_CatInsu_Id",
                table: "Insumo",
                column: "CatInsu_Id",
                principalTable: "Categorias_Insumos",
                principalColumn: "CatInsu_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Unidades_Medidas_UndMed_Id",
                table: "Insumo",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
