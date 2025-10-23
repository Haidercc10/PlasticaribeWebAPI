using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class ControlCalidadExtrusion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CcExt_AlDardo",
                table: "ControlCalidad_Extrusion",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CcExt_Brillo",
                table: "ControlCalidad_Extrusion",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre1",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre10",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre11",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre12",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre13",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre14",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre15",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre16",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre2",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre3",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre4",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre5",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre6",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre7",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre8",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CcExt_Calibre9",
                table: "ControlCalidad_Extrusion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CcExt_Geles",
                table: "ControlCalidad_Extrusion",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CcExt_Quemado",
                table: "ControlCalidad_Extrusion",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CcExt_AlDardo",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Brillo",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre1",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre10",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre11",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre12",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre13",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre14",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre15",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre16",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre2",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre3",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre4",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre5",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre6",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre7",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre8",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Calibre9",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Geles",
                table: "ControlCalidad_Extrusion");

            migrationBuilder.DropColumn(
                name: "CcExt_Quemado",
                table: "ControlCalidad_Extrusion");
        }
    }
}
