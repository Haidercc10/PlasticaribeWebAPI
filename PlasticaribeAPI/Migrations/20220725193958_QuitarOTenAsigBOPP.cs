using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class QuitarOTenAsigBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsigBOPP_OrdenTrabajo",
                table: "Asignaciones_BOPP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AsigBOPP_OrdenTrabajo",
                table: "Asignaciones_BOPP",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
