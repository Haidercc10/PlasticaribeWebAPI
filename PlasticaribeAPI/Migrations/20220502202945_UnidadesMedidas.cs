using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class UnidadesMedidas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Unidades_Medidas",
                columns: table => new
                {
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    UndMed_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UndMed_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    UndMed_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades_Medidas", x => x.UndMed_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Unidades_Medidas");
        }
    }
}
