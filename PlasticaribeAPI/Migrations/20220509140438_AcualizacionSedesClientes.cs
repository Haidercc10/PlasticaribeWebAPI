using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AcualizacionSedesClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "SedeCliente_Direccion",
                table: "Sedes_Clientes",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SedeCliente_Direccion",
                table: "Sedes_Clientes");

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
