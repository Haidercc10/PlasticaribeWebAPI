using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CreacionSedesClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sedes_Clientes",
                columns: table => new
                {
                    SedeCli_Id = table.Column<int>(type: "int", nullable: false),
                    SedeCli_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SedeCli_Ciudad = table.Column<string>(type: "varchar(50)", nullable: false),
                    SedeCli_CodPostal = table.Column<string>(type: "varchar(50)", nullable: false),
                    Usu_IdUsua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes_Clientes", x => x.SedeCli_Id);
                    table.ForeignKey(
                        name: "FK_Sedes_Clientes_Usuarios_Usu_IdUsua_Id",
                        column: x => x.Usu_IdUsua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_Clientes_Usu_IdUsua_Id",
                table: "Sedes_Clientes",
                column: "Usu_IdUsua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sedes_Clientes");
        }
    }
}
