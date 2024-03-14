using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Despacho4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsignacionesProductos_FacturasVentas",
                columns: table => new
                {
                    AsigProdFV_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaVta_Id = table.Column<string>(type: "varchar(100)", nullable: true),
                    NotaCredito_Id = table.Column<string>(type: "varchar(100)", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    AsigProdFV_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    AsigProdFV_Observacion = table.Column<string>(type: "text", nullable: true),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usua_Conductor = table.Column<long>(type: "bigint", nullable: false),
                    AsigProdFV_PlacaCamion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionesProductos_FacturasVentas", x => x.AsigProdFV_Id);
                    table.ForeignKey(
                        name: "FK_AsignacionesProductos_FacturasVentas_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AsignacionesProductos_FacturasVentas_Usuarios_Usua_Conductor",
                        column: x => x.Usua_Conductor,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AsignacionesProductos_FacturasVentas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntradasRollos_Productos",
                columns: table => new
                {
                    EntRolloProd_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntRolloProd_OT = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    EntRolloProd_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    EntRolloProd_Observacion = table.Column<string>(type: "text", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradasRollos_Productos", x => x.EntRolloProd_Id);
                    table.ForeignKey(
                        name: "FK_EntradasRollos_Productos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntradasRollos_Productos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntradasRollos_Productos_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposDevoluciones_ProductosFacturados",
                columns: table => new
                {
                    TipoDevProdFact_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDevProdFact_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TipoDevProdFact_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDevoluciones_ProductosFacturados", x => x.TipoDevProdFact_Id);
                });

            migrationBuilder.CreateTable(
                name: "DetallesAsignacionesProductos_FacturasVentas",
                columns: table => new
                {
                    AsigProdFV_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    DtAsigProdFV_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAsignacionesProductos_FacturasVentas", x => new { x.AsigProdFV_Id, x.Prod_Id });
                    table.ForeignKey(
                        name: "FK_DetallesAsignacionesProductos_FacturasVentas_AsignacionesProductos_FacturasVentas_AsigProdFV_Id",
                        column: x => x.AsigProdFV_Id,
                        principalTable: "AsignacionesProductos_FacturasVentas",
                        principalColumn: "AsigProdFV_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignacionesProductos_FacturasVentas_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesAsignacionesProductos_FacturasVentas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesEntradasRollos_Productos",
                columns: table => new
                {
                    DtEntRolloProd_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntRolloProd_Id = table.Column<long>(type: "bigint", nullable: false),
                    Rollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtEntRolloProd_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesEntradasRollos_Productos", x => x.DtEntRolloProd_Codigo);
                    table.ForeignKey(
                        name: "FK_DetallesEntradasRollos_Productos_EntradasRollos_Productos_EntRolloProd_Id",
                        column: x => x.EntRolloProd_Id,
                        principalTable: "EntradasRollos_Productos",
                        principalColumn: "EntRolloProd_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesEntradasRollos_Productos_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesEntradasRollos_Productos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones_ProductosFacturados",
                columns: table => new
                {
                    DevProdFact_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaVta_Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    DevProdFact_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    DevProdFact_Observacion = table.Column<string>(type: "text", nullable: true),
                    TipoDevProdFact_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoluciones_ProductosFacturados", x => x.DevProdFact_Id);
                    table.ForeignKey(
                        name: "FK_Devoluciones_ProductosFacturados_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devoluciones_ProductosFacturados_TiposDevoluciones_ProductosFacturados_TipoDevProdFact_Id",
                        column: x => x.TipoDevProdFact_Id,
                        principalTable: "TiposDevoluciones_ProductosFacturados",
                        principalColumn: "TipoDevProdFact_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesDevoluciones_ProductosFacturados",
                columns: table => new
                {
                    DevProdFact_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    DtDevProdFact_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesDevoluciones_ProductosFacturados", x => new { x.DevProdFact_Id, x.Prod_Id });
                    table.ForeignKey(
                        name: "FK_DetallesDevoluciones_ProductosFacturados_Devoluciones_ProductosFacturados_DevProdFact_Id",
                        column: x => x.DevProdFact_Id,
                        principalTable: "Devoluciones_ProductosFacturados",
                        principalColumn: "DevProdFact_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesDevoluciones_ProductosFacturados_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesDevoluciones_ProductosFacturados_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesProductos_FacturasVentas_Cli_Id",
                table: "AsignacionesProductos_FacturasVentas",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesProductos_FacturasVentas_Usua_Conductor",
                table: "AsignacionesProductos_FacturasVentas",
                column: "Usua_Conductor");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesProductos_FacturasVentas_Usua_Id",
                table: "AsignacionesProductos_FacturasVentas",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignacionesProductos_FacturasVentas_Prod_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignacionesProductos_FacturasVentas_UndMed_Id",
                table: "DetallesAsignacionesProductos_FacturasVentas",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_Prod_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_ProductosFacturados_UndMed_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesEntradasRollos_Productos_EntRolloProd_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "EntRolloProd_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesEntradasRollos_Productos_Estado_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesEntradasRollos_Productos_UndMed_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Cli_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_TipoDevProdFact_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "TipoDevProdFact_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasRollos_Productos_Prod_Id",
                table: "EntradasRollos_Productos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasRollos_Productos_UndMed_Id",
                table: "EntradasRollos_Productos",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasRollos_Productos_Usua_Id",
                table: "EntradasRollos_Productos",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAsignacionesProductos_FacturasVentas");

            migrationBuilder.DropTable(
                name: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.DropTable(
                name: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropTable(
                name: "AsignacionesProductos_FacturasVentas");

            migrationBuilder.DropTable(
                name: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropTable(
                name: "EntradasRollos_Productos");

            migrationBuilder.DropTable(
                name: "TiposDevoluciones_ProductosFacturados");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
