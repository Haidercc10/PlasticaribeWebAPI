using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambiosTablaOrdenTrabajo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Corte",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Extrusion",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Impresion",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Laminado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rotograbado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sellado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corte",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Extrusion",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Impresion",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Laminado",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Rotograbado",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Sellado",
                table: "Orden_Trabajo");
        }
    }
}
