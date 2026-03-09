using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Sales_InRepositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Vendedor",
                table: "Reposiciones",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reposiciones_Usua_Vendedor",
                table: "Reposiciones",
                column: "Usua_Vendedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Reposiciones_Usuarios_Usua_Vendedor",
                table: "Reposiciones",
                column: "Usua_Vendedor",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reposiciones_Usuarios_Usua_Vendedor",
                table: "Reposiciones");

            migrationBuilder.DropIndex(
                name: "IX_Reposiciones_Usua_Vendedor",
                table: "Reposiciones");

            migrationBuilder.DropColumn(
                name: "Usua_Vendedor",
                table: "Reposiciones");
        }
    }
}
