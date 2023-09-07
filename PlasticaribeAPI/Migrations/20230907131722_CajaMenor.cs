using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CajaMenor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoSalidas_CajaMenor",
                columns: table => new
                {
                    TpSal_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpSal_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TpSal_Descripcion = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSalidas_CajaMenor", x => x.TpSal_Id);
                });

            migrationBuilder.CreateTable(
                name: "CajaMenor_Plasticaribe",
                columns: table => new
                {
                    CajaMenor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CajaMenor_FechaRegistro = table.Column<DateTime>(type: "date", nullable: false),
                    CajaMenor_HoraRegistro = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    CajaMenor_FechaSalida = table.Column<DateTime>(type: "date", nullable: false),
                    CajaMenor_ValorSalida = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CajaMenor_Observacion = table.Column<string>(type: "varchar(max)", nullable: false, defaultValue: ""),
                    TpSal_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CajaMenor_Plasticaribe", x => x.CajaMenor_Id);
                    table.ForeignKey(
                        name: "FK_CajaMenor_Plasticaribe_TipoSalidas_CajaMenor_TpSal_Id",
                        column: x => x.TpSal_Id,
                        principalTable: "TipoSalidas_CajaMenor",
                        principalColumn: "TpSal_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CajaMenor_Plasticaribe_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CajaMenor_Plasticaribe_TpSal_Id",
                table: "CajaMenor_Plasticaribe",
                column: "TpSal_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CajaMenor_Plasticaribe_Usua_Id",
                table: "CajaMenor_Plasticaribe",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CajaMenor_Plasticaribe");

            migrationBuilder.DropTable(
                name: "TipoSalidas_CajaMenor");
        }
    }
}
