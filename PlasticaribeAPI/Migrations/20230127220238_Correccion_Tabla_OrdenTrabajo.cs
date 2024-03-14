﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CorreccionTablaOrdenTrabajo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corte",
                table: "Orden_Trabajo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Corte",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
