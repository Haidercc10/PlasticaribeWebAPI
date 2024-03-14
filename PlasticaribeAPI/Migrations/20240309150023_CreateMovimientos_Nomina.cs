using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CreateMovimientos_Nomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Movimientos_Nomina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trabajador_Id = table.Column<long>(type: "bigint", nullable: false),
                    CodigoMovimento = table.Column<int>(type: "int", nullable: false),
                    NombreMovimento = table.Column<string>(type: "varchar(100)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorDeuda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorPagado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorAbonado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorFinalDeuda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Hora = table.Column<string>(type: "varchar(50)", nullable: false),
                    Observacaion = table.Column<string>(type: "varchar(max)", nullable: false, defaultValue: ""),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Creador_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos_Nomina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Nomina_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientos_Nomina_Usuarios_Creador_Id",
                        column: x => x.Creador_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientos_Nomina_Usuarios_Trabajador_Id",
                        column: x => x.Trabajador_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_Nomina_Creador_Id",
                table: "Movimientos_Nomina",
                column: "Creador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_Nomina_Estado_Id",
                table: "Movimientos_Nomina",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_Nomina_Trabajador_Id",
                table: "Movimientos_Nomina",
                column: "Trabajador_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos_Nomina");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
