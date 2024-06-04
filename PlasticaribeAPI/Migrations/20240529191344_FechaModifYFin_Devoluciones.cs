using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FechaModifYFin_Devoluciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DevProdFact_FechaFinalizado",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DevProdFact_FechaModificado",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevProdFact_HoraFinalizado",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevProdFact_HoraModificado",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevProdFact_FechaFinalizado",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_FechaModificado",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_HoraFinalizado",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_HoraModificado",
                table: "Devoluciones_ProductosFacturados");
        }
    }
}
