using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMovimientosApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimientosAplicacion",
                columns: table => new
                {
                    MovAppId = table.Column<int>(name: "MovApp_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuaId = table.Column<long>(name: "Usua_Id", type: "bigint", nullable: false),
                    MovAppNombre = table.Column<string>(name: "MovApp_Nombre", type: "varchar(50)", nullable: false),
                    MovAppDescripcion = table.Column<string>(name: "MovApp_Descripcion", type: "text", nullable: false),
                    MovAppFecha = table.Column<DateTime>(name: "MovApp_Fecha", type: "date", nullable: false),
                    MovAppHora = table.Column<string>(name: "MovApp_Hora", type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosAplicacion", x => x.MovAppId);
                    table.ForeignKey(
                        name: "FK_MovimientosAplicacion_Usuarios_Usua_Id",
                        column: x => x.UsuaId,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAplicacion_Usua_Id",
                table: "MovimientosAplicacion",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosAplicacion");
        }
    }
}
