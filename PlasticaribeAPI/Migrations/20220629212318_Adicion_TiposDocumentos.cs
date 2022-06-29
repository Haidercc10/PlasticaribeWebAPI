using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_TiposDocumentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipos_Documentos",
                columns: table => new
                {
                    TpDoc_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    TpDoc_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TpDoc_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Documentos", x => x.TpDoc_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos_Documentos");
        }
    }
}
