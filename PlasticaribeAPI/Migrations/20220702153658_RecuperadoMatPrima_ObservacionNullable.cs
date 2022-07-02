using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class RecuperadoMatPrima_ObservacionNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecMp_Observacion",
                table: "Recuperados_MatPrima",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecMp_Observacion",
                table: "Recuperados_MatPrima",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
