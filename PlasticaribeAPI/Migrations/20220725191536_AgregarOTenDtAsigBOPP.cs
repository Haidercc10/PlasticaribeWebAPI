using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AgregarOTenDtAsigBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DtAsigBOPP_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtAsigBOPP_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP");
        }
    }
}
