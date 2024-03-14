using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
