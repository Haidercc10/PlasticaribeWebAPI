using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Inventarios_Areas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventarios_Areas",
                columns: table => new
                {
                    InvCodigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OT = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    InvStock = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InvPrecio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    InvFecha_Inventario = table.Column<DateTime>(type: "date", nullable: false),
                    InvFecha_Registro = table.Column<DateTime>(type: "date", nullable: false),
                    InvHora_Registro = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    InvObservacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios_Areas", x => x.InvCodigo);
                    table.ForeignKey(
                        name: "FK_Inventarios_Areas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Areas_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Areas_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Areas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Areas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Areas_MatPri_Id",
                table: "Inventarios_Areas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Areas_Proceso_Id",
                table: "Inventarios_Areas",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Areas_Prod_Id",
                table: "Inventarios_Areas",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Areas_UndMed_Id",
                table: "Inventarios_Areas",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Areas_Usua_Id",
                table: "Inventarios_Areas",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventarios_Areas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
