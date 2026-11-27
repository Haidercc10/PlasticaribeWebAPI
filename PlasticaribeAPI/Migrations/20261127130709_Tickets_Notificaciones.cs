using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Tickets_Notificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets_Notificaciones",
                columns: table => new
                {
                    TicketNot_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    TicketRev_Id = table.Column<long>(type: "bigint", nullable: true),
                    UsuaAutor_Id = table.Column<long>(type: "bigint", nullable: false),
                    TicketNot_Tipo = table.Column<int>(type: "int", nullable: false),
                    TicketNot_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketNot_Leida = table.Column<bool>(type: "bit", nullable: false),
                    TicketNot_FechaLeida = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets_Notificaciones", x => x.TicketNot_Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Notificaciones_Tickets_Revisados_TicketRev_Id",
                        column: x => x.TicketRev_Id,
                        principalTable: "Tickets_Revisados",
                        principalColumn: "TicketRev_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Notificaciones_Tickets_Ticket_Id",
                        column: x => x.Ticket_Id,
                        principalTable: "Tickets",
                        principalColumn: "Ticket_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Notificaciones_Usuarios_UsuaAutor_Id",
                        column: x => x.UsuaAutor_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Notificaciones_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Notificaciones_Ticket_Id",
                table: "Tickets_Notificaciones",
                column: "Ticket_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Notificaciones_TicketRev_Id",
                table: "Tickets_Notificaciones",
                column: "TicketRev_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Notificaciones_Usua_Id_TicketNot_Leida_Ticket_Id",
                table: "Tickets_Notificaciones",
                columns: new[] { "Usua_Id", "TicketNot_Leida", "Ticket_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Notificaciones_UsuaAutor_Id",
                table: "Tickets_Notificaciones",
                column: "UsuaAutor_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets_Notificaciones");
        }
    }
}
