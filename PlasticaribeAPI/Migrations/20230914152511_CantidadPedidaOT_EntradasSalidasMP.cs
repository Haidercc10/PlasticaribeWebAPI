using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CantidadPedidaOT_EntradasSalidasMP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cant_PedidaOT",
                table: "Entradas_Salidas_MP",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Id",
                table: "Entradas_Salidas_MP",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "Kg");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Salidas_MP_UndMed_Id",
                table: "Entradas_Salidas_MP",
                column: "UndMed_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Salidas_MP_Unidades_Medidas_UndMed_Id",
                table: "Entradas_Salidas_MP",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Salidas_MP_Unidades_Medidas_UndMed_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Salidas_MP_UndMed_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Cant_PedidaOT",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "UndMed_Id",
                table: "Entradas_Salidas_MP");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
