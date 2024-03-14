using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Cambio_DataContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Estados_Estado_Id",
                table: "NominaDetallada_Plasticaribe");

            migrationBuilder.DropForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Tipos_Nomina_Estado_Nomina",
                table: "NominaDetallada_Plasticaribe");

            migrationBuilder.DropIndex(
                name: "IX_NominaDetallada_Plasticaribe_Estado_Id",
                table: "NominaDetallada_Plasticaribe");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "NominaDetallada_Plasticaribe");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetallada_Plasticaribe_TipoNomina",
                table: "NominaDetallada_Plasticaribe",
                column: "TipoNomina");

            migrationBuilder.AddForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Estados_Estado_Nomina",
                table: "NominaDetallada_Plasticaribe",
                column: "Estado_Nomina",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Tipos_Nomina_TipoNomina",
                table: "NominaDetallada_Plasticaribe",
                column: "TipoNomina",
                principalTable: "Tipos_Nomina",
                principalColumn: "TpNomina_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Estados_Estado_Nomina",
                table: "NominaDetallada_Plasticaribe");

            migrationBuilder.DropForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Tipos_Nomina_TipoNomina",
                table: "NominaDetallada_Plasticaribe");

            migrationBuilder.DropIndex(
                name: "IX_NominaDetallada_Plasticaribe_TipoNomina",
                table: "NominaDetallada_Plasticaribe");

            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "NominaDetallada_Plasticaribe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetallada_Plasticaribe_Estado_Id",
                table: "NominaDetallada_Plasticaribe",
                column: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Estados_Estado_Id",
                table: "NominaDetallada_Plasticaribe",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NominaDetallada_Plasticaribe_Tipos_Nomina_Estado_Nomina",
                table: "NominaDetallada_Plasticaribe",
                column: "Estado_Nomina",
                principalTable: "Tipos_Nomina",
                principalColumn: "TpNomina_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
