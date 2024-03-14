using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class AdicionSolicitudes_Asignaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SolMpExt_Id",
                table: "Asignaciones_MatPrima",
                type: "bigint",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_SolMpExt_Id",
                table: "Asignaciones_MatPrima",
                column: "SolMpExt_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_MatPrima_Solicitud_MatPrimaExtrusion_SolMpExt_Id",
                table: "Asignaciones_MatPrima",
                column: "SolMpExt_Id",
                principalTable: "Solicitud_MatPrimaExtrusion",
                principalColumn: "SolMpExt_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_MatPrima_Solicitud_MatPrimaExtrusion_SolMpExt_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_MatPrima_SolMpExt_Id",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "SolMpExt_Id",
                table: "Asignaciones_MatPrima");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
