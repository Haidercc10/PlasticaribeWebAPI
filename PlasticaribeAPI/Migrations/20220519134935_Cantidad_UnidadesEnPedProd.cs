using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Cantidad_UnidadesEnPedProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PedExtProd_Cantidad",
                table: "PedidosExternos_Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Id",
                table: "PedidosExternos_Productos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExternos_Productos_UndMed_Id",
                table: "PedidosExternos_Productos",
                column: "UndMed_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Unidades_Medidas_UndMed_Id",
                table: "PedidosExternos_Productos",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Unidades_Medidas_UndMed_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_PedidosExternos_Productos_UndMed_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "PedExtProd_Cantidad",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "UndMed_Id",
                table: "PedidosExternos_Productos");
        }
    }
}
