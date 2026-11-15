using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class VistasFavoritas_SieteYUnicoPorUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VistasFavoritas_Usua_Id",
                table: "VistasFavoritas");

            migrationBuilder.AddColumn<int>(
                name: "VistaFav_Num6",
                table: "VistasFavoritas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VistaFav_Num7",
                table: "VistasFavoritas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // El front anterior podía crear varias filas por usuario: se conserva la más reciente (mayor id)
            // antes de crear el índice único.
            migrationBuilder.Sql(@"
                WITH Duplicadas AS (
                    SELECT VistasFav_Id, ROW_NUMBER() OVER (PARTITION BY Usua_Id ORDER BY VistasFav_Id DESC) AS Fila
                    FROM VistasFavoritas
                )
                DELETE FROM Duplicadas WHERE Fila > 1;");

            migrationBuilder.CreateIndex(
                name: "IX_VistasFavoritas_Usua_Id",
                table: "VistasFavoritas",
                column: "Usua_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VistasFavoritas_Usua_Id",
                table: "VistasFavoritas");

            migrationBuilder.DropColumn(
                name: "VistaFav_Num6",
                table: "VistasFavoritas");

            migrationBuilder.DropColumn(
                name: "VistaFav_Num7",
                table: "VistasFavoritas");

            migrationBuilder.CreateIndex(
                name: "IX_VistasFavoritas_Usua_Id",
                table: "VistasFavoritas",
                column: "Usua_Id");
        }
    }
}
