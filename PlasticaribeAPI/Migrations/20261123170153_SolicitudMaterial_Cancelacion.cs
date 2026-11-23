using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SolicitudMaterial_Cancelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SolMpExt_FechaCancelacion",
                table: "Solicitud_MatPrimaExtrusion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SolMpExt_MotivoCancelacion",
                table: "Solicitud_MatPrimaExtrusion",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Usua_IdCancela",
                table: "Solicitud_MatPrimaExtrusion",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_MatPrimaExtrusion_Usua_IdCancela",
                table: "Solicitud_MatPrimaExtrusion",
                column: "Usua_IdCancela");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitud_MatPrimaExtrusion_Usuarios_Usua_IdCancela",
                table: "Solicitud_MatPrimaExtrusion",
                column: "Usua_IdCancela",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitud_MatPrimaExtrusion_Usuarios_Usua_IdCancela",
                table: "Solicitud_MatPrimaExtrusion");

            migrationBuilder.DropIndex(
                name: "IX_Solicitud_MatPrimaExtrusion_Usua_IdCancela",
                table: "Solicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "SolMpExt_FechaCancelacion",
                table: "Solicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "SolMpExt_MotivoCancelacion",
                table: "Solicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "Usua_IdCancela",
                table: "Solicitud_MatPrimaExtrusion");
        }
    }
}
