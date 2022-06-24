using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_Provedor_MateriaPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedores_MateriasPrimas",
                columns: table => new
                {
                    Prov_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores_MateriasPrimas", x => new { x.Prov_Id, x.MatPri_Id });
                    table.ForeignKey(
                        name: "FK_Proveedores_MateriasPrimas_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proveedores_MateriasPrimas_Proveedores_Prov_Id",
                        column: x => x.Prov_Id,
                        principalTable: "Proveedores",
                        principalColumn: "Prov_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_MateriasPrimas_MatPri_Id",
                table: "Proveedores_MateriasPrimas",
                column: "MatPri_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proveedores_MateriasPrimas");
        }
    }
}
