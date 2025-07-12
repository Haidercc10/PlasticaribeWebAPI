using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FieldsDvs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Asesor_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevProdFact_Responsable",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DtDevProdFact_Factura",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DtDevProdFact_OT",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DtDevProdFact_PesoBruto",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DtDevProdFact_PesoNeto",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Asesor_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Asesor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "UsuaFinaliza_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_Asesor_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Asesor_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "UsuaFinaliza_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_Asesor_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_Asesor_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Asesor_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_Responsable",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DtDevProdFact_Factura",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DtDevProdFact_OT",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DtDevProdFact_PesoBruto",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DtDevProdFact_PesoNeto",
                table: "DetallesDevoluciones_ProductosFacturados");
        }
    }
}
