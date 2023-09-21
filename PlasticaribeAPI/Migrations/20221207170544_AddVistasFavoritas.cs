using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVistasFavoritas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VistasFavoritas",
                columns: table => new
                {
                    VistasFavId = table.Column<int>(name: "VistasFav_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuaId = table.Column<long>(name: "Usua_Id", type: "bigint", nullable: false),
                    VistaFavNum1 = table.Column<int>(name: "VistaFav_Num1", type: "int", nullable: false),
                    VistaFavNum2 = table.Column<int>(name: "VistaFav_Num2", type: "int", nullable: false),
                    VistaFavNum3 = table.Column<int>(name: "VistaFav_Num3", type: "int", nullable: false),
                    VistaFavNum4 = table.Column<int>(name: "VistaFav_Num4", type: "int", nullable: false),
                    VistaFavNum5 = table.Column<int>(name: "VistaFav_Num5", type: "int", nullable: false),
                    VistaFavFecha = table.Column<DateTime>(name: "VistaFav_Fecha", type: "datetime2", nullable: false),
                    VistaFavHora = table.Column<string>(name: "VistaFav_Hora", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VistasFavoritas", x => x.VistasFavId);
                    table.ForeignKey(
                        name: "FK_VistasFavoritas_Usuarios_Usua_Id",
                        column: x => x.UsuaId,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VistasFavoritas_Usua_Id",
                table: "VistasFavoritas",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VistasFavoritas");
        }
    }
}
