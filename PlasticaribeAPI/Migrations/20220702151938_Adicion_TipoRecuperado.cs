using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_TipoRecuperado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipos_Recuperados",
                columns: table => new
                {
                    TpRecu_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    TpRecu_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TpRecu_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Recuperados", x => x.TpRecu_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos_Recuperados");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
