using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Field_WeightInPlanillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pla_ValorFactura",
                table: "Detalles_PlanillaDespacho",
                newName: "DtPla_ValorFactura");

            migrationBuilder.AddColumn<decimal>(
                name: "Pla_PesoTotal",
                table: "Planillas_Despacho",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DtPla_PesoBruto",
                table: "Detalles_PlanillaDespacho",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pla_PesoTotal",
                table: "Planillas_Despacho");

            migrationBuilder.DropColumn(
                name: "DtPla_PesoBruto",
                table: "Detalles_PlanillaDespacho");

            migrationBuilder.RenameColumn(
                name: "DtPla_ValorFactura",
                table: "Detalles_PlanillaDespacho",
                newName: "Pla_ValorFactura");
        }
    }
}
