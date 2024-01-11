using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConceptosAutomaticos_Proveedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReteFuente",
                table: "Proveedores",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ReteICA",
                table: "Proveedores",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ReteIVA",
                table: "Proveedores",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_ReteFuente",
                table: "Proveedores",
                column: "ReteFuente");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_ReteICA",
                table: "Proveedores",
                column: "ReteICA");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_ReteIVA",
                table: "Proveedores",
                column: "ReteIVA");

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_Conceptos_Automaticos_ReteFuente",
                table: "Proveedores",
                column: "ReteFuente",
                principalTable: "Conceptos_Automaticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_Conceptos_Automaticos_ReteICA",
                table: "Proveedores",
                column: "ReteICA",
                principalTable: "Conceptos_Automaticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_Conceptos_Automaticos_ReteIVA",
                table: "Proveedores",
                column: "ReteIVA",
                principalTable: "Conceptos_Automaticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_Conceptos_Automaticos_ReteFuente",
                table: "Proveedores");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_Conceptos_Automaticos_ReteICA",
                table: "Proveedores");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_Conceptos_Automaticos_ReteIVA",
                table: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Proveedores_ReteFuente",
                table: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Proveedores_ReteICA",
                table: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Proveedores_ReteIVA",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "ReteFuente",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "ReteICA",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "ReteIVA",
                table: "Proveedores");
        }
    }
}
