using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposEnSolicitudesYDetalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SolMpExt_FechaEstimadaEntrega",
                table: "Solicitud_MatPrimaExtrusion",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DtSolMpExt_CantidadEntregada",
                table: "DetSolicitud_MatPrimaExtrusion",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true, 
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DtSolMpExt_CantidadFaltante",
                table: "DetSolicitud_MatPrimaExtrusion",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolMpExt_FechaEstimadaEntrega",
                table: "Solicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "DtSolMpExt_CantidadEntregada",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "DtSolMpExt_CantidadFaltante",
                table: "DetSolicitud_MatPrimaExtrusion");
        }
    }
}
