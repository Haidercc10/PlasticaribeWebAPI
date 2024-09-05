using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CodigoObservacion_MaqInternas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaqInt_Codigo",
                table: "Maquilas_Internas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaqInt_Observacion",
                table: "Maquilas_Internas",
                type: "varchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaqInt_Codigo",
                table: "Maquilas_Internas");

            migrationBuilder.DropColumn(
                name: "MaqInt_Observacion",
                table: "Maquilas_Internas");
        }
    }
}
