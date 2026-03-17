using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Changes_FinishedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpresionDobleCara",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "PedExtProd_Impresion",
                table: "Producto_Terminado");

            migrationBuilder.RenameColumn(
                name: "Pt_Fuelle",
                table: "Producto_Terminado",
                newName: "Pt_Solapa");

            migrationBuilder.RenameColumn(
                name: "PedExtProd_Tratado",
                table: "Producto_Terminado",
                newName: "Pt_Laminado");

            migrationBuilder.RenameColumn(
                name: "PedExtProd_NroEmbobinado",
                table: "Producto_Terminado",
                newName: "Tratado_Id");

            migrationBuilder.RenameColumn(
                name: "PedExtProd_Calibre",
                table: "Producto_Terminado",
                newName: "Pt_Calibre");

            migrationBuilder.AddColumn<decimal>(
                name: "Pt_FuelleDer",
                table: "Producto_Terminado",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Pt_FuelleFondo",
                table: "Producto_Terminado",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Pt_FuelleIzq",
                table: "Producto_Terminado",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pt_ImpresionFD",
                table: "Producto_Terminado",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Pt_NroEmbobinado",
                table: "Producto_Terminado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_ALF",
                table: "Producto_Terminado",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Calibre",
                table: "Producto_Terminado",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_Tratado_Id",
                table: "Producto_Terminado",
                column: "Tratado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_UndMed_ALF",
                table: "Producto_Terminado",
                column: "UndMed_ALF");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Terminado_UndMed_Calibre",
                table: "Producto_Terminado",
                column: "UndMed_Calibre");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Terminado_Tratado_Tratado_Id",
                table: "Producto_Terminado",
                column: "Tratado_Id",
                principalTable: "Tratado",
                principalColumn: "Tratado_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Terminado_Unidades_Medidas_UndMed_ALF",
                table: "Producto_Terminado",
                column: "UndMed_ALF",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Terminado_Unidades_Medidas_UndMed_Calibre",
                table: "Producto_Terminado",
                column: "UndMed_Calibre",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Terminado_Tratado_Tratado_Id",
                table: "Producto_Terminado");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Terminado_Unidades_Medidas_UndMed_ALF",
                table: "Producto_Terminado");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Terminado_Unidades_Medidas_UndMed_Calibre",
                table: "Producto_Terminado");

            migrationBuilder.DropIndex(
                name: "IX_Producto_Terminado_Tratado_Id",
                table: "Producto_Terminado");

            migrationBuilder.DropIndex(
                name: "IX_Producto_Terminado_UndMed_ALF",
                table: "Producto_Terminado");

            migrationBuilder.DropIndex(
                name: "IX_Producto_Terminado_UndMed_Calibre",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "Pt_FuelleDer",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "Pt_FuelleFondo",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "Pt_FuelleIzq",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "Pt_ImpresionFD",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "Pt_NroEmbobinado",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "UndMed_ALF",
                table: "Producto_Terminado");

            migrationBuilder.DropColumn(
                name: "UndMed_Calibre",
                table: "Producto_Terminado");

            migrationBuilder.RenameColumn(
                name: "Tratado_Id",
                table: "Producto_Terminado",
                newName: "PedExtProd_NroEmbobinado");

            migrationBuilder.RenameColumn(
                name: "Pt_Solapa",
                table: "Producto_Terminado",
                newName: "Pt_Fuelle");

            migrationBuilder.RenameColumn(
                name: "Pt_Laminado",
                table: "Producto_Terminado",
                newName: "PedExtProd_Tratado");

            migrationBuilder.RenameColumn(
                name: "Pt_Calibre",
                table: "Producto_Terminado",
                newName: "PedExtProd_Calibre");

            migrationBuilder.AddColumn<bool>(
                name: "ImpresionDobleCara",
                table: "Producto_Terminado",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PedExtProd_Impresion",
                table: "Producto_Terminado",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
