using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangesAutorizacionesExcesoOT2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutEx_HoraConsumoUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AutEx_HoraRespuestaUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AutEx_HoraSolicitudUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutEx_HoraConsumoUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "AutEx_HoraRespuestaUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "AutEx_HoraSolicitudUtc",
                table: "Autorizaciones_ExcesoOT");
        }
    }
}
