using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AdiciosTipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoIdentificaciones",
                columns: table => new
                {
                    TipoIdentificacion_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    TipoIdentificacion_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    TipoIdentificacion_Descripcion = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIdentificaciones", x => x.TipoIdentificacion_Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    TipoUsuario_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    TipoUsuario_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoUsuario_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    TipoUsuario_Descripcion = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.TipoUsuario_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoIdentificaciones");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
