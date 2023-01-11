using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPedidoEstadoProcesosOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EstProcOT_Pedido",
                table: "Estados_ProcesosOT",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstProcOT_Pedido",
                table: "Estados_ProcesosOT");
        }
    }
}
