using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class PalletId_InEntryAndOF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Pallet_Id",
                table: "EntradasRollos_Productos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Pallet_Id",
                table: "DetallesEntradasRollos_Productos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Pallet_Id",
                table: "Detalles_OrdenFacturacion",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pallet_Id",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Pallet_Id",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Pallet_Id",
                table: "Detalles_OrdenFacturacion");
        }
    }
}
