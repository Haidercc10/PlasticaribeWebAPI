using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class adicion_ClaseAsignacion_MatPrimaXTintas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asignaciones_MatPrimasXTintas",
                columns: table => new
                {
                    AsigMPxTinta_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    AsigMPxTinta_Cantidad = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    AsigMPxTinta_FechaEntrega = table.Column<DateTime>(type: "Date", nullable: false),
                    AsigMPxTinta_Observacion = table.Column<string>(type: "text", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaciones_MatPrimasXTintas", x => x.AsigMPxTinta_Id);
                    table.ForeignKey(
                        name: "FK_Asignaciones_MatPrimasXTintas_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asignaciones_MatPrimasXTintas_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asignaciones_MatPrimasXTintas_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asignaciones_MatPrimasXTintas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrimasXTintas_Estado_Id",
                table: "Asignaciones_MatPrimasXTintas",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrimasXTintas_Tinta_Id",
                table: "Asignaciones_MatPrimasXTintas",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrimasXTintas_UndMed_Id",
                table: "Asignaciones_MatPrimasXTintas",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrimasXTintas_Usua_Id",
                table: "Asignaciones_MatPrimasXTintas",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaciones_MatPrimasXTintas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
