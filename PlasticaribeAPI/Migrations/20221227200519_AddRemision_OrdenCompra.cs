using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class AddRemisionOrdenCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remision_OrdenCompra",
                columns: table => new
                {
                    OcId = table.Column<long>(name: "Oc_Id", type: "bigint", nullable: false),
                    RemId = table.Column<int>(name: "Rem_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remision_OrdenCompra", x => new { x.OcId, x.RemId });
                    table.ForeignKey(
                        name: "FK_Remision_OrdenCompra_Ordenes_Compras_Oc_Id",
                        column: x => x.OcId,
                        principalTable: "Ordenes_Compras",
                        principalColumn: "Oc_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remision_OrdenCompra_Remisiones_Rem_Id",
                        column: x => x.RemId,
                        principalTable: "Remisiones",
                        principalColumn: "Rem_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remision_OrdenCompra_Rem_Id",
                table: "Remision_OrdenCompra",
                column: "Rem_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remision_OrdenCompra");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
