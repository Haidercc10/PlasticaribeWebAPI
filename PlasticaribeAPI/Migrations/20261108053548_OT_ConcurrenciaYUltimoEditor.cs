using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class OT_ConcurrenciaYUltimoEditor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Ot_ClaveSolicitud",
                table: "Orden_Trabajo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ot_FechaModifica",
                table: "Orden_Trabajo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Ot_RowVersion",
                table: "Orden_Trabajo",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Ot_UsuarioModifica",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Numero_OT",
                table: "Orden_Trabajo",
                column: "Numero_OT");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Ot_ClaveSolicitud",
                table: "Orden_Trabajo",
                column: "Ot_ClaveSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Ot_UsuarioModifica",
                table: "Orden_Trabajo",
                column: "Ot_UsuarioModifica");

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_Trabajo_Usuarios_Ot_UsuarioModifica",
                table: "Orden_Trabajo",
                column: "Ot_UsuarioModifica",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orden_Trabajo_Usuarios_Ot_UsuarioModifica",
                table: "Orden_Trabajo");

            migrationBuilder.DropIndex(
                name: "IX_Orden_Trabajo_Numero_OT",
                table: "Orden_Trabajo");

            migrationBuilder.DropIndex(
                name: "IX_Orden_Trabajo_Ot_ClaveSolicitud",
                table: "Orden_Trabajo");

            migrationBuilder.DropIndex(
                name: "IX_Orden_Trabajo_Ot_UsuarioModifica",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Ot_ClaveSolicitud",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Ot_FechaModifica",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Ot_RowVersion",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Ot_UsuarioModifica",
                table: "Orden_Trabajo");
        }
    }
}
