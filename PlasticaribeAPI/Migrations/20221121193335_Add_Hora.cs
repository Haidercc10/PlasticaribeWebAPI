using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Add_Hora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Usua_Fecha",
                table: "Usuarios",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usua_Hora",
                table: "Usuarios",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Tinta_FechaIngreso",
                table: "Tintas",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tinta_Hora",
                table: "Tintas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SedeCli_Fecha",
                table: "Sedes_Clientes",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SedeCli_Hora",
                table: "Sedes_Clientes",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prod_Hora",
                table: "Proveedores",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Prov_Fecha",
                table: "Proveedores",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Prod_Fecha",
                table: "Productos",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prod_Hora",
                table: "Productos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreEntRollo_Hora",
                table: "PreEntrega_RollosDespacho",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PedExt_HoraCreacion",
                table: "Pedidos_Externos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ot_Hora",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MatPri_Fecha",
                table: "Materias_Primas",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatPri_Hora",
                table: "Materias_Primas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fPen_Fecha",
                table: "FondosPensiones",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fPen_Hora",
                table: "FondosPensiones",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Creacion",
                table: "Fallas_Tecnicas",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hora_Creacion",
                table: "Fallas_Tecnicas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExProd_Fecha",
                table: "Existencias_Productos",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExProd_Hora",
                table: "Existencias_Productos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "eps_Fecha",
                table: "EPS",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "eps_Hora",
                table: "EPS",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntRolloProd_Hora",
                table: "EntradasRollos_Productos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "entTinta_Hora",
                table: "Entradas_Tintas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Empresa_Fecha",
                table: "Empresas",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Empresa_Hora",
                table: "Empresas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevProdFact_Hora",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Cli_Fecha",
                table: "Clientes",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cli_Hora",
                table: "Clientes",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BoppGen_FechaIngreso",
                table: "Bopp_Generico",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoppGen_Hora",
                table: "Bopp_Generico",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BOPP_Hora",
                table: "BOPP",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsigProdFV_Hora",
                table: "AsignacionesProductos_FacturasVentas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsigProdFV_HoraEnvio",
                table: "AsignacionesProductos_FacturasVentas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsigMPxTinta_Hora",
                table: "Asignaciones_MatPrimasXTintas",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsigMp_Hora",
                table: "Asignaciones_MatPrima",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsigBOPP_Hora",
                table: "Asignaciones_BOPP",
                type: "varchar(10)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usua_Fecha",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Usua_Hora",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Tinta_FechaIngreso",
                table: "Tintas");

            migrationBuilder.DropColumn(
                name: "Tinta_Hora",
                table: "Tintas");

            migrationBuilder.DropColumn(
                name: "SedeCli_Fecha",
                table: "Sedes_Clientes");

            migrationBuilder.DropColumn(
                name: "SedeCli_Hora",
                table: "Sedes_Clientes");

            migrationBuilder.DropColumn(
                name: "Prod_Hora",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Prov_Fecha",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Prod_Fecha",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Hora",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PreEntRollo_Hora",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "PedExt_HoraCreacion",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "Ot_Hora",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "MatPri_Fecha",
                table: "Materias_Primas");

            migrationBuilder.DropColumn(
                name: "MatPri_Hora",
                table: "Materias_Primas");

            migrationBuilder.DropColumn(
                name: "fPen_Fecha",
                table: "FondosPensiones");

            migrationBuilder.DropColumn(
                name: "fPen_Hora",
                table: "FondosPensiones");

            migrationBuilder.DropColumn(
                name: "Fecha_Creacion",
                table: "Fallas_Tecnicas");

            migrationBuilder.DropColumn(
                name: "Hora_Creacion",
                table: "Fallas_Tecnicas");

            migrationBuilder.DropColumn(
                name: "ExProd_Fecha",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "ExProd_Hora",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "eps_Fecha",
                table: "EPS");

            migrationBuilder.DropColumn(
                name: "eps_Hora",
                table: "EPS");

            migrationBuilder.DropColumn(
                name: "EntRolloProd_Hora",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "entTinta_Hora",
                table: "Entradas_Tintas");

            migrationBuilder.DropColumn(
                name: "Empresa_Fecha",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Empresa_Hora",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "DevProdFact_Hora",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevMatPri_Hora",
                table: "Devoluciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "Cli_Fecha",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Cli_Hora",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "BoppGen_FechaIngreso",
                table: "Bopp_Generico");

            migrationBuilder.DropColumn(
                name: "BoppGen_Hora",
                table: "Bopp_Generico");

            migrationBuilder.DropColumn(
                name: "BOPP_Hora",
                table: "BOPP");

            migrationBuilder.DropColumn(
                name: "AsigProdFV_Hora",
                table: "AsignacionesProductos_FacturasVentas");

            migrationBuilder.DropColumn(
                name: "AsigProdFV_HoraEnvio",
                table: "AsignacionesProductos_FacturasVentas");

            migrationBuilder.DropColumn(
                name: "AsigMPxTinta_Hora",
                table: "Asignaciones_MatPrimasXTintas");

            migrationBuilder.DropColumn(
                name: "AsigMp_Hora",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "AsigBOPP_Hora",
                table: "Asignaciones_BOPP");
        }
    }
}
