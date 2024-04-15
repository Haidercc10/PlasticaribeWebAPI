using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AggProvIdInBOPP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Prov_Id",
                table: "BOPP",
                type: "bigint",
                nullable: false,
                defaultValue: 800188732);

            migrationBuilder.CreateIndex(
                name: "IX_BOPP_Prov_Id",
                table: "BOPP",
                column: "Prov_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BOPP_Proveedores_Prov_Id",
                table: "BOPP",
                column: "Prov_Id",
                principalTable: "Proveedores",
                principalColumn: "Prov_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOPP_Proveedores_Prov_Id",
                table: "BOPP");

            migrationBuilder.DropIndex(
                name: "IX_BOPP_Prov_Id",
                table: "BOPP");

            migrationBuilder.DropColumn(
                name: "Prov_Id",
                table: "BOPP");
        }
    }
}
