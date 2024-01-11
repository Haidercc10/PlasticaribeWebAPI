using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_RecuperadoMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recuperados_MatPrima",
                columns: table => new
                {
                    RecMp_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecMp_FechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    RecMp_Observacion = table.Column<string>(type: "text", nullable: false),
                    Proc_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recuperados_MatPrima", x => x.RecMp_Id);
                    table.ForeignKey(
                        name: "FK_Recuperados_MatPrima_Procesos_Proc_Id",
                        column: x => x.Proc_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recuperados_MatPrima_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recuperados_MatPrima_Proc_Id",
                table: "Recuperados_MatPrima",
                column: "Proc_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recuperados_MatPrima_Usua_Id",
                table: "Recuperados_MatPrima",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recuperados_MatPrima");
        }
    }
}
