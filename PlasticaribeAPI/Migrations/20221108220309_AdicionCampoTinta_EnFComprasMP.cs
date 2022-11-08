using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionCampoTinta_EnFComprasMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Facturas_Compras_Facco_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Materias_Primas_MatPri_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacturasCompras_MateriaPrimas",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.AddColumn<long>(
                name: "Facco_Codigo",
                table: "FacturasCompras_MateriaPrimas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Tinta_Id",
                table: "FacturasCompras_MateriaPrimas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacturasCompras_MateriaPrimas",
                table: "FacturasCompras_MateriaPrimas",
                column: "Facco_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasCompras_MateriaPrimas_Facco_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "Facco_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasCompras_MateriaPrimas_Tinta_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Facturas_Compras_Facco_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "Facco_Id",
                principalTable: "Facturas_Compras",
                principalColumn: "Facco_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Materias_Primas_MatPri_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Tintas_Tinta_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Facturas_Compras_Facco_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Materias_Primas_MatPri_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Tintas_Tinta_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacturasCompras_MateriaPrimas",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropIndex(
                name: "IX_FacturasCompras_MateriaPrimas_Facco_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropIndex(
                name: "IX_FacturasCompras_MateriaPrimas_Tinta_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropColumn(
                name: "Facco_Codigo",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.DropColumn(
                name: "Tinta_Id",
                table: "FacturasCompras_MateriaPrimas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacturasCompras_MateriaPrimas",
                table: "FacturasCompras_MateriaPrimas",
                columns: new[] { "Facco_Id", "MatPri_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Facturas_Compras_Facco_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "Facco_Id",
                principalTable: "Facturas_Compras",
                principalColumn: "Facco_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturasCompras_MateriaPrimas_Materias_Primas_MatPri_Id",
                table: "FacturasCompras_MateriaPrimas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
