using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Precargue_RollosDespacho4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Precargue_Despacho",
                columns: table => new
                {
                    Pcd_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    OF_Id = table.Column<int>(type: "int", nullable: true),
                    Pcd_FechaCrea = table.Column<DateTime>(type: "date", nullable: false),
                    Pcd_HoraCrea = table.Column<string>(type: "varchar(50)", nullable: false),
                    Usua_Crea = table.Column<long>(type: "bigint", nullable: false),
                    Pcd_Observacion = table.Column<string>(type: "varchar(max)", nullable: true),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Pcd_FechaModifica = table.Column<DateTime>(type: "date", nullable: true),
                    Pcd_HoraModifica = table.Column<string>(type: "varchar(50)", nullable: true),
                    Usua_Modifica = table.Column<long>(type: "bigint", nullable: false),
                    Pcd_ObservacionModifica = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precargue_Despacho", x => x.Pcd_Id);
                    table.ForeignKey(
                        name: "FK_Precargue_Despacho_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Precargue_Despacho_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Precargue_Despacho_OrdenFacturacion_OF_Id",
                        column: x => x.OF_Id,
                        principalTable: "OrdenFacturacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Precargue_Despacho_Usuarios_Usua_Crea",
                        column: x => x.Usua_Crea,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Precargue_Despacho_Usuarios_Usua_Modifica",
                        column: x => x.Usua_Modifica,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_PrecargueDespacho",
                columns: table => new
                {
                    DtlPcd_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pcd_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    DtlPcd_Rollo = table.Column<long>(type: "bigint", nullable: false),
                    DtlPcd_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_PrecargueDespacho", x => x.DtlPcd_Codigo);
                    table.ForeignKey(
                        name: "FK_Detalles_PrecargueDespacho_Precargue_Despacho_Pcd_Id",
                        column: x => x.Pcd_Id,
                        principalTable: "Precargue_Despacho",
                        principalColumn: "Pcd_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_PrecargueDespacho_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_PrecargueDespacho_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_PrecargueDespacho_Pcd_Id",
                table: "Detalles_PrecargueDespacho",
                column: "Pcd_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_PrecargueDespacho_Prod_Id",
                table: "Detalles_PrecargueDespacho",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_PrecargueDespacho_UndMed_Id",
                table: "Detalles_PrecargueDespacho",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Precargue_Despacho_Cli_Id",
                table: "Precargue_Despacho",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Precargue_Despacho_Estado_Id",
                table: "Precargue_Despacho",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Precargue_Despacho_OF_Id",
                table: "Precargue_Despacho",
                column: "OF_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Precargue_Despacho_Usua_Crea",
                table: "Precargue_Despacho",
                column: "Usua_Crea");

            migrationBuilder.CreateIndex(
                name: "IX_Precargue_Despacho_Usua_Modifica",
                table: "Precargue_Despacho",
                column: "Usua_Modifica");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_PrecargueDespacho");

            migrationBuilder.DropTable(
                name: "Precargue_Despacho");
        }
    }
}
