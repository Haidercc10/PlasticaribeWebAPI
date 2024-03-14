using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class TiposUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "TipoUsuario_Codigo",
                table: "TipoUsuarios");*/

            migrationBuilder.CreateTable(
                name: "Tipos_Usuarios",
                columns: table => new
                {
                    tpUsu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tpUsu_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    tpUsu_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Usuarios", x => x.tpUsu_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos_Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "TipoUsuario_Codigo",
                table: "TipoUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
