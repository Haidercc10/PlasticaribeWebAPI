using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
