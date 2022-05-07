using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class adicionTiposEstados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "Usuarios",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Id",
                table: "Clientes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Existencia_Producto",
                columns: table => new
                {
                    ExProd_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false),
                    ExProd_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    ExProd_Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExProd_PrecioExistencia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExProd_PrecioSinInflacion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExProd_PrecioTotalFinal = table.Column<string>(type: "nvarchar(max)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Existencia_Producto", x => x.ExProd_Id);
                    table.ForeignKey(
                        name: "FK_Existencia_Producto_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Existencia_Producto_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Existencia_Producto_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Cascade);
                });*/

            migrationBuilder.CreateTable(
                name: "Tipos_Estados",
                columns: table => new
                {
                    TpEstado_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpEstado_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    TpEstado_Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Estados", x => x.TpEstado_Id);
                });

            /*migrationBuilder.CreateTable(
                name: "Tipos_Monedas",
                columns: table => new
                {
                    TpMoneda_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    TpMoneda_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpMoneda_Nombre = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Monedas", x => x.TpMoneda_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Usua_Id",
                table: "Clientes",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Existencia_Producto_Prod_Id",
                table: "Existencia_Producto",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Existencia_Producto_TpBod_Id",
                table: "Existencia_Producto",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Existencia_Producto_UndMed_Id",
                table: "Existencia_Producto",
                column: "UndMed_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_Usua_Id",
                table: "Clientes",
                column: "Usua_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Cascade);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_Usua_Id",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Existencia_Producto");*/

            migrationBuilder.DropTable(
                name: "Tipos_Estados");

            /*migrationBuilder.DropTable(
                name: "Tipos_Monedas");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_Usua_Id",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Usua_Id",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "Usuarios",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id");*/
        }
    }
}
