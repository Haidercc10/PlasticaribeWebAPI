using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Change_Facturas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Bopp_Id",
                table: "Remisiones_MateriasPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Rem_Hora",
                table: "Remisiones",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Bopp_Id",
                table: "FacturasCompras_MateriaPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Facco_Hora",
                table: "Facturas_Compras",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_MateriasPrimas_Bopp_Id",
                table: "Remisiones_MateriasPrimas",
                column: "Bopp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasCompras_MateriaPrimas_Bopp_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "Bopp_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Bopp_Generico_Bopp_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "Bopp_Id",
                principalTable: "Bopp_Generico",
                principalColumn: "BoppGen_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Bopp_Generico_Bopp_Id",
                table: "Remisiones_MateriasPrimas",
                column: "Bopp_Id",
                principalTable: "Bopp_Generico",
                principalColumn: "BoppGen_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Bopp_Generico_Bopp_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_Remisiones_MateriasPrimas_Bopp_Generico_Bopp_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_Remisiones_MateriasPrimas_Bopp_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_FacturasCompras_MateriaPrimas_Bopp_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropColumn(
                name: "Bopp_Id",
                table: "Remisiones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "Rem_Hora",
                table: "Remisiones");

            migrationBuilder.DropColumn(
                name: "Bopp_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropColumn(
                name: "Facco_Hora",
                table: "Facturas_Compras");
        }
    }
}
