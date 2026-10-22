using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CamposNuevos_OrdenesTrabajo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_Calibre",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SelladoCorte_Solapa",
                table: "OT_Sellado_Corte",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Laminado_Observacion",
                table: "OT_Laminado",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impresion_Observacion",
                table: "OT_Impresion",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Extrusion_Grafilado",
                table: "OT_Extrusion",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extrusion_Observacion",
                table: "OT_Extrusion",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ot_FechaEntrega",
                table: "Orden_Trabajo",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelladoCorte_Calibre",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "SelladoCorte_Solapa",
                table: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "Laminado_Observacion",
                table: "OT_Laminado");

            migrationBuilder.DropColumn(
                name: "Impresion_Observacion",
                table: "OT_Impresion");

            migrationBuilder.DropColumn(
                name: "Extrusion_Grafilado",
                table: "OT_Extrusion");

            migrationBuilder.DropColumn(
                name: "Extrusion_Observacion",
                table: "OT_Extrusion");

            migrationBuilder.DropColumn(
                name: "Ot_FechaEntrega",
                table: "Orden_Trabajo");
        }
    }
}
