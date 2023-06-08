using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Creacion_EventosCalendario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventosCalendario",
                columns: table => new
                {
                    EventoCal_Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    EventoCal_FechaCreacion = table.Column<DateTime>(type: "date", nullable: false),
                    EventoCal_HoraCreacion = table.Column<string>(type: "varchar(10)", nullable: false),
                    EventoCal_Nombre = table.Column<string>(type: "varchar(max)", nullable: false),
                    EventoCal_Descripcion = table.Column<string>(type: "varchar(max)", nullable: false),
                    EventoCal_FechaInicial = table.Column<DateTime>(type: "date", nullable: false),
                    EventoCal_HoraInicial = table.Column<string>(type: "varchar(10)", nullable: false),
                    EventoCal_FechaFinal = table.Column<string>(type: "varchar(10)", nullable: false),
                    EventoCal_HoraFinal = table.Column<string>(type: "varchar(10)", nullable: false),
                    EventoCal_Visibilidad = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosCalendario", x => x.EventoCal_Id);
                    table.ForeignKey(
                        name: "FK_EventosCalendario_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventosCalendario_Usua_Id",
                table: "EventosCalendario",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventosCalendario");
        }
    }
}
