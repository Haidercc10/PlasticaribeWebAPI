using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class RelacionMtMPedidosProductos8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidosExternos_Productos",
                columns: table => new
                {
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    PedExt_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosExternos_Productos", x => new { x.Prod_Id, x.PedExt_Id });
                    table.ForeignKey(
                        name: "FK_PedidosExternos_Productos_Pedidos_Externos_PedExt_Id",
                        column: x => x.PedExt_Id,
                        principalTable: "Pedidos_Externos",
                        principalColumn: "PedExt_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosExternos_Productos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosExternos_Productos_PedExt_Id",
                table: "PedidosExternos_Productos",
                column: "PedExt_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosExternos_Productos");
        }
    }
}
