using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Productos_HistorialNombres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos_HistorialNombres",
                columns: table => new
                {
                    ProdHN_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    ProdHN_NombreAnterior = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProdHN_NombreNuevo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ot_Id = table.Column<long>(type: "bigint", nullable: false),
                    ProdHN_Origen = table.Column<string>(type: "varchar(20)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    ProdHN_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos_HistorialNombres", x => x.ProdHN_Id);
                    table.ForeignKey(
                        name: "FK_Productos_HistorialNombres_Orden_Trabajo_Ot_Id",
                        column: x => x.Ot_Id,
                        principalTable: "Orden_Trabajo",
                        principalColumn: "Ot_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_HistorialNombres_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_HistorialNombres_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_HistorialNombres_Ot_Id",
                table: "Productos_HistorialNombres",
                column: "Ot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_HistorialNombres_Prod_Id",
                table: "Productos_HistorialNombres",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_HistorialNombres_Usua_Id",
                table: "Productos_HistorialNombres",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos_HistorialNombres");
        }
    }
}
