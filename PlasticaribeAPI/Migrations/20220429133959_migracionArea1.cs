using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class migracionArea1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
<<<<<<< Updated upstream:PlasticaribeAPI/Migrations/20220425215350_EightCreate.cs
                    area_Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    area_Codigo = table.Column<int>(type: "int", nullable: false),
=======
                    area_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
>>>>>>> Stashed changes:PlasticaribeAPI/Migrations/20220429133959_migracionArea1.cs
                    area_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    area_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.area_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
