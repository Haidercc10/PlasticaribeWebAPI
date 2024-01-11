using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CreacionTipoMoneda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Tipos_Monedas",
                columns: table => new
                {
                    TpMoneda_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    TpMoneda_Codigo = table.Column<int>(type: "int", nullable: false),
                    TpMoneda_Nombre = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Monedas", x => x.TpMoneda_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos_Monedas");


        }
    }
}
