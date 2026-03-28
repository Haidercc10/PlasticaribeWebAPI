using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsInProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prod_Embobinado",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_FuelleDer",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_FuelleFondo",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_FuelleIzq",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prod_Impresion",
                table: "Productos",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Prod_Laminado",
                table: "Productos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_Solapa",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TpImpresion_Id",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tratado_Id",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UndMedCalibre",
                table: "Productos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TpImpresion_Id",
                table: "Productos",
                column: "TpImpresion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Tratado_Id",
                table: "Productos",
                column: "Tratado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UndMedCalibre",
                table: "Productos",
                column: "UndMedCalibre");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tipos_Impresion_TpImpresion_Id",
                table: "Productos",
                column: "TpImpresion_Id",
                principalTable: "Tipos_Impresion",
                principalColumn: "TpImpresion_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tratado_Tratado_Id",
                table: "Productos",
                column: "Tratado_Id",
                principalTable: "Tratado",
                principalColumn: "Tratado_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Unidades_Medidas_UndMedCalibre",
                table: "Productos",
                column: "UndMedCalibre",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tipos_Impresion_TpImpresion_Id",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tratado_Tratado_Id",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Unidades_Medidas_UndMedCalibre",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_TpImpresion_Id",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Tratado_Id",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_UndMedCalibre",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Embobinado",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_FuelleDer",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_FuelleFondo",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_FuelleIzq",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Impresion",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Laminado",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Solapa",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TpImpresion_Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Tratado_Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UndMedCalibre",
                table: "Productos");
        }
    }
}
