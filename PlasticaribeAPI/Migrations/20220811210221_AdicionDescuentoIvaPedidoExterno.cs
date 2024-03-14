using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AdicionDescuentoIvaPedidoExterno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedExt_Descuento",
                table: "Pedidos_Externos",
                type: "int",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PedExt_Iva",
                table: "Pedidos_Externos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedExt_Descuento",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "PedExt_Iva",
                table: "Pedidos_Externos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
