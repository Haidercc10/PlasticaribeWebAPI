using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsInClassLoans2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ptm_Hora",
                table: "Prestamos",
                newName: "Ptm_HoraRegistro");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ptm_FechaUltCuota",
                table: "Prestamos",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "Ptm_FechaRegistro",
                table: "Prestamos",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 15);

            migrationBuilder.AddColumn<string>(
                name: "Ptm_LapsoCuotas",
                table: "Prestamos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 17);

            migrationBuilder.AddColumn<int>(
                name: "Ptm_NroCuotas",
                table: "Prestamos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ptm_FechaRegistro",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "Ptm_LapsoCuotas",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "Ptm_NroCuotas",
                table: "Prestamos");

            migrationBuilder.RenameColumn(
                name: "Ptm_HoraRegistro",
                table: "Prestamos",
                newName: "Ptm_Hora");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ptm_FechaUltCuota",
                table: "Prestamos",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
