using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Insumos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    Insu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Insu_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Insu_Stock = table.Column<string>(type: "varchar(100)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    CatInsu_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.Insu_Id);
                    table.ForeignKey(
                        name: "FK_Insumo_Categorias_Insumos_CatInsu_Id",
                        column: x => x.CatInsu_Id,
                        principalTable: "Categorias_Insumos",
                        principalColumn: "CatInsu_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insumo_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_CatInsu_Id",
                table: "Insumo",
                column: "CatInsu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_UndMed_Id",
                table: "Insumo",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insumo");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
