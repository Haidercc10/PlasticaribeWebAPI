using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class CambiosEntradaRollos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Unidades_Medidas_UndMed_Id",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradasRollos_Productos_Productos_Prod_Id",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradasRollos_Productos_Unidades_Medidas_UndMed_Id",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_EntradasRollos_Productos_Prod_Id",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_EntradasRollos_Productos_UndMed_Id",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "EntRolloProd_OT",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "EntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "UndMed_Id",
                table: "EntradasRollos_Productos");

            migrationBuilder.RenameColumn(
                name: "UndMed_Id",
                table: "DetallesEntradasRollos_Productos",
                newName: "UndMed_Rollo");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesEntradasRollos_Productos_UndMed_Id",
                table: "DetallesEntradasRollos_Productos",
                newName: "IX_DetallesEntradasRollos_Productos_UndMed_Rollo");

            migrationBuilder.AddColumn<long>(
                name: "DtEntRolloProd_OT",
                table: "DetallesEntradasRollos_Productos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "DetallesEntradasRollos_Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Prod",
                table: "DetallesEntradasRollos_Productos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesEntradasRollos_Productos_Prod_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesEntradasRollos_Productos_UndMed_Prod",
                table: "DetallesEntradasRollos_Productos",
                column: "UndMed_Prod");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Productos_Prod_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Unidades_Medidas_UndMed_Prod",
                table: "DetallesEntradasRollos_Productos",
                column: "UndMed_Prod",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Unidades_Medidas_UndMed_Rollo",
                table: "DetallesEntradasRollos_Productos",
                column: "UndMed_Rollo",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Productos_Prod_Id",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Unidades_Medidas_UndMed_Prod",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Unidades_Medidas_UndMed_Rollo",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_DetallesEntradasRollos_Productos_Prod_Id",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_DetallesEntradasRollos_Productos_UndMed_Prod",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "DtEntRolloProd_OT",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropColumn(
                name: "UndMed_Prod",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.RenameColumn(
                name: "UndMed_Rollo",
                table: "DetallesEntradasRollos_Productos",
                newName: "UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesEntradasRollos_Productos_UndMed_Rollo",
                table: "DetallesEntradasRollos_Productos",
                newName: "IX_DetallesEntradasRollos_Productos_UndMed_Id");

            migrationBuilder.AddColumn<long>(
                name: "EntRolloProd_OT",
                table: "EntradasRollos_Productos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "EntradasRollos_Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Id",
                table: "EntradasRollos_Productos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasRollos_Productos_Prod_Id",
                table: "EntradasRollos_Productos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasRollos_Productos_UndMed_Id",
                table: "EntradasRollos_Productos",
                column: "UndMed_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Unidades_Medidas_UndMed_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasRollos_Productos_Productos_Prod_Id",
                table: "EntradasRollos_Productos",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasRollos_Productos_Unidades_Medidas_UndMed_Id",
                table: "EntradasRollos_Productos",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
