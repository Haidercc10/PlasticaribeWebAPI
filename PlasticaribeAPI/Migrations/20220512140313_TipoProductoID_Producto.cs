using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class TipoProductoID_Producto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tipos_Productos_TpProd_Id",
                table: "Productos");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tipos_Productos_TpProd_Id",
                table: "Productos",
                column: "TpProd_Id",
                principalTable: "Tipos_Productos",
                principalColumn: "TpProd_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tipos_Productos_TpProd_Id",
                table: "Productos");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tipos_Productos_TpProd_Id",
                table: "Productos",
                column: "TpProd_Id",
                principalTable: "Tipos_Productos",
                principalColumn: "TpProd_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
