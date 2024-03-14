using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Entrada_Tintas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entradas_Tintas",
                columns: table => new
                {
                    EntTinta_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entTinta_FechaEntrada = table.Column<DateTime>(type: "Date", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    EntTinta_Observacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas_Tintas", x => x.EntTinta_Id);
                    table.ForeignKey(
                        name: "FK_Entradas_Tintas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_EntradaTintas",
                columns: table => new
                {
                    EntTinta_Id = table.Column<int>(type: "int", nullable: false),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtEntTinta_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_EntradaTintas", x => new { x.EntTinta_Id, x.Tinta_Id });
                    table.ForeignKey(
                        name: "FK_Detalles_EntradaTintas_Entradas_Tintas_EntTinta_Id",
                        column: x => x.EntTinta_Id,
                        principalTable: "Entradas_Tintas",
                        principalColumn: "EntTinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_EntradaTintas_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_EntradaTintas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_EntradaTintas_Tinta_Id",
                table: "Detalles_EntradaTintas",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_EntradaTintas_UndMed_Id",
                table: "Detalles_EntradaTintas",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Tintas_Usua_Id",
                table: "Entradas_Tintas",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_EntradaTintas");

            migrationBuilder.DropTable(
                name: "Entradas_Tintas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
