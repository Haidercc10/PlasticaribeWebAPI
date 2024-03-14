using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Vistas_Permisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vistas_Permisos",
                columns: table => new
                {
                    Vp_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vp_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vp_Icono_Menu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vp_Icono_Dock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vp_Ruta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vp_Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vp_Id_Roles = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vistas_Permisos", x => x.Vp_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vistas_Permisos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
