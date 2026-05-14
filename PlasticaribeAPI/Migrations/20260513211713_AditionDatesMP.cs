using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AditionDatesMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Doc_FechaEntrega",
                table: "Detalles_OrdenesCompras",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AsigMPxTinta_FechaRealEntrega",
                table: "Asignaciones_MatPrimasXTintas",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AsigMp_FechaRealEntrega",
                table: "Asignaciones_MatPrima",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AsigBOPP_FechaRealEntrega",
                table: "Asignaciones_BOPP",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doc_FechaEntrega",
                table: "Detalles_OrdenesCompras");

            migrationBuilder.DropColumn(
                name: "AsigMPxTinta_FechaRealEntrega",
                table: "Asignaciones_MatPrimasXTintas");

            migrationBuilder.DropColumn(
                name: "AsigMp_FechaRealEntrega",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "AsigBOPP_FechaRealEntrega",
                table: "Asignaciones_BOPP");
        }
    }
}
