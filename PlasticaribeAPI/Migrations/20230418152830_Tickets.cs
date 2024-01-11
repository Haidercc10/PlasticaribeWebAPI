using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Ticket_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Ticket_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Ticket_Descripcion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Ticket_RutaImagen = table.Column<string>(type: "varchar(max)", nullable: false),
                    Ticket_NombreImagen = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Ticket_Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets_Revisados",
                columns: table => new
                {
                    TicketRev_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketRev_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    TicketRev_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    TicketRev_Descripcion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Ticket_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets_Revisados", x => x.TicketRev_Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Revisados_Tickets_Ticket_Id",
                        column: x => x.Ticket_Id,
                        principalTable: "Tickets",
                        principalColumn: "Ticket_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Revisados_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Estado_Id",
                table: "Tickets",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Usua_Id",
                table: "Tickets",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Revisados_Ticket_Id",
                table: "Tickets_Revisados",
                column: "Ticket_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Revisados_Usua_Id",
                table: "Tickets_Revisados",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets_Revisados");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
