using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AditionOrderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Usua_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<long>(
                name: "UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<long>(
                name: "UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<int>(
                name: "TipoDevProdFact_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 25);

            migrationBuilder.AlterColumn<int>(
                name: "Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "FacturaVta_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_Responsable",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "DevProdFact_Reposicion",
                table: "Devoluciones_ProductosFacturados",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_ObservacionGestion",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_Observacion",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<bool>(
                name: "DevProdFact_NotaCredito",
                table: "Devoluciones_ProductosFacturados",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_HoraModificado",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_HoraFinalizado",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 23);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_Hora",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_FechaModificado",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_FechaFinalizado",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 22);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_Fecha",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date")
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<long>(
                name: "Cli_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<long>(
                name: "Asesor_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<long>(
                name: "DevProdFact_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "DevProdFact_FechaGestion",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 18);

            migrationBuilder.AddColumn<string>(
                name: "DevProdFact_HoraGestion",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 19);

            migrationBuilder.AddColumn<string>(
                name: "DevProdFact_ObservacionFinal",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 24);

            migrationBuilder.AddColumn<string>(
                name: "DevProdFact_ObservacionModificado",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 16);

            /*migrationBuilder.AddColumn<long>(
                name: "Rep_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 26);*/

            migrationBuilder.AddColumn<long>(
                name: "UsuaGestiona_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 17);

            /*migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_Rep_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Rep_Id");*/

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductosFacturados_UsuaGestiona_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "UsuaGestiona_Id");

            /*migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Reposiciones_Rep_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "Rep_Id",
                principalTable: "Reposiciones",
                principalColumn: "Rep_Id",
                onDelete: ReferentialAction.Restrict);*/

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_UsuaGestiona_Id",
                table: "Devoluciones_ProductosFacturados",
                column: "UsuaGestiona_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Reposiciones_Rep_Id",
                table: "Devoluciones_ProductosFacturados");*/

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_ProductosFacturados_Usuarios_UsuaGestiona_Id",
                table: "Devoluciones_ProductosFacturados");

            /*migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_Rep_Id",
                table: "Devoluciones_ProductosFacturados");*/

            migrationBuilder.DropIndex(
                name: "IX_Devoluciones_ProductosFacturados_UsuaGestiona_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_FechaGestion",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_HoraGestion",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_ObservacionFinal",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.DropColumn(
                name: "DevProdFact_ObservacionModificado",
                table: "Devoluciones_ProductosFacturados");

            /*migrationBuilder.DropColumn(
                name: "Rep_Id",
                table: "Devoluciones_ProductosFacturados");*/

            migrationBuilder.DropColumn(
                name: "UsuaGestiona_Id",
                table: "Devoluciones_ProductosFacturados");

            migrationBuilder.AlterColumn<long>(
                name: "Usua_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<long>(
                name: "UsuaModifica_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<long>(
                name: "UsuaFinaliza_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<int>(
                name: "TipoDevProdFact_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 25);

            migrationBuilder.AlterColumn<int>(
                name: "Id_OrdenFact",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "FacturaVta_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_Responsable",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<bool>(
                name: "DevProdFact_Reposicion",
                table: "Devoluciones_ProductosFacturados",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_ObservacionGestion",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_Observacion",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<bool>(
                name: "DevProdFact_NotaCredito",
                table: "Devoluciones_ProductosFacturados",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_HoraModificado",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_HoraFinalizado",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 23);

            migrationBuilder.AlterColumn<string>(
                name: "DevProdFact_Hora",
                table: "Devoluciones_ProductosFacturados",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_FechaModificado",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_FechaFinalizado",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 22);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DevProdFact_Fecha",
                table: "Devoluciones_ProductosFacturados",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date")
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<long>(
                name: "Cli_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<long>(
                name: "Asesor_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<long>(
                name: "DevProdFact_Id",
                table: "Devoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", 0)
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
