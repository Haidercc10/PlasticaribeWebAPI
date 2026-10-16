using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangesAutorizacionesExcesoOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autorizaciones_ExcesoOT_Asignaciones_MatPrima_AsignacionMatPrimaId",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "FechaConsumoUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "FechaRespuestaUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "FechaSolicitudUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.RenameColumn(
                name: "OrdenTrabajo",
                table: "Autorizaciones_ExcesoOT",
                newName: "AutEx_OrdenTrabajo");

            migrationBuilder.RenameColumn(
                name: "MotivoRespuesta",
                table: "Autorizaciones_ExcesoOT",
                newName: "AutEx_MotivoRespuesta");

            migrationBuilder.RenameColumn(
                name: "Motivo",
                table: "Autorizaciones_ExcesoOT",
                newName: "AutEx_Motivo");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Autorizaciones_ExcesoOT",
                newName: "Estado_Id");

            migrationBuilder.RenameColumn(
                name: "CantidadExcesoKg",
                table: "Autorizaciones_ExcesoOT",
                newName: "AutEx_CantidadKg");

            migrationBuilder.RenameColumn(
                name: "AsignacionMatPrimaId",
                table: "Autorizaciones_ExcesoOT",
                newName: "AsigMp_Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Autorizaciones_ExcesoOT",
                newName: "AutEx_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Autorizaciones_ExcesoOT_OrdenTrabajo_Estado",
                table: "Autorizaciones_ExcesoOT",
                newName: "IX_Autorizaciones_ExcesoOT_AutEx_OrdenTrabajo_Estado_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Autorizaciones_ExcesoOT_AsignacionMatPrimaId",
                table: "Autorizaciones_ExcesoOT",
                newName: "IX_Autorizaciones_ExcesoOT_AsigMp_Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "AutEx_FechaConsumoUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AutEx_FechaRespuestaUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AutEx_FechaSolicitudUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_ExcesoOT_Estado_Id",
                table: "Autorizaciones_ExcesoOT",
                column: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Autorizaciones_ExcesoOT_Asignaciones_MatPrima_AsigMp_Id",
                table: "Autorizaciones_ExcesoOT",
                column: "AsigMp_Id",
                principalTable: "Asignaciones_MatPrima",
                principalColumn: "AsigMp_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Autorizaciones_ExcesoOT_Estados_Estado_Id",
                table: "Autorizaciones_ExcesoOT",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autorizaciones_ExcesoOT_Asignaciones_MatPrima_AsigMp_Id",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropForeignKey(
                name: "FK_Autorizaciones_ExcesoOT_Estados_Estado_Id",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropIndex(
                name: "IX_Autorizaciones_ExcesoOT_Estado_Id",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "AutEx_FechaConsumoUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "AutEx_FechaRespuestaUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.DropColumn(
                name: "AutEx_FechaSolicitudUtc",
                table: "Autorizaciones_ExcesoOT");

            migrationBuilder.RenameColumn(
                name: "Estado_Id",
                table: "Autorizaciones_ExcesoOT",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "AutEx_OrdenTrabajo",
                table: "Autorizaciones_ExcesoOT",
                newName: "OrdenTrabajo");

            migrationBuilder.RenameColumn(
                name: "AutEx_MotivoRespuesta",
                table: "Autorizaciones_ExcesoOT",
                newName: "MotivoRespuesta");

            migrationBuilder.RenameColumn(
                name: "AutEx_Motivo",
                table: "Autorizaciones_ExcesoOT",
                newName: "Motivo");

            migrationBuilder.RenameColumn(
                name: "AutEx_CantidadKg",
                table: "Autorizaciones_ExcesoOT",
                newName: "CantidadExcesoKg");

            migrationBuilder.RenameColumn(
                name: "AsigMp_Id",
                table: "Autorizaciones_ExcesoOT",
                newName: "AsignacionMatPrimaId");

            migrationBuilder.RenameColumn(
                name: "AutEx_Id",
                table: "Autorizaciones_ExcesoOT",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Autorizaciones_ExcesoOT_AutEx_OrdenTrabajo_Estado_Id",
                table: "Autorizaciones_ExcesoOT",
                newName: "IX_Autorizaciones_ExcesoOT_OrdenTrabajo_Estado");

            migrationBuilder.RenameIndex(
                name: "IX_Autorizaciones_ExcesoOT_AsigMp_Id",
                table: "Autorizaciones_ExcesoOT",
                newName: "IX_Autorizaciones_ExcesoOT_AsignacionMatPrimaId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaConsumoUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRespuestaUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitudUtc",
                table: "Autorizaciones_ExcesoOT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Autorizaciones_ExcesoOT_Asignaciones_MatPrima_AsignacionMatPrimaId",
                table: "Autorizaciones_ExcesoOT",
                column: "AsignacionMatPrimaId",
                principalTable: "Asignaciones_MatPrima",
                principalColumn: "AsigMp_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
