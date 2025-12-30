using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Details_Sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PedExtProd_PrecioUnitario",
                table: "PedidosExternos_Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "PedidosExternos_Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PedExtProd_OT",
                table: "PedidosExternos_Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PedExtProd_Observacion",
                table: "PedidosExternos_Productos",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PedExtProd_Referencia",
                table: "PedidosExternos_Productos",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExternos_Productos_Estado_Id",
                table: "PedidosExternos_Productos",
                column: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Estados_Estado_Id",
                table: "PedidosExternos_Productos",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Estados_Estado_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_PedidosExternos_Productos_Estado_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "PedExtProd_OT",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "PedExtProd_Observacion",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropColumn(
                name: "PedExtProd_Referencia",
                table: "PedidosExternos_Productos");

            migrationBuilder.AlterColumn<decimal>(
                name: "PedExtProd_PrecioUnitario",
                table: "PedidosExternos_Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);
        }
    }
}
