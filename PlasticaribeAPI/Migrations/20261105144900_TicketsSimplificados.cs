using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class TicketsSimplificados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoNuevo_Id",
                table: "Tickets_Revisados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ticket_Asunto",
                table: "Tickets",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Ticket_FechaCreacion",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Ticket_FechaResolucion",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoTicket_Id",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 5); // Tickets existentes quedan como "OTRO"

            migrationBuilder.CreateTable(
                name: "Tickets_Adjuntos",
                columns: table => new
                {
                    Adj_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Id = table.Column<long>(type: "bigint", nullable: false),
                    Adj_NombreOriginal = table.Column<string>(type: "varchar(255)", nullable: false),
                    Adj_RutaRelativa = table.Column<string>(type: "varchar(300)", nullable: false),
                    Adj_TipoContenido = table.Column<string>(type: "varchar(50)", nullable: false),
                    Adj_TamanoBytes = table.Column<long>(type: "bigint", nullable: false),
                    Adj_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets_Adjuntos", x => x.Adj_Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Adjuntos_Tickets_Ticket_Id",
                        column: x => x.Ticket_Id,
                        principalTable: "Tickets",
                        principalColumn: "Ticket_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Adjuntos_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets_Tipos",
                columns: table => new
                {
                    TipoTicket_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTicket_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    TipoTicket_Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets_Tipos", x => x.TipoTicket_Id);
                });

            migrationBuilder.InsertData(
                table: "Tickets_Tipos",
                columns: new[] { "TipoTicket_Id", "TipoTicket_Activo", "TipoTicket_Nombre" },
                values: new object[,]
                {
                    { 1, true, "INCIDENTE" },
                    { 2, true, "SOLICITUD" },
                    { 3, true, "REQUERIMIENTO" },
                    { 4, true, "MEJORA" },
                    { 5, true, "OTRO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Revisados_EstadoNuevo_Id",
                table: "Tickets_Revisados",
                column: "EstadoNuevo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Ticket_FechaCreacion",
                table: "Tickets",
                column: "Ticket_FechaCreacion");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TipoTicket_Id",
                table: "Tickets",
                column: "TipoTicket_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Adjuntos_Ticket_Id",
                table: "Tickets_Adjuntos",
                column: "Ticket_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Adjuntos_Usua_Id",
                table: "Tickets_Adjuntos",
                column: "Usua_Id");

            // Datos existentes: asunto = inicio de la descripción, fecha de creación = Fecha + Hora
            migrationBuilder.Sql(@"
UPDATE Tickets
SET Ticket_Asunto = LEFT(LTRIM(REPLACE(REPLACE(Ticket_Descripcion, CHAR(13), ' '), CHAR(10), ' ')), 100),
    Ticket_FechaCreacion = COALESCE(TRY_CAST(CONVERT(varchar(10), Ticket_Fecha, 23) + ' ' + Ticket_Hora AS datetime2),
                                    CAST(Ticket_Fecha AS datetime2));");

            // Los registros de Tickets_Revisados existentes eran todos resoluciones
            migrationBuilder.Sql(@"UPDATE Tickets_Revisados SET EstadoNuevo_Id = 30 WHERE EstadoNuevo_Id IS NULL;");

            // Fecha de resolución de los tickets resueltos = último registro de Tickets_Revisados
            migrationBuilder.Sql(@"
UPDATE t
SET Ticket_FechaResolucion = r.Fecha
FROM Tickets t
CROSS APPLY (
    SELECT TOP 1 COALESCE(TRY_CAST(CONVERT(varchar(10), tr.TicketRev_Fecha, 23) + ' ' + tr.TicketRev_Hora AS datetime2),
                          CAST(tr.TicketRev_Fecha AS datetime2)) AS Fecha
    FROM Tickets_Revisados tr
    WHERE tr.Ticket_Id = t.Ticket_Id
    ORDER BY tr.TicketRev_Id DESC
) r
WHERE t.Estado_Id = 30;");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tickets_Tipos_TipoTicket_Id",
                table: "Tickets",
                column: "TipoTicket_Id",
                principalTable: "Tickets_Tipos",
                principalColumn: "TipoTicket_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Revisados_Estados_EstadoNuevo_Id",
                table: "Tickets_Revisados",
                column: "EstadoNuevo_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tickets_Tipos_TipoTicket_Id",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Revisados_Estados_EstadoNuevo_Id",
                table: "Tickets_Revisados");

            migrationBuilder.DropTable(
                name: "Tickets_Adjuntos");

            migrationBuilder.DropTable(
                name: "Tickets_Tipos");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_Revisados_EstadoNuevo_Id",
                table: "Tickets_Revisados");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_Ticket_FechaCreacion",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TipoTicket_Id",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "EstadoNuevo_Id",
                table: "Tickets_Revisados");

            migrationBuilder.DropColumn(
                name: "Ticket_Asunto",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Ticket_FechaCreacion",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Ticket_FechaResolucion",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TipoTicket_Id",
                table: "Tickets");
        }
    }
}
