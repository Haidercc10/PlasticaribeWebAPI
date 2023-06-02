using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cambio_OT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Ot_Cyrel",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<bool>(
                name: "Ot_Corte",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ot_Cyrel",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Ot_Corte",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
