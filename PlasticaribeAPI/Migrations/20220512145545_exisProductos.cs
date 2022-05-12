using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class exisProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Existencia_Producto_Productos_Prod_Id",
                table: "Existencia_Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencia_Producto_Tipos_Bodegas_TpBod_Id",
                table: "Existencia_Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencia_Producto_Unidades_Medidas_UndMed_Id",
                table: "Existencia_Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Existencia_Producto",
                table: "Existencia_Producto");

            migrationBuilder.RenameTable(
                name: "Existencia_Producto",
                newName: "Existencias_Productos");

            migrationBuilder.RenameIndex(
                name: "IX_Existencia_Producto_UndMed_Id",
                table: "Existencias_Productos",
                newName: "IX_Existencias_Productos_UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencia_Producto_TpBod_Id",
                table: "Existencias_Productos",
                newName: "IX_Existencias_Productos_TpBod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencia_Producto_Prod_Id",
                table: "Existencias_Productos",
                newName: "IX_Existencias_Productos_Prod_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Existencias_Productos",
                table: "Existencias_Productos",
                column: "ExProd_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Productos_Prod_Id",
                table: "Existencias_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Tipos_Bodegas_TpBod_Id",
                table: "Existencias_Productos",
                column: "TpBod_Id",
                principalTable: "Tipos_Bodegas",
                principalColumn: "TpBod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Unidades_Medidas_UndMed_Id",
                table: "Existencias_Productos",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Productos_Prod_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Tipos_Bodegas_TpBod_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Unidades_Medidas_UndMed_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Existencias_Productos",
                table: "Existencias_Productos");

            migrationBuilder.RenameTable(
                name: "Existencias_Productos",
                newName: "Existencia_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Existencias_Productos_UndMed_Id",
                table: "Existencia_Producto",
                newName: "IX_Existencia_Producto_UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencias_Productos_TpBod_Id",
                table: "Existencia_Producto",
                newName: "IX_Existencia_Producto_TpBod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencias_Productos_Prod_Id",
                table: "Existencia_Producto",
                newName: "IX_Existencia_Producto_Prod_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Existencia_Producto",
                table: "Existencia_Producto",
                column: "ExProd_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Existencia_Producto_Productos_Prod_Id",
                table: "Existencia_Producto",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencia_Producto_Tipos_Bodegas_TpBod_Id",
                table: "Existencia_Producto",
                column: "TpBod_Id",
                principalTable: "Tipos_Bodegas",
                principalColumn: "TpBod_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencia_Producto_Unidades_Medidas_UndMed_Id",
                table: "Existencia_Producto",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
