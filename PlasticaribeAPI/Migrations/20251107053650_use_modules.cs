using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class use_modules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usabilidad_Modulos",
                columns: table => new
                {
                    Usm_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usm_Modulo = table.Column<string>(type: "varchar(200)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usm_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Usm_Hora = table.Column<string>(type: "varchar(20)", nullable: false),
                    Usm_Accion = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usabilidad_Modulos", x => x.Usm_Id);
                    table.ForeignKey(
                        name: "FK_Usabilidad_Modulos_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usabilidad_Modulos_Usua_Id",
                table: "Usabilidad_Modulos",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usabilidad_Modulos");
        }
    }
}
