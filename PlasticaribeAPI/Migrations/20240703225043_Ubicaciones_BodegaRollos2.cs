using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Ubicaciones_BodegaRollos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ubicaciones_BodegaRollos",
                columns: table => new
                {
                    UBR_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UBR_Bodega = table.Column<string>(type: "varchar(50)", nullable: false),
                    UBR_Id = table.Column<int>(type: "int", nullable: false),
                    UBR_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    UBR_SubId = table.Column<int>(type: "int", nullable: false),
                    UBR_SubNombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    UBR_Zona = table.Column<int>(type: "int", nullable: false),
                    UBR_Nomenclatura = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones_BodegaRollos", x => x.UBR_Codigo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ubicaciones_BodegaRollos");
        }
    }
}
