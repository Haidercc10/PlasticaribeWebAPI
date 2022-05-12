using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CreateExistenciaProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Existencias_Productos",
                 columns: table => new
                 {
                     ExProd_Id = table.Column<long>(type: "bigint", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     Prod_Id = table.Column<int>(type: "int", nullable: false),
                     ExProd_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                     TpBod_Id = table.Column<int>(type: "int", nullable: false),
                     UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                     ExProd_Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                     ExProd_PrecioExistencia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                     ExProd_PrecioSinInflacion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                     ExProd_PrecioTotalFinal = table.Column<string>(type: "nvarchar(max)", precision: 18, scale: 2, nullable: true),
                     TpMoneda_Id = table.Column<int>(type: "varchar(50)", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Existencias_Productos", x => x.ExProd_Id);
                     table.ForeignKey(
                         name: "FK_Existencias_Productos_Productos_Prod_Id",
                         column: x => x.Prod_Id,
                         principalTable: "Productos",
                         principalColumn: "Prod_Id",
                         onDelete: ReferentialAction.Cascade);
                     table.ForeignKey(
                         name: "FK_Existencias_Productos_Tipos_Bodegas_TpBod_Id",
                         column: x => x.TpBod_Id,
                         principalTable: "Tipos_Bodegas",
                         principalColumn: "TpBod_Id",
                         onDelete: ReferentialAction.Cascade);
                     table.ForeignKey(
                         name: "FK_Existencias_Productos_Unidades_Medidas_UndMed_Id",
                         column: x => x.UndMed_Id,
                         principalTable: "Unidades_Medidas",
                         principalColumn: "UndMed_Id",
                         onDelete: ReferentialAction.Cascade);
                     table.ForeignKey(
                         name: "FK_Existencias_Productos_Tipos_Monedas_TpMoneda_Id",
                         column: x => x.TpMoneda_Id,
                         principalTable: "Tipos_Monedas",
                         principalColumn: "TpMoneda_Id",
                         onDelete: ReferentialAction.Cascade);
                 });

                    migrationBuilder.CreateIndex(
                       name: "IX_Existencias_Productos_Prod_Id",
                       table: "Existencias_Productos",
                       column: "Prod_Id");

                    migrationBuilder.CreateIndex(
                        name: "IX_Existencias_Productos_TpBod_Id",
                        table: "Existencias_Productos",
                        column: "TpBod_Id");

                    migrationBuilder.CreateIndex(
                        name: "IX_Existencias_Productos_UndMed_Id",
                        table: "Existencias_Productos",
                        column: "UndMed_Id");

                    migrationBuilder.CreateIndex(
                       name: "IX_Existencias_Productos_TpMoneda_Id",
                       table: "Existencias_Productos",
                       column: "TpMoneda_Id");


            /* migrationBuilder.DropForeignKey(
                name: "FK_Existencia_Producto_Productos_Prod_Id",
                table: "Existencia_Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencia_Producto_Tipos_Bodegas_TpBod_Id",
                table: "Existencia_Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencia_Producto_Unidades_Medidas_UndMed_Id",
                table: "Existencia_Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Existencia_Producto",
                table: "Existencia_Producto");

            migrationBuilder.RenameTable(
                name: "Existencia_Producto",
                newName: "Existencias_Productos");

            migrationBuilder.RenameIndex(
                name: "IX_Existencia_Producto_UndMed_Id",
                table: "Existencias_Productos",
                newName: "IX_Existencias_Productos_UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencia_Producto_TpBod_Id",
                table: "Existencias_Productos",
                newName: "IX_Existencias_Productos_TpBod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencia_Producto_Prod_Id",
                table: "Existencias_Productos",
                newName: "IX_Existencias_Productos_Prod_Id");

            migrationBuilder.AlterColumn<string>(
                name: "UndMed_Id",
                table: "Existencias_Productos",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "TpBod_Id",
                table: "Existencias_Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "Prod_Id",
                table: "Existencias_Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<string>(
                name: "TpMoneda_Id",
                table: "Existencias_Productos",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Existencias_Productos",
                table: "Existencias_Productos",
                column: "ExProd_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_Productos_TpMoneda_Id",
                table: "Existencias_Productos",
                column: "TpMoneda_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Productos_Prod_Id",
                table: "Existencias_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Tipos_Bodegas_TpBod_Id",
                table: "Existencias_Productos",
                column: "TpBod_Id",
                principalTable: "Tipos_Bodegas",
                principalColumn: "TpBod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Tipos_Monedas_TpMoneda_Id",
                table: "Existencias_Productos",
                column: "TpMoneda_Id",
                principalTable: "Tipos_Monedas",
                principalColumn: "TpMoneda_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Unidades_Medidas_UndMed_Id",
                table: "Existencias_Productos",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict); */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Existencias_Productos");
            /*migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Productos_Prod_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Tipos_Bodegas_TpBod_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Tipos_Monedas_TpMoneda_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Unidades_Medidas_UndMed_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Existencias_Productos",
                table: "Existencias_Productos");

            migrationBuilder.DropIndex(
                name: "IX_Existencias_Productos_TpMoneda_Id",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "TpMoneda_Id",
                table: "Existencias_Productos");

            migrationBuilder.RenameTable(
                name: "Existencias_Productos",
                newName: "Existencia_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Existencias_Productos_UndMed_Id",
                table: "Existencia_Producto",
                newName: "IX_Existencia_Producto_UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencias_Productos_TpBod_Id",
                table: "Existencia_Producto",
                newName: "IX_Existencia_Producto_TpBod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Existencias_Productos_Prod_Id",
                table: "Existencia_Producto",
                newName: "IX_Existencia_Producto_Prod_Id");

            migrationBuilder.AlterColumn<string>(
                name: "UndMed_Id",
                table: "Existencia_Producto",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "TpBod_Id",
                table: "Existencia_Producto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "Prod_Id",
                table: "Existencia_Producto",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Existencia_Producto",
                table: "Existencia_Producto",
                column: "ExProd_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Existencia_Producto_Productos_Prod_Id",
                table: "Existencia_Producto",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencia_Producto_Tipos_Bodegas_TpBod_Id",
                table: "Existencia_Producto",
                column: "TpBod_Id",
                principalTable: "Tipos_Bodegas",
                principalColumn: "TpBod_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Existencia_Producto_Unidades_Medidas_UndMed_Id",
                table: "Existencia_Producto",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Cascade);*/
        }
    }
}
