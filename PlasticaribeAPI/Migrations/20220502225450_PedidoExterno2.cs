using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class PedidoExterno2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos_Externos",
                columns: table => new
                {
                    PedExt_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedExt_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    PedExt_FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PedExt_FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Empresa_Id = table.Column<long>(type: "bigint", nullable: false),
                    SedeCliente = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    PedExt_Observacion = table.Column<string>(type: "text", nullable: false),
                    PedExt_PrecioTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PedExt_Archivo = table.Column<byte[]>(type: "binary(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos_Externos", x => x.PedExt_Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Externos_Empresas_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresas",
                        principalColumn: "Empresa_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Externos_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Externos_Sedes_Clientes_SedeCliente",
                        column: x => x.SedeCliente,
                        principalTable: "Sedes_Clientes",
                        principalColumn: "SedeCli_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Externos_Empresa_Id",
                table: "Pedidos_Externos",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Externos_Estado_Id",
                table: "Pedidos_Externos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Externos_SedeCliente",
                table: "Pedidos_Externos",
                column: "SedeCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos_Externos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
