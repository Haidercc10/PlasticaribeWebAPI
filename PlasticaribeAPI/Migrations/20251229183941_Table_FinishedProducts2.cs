using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Table_FinishedProducts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Terminado_Tipos_Impresion_TpSellado_Id",
                table: "Producto_Terminado");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_TpImpresion_Id",
                table: "Producto_Terminado",
                column: "TpImpresion_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Terminado_Tipos_Impresion_TpImpresion_Id",
                table: "Producto_Terminado",
                column: "TpImpresion_Id",
                principalTable: "Tipos_Impresion",
                principalColumn: "TpImpresion_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Terminado_Tipos_Impresion_TpImpresion_Id",
                table: "Producto_Terminado");

            migrationBuilder.DropIndex(
                name: "IX_Producto_Terminado_TpImpresion_Id",
                table: "Producto_Terminado");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Terminado_Tipos_Impresion_TpSellado_Id",
                table: "Producto_Terminado",
                column: "TpSellado_Id",
                principalTable: "Tipos_Impresion",
                principalColumn: "TpImpresion_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
