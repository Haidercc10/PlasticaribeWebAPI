using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class SedesClientesRealciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Sedes_Clientes_Clientes_Cli_Id1",
                table: "Sedes_Clientes");*/

            /*migrationBuilder.RenameColumn(
                name: "Cli_Id1",
                table: "Sedes_Clientes",
                newName: "Cli_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Sedes_Clientes_Cli_Id1",
                table: "Sedes_Clientes",
                newName: "IX_Sedes_Clientes_Cli_Id");*/

            migrationBuilder.AddForeignKey(
                name: "FK_Sedes_Clientes_Clientes_Cli_Id",
                table: "Sedes_Clientes",
                column: "Cli_Id",
                principalTable: "Clientes",
                principalColumn: "Cli_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_Sedes_Clientes_Clientes_Cli_Id",
                 table: "Sedes_Clientes");*/

            /*migrationBuilder.RenameColumn(
                name: "Cli_Id",
                table: "Sedes_Clientes",
                newName: "Cli_Id1");

            migrationBuilder.RenameIndex(
                name: "IX_Sedes_Clientes_Cli_Id",
                table: "Sedes_Clientes",
                newName: "IX_Sedes_Clientes_Cli_Id1");*/

            migrationBuilder.AddForeignKey(
                name: "FK_Sedes_Clientes_Clientes_Cli_Id",
                table: "Sedes_Clientes",
                column: "Cli_Id",
                principalTable: "Clientes",
                principalColumn: "Cli_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
