using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FieldsInSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PedExt_DireccionEntrega",
                table: "Pedidos_Externos",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PedExt_Oc",
                table: "Pedidos_Externos",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedExt_DireccionEntrega",
                table: "Pedidos_Externos");

            migrationBuilder.DropColumn(
                name: "PedExt_Oc",
                table: "Pedidos_Externos");
        }
    }
}
