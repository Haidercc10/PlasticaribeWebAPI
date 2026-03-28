using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Material_InControlQualityExt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Material_Id",
                table: "ControlCalidad_Extrusion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Extrusion_Material_Id",
                table: "ControlCalidad_Extrusion",
                column: "Material_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlCalidad_Extrusion_Materiales_MatPrima_Material_Id",
                table: "ControlCalidad_Extrusion",
                column: "Material_Id",
                principalTable: "Materiales_MatPrima",
                principalColumn: "Material_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlCalidad_Extrusion_Materiales_MatPrima_Material_Id",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropIndex(
                name: "IX_ControlCalidad_Extrusion_Material_Id",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "Material_Id",
                table: "ControlCalidad_Extrusion");
        }
    }
}
