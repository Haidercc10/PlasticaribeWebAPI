using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Ficha_Complementaria_Item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ficha_Complementaria_Item",
                columns: table => new
                {
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Embobinado_Id = table.Column<int>(type: "int", nullable: true),
                    CodigoBarras = table.Column<string>(type: "varchar(20)", nullable: true),
                    Repeticiones = table.Column<string>(type: "varchar(50)", nullable: true),
                    PasoGuias = table.Column<string>(type: "varchar(50)", nullable: true),
                    UbicacionGuia = table.Column<string>(type: "varchar(100)", nullable: true),
                    LargoContinuo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MezclaAjustada = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usua_Modifica = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Modifica = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ficha_Complementaria_Item", x => x.Prod_Id);
                    table.ForeignKey(
                        name: "FK_Ficha_Complementaria_Item_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ficha_Complementaria_Item_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ficha_Complementaria_Item_Usuarios_Usua_Modifica",
                        column: x => x.Usua_Modifica,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ficha_Complementaria_Item_Usua_Id",
                table: "Ficha_Complementaria_Item",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ficha_Complementaria_Item_Usua_Modifica",
                table: "Ficha_Complementaria_Item",
                column: "Usua_Modifica");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ficha_Complementaria_Item");
        }
    }
}
