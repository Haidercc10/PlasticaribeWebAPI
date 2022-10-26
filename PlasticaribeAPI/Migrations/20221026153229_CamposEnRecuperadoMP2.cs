using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CamposEnRecuperadoMP2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecMp_FechaEntrega",
                table: "Recuperados_MatPrima",
                type: "date",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RecMp_HoraIngreso",
                table: "Recuperados_MatPrima",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "00:00:00")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<int>(
                name: "RecMp_Maquina",
                table: "Recuperados_MatPrima",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Turno_Id",
                table: "Recuperados_MatPrima",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "NE");

            migrationBuilder.AddColumn<long>(
                name: "Usua_Operador",
                table: "Recuperados_MatPrima",
                type: "bigint",
                nullable: false,
                defaultValue: 112);

            migrationBuilder.CreateIndex(
                name: "IX_Recuperados_MatPrima_Turno_Id",
                table: "Recuperados_MatPrima",
                column: "Turno_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recuperados_MatPrima_Usua_Operador",
                table: "Recuperados_MatPrima",
                column: "Usua_Operador");

            migrationBuilder.AddForeignKey(
                name: "FK_Recuperados_MatPrima_Turnos_Turno_Id",
                table: "Recuperados_MatPrima",
                column: "Turno_Id",
                principalTable: "Turnos",
                principalColumn: "Turno_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recuperados_MatPrima_Usuarios_Usua_Operador",
                table: "Recuperados_MatPrima",
                column: "Usua_Operador",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recuperados_MatPrima_Turnos_Turno_Id",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropForeignKey(
                name: "FK_Recuperados_MatPrima_Usuarios_Usua_Operador",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropIndex(
                name: "IX_Recuperados_MatPrima_Turno_Id",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropIndex(
                name: "IX_Recuperados_MatPrima_Usua_Operador",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropColumn(
                name: "RecMp_FechaEntrega",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropColumn(
                name: "RecMp_HoraIngreso",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropColumn(
                name: "RecMp_Maquina",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropColumn(
                name: "Turno_Id",
                table: "Recuperados_MatPrima");

            migrationBuilder.DropColumn(
                name: "Usua_Operador",
                table: "Recuperados_MatPrima");
        }
    }
}
