using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class adicion_ClaseTintas_MateriasPrimas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tintas_MateriasPrimas",
                columns: table => new
                {
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tintas_MateriasPrimas", x => new { x.Tinta_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_Tintas_MateriasPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tintas_MateriasPrimas_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tintas_MateriasPrimas_MatPri_Id",
                table: "Tintas_MateriasPrimas",
                column: "MatPri_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tintas_MateriasPrimas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
