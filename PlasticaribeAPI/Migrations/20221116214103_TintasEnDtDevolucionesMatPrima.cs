using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class TintasEnDtDevolucionesMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Devoluciones_MatPrima_DevMatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesDevoluciones_MateriasPrimas",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.AddColumn<long>(
                name: "DtDevMatPri_Codigo",
                table: "DetallesDevoluciones_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                type: "bigint",
                nullable: true,
                defaultValue: 2001);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesDevoluciones_MateriasPrimas",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "DtDevMatPri_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_DevMatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "DevMatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Devoluciones_MatPrima_DevMatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "DevMatPri_Id",
                principalTable: "Devoluciones_MatPrima",
                principalColumn: "DevMatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Tintas_Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Devoluciones_MatPrima_DevMatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Tintas_Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesDevoluciones_MateriasPrimas",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_DevMatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "DtDevMatPri_Codigo",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesDevoluciones_MateriasPrimas",
                table: "DetallesDevoluciones_MateriasPrimas",
                columns: new[] { "DevMatPri_Id", "MatPri_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Devoluciones_MatPrima_DevMatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "DevMatPri_Id",
                principalTable: "Devoluciones_MatPrima",
                principalColumn: "DevMatPri_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
