using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fechas_InicioFin_Produccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Fin_Produccion",
                table: "Certificados_Calidad",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Inicio_Produccion",
                table: "Certificados_Calidad",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha_Fin_Produccion",
                table: "Certificados_Calidad");

            migrationBuilder.DropColumn(
                name: "Fecha_Inicio_Produccion",
                table: "Certificados_Calidad");
        }
    }
}
