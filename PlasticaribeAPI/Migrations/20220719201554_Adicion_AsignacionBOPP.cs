using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_AsignacionBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asignaciones_BOPP",
                columns: table => new
                {
                    AsigBOPP_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsigBOPP_OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    AsigBOPP_FechaEntrega = table.Column<DateTime>(type: "Date", nullable: false),
                    AsigBOPP_Observacion = table.Column<string>(type: "text", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaciones_BOPP", x => x.AsigBOPP_Id);
                    table.ForeignKey(
                        name: "FK_Asignaciones_BOPP_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asignaciones_BOPP_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_BOPP_Estado_Id",
                table: "Asignaciones_BOPP",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_BOPP_Usua_Id",
                table: "Asignaciones_BOPP",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaciones_BOPP");
        }
    }
}
