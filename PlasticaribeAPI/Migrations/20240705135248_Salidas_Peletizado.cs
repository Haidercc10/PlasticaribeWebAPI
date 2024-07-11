using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Salidas_Peletizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salidas_Peletizado",
                columns: table => new
                {
                    SalPel_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    SalPel_Peso = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    SalPel_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    SalPel_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    SalPel_FechaAprobado = table.Column<DateTime>(type: "date", nullable: false),
                    SalPel_HoraAprobado = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    SalPel_Observacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salidas_Peletizado", x => x.SalPel_Id);
                    table.ForeignKey(
                        name: "FK_Salidas_Peletizado_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salidas_Peletizado_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salidas_Peletizado_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_SalidasPeletizado",
                columns: table => new
                {
                    DtSalPel_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalPel_Id = table.Column<long>(type: "bigint", nullable: false),
                    IngPel_Id = table.Column<long>(type: "bigint", nullable: false),
                    TpRecu_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    DtSalPel_Peso = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_SalidasPeletizado", x => x.DtSalPel_Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_SalidasPeletizado_Ingreso_Peletizado_IngPel_Id",
                        column: x => x.IngPel_Id,
                        principalTable: "Ingreso_Peletizado",
                        principalColumn: "IngPel_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SalidasPeletizado_Salidas_Peletizado_SalPel_Id",
                        column: x => x.SalPel_Id,
                        principalTable: "Salidas_Peletizado",
                        principalColumn: "SalPel_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SalidasPeletizado_Tipos_Recuperados_TpRecu_Id",
                        column: x => x.TpRecu_Id,
                        principalTable: "Tipos_Recuperados",
                        principalColumn: "TpRecu_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SalidasPeletizado_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SalidasPeletizado_IngPel_Id",
                table: "Detalles_SalidasPeletizado",
                column: "IngPel_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SalidasPeletizado_SalPel_Id",
                table: "Detalles_SalidasPeletizado",
                column: "SalPel_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SalidasPeletizado_TpRecu_Id",
                table: "Detalles_SalidasPeletizado",
                column: "TpRecu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SalidasPeletizado_UndMed_Id",
                table: "Detalles_SalidasPeletizado",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Peletizado_Estado_Id",
                table: "Salidas_Peletizado",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Peletizado_MatPri_Id",
                table: "Salidas_Peletizado",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Peletizado_Usua_Id",
                table: "Salidas_Peletizado",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_SalidasPeletizado");

            migrationBuilder.DropTable(
                name: "Salidas_Peletizado");
        }
    }
}
