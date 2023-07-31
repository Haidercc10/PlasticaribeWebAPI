using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Nomina_Plasticaribe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nomina_Plasticaribe",
                columns: table => new
                {
                    Nomina_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nomina_FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nomina_HoraRegistro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Nomina_FechaIncial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nomina_FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nomina_Costo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Nomina_Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomina_Plasticaribe", x => x.Nomina_Id);
                    table.ForeignKey(
                        name: "FK_Nomina_Plasticaribe_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_Plasticaribe_Usua_Id",
                table: "Nomina_Plasticaribe",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nomina_Plasticaribe");
        }
    }
}
