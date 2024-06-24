using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Relacion_MatPrimaMaterialPigmento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatPrimas_Materiales_Pigmentos",
                columns: table => new
                {
                    Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    Material_Id = table.Column<int>(type: "int", nullable: false),
                    Pigmt_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatPrimas_Materiales_Pigmentos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_MatPrimas_Materiales_Pigmentos_Materiales_MatPrima_Material_Id",
                        column: x => x.Material_Id,
                        principalTable: "Materiales_MatPrima",
                        principalColumn: "Material_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatPrimas_Materiales_Pigmentos_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatPrimas_Materiales_Pigmentos_Pigmentos_Pigmt_Id",
                        column: x => x.Pigmt_Id,
                        principalTable: "Pigmentos",
                        principalColumn: "Pigmt_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatPrimas_Materiales_Pigmentos_Material_Id",
                table: "MatPrimas_Materiales_Pigmentos",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatPrimas_Materiales_Pigmentos_MatPri_Id",
                table: "MatPrimas_Materiales_Pigmentos",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatPrimas_Materiales_Pigmentos_Pigmt_Id",
                table: "MatPrimas_Materiales_Pigmentos",
                column: "Pigmt_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatPrimas_Materiales_Pigmentos");
        }
    }
}
