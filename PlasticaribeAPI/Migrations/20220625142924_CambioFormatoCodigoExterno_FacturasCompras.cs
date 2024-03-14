using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class CambioFormatoCodigoExterno_FacturasCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Facco_Codigo",
                table: "Facturas_Compras",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Facco_Codigo",
                table: "Facturas_Compras",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
