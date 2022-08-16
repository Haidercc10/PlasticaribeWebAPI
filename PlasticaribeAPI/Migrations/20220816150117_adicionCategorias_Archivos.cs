using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class adicionCategorias_Archivos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Categorias_Categoria_Id",
                table: "Archivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "Categorias_Archivos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias_Archivos",
                table: "Categorias_Archivos",
                column: "Categoria_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Categorias_Archivos_Categoria_Id",
                table: "Archivos",
                column: "Categoria_Id",
                principalTable: "Categorias_Archivos",
                principalColumn: "Categoria_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Categorias_Archivos_Categoria_Id",
                table: "Archivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias_Archivos",
                table: "Categorias_Archivos");

            migrationBuilder.RenameTable(
                name: "Categorias_Archivos",
                newName: "Categorias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "Categoria_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Categorias_Categoria_Id",
                table: "Archivos",
                column: "Categoria_Id",
                principalTable: "Categorias",
                principalColumn: "Categoria_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
