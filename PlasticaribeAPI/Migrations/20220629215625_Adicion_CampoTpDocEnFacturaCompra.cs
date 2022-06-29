using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Adicion_CampoTpDocEnFacturaCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TpDoc_Id",
                table: "Facturas_Compras",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Compras_TpDoc_Id",
                table: "Facturas_Compras",
                column: "TpDoc_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Compras_Tipos_Documentos_TpDoc_Id",
                table: "Facturas_Compras",
                column: "TpDoc_Id",
                principalTable: "Tipos_Documentos",
                principalColumn: "TpDoc_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Compras_Tipos_Documentos_TpDoc_Id",
                table: "Facturas_Compras");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_Compras_TpDoc_Id",
                table: "Facturas_Compras");

            migrationBuilder.DropColumn(
                name: "TpDoc_Id",
                table: "Facturas_Compras");
        }
    }
}
