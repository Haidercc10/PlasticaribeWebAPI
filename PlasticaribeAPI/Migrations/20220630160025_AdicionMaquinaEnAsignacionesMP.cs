using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AdicionMaquinaEnAsignacionesMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsigMp_Maquina",
                table: "Asignaciones_MatPrima",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsigMp_Maquina",
                table: "Asignaciones_MatPrima");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
