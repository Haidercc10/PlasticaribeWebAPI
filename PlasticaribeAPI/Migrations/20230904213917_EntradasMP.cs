using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class EntradasMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movimientros_Entradas_MP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    Bopp_Id = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad_Entrada = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Precio_Unitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tipo_Entrada = table.Column<string>(type: "varchar(10)", nullable: false),
                    Codigo_Entrada = table.Column<int>(type: "int", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Cantidad_Asignada = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cantidad_Disponible = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Fecha_Entrada = table.Column<DateTime>(type: "date", nullable: false),
                    Hora_Entrada = table.Column<string>(type: "varchar(10)", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientros_Entradas_MP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientros_Entradas_MP_Bopp_Generico_Bopp_Id",
                        column: x => x.Bopp_Id,
                        principalTable: "Bopp_Generico",
                        principalColumn: "BoppGen_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientros_Entradas_MP_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientros_Entradas_MP_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientros_Entradas_MP_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientros_Entradas_MP_Tipos_Documentos_Tipo_Entrada",
                        column: x => x.Tipo_Entrada,
                        principalTable: "Tipos_Documentos",
                        principalColumn: "TpDoc_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientros_Entradas_MP_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entradas_Salidas_MP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Entrada = table.Column<int>(type: "int", nullable: false),
                    Tipo_Salida = table.Column<string>(type: "varchar(10)", nullable: false),
                    Codigo_Salida = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas_Salidas_MP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entradas_Salidas_MP_Movimientros_Entradas_MP_Id_Entrada",
                        column: x => x.Id_Entrada,
                        principalTable: "Movimientros_Entradas_MP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entradas_Salidas_MP_Tipos_Documentos_Tipo_Salida",
                        column: x => x.Tipo_Salida,
                        principalTable: "Tipos_Documentos",
                        principalColumn: "TpDoc_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Salidas_MP_Id_Entrada",
                table: "Entradas_Salidas_MP",
                column: "Id_Entrada");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Salidas_MP_Tipo_Salida",
                table: "Entradas_Salidas_MP",
                column: "Tipo_Salida");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientros_Entradas_MP_Bopp_Id",
                table: "Movimientros_Entradas_MP",
                column: "Bopp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientros_Entradas_MP_Estado_Id",
                table: "Movimientros_Entradas_MP",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientros_Entradas_MP_MatPri_Id",
                table: "Movimientros_Entradas_MP",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientros_Entradas_MP_Tinta_Id",
                table: "Movimientros_Entradas_MP",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientros_Entradas_MP_Tipo_Entrada",
                table: "Movimientros_Entradas_MP",
                column: "Tipo_Entrada");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientros_Entradas_MP_UndMed_Id",
                table: "Movimientros_Entradas_MP",
                column: "UndMed_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entradas_Salidas_MP");

            migrationBuilder.DropTable(
                name: "Movimientros_Entradas_MP");
        }
    }
}
