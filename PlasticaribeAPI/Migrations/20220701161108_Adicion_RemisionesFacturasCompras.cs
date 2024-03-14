using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_RemisionesFacturasCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remisiones_FacturasCompras",
                columns: table => new
                {
                    Rem_Id = table.Column<int>(type: "int", nullable: false),
                    Facco_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remisiones_FacturasCompras", x => new { x.Facco_Id, x.Rem_Id });
                    table.ForeignKey(
                        name: "FK_Remisiones_FacturasCompras_Facturas_Compras_Facco_Id",
                        column: x => x.Facco_Id,
                        principalTable: "Facturas_Compras",
                        principalColumn: "Facco_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remisiones_FacturasCompras_Remisiones_Rem_Id",
                        column: x => x.Rem_Id,
                        principalTable: "Remisiones",
                        principalColumn: "Rem_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remisiones_FacturasCompras_Rem_Id",
                table: "Remisiones_FacturasCompras",
                column: "Rem_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remisiones_FacturasCompras");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
