using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cumplimiento_Facturacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cumplimiento_Facturacion",
                columns: table => new
                {
                    Cufa_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cufa_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Cufa_FacturadoDia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cufa_MetaDia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cufa_FacturadoMes = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Cufa_MetaMes = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Cufa_FacturadoAnual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Cufa_MetaAnual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Cufa_PorcentajeDia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, computedColumnSql: "CASE WHEN Cufa_MetaDia > 0 THEN (Cufa_FacturadoDia / Cufa_MetaDia) * 100 ELSE 0 END", stored: true),
                    Cufa_PorcentajeMes = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, computedColumnSql: "CASE WHEN Cufa_MetaMes > 0 THEN (Cufa_FacturadoMes / Cufa_MetaMes) * 100 ELSE 0 END", stored: true),
                    Cufa_PorcentajeAnual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, computedColumnSql: "CASE WHEN Cufa_MetaAnual > 0 THEN (Cufa_FacturadoAnual / Cufa_MetaAnual) * 100 ELSE 0 END", stored: true),
                    Cufa_FechaRegistro = table.Column<DateTime>(type: "date", nullable: false),
                    Cufa_HoraRegistro = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cumplimiento_Facturacion", x => x.Cufa_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cumplimiento_Facturacion");
        }
    }
}
