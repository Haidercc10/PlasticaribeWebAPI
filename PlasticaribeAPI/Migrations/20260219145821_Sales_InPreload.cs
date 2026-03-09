using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Sales_InPreload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Usua_Vendedor",
                table: "Precargue_Despacho",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Precargue_Despacho_Usua_Vendedor",
                table: "Precargue_Despacho",
                column: "Usua_Vendedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Precargue_Despacho_Usuarios_Usua_Vendedor",
                table: "Precargue_Despacho",
                column: "Usua_Vendedor",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Precargue_Despacho_Usuarios_Usua_Vendedor",
                table: "Precargue_Despacho");

            migrationBuilder.DropIndex(
                name: "IX_Precargue_Despacho_Usua_Vendedor",
                table: "Precargue_Despacho");

            migrationBuilder.DropColumn(
                name: "Usua_Vendedor",
                table: "Precargue_Despacho");
        }
    }
}
