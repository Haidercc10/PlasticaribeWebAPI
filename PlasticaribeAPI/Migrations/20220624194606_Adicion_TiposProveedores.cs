using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_TiposProveedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipos_Proveedores",
                columns: table => new
                {
                    TpProv_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpProv_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    TpProv_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Proveedores", x => x.TpProv_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos_Proveedores");
        }
    }
}
