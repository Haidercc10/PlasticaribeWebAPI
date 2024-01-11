using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Certificados_Calidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificados_Calidad",
                columns: table => new
                {
                    Consecutivo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orden_Trabajo = table.Column<long>(type: "bigint", nullable: false),
                    Cliente = table.Column<string>(type: "varchar(max)", nullable: false),
                    Item = table.Column<int>(type: "int", nullable: false),
                    Referencia = table.Column<string>(type: "varchar(max)", nullable: false),
                    Cantidad_Producir = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Presentacion_Producto = table.Column<string>(type: "varchar(10)", nullable: false),
                    Fecha_Orden = table.Column<DateTime>(type: "date", nullable: false),
                    Unidad_Calibre = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nominal_Calibre = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tolerancia_Calibre = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Minimo_Calibre = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Maximo_Calibre = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unidad_AnchoFrente = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nominal_AnchoFrente = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tolerancia_AnchoFrente = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Minimo_AnchoFrente = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Maximo_AnchoFrente = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unidad_AnchoFuelle = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nominal_AnchoFuelle = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tolerancia_AnchoFuelle = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Minimo_AnchoFuelle = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Maximo_AnchoFuelle = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unidad_LargoRepeticion = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nominal_LargoRepeticion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tolerancia_LargoRepeticion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Minimo_LargoRepeticion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Maximo_LargoRepeticion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unidad_Cof = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nominal_Cof = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tolerancia_Cof = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Minimo_Cof = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Maximo_Cof = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Material = table.Column<string>(type: "varchar(max)", nullable: false),
                    Resistencia = table.Column<string>(type: "varchar(max)", nullable: false),
                    Sellabilidad = table.Column<string>(type: "varchar(max)", nullable: false),
                    Transparencia = table.Column<string>(type: "varchar(max)", nullable: false),
                    Tratado = table.Column<string>(type: "varchar(max)", nullable: false),
                    Impresion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Fecha_Registro = table.Column<DateTime>(type: "date", nullable: false),
                    Hora_Registro = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados_Calidad", x => x.Consecutivo);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Productos_Item",
                        column: x => x.Item,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Unidades_Medidas_Presentacion_Producto",
                        column: x => x.Presentacion_Producto,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Unidades_Medidas_Unidad_AnchoFrente",
                        column: x => x.Unidad_AnchoFrente,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Unidades_Medidas_Unidad_AnchoFuelle",
                        column: x => x.Unidad_AnchoFuelle,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Unidades_Medidas_Unidad_Calibre",
                        column: x => x.Unidad_Calibre,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Unidades_Medidas_Unidad_Cof",
                        column: x => x.Unidad_Cof,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Unidades_Medidas_Unidad_LargoRepeticion",
                        column: x => x.Unidad_LargoRepeticion,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Certificados_Calidad_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Item",
                table: "Certificados_Calidad",
                column: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Presentacion_Producto",
                table: "Certificados_Calidad",
                column: "Presentacion_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Unidad_AnchoFrente",
                table: "Certificados_Calidad",
                column: "Unidad_AnchoFrente");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Unidad_AnchoFuelle",
                table: "Certificados_Calidad",
                column: "Unidad_AnchoFuelle");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Unidad_Calibre",
                table: "Certificados_Calidad",
                column: "Unidad_Calibre");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Unidad_Cof",
                table: "Certificados_Calidad",
                column: "Unidad_Cof");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Unidad_LargoRepeticion",
                table: "Certificados_Calidad",
                column: "Unidad_LargoRepeticion");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_Calidad_Usua_Id",
                table: "Certificados_Calidad",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificados_Calidad");
        }
    }
}
