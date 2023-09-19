using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FechaHoraEliminacionRollosDesechos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Rollo_FechaEliminacion",
                table: "Rollos_Desechos",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rollo_HoraEliminacion",
                table: "Rollos_Desechos",
                type: "varchar(10)",
                nullable: true);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rollo_FechaEliminacion",
                table: "Rollos_Desechos");

            migrationBuilder.DropColumn(
                name: "Rollo_HoraEliminacion",
                table: "Rollos_Desechos");


        }
    }
}
