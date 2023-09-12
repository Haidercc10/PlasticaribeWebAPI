using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Producto_EnEntradasSalidasMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "Entradas_Salidas_MP",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Salidas_MP_Prod_Id",
                table: "Entradas_Salidas_MP",
                column: "Prod_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Salidas_MP_Productos_Prod_Id",
                table: "Entradas_Salidas_MP",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Salidas_MP_Productos_Prod_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Salidas_MP_Prod_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "Entradas_Salidas_MP");
        }
    }
}
