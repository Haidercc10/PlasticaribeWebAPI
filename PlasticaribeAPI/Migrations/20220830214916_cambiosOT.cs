using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class cambiosOT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OT_Impresion_Pistas_Pista_Id",
                table: "OT_Impresion");

            migrationBuilder.DropForeignKey(
                name: "FK_OT_Impresion_Rodillos_Rodillo_Id",
                table: "OT_Impresion");

            migrationBuilder.DropForeignKey(
                name: "FK_OT_Impresion_Tintas_Tinta8Tinta_Id",
                table: "OT_Impresion");

            migrationBuilder.DropIndex(
                name: "IX_OT_Impresion_Pista_Id",
                table: "OT_Impresion");

            migrationBuilder.DropIndex(
                name: "IX_OT_Impresion_Rodillo_Id",
                table: "OT_Impresion");

            migrationBuilder.DropIndex(
                name: "IX_OT_Impresion_Tinta8Tinta_Id",
                table: "OT_Impresion");

            migrationBuilder.DropColumn(
                name: "Tinta8Tinta_Id",
                table: "OT_Impresion");

            migrationBuilder.AlterColumn<decimal>(
                name: "Extrusion_Calibre",
                table: "OT_Extrusion",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Extrusion_Ancho3",
                table: "OT_Extrusion",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Extrusion_Ancho2",
                table: "OT_Extrusion",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Extrusion_Ancho1",
                table: "OT_Extrusion",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Id",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta7_Id",
                table: "OT_Impresion",
                column: "Tinta7_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_UndMed_Id",
                table: "Orden_Trabajo",
                column: "UndMed_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_Trabajo_Unidades_Medidas_UndMed_Id",
                table: "Orden_Trabajo",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OT_Impresion_Tintas_Tinta7_Id",
                table: "OT_Impresion",
                column: "Tinta7_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orden_Trabajo_Unidades_Medidas_UndMed_Id",
                table: "Orden_Trabajo");

            migrationBuilder.DropForeignKey(
                name: "FK_OT_Impresion_Tintas_Tinta7_Id",
                table: "OT_Impresion");

            migrationBuilder.DropIndex(
                name: "IX_OT_Impresion_Tinta7_Id",
                table: "OT_Impresion");

            migrationBuilder.DropIndex(
                name: "IX_Orden_Trabajo_UndMed_Id",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "UndMed_Id",
                table: "Orden_Trabajo");

            migrationBuilder.AddColumn<long>(
                name: "Tinta8Tinta_Id",
                table: "OT_Impresion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Extrusion_Calibre",
                table: "OT_Extrusion",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Extrusion_Ancho3",
                table: "OT_Extrusion",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Extrusion_Ancho2",
                table: "OT_Extrusion",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Extrusion_Ancho1",
                table: "OT_Extrusion",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Pista_Id",
                table: "OT_Impresion",
                column: "Pista_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Rodillo_Id",
                table: "OT_Impresion",
                column: "Rodillo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta8Tinta_Id",
                table: "OT_Impresion",
                column: "Tinta8Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OT_Impresion_Pistas_Pista_Id",
                table: "OT_Impresion",
                column: "Pista_Id",
                principalTable: "Pistas",
                principalColumn: "Pista_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OT_Impresion_Rodillos_Rodillo_Id",
                table: "OT_Impresion",
                column: "Rodillo_Id",
                principalTable: "Rodillos",
                principalColumn: "Rodillo_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OT_Impresion_Tintas_Tinta8Tinta_Id",
                table: "OT_Impresion",
                column: "Tinta8Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id");
        }
    }
}
