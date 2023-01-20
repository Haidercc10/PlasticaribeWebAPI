using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionLlavePrimariaMaterialEnProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14);

            migrationBuilder.AddColumn<int>(
                name: "Pigmt_Id",
                table: "Productos",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 15);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Pigmt_Id",
                table: "Productos",
                column: "Pigmt_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Materials_Pigmt_Id",
                table: "Productos",
                column: "Pigmt_Id",
                principalTable: "Materials",
                principalColumn: "Pigmt_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Materials_Pigmt_Id",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Pigmt_Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Pigmt_Id",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 14);
        }
    }
}
