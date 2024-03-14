﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AdicionPrecioFinalPedidoExterno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PedExt_PrecioTotalFinal",
                table: "Pedidos_Externos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedExt_PrecioTotalFinal",
                table: "Pedidos_Externos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
