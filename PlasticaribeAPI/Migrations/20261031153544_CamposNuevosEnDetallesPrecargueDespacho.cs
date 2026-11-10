using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CamposNuevosEnDetallesPrecargueDespacho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DtlPcd_OT",
                table: "Detalles_PrecargueDespacho",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DtlPcd_PesoBruto",
                table: "Detalles_PrecargueDespacho",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DtlPcd_PesoNeto",
                table: "Detalles_PrecargueDespacho",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtlPcd_Referencia",
                table: "Detalles_PrecargueDespacho",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtlPcd_Ubicacion",
                table: "Detalles_PrecargueDespacho",
                type: "varchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtlPcd_OT",
                table: "Detalles_PrecargueDespacho");

            migrationBuilder.DropColumn(
                name: "DtlPcd_PesoBruto",
                table: "Detalles_PrecargueDespacho");

            migrationBuilder.DropColumn(
                name: "DtlPcd_PesoNeto",
                table: "Detalles_PrecargueDespacho");

            migrationBuilder.DropColumn(
                name: "DtlPcd_Referencia",
                table: "Detalles_PrecargueDespacho");

            migrationBuilder.DropColumn(
                name: "DtlPcd_Ubicacion",
                table: "Detalles_PrecargueDespacho");
        }
    }
}
