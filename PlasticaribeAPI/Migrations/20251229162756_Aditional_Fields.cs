using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Aditional_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PedExtProd_Referencia",
                table: "PedidosExternos_Productos",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AddColumn<bool>(
                name: "ImpresionDobleCara",
                table: "PedidosExternos_Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Material_Id",
                table: "PedidosExternos_Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PedExtProd_Calibre",
                table: "PedidosExternos_Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "PedExtProd_Impresion",
                table: "PedidosExternos_Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PedExtProd_NroEmbobinado",
                table: "PedidosExternos_Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pigmt_Id",
                table: "PedidosExternos_Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExternos_Productos_Material_Id",
                table: "PedidosExternos_Productos",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExternos_Productos_Pigmt_Id",
                table: "PedidosExternos_Productos",
                column: "Pigmt_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Materiales_MatPrima_Material_Id",
                table: "PedidosExternos_Productos",
                column: "Material_Id",
                principalTable: "Materiales_MatPrima",
                principalColumn: "Material_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Pigmentos_Pigmt_Id",
                table: "PedidosExternos_Productos",
                column: "Pigmt_Id",
                principalTable: "Pigmentos",
                principalColumn: "Pigmt_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Materiales_MatPrima_Material_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Pigmentos_Pigmt_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_PedidosExternos_Productos_Material_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_PedidosExternos_Productos_Pigmt_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "ImpresionDobleCara",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "Material_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "PedExtProd_Calibre",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "PedExtProd_Impresion",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "PedExtProd_NroEmbobinado",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "Pigmt_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.AlterColumn<string>(
                name: "PedExtProd_Referencia",
                table: "PedidosExternos_Productos",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);
        }
    }
}
