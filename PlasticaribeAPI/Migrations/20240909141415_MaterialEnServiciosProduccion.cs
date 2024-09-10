using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class MaterialEnServiciosProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Impreso",
                table: "Servicios_Produccion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Material_Id",
                table: "Servicios_Produccion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Produccion_Material_Id",
                table: "Servicios_Produccion",
                column: "Material_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Produccion_Materiales_MatPrima_Material_Id",
                table: "Servicios_Produccion",
                column: "Material_Id",
                principalTable: "Materiales_MatPrima",
                principalColumn: "Material_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Produccion_Materiales_MatPrima_Material_Id",
                table: "Servicios_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_Produccion_Material_Id",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "Material_Id",
                table: "Servicios_Produccion");
        }
    }
}
