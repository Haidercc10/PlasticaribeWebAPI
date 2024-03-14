using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AdicionMaterial_IdEnProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Material_Id",
                table: "Productos",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 16);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Material_Id",
                table: "Productos",
                column: "Material_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Materiales_MatPrima_Material_Id",
                table: "Productos",
                column: "Material_Id",
                principalTable: "Materiales_MatPrima",
                principalColumn: "Material_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Materiales_MatPrima_Material_Id",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Material_Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Material_Id",
                table: "Productos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
