using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Production_Day : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produccion_Diaria",
                columns: table => new
                {
                    Prd_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prd_Ano = table.Column<int>(type: "int", nullable: false),
                    Prd_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Prd_Maquina = table.Column<int>(type: "int", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Prd_Peso = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Prd_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Prd_Meta = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Prd_Porcentaje = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Turno_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Prd_FechaRegistro = table.Column<DateTime>(type: "date", nullable: false),
                    Prd_HoraRegistro = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produccion_Diaria", x => x.Prd_Id);
                    table.ForeignKey(
                        name: "FK_Produccion_Diaria_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produccion_Diaria_Turnos_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turnos",
                        principalColumn: "Turno_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Diaria_Proceso_Id",
                table: "Produccion_Diaria",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Diaria_Turno_Id",
                table: "Produccion_Diaria",
                column: "Turno_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produccion_Diaria");
        }
    }
}
