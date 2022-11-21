using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class campoBOPPEnDevolMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BOPP_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_BOPP_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "BOPP_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_BOPP_BOPP_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "BOPP_Id",
                principalTable: "BOPP",
                principalColumn: "BOPP_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_BOPP_BOPP_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_BOPP_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "BOPP_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.AlterColumn<long>(
                name: "Tinta_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
