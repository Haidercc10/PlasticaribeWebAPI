using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CantidadMaterialSaliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MatPri_Id",
                table: "Entradas_Salidas_MP",
                type: "bigint",
                nullable: false,
                defaultValue: 84);

            migrationBuilder.AddColumn<long>(
                name: "Tinta_Id",
                table: "Entradas_Salidas_MP",
                type: "bigint",
                nullable: false,
                defaultValue: 2001);

            migrationBuilder.AddColumn<long>(
                name: "Bopp_Id",
                table: "Entradas_Salidas_MP",
                type: "bigint",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad_Salida",
                table: "Entradas_Salidas_MP",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Salidas_MP_Bopp_Id",
                table: "Entradas_Salidas_MP",
                column: "Bopp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Salidas_MP_MatPri_Id",
                table: "Entradas_Salidas_MP",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Salidas_MP_Tinta_Id",
                table: "Entradas_Salidas_MP",
                column: "Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Salidas_MP_Bopp_Generico_Bopp_Id",
                table: "Entradas_Salidas_MP",
                column: "Bopp_Id",
                principalTable: "Bopp_Generico",
                principalColumn: "BoppGen_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Salidas_MP_Materias_Primas_MatPri_Id",
                table: "Entradas_Salidas_MP",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Salidas_MP_Tintas_Tinta_Id",
                table: "Entradas_Salidas_MP",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Salidas_MP_Bopp_Generico_Bopp_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Salidas_MP_Materias_Primas_MatPri_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Salidas_MP_Tintas_Tinta_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Salidas_MP_Bopp_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Salidas_MP_MatPri_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Salidas_MP_Tinta_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Bopp_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Cantidad_Salida",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "MatPri_Id",
                table: "Entradas_Salidas_MP");

            migrationBuilder.DropColumn(
                name: "Tinta_Id",
                table: "Entradas_Salidas_MP");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
