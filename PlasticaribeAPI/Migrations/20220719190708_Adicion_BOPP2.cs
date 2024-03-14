using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_BOPP2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BOPP",
                columns: table => new
                {
                    BOPP_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BOPP_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    BOPP_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    BOPP_Serial = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    BOPP_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    CatMP_Id = table.Column<int>(type: "int", nullable: false),
                    BOPP_Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOPP", x => x.BOPP_Id);
                    table.ForeignKey(
                        name: "FK_BOPP_Categorias_MatPrima_CatMP_Id",
                        column: x => x.CatMP_Id,
                        principalTable: "Categorias_MatPrima",
                        principalColumn: "CatMP_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BOPP_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BOPP_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_CatMP_Id",
                table: "BOPP",
                column: "CatMP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_TpBod_Id",
                table: "BOPP",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_UndMed_Id",
                table: "BOPP",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOPP");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
