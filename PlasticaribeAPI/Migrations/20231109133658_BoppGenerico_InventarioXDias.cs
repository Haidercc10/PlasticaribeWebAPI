﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class BoppGenerico_InventarioXDias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Bopp_Id",
                table: "InventarioInicialXDias_MatPrima",
                type: "bigint",
                nullable: true, 
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bopp_Id",
                table: "InventarioInicialXDias_MatPrima");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
