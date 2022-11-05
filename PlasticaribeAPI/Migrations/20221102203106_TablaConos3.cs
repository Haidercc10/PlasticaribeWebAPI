using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class TablaConos3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Conos",
                columns: table => new
                {
                    Cono_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Cono_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cono_KgXCmsAncho = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Cono_Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conos", x => x.Cono_Id);
                });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "Conos");       

        }
    }
}
