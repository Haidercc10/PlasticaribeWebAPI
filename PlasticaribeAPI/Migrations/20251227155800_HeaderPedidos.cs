using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class HeaderPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedExt_Descuento",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "PedExt_Iva",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "PedExt_PrecioTotalFinal",
                table: "Pedidos_Externos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PedExt_FechaEntrega",
                table: "Pedidos_Externos",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PedExt_FechaCreacion",
                table: "Pedidos_Externos",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "PedExt_DireccionEntrega",
                table: "Pedidos_Externos",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PedExt_FechaEntrega",
                table: "Pedidos_Externos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PedExt_FechaCreacion",
                table: "Pedidos_Externos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "PedExt_DireccionEntrega",
                table: "Pedidos_Externos",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "PedExt_Descuento",
                table: "Pedidos_Externos",
                type: "decimal(18,2)",
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

            migrationBuilder.AddColumn<decimal>(
                name: "PedExt_PrecioTotalFinal",
                table: "Pedidos_Externos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
