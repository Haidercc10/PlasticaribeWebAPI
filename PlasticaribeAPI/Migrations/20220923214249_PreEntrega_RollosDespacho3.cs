using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class PreEntrega_RollosDespacho3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Id",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropForeignKey(
                name: "FK_PreEntrega_RollosDespacho_Clientes_Cli_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropForeignKey(
                name: "FK_PreEntrega_RollosDespacho_Productos_Prod_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropForeignKey(
                name: "FK_PreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropIndex(
                name: "IX_PreEntrega_RollosDespacho_Cli_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropIndex(
                name: "IX_PreEntrega_RollosDespacho_Prod_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropIndex(
                name: "IX_PreEntrega_RollosDespacho_UndMed_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "Cli_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "PreEntRollo_OT",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "UndMed_Id",
                table: "PreEntrega_RollosDespacho");

            migrationBuilder.RenameColumn(
                name: "UndMed_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                newName: "UndMed_Rollo");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_UndMed_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                newName: "IX_DetallesPreEntrega_RollosDespacho_UndMed_Rollo");

            migrationBuilder.AddColumn<long>(
                name: "Cli_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<long>(
                name: "DtlPreEntRollo_OT",
                table: "DetallesPreEntrega_RollosDespacho",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Producto",
                table: "DetallesPreEntrega_RollosDespacho",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 4);
            
            migrationBuilder.CreateIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_Cli_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_Prod_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_UndMed_Producto",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "UndMed_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Clientes_Cli_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "Cli_Id",
                principalTable: "Clientes",
                principalColumn: "Cli_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Productos_Prod_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Producto",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "UndMed_Producto",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Rollo",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "UndMed_Rollo",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Clientes_Cli_Id",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Productos_Prod_Id",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Producto",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Rollo",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_Cli_Id",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_Prod_Id",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_UndMed_Producto",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "Cli_Id",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "DtlPreEntRollo_OT",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "UndMed_Producto",
                table: "DetallesPreEntrega_RollosDespacho");

            migrationBuilder.DropColumn(
                name: "AsigProdFV_FechaEnvio",
                table: "AsignacionesProductos_FacturasVentas");

            migrationBuilder.RenameColumn(
                name: "UndMed_Rollo",
                table: "DetallesPreEntrega_RollosDespacho",
                newName: "UndMed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPreEntrega_RollosDespacho_UndMed_Rollo",
                table: "DetallesPreEntrega_RollosDespacho",
                newName: "IX_DetallesPreEntrega_RollosDespacho_UndMed_Id");

            migrationBuilder.AddColumn<long>(
                name: "Cli_Id",
                table: "PreEntrega_RollosDespacho",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PreEntRollo_OT",
                table: "PreEntrega_RollosDespacho",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "PreEntrega_RollosDespacho",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UndMed_Id",
                table: "PreEntrega_RollosDespacho",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PreEntrega_RollosDespacho_Cli_Id",
                table: "PreEntrega_RollosDespacho",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreEntrega_RollosDespacho_Prod_Id",
                table: "PreEntrega_RollosDespacho",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreEntrega_RollosDespacho_UndMed_Id",
                table: "PreEntrega_RollosDespacho",
                column: "UndMed_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Id",
                table: "DetallesPreEntrega_RollosDespacho",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreEntrega_RollosDespacho_Clientes_Cli_Id",
                table: "PreEntrega_RollosDespacho",
                column: "Cli_Id",
                principalTable: "Clientes",
                principalColumn: "Cli_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreEntrega_RollosDespacho_Productos_Prod_Id",
                table: "PreEntrega_RollosDespacho",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreEntrega_RollosDespacho_Unidades_Medidas_UndMed_Id",
                table: "PreEntrega_RollosDespacho",
                column: "UndMed_Id",
                principalTable: "Unidades_Medidas",
                principalColumn: "UndMed_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
