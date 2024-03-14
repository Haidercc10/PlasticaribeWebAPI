using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class PruebaImagenUrl_CatInsumo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatInsu_UrlImagen",
                table: "Categorias_Insumos",
                type: "varchar(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatInsu_UrlImagen",
                table: "Categorias_Insumos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
