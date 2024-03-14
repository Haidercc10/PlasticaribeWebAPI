﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Adicion_OT_EntradasSalidasMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Orden_Trabajo",
                table: "Entradas_Salidas_MP",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden_Trabajo",
                table: "Entradas_Salidas_MP");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
