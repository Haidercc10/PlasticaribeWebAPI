using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Limpieza2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "Cli_Direccion",
                table: "Clientes");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AddColumn<string>(
                name: "Cli_Direccion",
                table: "Clientes",
                type: "varchar(60)",
                nullable: false,
                defaultValue: "");*/
        }
    }
}
