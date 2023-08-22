using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Controles_Calidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlCalidad_CorteDoblado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Fecha_Registro = table.Column<DateTime>(type: "date", nullable: false),
                    Hora_Resgitros = table.Column<string>(type: "varchar(10)", nullable: false),
                    Turno_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Maquina = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ronda = table.Column<string>(type: "varchar(50)", nullable: false),
                    Orden_Trabajo = table.Column<long>(type: "bigint", nullable: false),
                    Cliente = table.Column<string>(type: "varchar(max)", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Nombre_Producto = table.Column<string>(type: "varchar(max)", nullable: false),
                    Ancho = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Calibre = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Codigo_Barras = table.Column<string>(type: "varchar(max)", nullable: false),
                    Tipo_Embobinado = table.Column<string>(type: "varchar(100)", nullable: false),
                    PasoEntre_Guia = table.Column<string>(type: "varchar(100)", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlCalidad_CorteDoblado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_CorteDoblado_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_CorteDoblado_Turnos_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turnos",
                        principalColumn: "Turno_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_CorteDoblado_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_CorteDoblado_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlCalidad_Extrusion",
                columns: table => new
                {
                    CcExt_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turno_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    CcExt_Maquina = table.Column<int>(type: "int", nullable: false),
                    CcExt_Ronda = table.Column<int>(type: "int", nullable: false),
                    CcExt_OT = table.Column<long>(type: "bigint", nullable: false),
                    CcExt_Cliente = table.Column<string>(type: "varchar(max)", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: true),
                    Referencia = table.Column<string>(type: "varchar(max)", nullable: false),
                    CcExt_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    Pigmento_Id = table.Column<int>(type: "int", nullable: false),
                    CcExt_AnchoTubular = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcExt_PesoMetro = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcExt_Ancho = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    CcExt_CalibreMax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcExt_CalibreMin = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcExt_CalibreProm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcExt_Apariencia = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcExt_Tratado = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcExt_Rasgado = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcExt_TipoBobina = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcExt_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    CcExt_Hora = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcExt_Observacion = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlCalidad_Extrusion", x => x.CcExt_Id);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Extrusion_Pigmentos_Pigmento_Id",
                        column: x => x.Pigmento_Id,
                        principalTable: "Pigmentos",
                        principalColumn: "Pigmt_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Extrusion_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Extrusion_Turnos_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turnos",
                        principalColumn: "Turno_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Extrusion_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Extrusion_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlCalidad_Impresion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Fecha_Registro = table.Column<DateTime>(type: "date", nullable: false),
                    Hora_Registro = table.Column<string>(type: "varchar(10)", nullable: false),
                    Turno_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Maquina = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ronda = table.Column<string>(type: "varchar(50)", nullable: false),
                    Orden_Trabajo = table.Column<long>(type: "bigint", nullable: false),
                    Cliente = table.Column<string>(type: "varchar(max)", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Nombre_Producto = table.Column<string>(type: "varchar(max)", nullable: false),
                    LoteRollo_SinImpresion = table.Column<string>(type: "varchar(100)", nullable: false),
                    Prueba_Tratado = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ancho_Rollo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Secuencia_Cian = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Magenta = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Amarillo = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Negro = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Base = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Pantone1 = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Pantone2 = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Pantone3 = table.Column<string>(type: "varchar(max)", nullable: false),
                    Secuencia_Pantone4 = table.Column<string>(type: "varchar(max)", nullable: false),
                    Tipo_Embobinado = table.Column<string>(type: "varchar(max)", nullable: false),
                    Codigo_Barras = table.Column<string>(type: "varchar(max)", nullable: false),
                    Texto = table.Column<string>(type: "varchar(max)", nullable: false),
                    Fotocelda_Izquierda = table.Column<bool>(type: "bit", nullable: false),
                    Fotcelda_Derecha = table.Column<bool>(type: "bit", nullable: false),
                    Registro_Colores = table.Column<string>(type: "varchar(max)", nullable: false),
                    Adherencia_Tinta = table.Column<string>(type: "varchar(max)", nullable: false),
                    Conformidad_Laminado = table.Column<string>(type: "varchar(max)", nullable: false),
                    PasoGuia_Repetecion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlCalidad_Impresion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Impresion_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Impresion_Turnos_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turnos",
                        principalColumn: "Turno_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Impresion_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlCalidad_Sellado",
                columns: table => new
                {
                    CcSel_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turno_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    CcSel_Maquina = table.Column<int>(type: "int", nullable: false),
                    CcSel_Ronda = table.Column<int>(type: "int", nullable: false),
                    CcSel_OT = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: true),
                    Referencia = table.Column<string>(type: "varchar(max)", nullable: false),
                    CcSel_Calibre = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcSel_Ancho = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcSel_Largo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_AL = table.Column<string>(type: "varchar(10)", nullable: false),
                    AnchoFuelle_Izq = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AnchoFuelle_Der = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AnchoFuelle_Abajo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_AF = table.Column<string>(type: "varchar(10)", nullable: false),
                    CcSel_Rasgado = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_PruebaFiltrado = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_PruebaPresion = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_Sellabilidad = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_Impresion = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_Precorte = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_Perforacion = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_CantBolsasxPaq = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CcSel_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    CcSel_Hora = table.Column<string>(type: "varchar(50)", nullable: false),
                    CcSel_Observacion = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlCalidad_Sellado", x => x.CcSel_Id);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Sellado_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Sellado_Turnos_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turnos",
                        principalColumn: "Turno_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Sellado_Unidades_Medidas_UndMed_AF",
                        column: x => x.UndMed_AF,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Sellado_Unidades_Medidas_UndMed_AL",
                        column: x => x.UndMed_AL,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControlCalidad_Sellado_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_CorteDoblado_Prod_Id",
                table: "ControlCalidad_CorteDoblado",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_CorteDoblado_Turno_Id",
                table: "ControlCalidad_CorteDoblado",
                column: "Turno_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_CorteDoblado_UndMed_Id",
                table: "ControlCalidad_CorteDoblado",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_CorteDoblado_Usua_Id",
                table: "ControlCalidad_CorteDoblado",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Extrusion_Pigmento_Id",
                table: "ControlCalidad_Extrusion",
                column: "Pigmento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Extrusion_Prod_Id",
                table: "ControlCalidad_Extrusion",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Extrusion_Turno_Id",
                table: "ControlCalidad_Extrusion",
                column: "Turno_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Extrusion_UndMed_Id",
                table: "ControlCalidad_Extrusion",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Extrusion_Usua_Id",
                table: "ControlCalidad_Extrusion",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Impresion_Prod_Id",
                table: "ControlCalidad_Impresion",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Impresion_Turno_Id",
                table: "ControlCalidad_Impresion",
                column: "Turno_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Impresion_Usua_Id",
                table: "ControlCalidad_Impresion",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Sellado_Prod_Id",
                table: "ControlCalidad_Sellado",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Sellado_Turno_Id",
                table: "ControlCalidad_Sellado",
                column: "Turno_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Sellado_UndMed_AF",
                table: "ControlCalidad_Sellado",
                column: "UndMed_AF");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Sellado_UndMed_AL",
                table: "ControlCalidad_Sellado",
                column: "UndMed_AL");

            migrationBuilder.CreateIndex(
                name: "IX_ControlCalidad_Sellado_Usua_Id",
                table: "ControlCalidad_Sellado",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlCalidad_CorteDoblado");

            migrationBuilder.DropTable(
                name: "ControlCalidad_Extrusion");

            migrationBuilder.DropTable(
                name: "ControlCalidad_Impresion");

            migrationBuilder.DropTable(
                name: "ControlCalidad_Sellado");
        }
    }
}
