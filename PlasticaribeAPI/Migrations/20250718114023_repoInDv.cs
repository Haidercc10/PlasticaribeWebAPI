using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class repoInDv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Reposiciones_Rep_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_Rep_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "Rep_Id",
                table: "Devoluciones_ProductosFacturados");*/

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_FechaGestion",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_FechaGestion",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true,
               // defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

         /*   migrationBuilder.AddColumn<long>(
                name: "Rep_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 26);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Rep_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Rep_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Reposiciones_Rep_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Rep_Id",
                principalTable: "Reposiciones",
                principalColumn: "Rep_Id",
                onDelete: ReferentialAction.Restrict);*/
        }
    }
}
