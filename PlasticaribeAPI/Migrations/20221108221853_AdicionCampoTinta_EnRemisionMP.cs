using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionCampoTinta_EnRemisionMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Remisiones_Rem_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remisiones_MateriasPrimas",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.AddColumn<long>(
                name: "RemiMatPri_Codigo",
                table: "Remisiones_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Tinta_Id",
                table: "Remisiones_MateriasPrimas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Tinta_Id",
                table: "FacturasCompras_MateriaPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remisiones_MateriasPrimas",
                table: "Remisiones_MateriasPrimas",
                column: "RemiMatPri_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_MateriasPrimas_Rem_Id",
                table: "Remisiones_MateriasPrimas",
                column: "Rem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_MateriasPrimas_Tinta_Id",
                table: "Remisiones_MateriasPrimas",
                column: "Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Remisiones_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Remisiones_Rem_Id",
                table: "Remisiones_MateriasPrimas",
                column: "Rem_Id",
                principalTable: "Remisiones",
                principalColumn: "Rem_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Tintas_Tinta_Id",
                table: "Remisiones_MateriasPrimas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Remisiones_Rem_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Tintas_Tinta_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Remisiones_MateriasPrimas",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_Remisiones_MateriasPrimas_Rem_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_Remisiones_MateriasPrimas_Tinta_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "RemiMatPri_Codigo",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "Tinta_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.AlterColumn<long>(
                name: "Tinta_Id",
                table: "FacturasCompras_MateriaPrimas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Remisiones_MateriasPrimas",
                table: "Remisiones_MateriasPrimas",
                columns: new[] { "Rem_Id", "MatPri_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Materias_Primas_MatPri_Id",
                table: "Remisiones_MateriasPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Remisiones_Rem_Id",
                table: "Remisiones_MateriasPrimas",
                column: "Rem_Id",
                principalTable: "Remisiones",
                principalColumn: "Rem_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
