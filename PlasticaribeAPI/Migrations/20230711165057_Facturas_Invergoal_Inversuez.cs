using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Facturas_Invergoal_Inversuez : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas_Invergoal_Inversuez",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Registro = table.Column<DateTime>(type: "date", nullable: false),
                    Hora_Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Nit_Empresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre_Empresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo_Factura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nit_Proveedor = table.Column<long>(type: "bigint", nullable: false),
                    Fecha_Factura = table.Column<DateTime>(type: "date", nullable: false),
                    Fecha_Vencimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Valor_Factura = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Cuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado_Factura = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas_Invergoal_Inversuez", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Invergoal_Inversuez_Estados_Estado_Factura",
                        column: x => x.Estado_Factura,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Invergoal_Inversuez_Proveedores_Nit_Proveedor",
                        column: x => x.Nit_Proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Prov_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Invergoal_Inversuez_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Invergoal_Inversuez_Estado_Factura",
                table: "Facturas_Invergoal_Inversuez",
                column: "Estado_Factura");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Invergoal_Inversuez_Nit_Proveedor",
                table: "Facturas_Invergoal_Inversuez",
                column: "Nit_Proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Invergoal_Inversuez_Usua_Id",
                table: "Facturas_Invergoal_Inversuez",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas_Invergoal_Inversuez");
        }
    }
}
