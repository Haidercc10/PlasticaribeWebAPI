using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Entradas_Salidas_MP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo_Entrada",
                table: "Entradas_Salidas_MP",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
               name: "Codigo_Entrada",
               table: "Entradas_Salidas_MP",
               type: "int",
               nullable: false,
               defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Registro",
                table: "Entradas_Salidas_MP",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Hora_Registro",
                table: "Entradas_Salidas_MP",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                 name: "IX_Entradas_Salidas_MP_Tipo_Entrada",
                 table: "Entradas_Salidas_MP",
                 column: "Tipo_Entrada");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Salidas_MP_Tipos_Documentos_Tipo_Entrada",
                table: "Entradas_Salidas_MP",
                column: "Tipo_Entrada",
                principalTable: "Tipos_Documentos",
                principalColumn: "TpDoc_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Salidas_MP_Tipos_Documentos_Tipo_Entrada",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Salidas_MP_Tipo_Entrada",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Codigo_Entrada",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Fecha_Registro",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Hora_Registro",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Tipo_Entrada",
                table: "Entradas_Salidas_MP");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
