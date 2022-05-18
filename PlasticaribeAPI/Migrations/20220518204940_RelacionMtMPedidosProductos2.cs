using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class RelacionMtMPedidosProductos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Pedidos_Productos",
                columns: table => new
                {
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    PedExt_Id = table.Column<long>(type: "bigint", nullable: false),
                   
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos_Productos", x => new { x.Prod_Id, x.PedExt_Id });
                    table.ForeignKey(
                        name: "FK_Pedidos_Productos_Pedidos_Externos_PedExt_Id",
                        column: x => x.PedExt_Id,
                        principalTable: "Pedidos_Externos",
                        principalColumn: "PedExt_Id");
                    table.ForeignKey(
                        name: "FK_Pedidos_Productos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Productos_PedExt_Id",
                table: "Pedidos_Productos",
                column: "PedExt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Productos_Prod_Id",
                table: "Pedidos_Productos",
                column: "Prod_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos_Productos");

           
        }
    }
}
