using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Areas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "area_Nombre",
                table: "Areas",
                newName: "Area_Nombre");

            migrationBuilder.RenameColumn(
                name: "area_Descripcion",
                table: "Areas",
                newName: "Area_Descripcion");

            migrationBuilder.RenameColumn(
                name: "area_Codigo",
                table: "Areas",
                newName: "Area_Codigo");

            migrationBuilder.RenameColumn(
                name: "area_Id",
                table: "Areas",
                newName: "Area_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Area_Nombre",
                table: "Areas",
                newName: "area_Nombre");

            migrationBuilder.RenameColumn(
                name: "Area_Descripcion",
                table: "Areas",
                newName: "area_Descripcion");

            migrationBuilder.RenameColumn(
                name: "Area_Codigo",
                table: "Areas",
                newName: "area_Codigo");

            migrationBuilder.RenameColumn(
                name: "Area_Id",
                table: "Areas",
                newName: "area_Id");
        }
    }
}
