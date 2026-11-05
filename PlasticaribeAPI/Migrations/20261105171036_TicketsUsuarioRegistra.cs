using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class TicketsUsuarioRegistra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UsuaRegistra_Id",
                table: "Tickets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UsuaRegistra_Id",
                table: "Tickets",
                column: "UsuaRegistra_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Usuarios_UsuaRegistra_Id",
                table: "Tickets",
                column: "UsuaRegistra_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Usuarios_UsuaRegistra_Id",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UsuaRegistra_Id",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UsuaRegistra_Id",
                table: "Tickets");
        }
    }
}
