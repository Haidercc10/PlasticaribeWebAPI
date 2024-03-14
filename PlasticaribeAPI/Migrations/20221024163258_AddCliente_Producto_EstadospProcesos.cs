using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AddCliente_Producto_EstadospProcesos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Cli_Id",
                table: "Estados_ProcesosOT",
                type: "bigint",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "Estados_ProcesosOT",
                type: "int",
                nullable: false,
                defaultValue: 991);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ProcesosOT_Cli_Id",
                table: "Estados_ProcesosOT",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ProcesosOT_Prod_Id",
                table: "Estados_ProcesosOT",
                column: "Prod_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_ProcesosOT_Clientes_Cli_Id",
                table: "Estados_ProcesosOT",
                column: "Cli_Id",
                principalTable: "Clientes",
                principalColumn: "Cli_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_ProcesosOT_Productos_Prod_Id",
                table: "Estados_ProcesosOT",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_ProcesosOT_Clientes_Cli_Id",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropForeignKey(
                name: "FK_Estados_ProcesosOT_Productos_Prod_Id",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ProcesosOT_Cli_Id",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ProcesosOT_Prod_Id",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "Cli_Id",
                table: "Estados_ProcesosOT");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "Estados_ProcesosOT");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
