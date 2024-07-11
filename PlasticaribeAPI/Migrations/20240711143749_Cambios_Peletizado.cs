using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cambios_Peletizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UndMed_Id",
                table: "Salidas_Peletizado",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "Kg");

            migrationBuilder.AddColumn<long>(
                name: "Usua_Aprueba",
                table: "Salidas_Peletizado",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IngPel_Codigo",
                table: "Ingreso_Peletizado",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Peletizado_UndMed_Id",
                table: "Salidas_Peletizado",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Peletizado_Usua_Aprueba",
                table: "Salidas_Peletizado",
                column: "Usua_Aprueba");

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Peletizado_Unidades_Medidas_UndMed_Id",
                table: "Salidas_Peletizado",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Peletizado_Usuarios_Usua_Aprueba",
                table: "Salidas_Peletizado",
                column: "Usua_Aprueba",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Peletizado_Unidades_Medidas_UndMed_Id",
                table: "Salidas_Peletizado");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Peletizado_Usuarios_Usua_Aprueba",
                table: "Salidas_Peletizado");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Peletizado_UndMed_Id",
                table: "Salidas_Peletizado");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Peletizado_Usua_Aprueba",
                table: "Salidas_Peletizado");

            migrationBuilder.DropColumn(
                name: "UndMed_Id",
                table: "Salidas_Peletizado");

            migrationBuilder.DropColumn(
                name: "Usua_Aprueba",
                table: "Salidas_Peletizado");

            migrationBuilder.DropColumn(
                name: "IngPel_Codigo",
                table: "Ingreso_Peletizado");
        }
    }
}
