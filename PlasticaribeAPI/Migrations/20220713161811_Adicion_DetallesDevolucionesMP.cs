using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_DetallesDevolucionesMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallesDevoluciones_MateriasPrimas",
                columns: table => new
                {
                    DevMatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtDevMatPri_CantidadDevuelta = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesDevoluciones_MateriasPrimas", x => new { x.DevMatPri_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_DetallesDevoluciones_MateriasPrimas_Devoluciones_MatPrima_DevMatPri_Id",
                        column: x => x.DevMatPri_Id,
                        principalTable: "Devoluciones_MatPrima",
                        principalColumn: "DevMatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesDevoluciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesDevoluciones_MateriasPrimas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_MatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_UndMed_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesDevoluciones_MateriasPrimas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
