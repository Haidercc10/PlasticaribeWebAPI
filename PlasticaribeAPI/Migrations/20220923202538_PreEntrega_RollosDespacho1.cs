using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class PreEntrega_RollosDespacho1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreEntrega_RollosDespacho",
                columns: table => new
                {
                    PreEntRollo_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreEntRollo_OT = table.Column<long>(type: "bigint", nullable: false),
                    PreEntRollo_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    PreEntRollo_Observacion = table.Column<string>(type: "text", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreEntrega_RollosDespacho", x => x.PreEntRollo_Id);
                    table.ForeignKey(
                        name: "FK_PreEntrega_RollosDespacho_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreEntrega_RollosDespacho_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreEntrega_RollosDespacho_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPreEntrega_RollosDespacho",
                columns: table => new
                {
                    DtlPreEntRollo_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreEntRollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    Rollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtlPreEntRollo_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPreEntrega_RollosDespacho", x => x.DtlPreEntRollo_Id);
                    table.ForeignKey(
                        name: "FK_DetallesPreEntrega_RollosDespacho_PreEntrega_RollosDespacho_PreEntRollo_Id",
                        column: x => x.PreEntRollo_Id,
                        principalTable: "PreEntrega_RollosDespacho",
                        principalColumn: "PreEntRollo_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPreEntrega_RollosDespacho_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_PreEntRollo_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "PreEntRollo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_Proceso_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_UndMed_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreEntrega_RollosDespacho_Cli_Id",
                table: "PreEntrega_RollosDespacho",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreEntrega_RollosDespacho_Prod_Id",
                table: "PreEntrega_RollosDespacho",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreEntrega_RollosDespacho_UndMed_Id",
                table: "PreEntrega_RollosDespacho",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreEntrega_RollosDespacho_Usua_Id",
                table: "PreEntrega_RollosDespacho",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropTable(
                name: "PreEntrega_RollosDespacho");
        }
    }
}
