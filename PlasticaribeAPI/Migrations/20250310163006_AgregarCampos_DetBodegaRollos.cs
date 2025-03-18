using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampos_DetBodegaRollos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DtBgRollo_FechaFab",
                table: "Detalles_BodegasRollos",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DtBgRollo_Maq",
                table: "Detalles_BodegasRollos",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtBgRollo_FechaFab",
                table: "Detalles_BodegasRollos");

            migrationBuilder.DropColumn(
                name: "DtBgRollo_Maq",
                table: "Detalles_BodegasRollos");
        }
    }
}
