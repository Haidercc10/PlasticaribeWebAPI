using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Trazabilidad_Produccion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Presentacion",
                table: "Trazabilidad_Produccion",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Presentacion",
                table: "Trazabilidad_Produccion",
                column: "Presentacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidad_Produccion_Unidades_Medidas_Presentacion",
                table: "Trazabilidad_Produccion",
                column: "Presentacion",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Produccion_Unidades_Medidas_Presentacion",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Trazabilidad_Produccion_Presentacion",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropColumn(
                name: "Presentacion",
                table: "Trazabilidad_Produccion");
        }
    }
}
