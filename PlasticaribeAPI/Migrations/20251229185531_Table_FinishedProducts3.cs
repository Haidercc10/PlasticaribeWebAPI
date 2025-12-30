using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Table_FinishedProducts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "Producto_Terminado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_Prod_Id",
                table: "Producto_Terminado",
                column: "Prod_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Terminado_Productos_Prod_Id",
                table: "Producto_Terminado",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Terminado_Productos_Prod_Id",
                table: "Producto_Terminado");

            migrationBuilder.DropIndex(
                name: "IX_Producto_Terminado_Prod_Id",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "Producto_Terminado");
        }
    }
}
