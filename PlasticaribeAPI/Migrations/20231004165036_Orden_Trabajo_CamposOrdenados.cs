using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Orden_Trabajo_CamposOrdenados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Ot_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "SedeCli_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "Prod_Id",
                table: "Orden_Trabajo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "UndMed_Id",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_CantidadPedida",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_ValorKg",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_ValorUnidad",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_PesoNetoKg",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_MargenAdicional",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_ValorOT",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<bool>(
                name: "Extrusion",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<bool>(
                name: "Impresion",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<bool>(
                name: "Rotograbado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<bool>(
                name: "Laminado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<bool>(
                name: "Ot_Corte",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<bool>(
                name: "Sellado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<bool>(
                name: "Ot_Cyrel",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<long>(
                name: "Mezcla_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<long>(
                name: "Usua_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ot_FechaCreacion",
                table: "Orden_Trabajo",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date")
                .Annotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<string>(
                name: "Ot_Hora",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Orden_Trabajo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<long>(
                name: "Id_Vendedor",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 22);

            migrationBuilder.AlterColumn<long>(
                name: "PedExt_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 23);

            migrationBuilder.AlterColumn<string>(
                name: "Ot_Observacion",
                table: "Orden_Trabajo",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)")
                .Annotation("Relational:ColumnOrder", 24);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Ot_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "SedeCli_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "Prod_Id",
                table: "Orden_Trabajo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "UndMed_Id",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_CantidadPedida",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_ValorKg",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_ValorUnidad",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_PesoNetoKg",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_MargenAdicional",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot_ValorOT",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<bool>(
                name: "Extrusion",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<bool>(
                name: "Impresion",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<bool>(
                name: "Rotograbado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<bool>(
                name: "Laminado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<bool>(
                name: "Ot_Corte",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<bool>(
                name: "Sellado",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<bool>(
                name: "Ot_Cyrel",
                table: "Orden_Trabajo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<long>(
                name: "Mezcla_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<long>(
                name: "Usua_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ot_FechaCreacion",
                table: "Orden_Trabajo",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date")
                .Annotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<string>(
                name: "Ot_Hora",
                table: "Orden_Trabajo",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 20);

            migrationBuilder.AlterColumn<int>(
                name: "Estado_Id",
                table: "Orden_Trabajo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 21);

            migrationBuilder.AlterColumn<long>(
                name: "Id_Vendedor",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 22);

            migrationBuilder.AlterColumn<long>(
                name: "PedExt_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 23);

            migrationBuilder.AlterColumn<string>(
                name: "Ot_Observacion",
                table: "Orden_Trabajo",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)")
                .Annotation("Relational:ColumnOrder", 24);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
