using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_ClaseRemisionTablaRemisiones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remisiones",
                columns: table => new
                {
                    Rem_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rem_Codigo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Rem_Fecha = table.Column<DateTime>(type: "Date", nullable: false),
                    Rem_PrecioEstimado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Prov_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    TpDoc_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Rem_Observacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remisiones", x => x.Rem_Id);
                    table.ForeignKey(
                        name: "FK_Remisiones_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remisiones_Proveedores_Prov_Id",
                        column: x => x.Prov_Id,
                        principalTable: "Proveedores",
                        principalColumn: "Prov_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remisiones_Tipos_Documentos_TpDoc_Id",
                        column: x => x.TpDoc_Id,
                        principalTable: "Tipos_Documentos",
                        principalColumn: "TpDoc_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remisiones_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_Estado_Id",
                table: "Remisiones",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_Prov_Id",
                table: "Remisiones",
                column: "Prov_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_TpDoc_Id",
                table: "Remisiones",
                column: "TpDoc_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_Usua_Id",
                table: "Remisiones",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remisiones");
        }
    }
}
