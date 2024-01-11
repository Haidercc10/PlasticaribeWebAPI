using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Add_OrdenCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bopp_Generico",
                columns: table => new
                {
                    BoppGen_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoppGen_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    BoppGen_Ancho = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BoppGen_Micra = table.Column<long>(type: "bigint", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bopp_Generico", x => x.BoppGen_Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes_Compras",
                columns: table => new
                {
                    Oc_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Oc_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Oc_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Prov_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Oc_ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Oc_PesoTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TpDoc_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Oc_Observacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes_Compras", x => x.Oc_Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Compras_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Compras_Proveedores_Prov_Id",
                        column: x => x.Prov_Id,
                        principalTable: "Proveedores",
                        principalColumn: "Prov_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Compras_Tipos_Documentos_TpDoc_Id",
                        column: x => x.TpDoc_Id,
                        principalTable: "Tipos_Documentos",
                        principalColumn: "TpDoc_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Compras_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_OrdenesCompras",
                columns: table => new
                {
                    Doc_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oc_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    BOPP_Id = table.Column<long>(type: "bigint", nullable: false),
                    Doc_CantidadPedida = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Doc_PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_OrdenesCompras", x => x.Doc_Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenesCompras_Bopp_Generico_BOPP_Id",
                        column: x => x.BOPP_Id,
                        principalTable: "Bopp_Generico",
                        principalColumn: "BoppGen_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenesCompras_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenesCompras_Ordenes_Compras_Oc_Id",
                        column: x => x.Oc_Id,
                        principalTable: "Ordenes_Compras",
                        principalColumn: "Oc_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenesCompras_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_OrdenesCompras_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesCompras_FacturasCompras",
                columns: table => new
                {
                    Oc_Id = table.Column<long>(type: "bigint", nullable: false),
                    Facco_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesCompras_FacturasCompras", x => new { x.Oc_Id, x.Facco_Id });
                    table.ForeignKey(
                        name: "FK_OrdenesCompras_FacturasCompras_Facturas_Compras_Facco_Id",
                        column: x => x.Facco_Id,
                        principalTable: "Facturas_Compras",
                        principalColumn: "Facco_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesCompras_FacturasCompras_Ordenes_Compras_Oc_Id",
                        column: x => x.Oc_Id,
                        principalTable: "Ordenes_Compras",
                        principalColumn: "Oc_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenesCompras_BOPP_Id",
                table: "Detalles_OrdenesCompras",
                column: "BOPP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenesCompras_MatPri_Id",
                table: "Detalles_OrdenesCompras",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenesCompras_Oc_Id",
                table: "Detalles_OrdenesCompras",
                column: "Oc_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenesCompras_Tinta_Id",
                table: "Detalles_OrdenesCompras",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenesCompras_UndMed_Id",
                table: "Detalles_OrdenesCompras",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compras_Estado_Id",
                table: "Ordenes_Compras",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compras_Prov_Id",
                table: "Ordenes_Compras",
                column: "Prov_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compras_TpDoc_Id",
                table: "Ordenes_Compras",
                column: "TpDoc_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Compras_Usua_Id",
                table: "Ordenes_Compras",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesCompras_FacturasCompras_Facco_Id",
                table: "OrdenesCompras_FacturasCompras",
                column: "Facco_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_OrdenesCompras");

            migrationBuilder.DropTable(
                name: "OrdenesCompras_FacturasCompras");

            migrationBuilder.DropTable(
                name: "Bopp_Generico");

            migrationBuilder.DropTable(
                name: "Ordenes_Compras");
        }
    }
}
