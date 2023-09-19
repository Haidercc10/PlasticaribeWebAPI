using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_AsignacionMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asignaciones_MatPrima",
                columns: table => new
                {
                    AsigMp_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsigMP_OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    AsigMp_FechaEntrega = table.Column<DateTime>(type: "Date", nullable: false),
                    Facco_Observacion = table.Column<string>(type: "text", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Area_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaciones_MatPrima", x => x.AsigMp_Id);
                    table.ForeignKey(
                        name: "FK_Asignaciones_MatPrima_Areas_Area_Id",
                        column: x => x.Area_Id,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asignaciones_MatPrima_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asignaciones_MatPrima_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_Area_Id",
                table: "Asignaciones_MatPrima",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_Estado_Id",
                table: "Asignaciones_MatPrima",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_Usua_Id",
                table: "Asignaciones_MatPrima",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaciones_MatPrima");
        }
    }
}
