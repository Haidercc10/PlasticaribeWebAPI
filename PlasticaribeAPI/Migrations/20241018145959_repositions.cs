using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class repositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reposiciones",
                columns: table => new
                {
                    Rep_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Rep_FechaCrea = table.Column<DateTime>(type: "date", nullable: false),
                    Rep_HoraCrea = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Crea = table.Column<long>(type: "bigint", nullable: false),
                    Rep_Observacion = table.Column<string>(type: "varchar(max)", nullable: true),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Rep_FechaSalida = table.Column<DateTime>(type: "date", nullable: true),
                    Rep_HoraSalida = table.Column<string>(type: "varchar(10)", nullable: true),
                    Usua_Salida = table.Column<long>(type: "bigint", nullable: false),
                    Rep_ObservacionSalida = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reposiciones", x => x.Rep_Id);
                    table.ForeignKey(
                        name: "FK_Reposiciones_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reposiciones_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reposiciones_Usuarios_Usua_Crea",
                        column: x => x.Usua_Crea,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reposiciones_Usuarios_Usua_Salida",
                        column: x => x.Usua_Salida,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_Reposiciones",
                columns: table => new
                {
                    DtlRep_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rep_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    DtlRep_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    DtlRep_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_Reposiciones", x => x.DtlRep_Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_Reposiciones_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_Reposiciones_Reposiciones_Rep_Id",
                        column: x => x.Rep_Id,
                        principalTable: "Reposiciones",
                        principalColumn: "Rep_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_Reposiciones_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Reposiciones_Prod_Id",
                table: "Detalles_Reposiciones",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Reposiciones_Rep_Id",
                table: "Detalles_Reposiciones",
                column: "Rep_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Reposiciones_UndMed_Id",
                table: "Detalles_Reposiciones",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reposiciones_Cli_Id",
                table: "Reposiciones",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reposiciones_Estado_Id",
                table: "Reposiciones",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reposiciones_Usua_Crea",
                table: "Reposiciones",
                column: "Usua_Crea");

            migrationBuilder.CreateIndex(
                name: "IX_Reposiciones_Usua_Salida",
                table: "Reposiciones",
                column: "Usua_Salida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_Reposiciones");

            migrationBuilder.DropTable(
                name: "Reposiciones");
        }
    }
}
