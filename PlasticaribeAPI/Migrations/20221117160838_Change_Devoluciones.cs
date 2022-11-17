using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Change_Devoluciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DevMatPri_Hora",
                table: "Devoluciones_MatPrima",
                type: "varchar(10)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevMatPri_Hora",
                table: "Devoluciones_MatPrima");
        }
    }
}
