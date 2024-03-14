using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or members
    public partial class AdicionMateriales_MatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materiales_MatPrima",
                columns: table => new
                {
                    Material_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Material_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Material_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales_MatPrima", x => x.Material_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materiales_MatPrima");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or members
}
