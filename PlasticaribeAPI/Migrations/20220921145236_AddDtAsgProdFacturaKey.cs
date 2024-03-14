using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AddDtAsgProdFacturaKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesDevoluciones_ProductosFacturados",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignacionesProductos_FacturasVentas",
                table: "DetallesAsignacionesProductos_FacturasVentas");

            migrationBuilder.AddColumn<int>(
                name: "DtDevProdFact_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "DtAsigProdFV_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesDevoluciones_ProductosFacturados",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "DtDevProdFact_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignacionesProductos_FacturasVentas",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                column: "DtAsigProdFV_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_DevProdFact_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "DevProdFact_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignacionesProductos_FacturasVentas_AsigProdFV_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                column: "AsigProdFV_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesDevoluciones_ProductosFacturados",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_DevProdFact_Id",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignacionesProductos_FacturasVentas",
                table: "DetallesAsignacionesProductos_FacturasVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesAsignacionesProductos_FacturasVentas_AsigProdFV_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas");

            migrationBuilder.DropColumn(
                name: "DtDevProdFact_Id",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DtAsigProdFV_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesDevoluciones_ProductosFacturados",
                table: "DetallesDevoluciones_ProductosFacturados",
                columns: new[] { "DevProdFact_Id", "Prod_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignacionesProductos_FacturasVentas",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                columns: new[] { "AsigProdFV_Id", "Prod_Id" });
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
