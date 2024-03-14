using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class adicionMatrial_OtExtrusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OT_Extrusion_Materias_Primas_Formato_Id",
                table: "OT_Extrusion");

            migrationBuilder.RenameColumn(
                name: "MatPri_Id",
                table: "OT_Extrusion",
                newName: "Material_Id");

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Id",
                table: "OT_Extrusion",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Extrusion_Material_Id",
                table: "OT_Extrusion",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Extrusion_UndMed_Id",
                table: "OT_Extrusion",
                column: "UndMed_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OT_Extrusion_Materiales_MatPrima_Material_Id",
                table: "OT_Extrusion",
                column: "Material_Id",
                principalTable: "Materiales_MatPrima",
                principalColumn: "Material_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OT_Extrusion_Unidades_Medidas_UndMed_Id",
                table: "OT_Extrusion",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OT_Extrusion_Materiales_MatPrima_Material_Id",
                table: "OT_Extrusion");

            migrationBuilder.DropForeignKey(
                name: "FK_OT_Extrusion_Unidades_Medidas_UndMed_Id",
                table: "OT_Extrusion");

            migrationBuilder.DropIndex(
                name: "IX_OT_Extrusion_Material_Id",
                table: "OT_Extrusion");

            migrationBuilder.DropIndex(
                name: "IX_OT_Extrusion_UndMed_Id",
                table: "OT_Extrusion");

            migrationBuilder.DropColumn(
                name: "UndMed_Id",
                table: "OT_Extrusion");

            migrationBuilder.RenameColumn(
                name: "Material_Id",
                table: "OT_Extrusion",
                newName: "MatPri_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OT_Extrusion_Materias_Primas_Formato_Id",
                table: "OT_Extrusion",
                column: "Formato_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
