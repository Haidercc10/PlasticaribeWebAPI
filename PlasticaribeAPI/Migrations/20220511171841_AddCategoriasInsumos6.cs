using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddCategoriasInsumos6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_Estados_Tipos_Estados_TpEstado_Id1",
                 table: "Estados");

             migrationBuilder.DropColumn(
                 name: "TpEstado_Id",
                 table: "Estados"); */

            migrationBuilder.CreateTable(
                name: "Categorias_Insumos",
                columns: table => new
                {
                    CatInsu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatInsu_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    CatInsu_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias_Insumos", x => x.CatInsu_Id);
                });

            /* migrationBuilder.AddForeignKey(
                 name: "FK_Estados_Tipos_Estados_TpEstado_Id1",
                 table: "Estados",
                 column: "TpEstado_Id1",
                 principalTable: "Tipos_Estados",
                 principalColumn: "TpEstado_Id",
                 onDelete: ReferentialAction.Restrict); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                  name: "FK_Estados_Tipos_Estados_TpEstado_Id1",
                  table: "Estados"); */

            migrationBuilder.DropTable(
                name: "Categorias_Insumos");

            /* migrationBuilder.AddColumn<int>(
                 name: "TpEstado_Id",
                 table: "Estados",
                 type: "int",
                 nullable: false,
                 defaultValue: 0);

             migrationBuilder.AddForeignKey(
                 name: "FK_Estados_Tipos_Estados_TpEstado_Id1",
                 table: "Estados",
                 column: "TpEstado_Id1",
                 principalTable: "Tipos_Estados",
                 principalColumn: "TpEstado_Id",
                 onDelete: ReferentialAction.Cascade); */
        }
    }
}
