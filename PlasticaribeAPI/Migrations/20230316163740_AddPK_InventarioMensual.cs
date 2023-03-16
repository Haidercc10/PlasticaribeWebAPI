using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPK_InventarioMensual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            Tablas que se editarán: 
             1) DetalleAsignaciones_Tintas
             2) DetallesAsignaciones_MateriasPrimas
             3) DetallesRecuperados_MateriasPrimas
             4) OrdenesCompras_FacturasCompras
             5) PedidosExternos_Productos
             6) Proveedores_MateriasPrimas
             7) Remision_OrdenCompra
             8) Remisiones_FacturasCompras
             9) Tintas_MateriasPrimas
             10) 
             11) Clientes_Productos
            */

            //Eliminacion de llaves foranes
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetalleAsignaciones_Tintas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Tintas_Tinta_Id",
                table: "DetalleAsignaciones_Tintas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetallesAsignaciones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesRecuperados_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Recuperados_MatPrima_RecMp_Id",
                table: "DetallesRecuperados_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "OrdenesCompras_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Ordenes_Compras_Oc_Id",
                table: "OrdenesCompras_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Pedidos_Externos_PedExt_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Productos_Prod_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Proveedores_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Proveedores_Prov_Id",
                table: "Proveedores_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Remision_OrdenCompra_Ordenes_Compras_Oc_Id",
                table: "Remision_OrdenCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_Remision_OrdenCompra_Remisiones_Rem_Id",
                table: "Remision_OrdenCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "Remisiones_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_FacturasCompras_Remisiones_Rem_Id",
                table: "Remisiones_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_Tintas_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Tintas_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tintas_MateriasPrimas_Tintas_Tinta_Id",
                table: "Tintas_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_EntradaTintas_Entradas_Tintas_EntTinta_Id",
                table: "Detalles_EntradaTintas");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_EntradaTintas_Tintas_Tinta_Id",
                table: "Detalles_EntradaTintas");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Productos_Clientes_Cli_Id",
                table: "Clientes_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Productos_Productos_Prod_Id",
                table: "Clientes_Productos");

            //Eliminacion de llave primaria
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleAsignaciones_Tintas",
                table: "DetalleAsignaciones_Tintas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignaciones_MateriasPrimas",
                table: "DetallesAsignaciones_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesRecuperados_MateriasPrimas",
                table: "DetallesRecuperados_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenesCompras_FacturasCompras",
                table: "OrdenesCompras_FacturasCompras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosExternos_Productos",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proveedores_MateriasPrimas",
                table: "Proveedores_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remision_OrdenCompra",
                table: "Remision_OrdenCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remisiones_FacturasCompras",
                table: "Remisiones_FacturasCompras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tintas_MateriasPrimas",
                table: "Tintas_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detalles_EntradaTintas",
                table: "Detalles_EntradaTintas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes_Productos",
                table: "Clientes_Productos");

            //Renombrando columnas
            migrationBuilder.RenameColumn(
                name: "DtEntRolloProd_Codigo",
                table: "DetallesEntradasRollos_Productos",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "DtAsigMPxTinta_Codigo",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                newName: "Codigo");

            //Creacion de columnas que se convertiran en llaves primarias
            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "DetalleAsignaciones_Tintas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "DetallesAsignaciones_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "DetallesRecuperados_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "OrdenesCompras_FacturasCompras",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "PedidosExternos_Productos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Proveedores_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Remision_OrdenCompra",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Remisiones_FacturasCompras",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Tintas_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Detalles_EntradaTintas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Clientes_Productos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "Tinta_Id",
                table: "InventarioInicialXDias_MatPrima",
                type: "bigint",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Enero",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Febrero",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Marzo",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Abril",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Mayo",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Junio",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Julio",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Agosto",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Septiembre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Octubre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Noviembre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Diciembre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            //Creacion de llaves primarias
            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleAsignaciones_Tintas",
                table: "DetalleAsignaciones_Tintas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignaciones_MateriasPrimas",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesRecuperados_MateriasPrimas",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenesCompras_FacturasCompras",
                table: "OrdenesCompras_FacturasCompras",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosExternos_Productos",
                table: "PedidosExternos_Productos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proveedores_MateriasPrimas",
                table: "Proveedores_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remision_OrdenCompra",
                table: "Remision_OrdenCompra",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remisiones_FacturasCompras",
                table: "Remisiones_FacturasCompras",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tintas_MateriasPrimas",
                table: "Tintas_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detalles_EntradaTintas",
                table: "Detalles_EntradaTintas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes_Productos",
                table: "Clientes_Productos",
                column: "Codigo");

            migrationBuilder.CreateTable(
                name: "Inventario_Mensual_Productos",
                columns: table => new
                {
                    Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_Id = table.Column<long>(type: "bigint", nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Enero = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Febrero = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Marzo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Abril = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Mayo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Junio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Julio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Agosto = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Septiembre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Octubre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Noviembre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Diciembre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario_Mensual_Productos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Inventario_Mensual_Productos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_Mensual_Productos_UndMed_Id",
                table: "Inventario_Mensual_Productos",
                column: "UndMed_Id");

            //Creacion de llaves foraneas

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetalleAsignaciones_Tintas",
                column: "AsigMp_Id",
                principalTable: "Asignaciones_MatPrima",
                principalColumn: "AsigMp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Tintas_Tinta_Id",
                table: "DetalleAsignaciones_Tintas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "AsigMp_Id",
                principalTable: "Asignaciones_MatPrima",
                principalColumn: "AsigMp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Recuperados_MatPrima_RecMp_Id",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "RecMp_Id",
                principalTable: "Recuperados_MatPrima",
                principalColumn: "RecMp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "OrdenesCompras_FacturasCompras",
                column: "Facco_Id",
                principalTable: "Facturas_Compras",
                principalColumn: "Facco_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Ordenes_Compras_Oc_Id",
                table: "OrdenesCompras_FacturasCompras",
                column: "Oc_Id",
                principalTable: "Ordenes_Compras",
                principalColumn: "Oc_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Pedidos_Externos_PedExt_Id",
                table: "PedidosExternos_Productos",
                column: "PedExt_Id",
                principalTable: "Pedidos_Externos",
                principalColumn: "PedExt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Productos_Prod_Id",
                table: "PedidosExternos_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Proveedores_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Proveedores_Prov_Id",
                table: "Proveedores_MateriasPrimas",
                column: "Prov_Id",
                principalTable: "Proveedores",
                principalColumn: "Prov_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remision_OrdenCompra_Ordenes_Compras_Oc_Id",
                table: "Remision_OrdenCompra",
                column: "Oc_Id",
                principalTable: "Ordenes_Compras",
                principalColumn: "Oc_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remision_OrdenCompra_Remisiones_Rem_Id",
                table: "Remision_OrdenCompra",
                column: "Rem_Id",
                principalTable: "Remisiones",
                principalColumn: "Rem_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "Remisiones_FacturasCompras",
                column: "Facco_Id",
                principalTable: "Facturas_Compras",
                principalColumn: "Facco_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_FacturasCompras_Remisiones_Rem_Id",
                table: "Remisiones_FacturasCompras",
                column: "Rem_Id",
                principalTable: "Remisiones",
                principalColumn: "Rem_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tintas_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Tintas_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tintas_MateriasPrimas_Tintas_Tinta_Id",
                table: "Tintas_MateriasPrimas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_EntradaTintas_Entradas_Tintas_EntTinta_Id",
                table: "Detalles_EntradaTintas",
                column: "EntTinta_Id",
                principalTable: "Entradas_Tintas",
                principalColumn: "EntTinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_EntradaTintas_Entradas_Tintas_Tinta_Id",
                table: "Detalles_EntradaTintas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Productos_Clientes_Cli_Id",
                table: "Clientes_Productos",
                column: "Cli_Id",
                principalTable: "Clientes",
                principalColumn: "Cli_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Productos_Productos_Prod_Id",
                table: "Clientes_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            Tablas que se editarán: 
             1) DetalleAsignaciones_Tintas
             2) DetallesAsignaciones_MateriasPrimas
             3) DetallesRecuperados_MateriasPrimas
             4) OrdenesCompras_FacturasCompras
             5) PedidosExternos_Productos
             6) Proveedores_MateriasPrimas
             7) Remision_OrdenCompra
             8) Remisiones_FacturasCompras
             9) Tintas_MateriasPrimas
             10) 
             11) Clientes_Productos
            */

            //Eliminacion de llaves foranes
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetalleAsignaciones_Tintas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Tintas_Tinta_Id",
                table: "DetalleAsignaciones_Tintas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetallesAsignaciones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesRecuperados_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Recuperados_MatPrima_RecMp_Id",
                table: "DetallesRecuperados_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "OrdenesCompras_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Ordenes_Compras_Oc_Id",
                table: "OrdenesCompras_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Pedidos_Externos_PedExt_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosExternos_Productos_Productos_Prod_Id",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Proveedores_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Proveedores_Prov_Id",
                table: "Proveedores_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Remision_OrdenCompra_Ordenes_Compras_Oc_Id",
                table: "Remision_OrdenCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_Remision_OrdenCompra_Remisiones_Rem_Id",
                table: "Remision_OrdenCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "Remisiones_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_FacturasCompras_Remisiones_Rem_Id",
                table: "Remisiones_FacturasCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_Tintas_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Tintas_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tintas_MateriasPrimas_Tintas_Tinta_Id",
                table: "Tintas_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_EntradaTintas_Entradas_Tintas_EntTinta_Id",
                table: "Detalles_EntradaTintas");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_EntradaTintas_Tintas_Tinta_Id",
                table: "Detalles_EntradaTintas");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Productos_Clientes_Cli_Id",
                table: "Clientes_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Productos_Productos_Prod_Id",
                table: "Clientes_Productos");

            //Eliminacion de llave primaria
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleAsignaciones_Tintas",
                table: "DetalleAsignaciones_Tintas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignaciones_MateriasPrimas",
                table: "DetallesAsignaciones_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesRecuperados_MateriasPrimas",
                table: "DetallesRecuperados_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenesCompras_FacturasCompras",
                table: "OrdenesCompras_FacturasCompras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidosExternos_Productos",
                table: "PedidosExternos_Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proveedores_MateriasPrimas",
                table: "Proveedores_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remision_OrdenCompra",
                table: "Remision_OrdenCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remisiones_FacturasCompras",
                table: "Remisiones_FacturasCompras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tintas_MateriasPrimas",
                table: "Tintas_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detalles_EntradaTintas",
                table: "Detalles_EntradaTintas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes_Productos",
                table: "Clientes_Productos");

            //Renombrando columnas
            migrationBuilder.RenameColumn(
                name: "DtEntRolloProd_Codigo",
                table: "DetallesEntradasRollos_Productos",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "DtAsigMPxTinta_Codigo",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                newName: "Codigo");

            //Creacion de columnas que se convertiran en llaves primarias
            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "DetalleAsignaciones_Tintas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "DetallesAsignaciones_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "DetallesRecuperados_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "OrdenesCompras_FacturasCompras",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "PedidosExternos_Productos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Proveedores_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Remision_OrdenCompra",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Remisiones_FacturasCompras",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Tintas_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Detalles_EntradaTintas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Codigo",
                table: "Clientes_Productos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "Tinta_Id",
                table: "InventarioInicialXDias_MatPrima",
                type: "bigint",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Enero",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Febrero",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Marzo",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Abril",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Mayo",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Junio",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Julio",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Agosto",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Septiembre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Octubre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Noviembre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Diciembre",
                table: "InventarioInicialXDias_MatPrima",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            //Creacion de llaves primarias
            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleAsignaciones_Tintas",
                table: "DetalleAsignaciones_Tintas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignaciones_MateriasPrimas",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesRecuperados_MateriasPrimas",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenesCompras_FacturasCompras",
                table: "OrdenesCompras_FacturasCompras",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidosExternos_Productos",
                table: "PedidosExternos_Productos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proveedores_MateriasPrimas",
                table: "Proveedores_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remision_OrdenCompra",
                table: "Remision_OrdenCompra",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remisiones_FacturasCompras",
                table: "Remisiones_FacturasCompras",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tintas_MateriasPrimas",
                table: "Tintas_MateriasPrimas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detalles_EntradaTintas",
                table: "Detalles_EntradaTintas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes_Productos",
                table: "Clientes_Productos",
                column: "Codigo");

            migrationBuilder.CreateTable(
                name: "Inventario_Mensual_Productos",
                columns: table => new
                {
                    Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_Id = table.Column<long>(type: "bigint", nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Enero = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Febrero = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Marzo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Abril = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Mayo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Junio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Julio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Agosto = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Septiembre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Octubre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Noviembre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Diciembre = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario_Mensual_Productos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Inventario_Mensual_Productos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_Mensual_Productos_UndMed_Id",
                table: "Inventario_Mensual_Productos",
                column: "UndMed_Id");

            //Creacion de llaves foraneas

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetalleAsignaciones_Tintas",
                column: "AsigMp_Id",
                principalTable: "Asignaciones_MatPrima",
                principalColumn: "AsigMp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleAsignaciones_Tintas_Tintas_Tinta_Id",
                table: "DetalleAsignaciones_Tintas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Asignaciones_MatPrima_AsigMp_Id",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "AsigMp_Id",
                principalTable: "Asignaciones_MatPrima",
                principalColumn: "AsigMp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesRecuperados_MateriasPrimas_Recuperados_MatPrima_RecMp_Id",
                table: "DetallesRecuperados_MateriasPrimas",
                column: "RecMp_Id",
                principalTable: "Recuperados_MatPrima",
                principalColumn: "RecMp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "OrdenesCompras_FacturasCompras",
                column: "Facco_Id",
                principalTable: "Facturas_Compras",
                principalColumn: "Facco_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesCompras_FacturasCompras_Ordenes_Compras_Oc_Id",
                table: "OrdenesCompras_FacturasCompras",
                column: "Oc_Id",
                principalTable: "Ordenes_Compras",
                principalColumn: "Oc_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Pedidos_Externos_PedExt_Id",
                table: "PedidosExternos_Productos",
                column: "PedExt_Id",
                principalTable: "Pedidos_Externos",
                principalColumn: "PedExt_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosExternos_Productos_Productos_Prod_Id",
                table: "PedidosExternos_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Proveedores_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_MateriasPrimas_Proveedores_Prov_Id",
                table: "Proveedores_MateriasPrimas",
                column: "Prov_Id",
                principalTable: "Proveedores",
                principalColumn: "Prov_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remision_OrdenCompra_Ordenes_Compras_Oc_Id",
                table: "Remision_OrdenCompra",
                column: "Oc_Id",
                principalTable: "Ordenes_Compras",
                principalColumn: "Oc_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remision_OrdenCompra_Remisiones_Rem_Id",
                table: "Remision_OrdenCompra",
                column: "Rem_Id",
                principalTable: "Remisiones",
                principalColumn: "Rem_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_FacturasCompras_Facturas_Compras_Facco_Id",
                table: "Remisiones_FacturasCompras",
                column: "Facco_Id",
                principalTable: "Facturas_Compras",
                principalColumn: "Facco_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_FacturasCompras_Remisiones_Rem_Id",
                table: "Remisiones_FacturasCompras",
                column: "Rem_Id",
                principalTable: "Remisiones",
                principalColumn: "Rem_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tintas_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Tintas_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tintas_MateriasPrimas_Tintas_Tinta_Id",
                table: "Tintas_MateriasPrimas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_EntradaTintas_Entradas_Tintas_EntTinta_Id",
                table: "Detalles_EntradaTintas",
                column: "EntTinta_Id",
                principalTable: "Entradas_Tintas",
                principalColumn: "EntTinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_EntradaTintas_Entradas_Tintas_Tinta_Id",
                table: "Detalles_EntradaTintas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Productos_Clientes_Cli_Id",
                table: "Clientes_Productos",
                column: "Cli_Id",
                principalTable: "Clientes",
                principalColumn: "Cli_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Productos_Productos_Prod_Id",
                table: "Clientes_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);
        }

    }
}
