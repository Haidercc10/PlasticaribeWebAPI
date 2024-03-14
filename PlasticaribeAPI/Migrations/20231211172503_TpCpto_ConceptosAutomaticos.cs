using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class TpCpto_ConceptosAutomaticos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TpCcpto_Id",
                table: "Conceptos_Automaticos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Conceptos_Automaticos_TpCcpto_Id",
                table: "Conceptos_Automaticos",
                column: "TpCcpto_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conceptos_Automaticos_Tipos_Conceptos_TpCcpto_Id",
                table: "Conceptos_Automaticos",
                column: "TpCcpto_Id",
                principalTable: "Tipos_Conceptos",
                principalColumn: "TpCcpto_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conceptos_Automaticos_Tipos_Conceptos_TpCcpto_Id",
                table: "Conceptos_Automaticos");

            migrationBuilder.DropIndex(
                name: "IX_Conceptos_Automaticos_TpCcpto_Id",
                table: "Conceptos_Automaticos");

            migrationBuilder.DropColumn(
                name: "TpCcpto_Id",
                table: "Conceptos_Automaticos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
