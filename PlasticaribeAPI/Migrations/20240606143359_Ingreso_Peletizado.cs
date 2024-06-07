using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Ingreso_Peletizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingreso_Peletizado",
                columns: table => new
                {
                    IngPel_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rollo_Id = table.Column<long>(type: "bigint", nullable: true),
                    TpRecu_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    OT = table.Column<long>(type: "bigint", nullable: true),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Material_Id = table.Column<int>(type: "int", nullable: false),
                    Falla_Id = table.Column<int>(type: "int", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    IngPel_Area1 = table.Column<bool>(type: "bit", nullable: false),
                    IngPel_Area2 = table.Column<bool>(type: "bit", nullable: false),
                    IngPel_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    IngPel_Observacion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    IngPel_FechaIngreso = table.Column<DateTime>(type: "Date", nullable: true),
                    IngPel_HoraIngreso = table.Column<string>(type: "varchar(10)", nullable: true),
                    Usua_Modifica = table.Column<long>(type: "bigint", nullable: true),
                    IngPel_FechaModifica = table.Column<DateTime>(type: "Date", nullable: true),
                    IngPel_HoraModifica = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingreso_Peletizado", x => x.IngPel_Id);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Fallas_Tecnicas_Falla_Id",
                        column: x => x.Falla_Id,
                        principalTable: "Fallas_Tecnicas",
                        principalColumn: "Falla_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Materiales_MatPrima_Material_Id",
                        column: x => x.Material_Id,
                        principalTable: "Materiales_MatPrima",
                        principalColumn: "Material_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Tipos_Recuperados_TpRecu_Id",
                        column: x => x.TpRecu_Id,
                        principalTable: "Tipos_Recuperados",
                        principalColumn: "TpRecu_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingreso_Peletizado_Usuarios_Usua_Modifica",
                        column: x => x.Usua_Modifica,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_Estado_Id",
                table: "Ingreso_Peletizado",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_Falla_Id",
                table: "Ingreso_Peletizado",
                column: "Falla_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_Material_Id",
                table: "Ingreso_Peletizado",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_MatPri_Id",
                table: "Ingreso_Peletizado",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_Proceso_Id",
                table: "Ingreso_Peletizado",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_Prod_Id",
                table: "Ingreso_Peletizado",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_TpRecu_Id",
                table: "Ingreso_Peletizado",
                column: "TpRecu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_UndMed_Id",
                table: "Ingreso_Peletizado",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_Usua_Id",
                table: "Ingreso_Peletizado",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_Peletizado_Usua_Modifica",
                table: "Ingreso_Peletizado",
                column: "Usua_Modifica");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingreso_Peletizado");
        }
    }
}
