using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddCategoriasInsumos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TpEstado_Id",
                table: "Estados",
                newName: "TpEstado_Id1");

            /* migrationBuilder.AddColumn<int>(
                 name: "TpEstado_Id1",
                 table: "Estados",
                 type: "int",
                 nullable: false,
                 defaultValue: 0); */

            /*migrationBuilder.CreateTable(
                name: "Tipos_Estados",
                columns: table => new
                {
                    TpEstado_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpEstado_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    TpEstado_Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Estados", x => x.TpEstado_Id);
                });*/

            /* migrationBuilder.CreateIndex(
                 name: "IX_Estados_TpEstado_Id1",
                 table: "Estados",
                 column: "TpEstado_Id1"); */

            /* migrationBuilder.AddForeignKey(
                 name: "FK_Estados_Tipos_Estados_TpEstado_Id1",
                 table: "Estados",
                 column: "TpEstado_Id1",
                 principalTable: "Tipos_Estados",
                 principalColumn: "TpEstado_Id",
                 onDelete: ReferentialAction.Cascade); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id1",
                table: "Estados");

            migrationBuilder.DropTable(
                name: "Tipos_Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_TpEstado_Id1",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "TpEstado_Id",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "TpEstado_Id1",
                table: "Estados");
        }
    }
}
