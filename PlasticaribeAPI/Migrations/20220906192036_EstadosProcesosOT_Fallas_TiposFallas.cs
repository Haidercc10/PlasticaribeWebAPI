using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class EstadosProcesosOT_Fallas_TiposFallas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipos_FallasTecnicas",
                columns: table => new
                {
                    TipoFalla_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoFalla_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TipoFalla_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_FallasTecnicas", x => x.TipoFalla_Id);
                });

            migrationBuilder.CreateTable(
                name: "Fallas_Tecnicas",
                columns: table => new
                {
                    Falla_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Falla_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Falla_Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    TipoFalla_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fallas_Tecnicas", x => x.Falla_Id);
                    table.ForeignKey(
                        name: "FK_Fallas_Tecnicas_Tipos_FallasTecnicas_TipoFalla_Id",
                        column: x => x.TipoFalla_Id,
                        principalTable: "Tipos_FallasTecnicas",
                        principalColumn: "TipoFalla_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estados_ProcesosOT",
                columns: table => new
                {
                    EstProcOT_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstProcOT_OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    EstProcOT_ExtrusionKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_ImpresionKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_RotograbadoKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_LaminadoKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_CorteKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_DobladoKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_SelladoKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_SelladoUnd = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_WiketiadoKg = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_WiketiadoUnd = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstProcOT_CantidadPedida = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Falla_Id = table.Column<int>(type: "int", nullable: false),
                    EstProcOT_Observacion = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados_ProcesosOT", x => x.EstProcOT_Id);
                    table.ForeignKey(
                        name: "FK_Estados_ProcesosOT_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estados_ProcesosOT_Fallas_Tecnicas_Falla_Id",
                        column: x => x.Falla_Id,
                        principalTable: "Fallas_Tecnicas",
                        principalColumn: "Falla_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estados_ProcesosOT_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ProcesosOT_Estado_Id",
                table: "Estados_ProcesosOT",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ProcesosOT_Falla_Id",
                table: "Estados_ProcesosOT",
                column: "Falla_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ProcesosOT_UndMed_Id",
                table: "Estados_ProcesosOT",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fallas_Tecnicas_TipoFalla_Id",
                table: "Fallas_Tecnicas",
                column: "TipoFalla_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estados_ProcesosOT");

            migrationBuilder.DropTable(
                name: "Fallas_Tecnicas");

            migrationBuilder.DropTable(
                name: "Tipos_FallasTecnicas");
        }
    }
}
