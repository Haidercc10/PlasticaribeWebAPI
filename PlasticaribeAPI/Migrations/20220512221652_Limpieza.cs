using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Limpieza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*  migrationBuilder.DropForeignKey(
                  name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                  table: "Clientes");

              migrationBuilder.DropForeignKey(
                  name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                  table: "Clientes");

              migrationBuilder.DropForeignKey(
                  name: "FK_Clientes_Usuarios_Usua_Id",
                  table: "Clientes");

              migrationBuilder.DropForeignKey(
                  name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                  table: "Empresas");

              migrationBuilder.DropForeignKey(
                  name: "FK_Sedes_Clientes_Clientes_Cli_Id1",
                  table: "Sedes_Clientes"); */

            migrationBuilder.DropForeignKey(
                name: "FK_Tipos_Bodegas_Areas_Area_Id",
                table: "Tipos_Bodegas");

            /* migrationBuilder.DropTable(
                 name: "Existencia_Producto"); */

            /* migrationBuilder.RenameColumn(
                 name: "Cli_Id1",
                 table: "Sedes_Clientes",
                 newName: "Cli_Id");

             migrationBuilder.RenameIndex(
                 name: "IX_Sedes_Clientes_Cli_Id1",
                 table: "Sedes_Clientes",
                 newName: "IX_Sedes_Clientes_Cli_Id");

             migrationBuilder.RenameColumn(
                 name: "Usua_Id",
                 table: "Clientes",
                 newName: "usua_Id");

             migrationBuilder.RenameIndex(
                 name: "IX_Clientes_Usua_Id",
                 table: "Clientes",
                 newName: "IX_Clientes_usua_Id");

             migrationBuilder.AddForeignKey(
                 name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                 table: "Clientes",
                 column: "TipoIdentificacion_Id",
                 principalTable: "TipoIdentificaciones",
                 principalColumn: "TipoIdentificacion_Id",
                 onDelete: ReferentialAction.Restrict);

             migrationBuilder.AddForeignKey(
                 name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                 table: "Clientes",
                 column: "TPCli_Id",
                 principalTable: "Tipos_Clientes",
                 principalColumn: "TPCli_Id",
                 onDelete: ReferentialAction.Restrict);

             migrationBuilder.AddForeignKey(
                 name: "FK_Clientes_Usuarios_usua_Id",
                 table: "Clientes",
                 column: "usua_Id",
                 principalTable: "Usuarios",
                 principalColumn: "Usua_Id",
                 onDelete: ReferentialAction.Restrict);

             migrationBuilder.AddForeignKey(
                 name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                 table: "Empresas",
                 column: "TipoIdentificacion_Id",
                 principalTable: "TipoIdentificaciones",
                 principalColumn: "TipoIdentificacion_Id",
                 onDelete: ReferentialAction.Restrict);

             migrationBuilder.AddForeignKey(
                 name: "FK_Sedes_Clientes_Clientes_Cli_Id",
                 table: "Sedes_Clientes",
                 column: "Cli_Id",
                 principalTable: "Clientes",
                 principalColumn: "Cli_Id",
                 onDelete: ReferentialAction.Restrict); */

            migrationBuilder.AddForeignKey(
                name: "FK_Tipos_Bodegas_Areas_Area_Id",
                table: "Tipos_Bodegas",
                column: "Area_Id",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                 table: "Clientes");

             migrationBuilder.DropForeignKey(
                 name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                 table: "Clientes");

             migrationBuilder.DropForeignKey(
                 name: "FK_Clientes_Usuarios_usua_Id",
                 table: "Clientes");

             migrationBuilder.DropForeignKey(
                 name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                 table: "Empresas");

             migrationBuilder.DropForeignKey(
                 name: "FK_Sedes_Clientes_Clientes_Cli_Id",
                 table: "Sedes_Clientes"); */

            migrationBuilder.DropForeignKey(
                name: "FK_Tipos_Bodegas_Areas_Area_Id",
                table: "Tipos_Bodegas");

            /*  migrationBuilder.RenameColumn(
                  name: "Cli_Id",
                  table: "Sedes_Clientes",
                  newName: "Cli_Id1");

              migrationBuilder.RenameIndex(
                  name: "IX_Sedes_Clientes_Cli_Id",
                  table: "Sedes_Clientes",
                  newName: "IX_Sedes_Clientes_Cli_Id1");

              migrationBuilder.RenameColumn(
                  name: "usua_Id",
                  table: "Clientes",
                  newName: "Usua_Id");

              migrationBuilder.RenameIndex(
                  name: "IX_Clientes_usua_Id",
                  table: "Clientes",
                  newName: "IX_Clientes_Usua_Id"); */

            /* migrationBuilder.CreateTable(
                 name: "Existencia_Producto",
                 columns: table => new
                 {
                     ExProd_Id = table.Column<long>(type: "bigint", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     Prod_Id = table.Column<int>(type: "int", nullable: false),
                     TpBod_Id = table.Column<int>(type: "int", nullable: false),
                     UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                     ExProd_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                 });

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
                 column: "UndMed_Id"); */

            /*  migrationBuilder.AddForeignKey(
                  name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                  table: "Clientes",
                  column: "TipoIdentificacion_Id",
                  principalTable: "TipoIdentificaciones",
                  principalColumn: "TipoIdentificacion_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                  table: "Clientes",
                  column: "TPCli_Id",
                  principalTable: "Tipos_Clientes",
                  principalColumn: "TPCli_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Clientes_Usuarios_Usua_Id",
                  table: "Clientes",
                  column: "Usua_Id",
                  principalTable: "Usuarios",
                  principalColumn: "Usua_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                  table: "Empresas",
                  column: "TipoIdentificacion_Id",
                  principalTable: "TipoIdentificaciones",
                  principalColumn: "TipoIdentificacion_Id",
                  onDelete: ReferentialAction.Cascade);

              migrationBuilder.AddForeignKey(
                  name: "FK_Sedes_Clientes_Clientes_Cli_Id1",
                  table: "Sedes_Clientes",
                  column: "Cli_Id1",
                  principalTable: "Clientes",
                  principalColumn: "Cli_Id",
                  onDelete: ReferentialAction.Cascade);*/

            migrationBuilder.AddForeignKey(
                name: "FK_Tipos_Bodegas_Areas_Area_Id",
                table: "Tipos_Bodegas",
                column: "Area_Id",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
