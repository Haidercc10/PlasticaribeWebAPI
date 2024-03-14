﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Iva_OrdenCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IVA",
                table: "Ordenes_Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Ordenes_Compras");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
