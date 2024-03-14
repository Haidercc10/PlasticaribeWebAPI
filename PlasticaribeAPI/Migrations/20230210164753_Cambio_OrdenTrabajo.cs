﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CambioOrdenTrabajo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MovApp_Nombre",
                table: "MovimientosAplicacion",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MovApp_Nombre",
                table: "MovimientosAplicacion",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
