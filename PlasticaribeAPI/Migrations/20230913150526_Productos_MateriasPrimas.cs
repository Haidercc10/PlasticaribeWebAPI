using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Productos_MateriasPrimas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos_MateriasPrimas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    ExProd_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    Bopp_Id = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad_Minima = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Fecha_Registro = table.Column<DateTime>(type: "date", nullable: false),
                    Hora_Registro = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos_MateriasPrimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_MateriasPrimas_Bopp_Generico_Bopp_Id",
                        column: x => x.Bopp_Id,
                        principalTable: "Bopp_Generico",
                        principalColumn: "BoppGen_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_MateriasPrimas_Existencias_Productos_ExProd_Id",
                        column: x => x.ExProd_Id,
                        principalTable: "Existencias_Productos",
                        principalColumn: "ExProd_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_MateriasPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_MateriasPrimas_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_MateriasPrimas_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_MateriasPrimas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_MateriasPrimas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MateriasPrimas_Bopp_Id",
                table: "Productos_MateriasPrimas",
                column: "Bopp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MateriasPrimas_ExProd_Id",
                table: "Productos_MateriasPrimas",
                column: "ExProd_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MateriasPrimas_MatPri_Id",
                table: "Productos_MateriasPrimas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MateriasPrimas_Prod_Id",
                table: "Productos_MateriasPrimas",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MateriasPrimas_Tinta_Id",
                table: "Productos_MateriasPrimas",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MateriasPrimas_UndMed_Id",
                table: "Productos_MateriasPrimas",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MateriasPrimas_Usua_Id",
                table: "Productos_MateriasPrimas",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos_MateriasPrimas");
        }
    }
}
