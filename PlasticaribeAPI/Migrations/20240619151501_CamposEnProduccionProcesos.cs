using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CamposEnProduccionProcesos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Produccion_Procesos",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Rollo_Asociado",
                table: "Produccion_Procesos",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Produccion_Procesos");

            migrationBuilder.DropColumn(
                name: "Rollo_Asociado",
                table: "Produccion_Procesos");
        }
    }
}
