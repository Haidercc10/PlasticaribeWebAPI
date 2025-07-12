using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDistinctFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Asesor_Id",
                table: "OrdenFacturacion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExProd_Existencias",
                table: "Existencias_Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExProd_UndEmpaque",
                table: "Existencias_Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OT",
                table: "Detalles_OrdenFacturacion",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Peso_Bruto",
                table: "Detalles_OrdenFacturacion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Peso_Neto",
                table: "Detalles_OrdenFacturacion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Detalles_OrdenFacturacion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenFacturacion_Asesor_Id",
                table: "OrdenFacturacion",
                column: "Asesor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenFacturacion_Usuarios_Asesor_Id",
                table: "OrdenFacturacion",
                column: "Asesor_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenFacturacion_Usuarios_Asesor_Id",
                table: "OrdenFacturacion");

            migrationBuilder.DropIndex(
                name: "IX_OrdenFacturacion_Asesor_Id",
                table: "OrdenFacturacion");

            migrationBuilder.DropColumn(
                name: "Asesor_Id",
                table: "OrdenFacturacion");

            migrationBuilder.DropColumn(
                name: "ExProd_Existencias",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "ExProd_UndEmpaque",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "OT",
                table: "Detalles_OrdenFacturacion");

            migrationBuilder.DropColumn(
                name: "Peso_Bruto",
                table: "Detalles_OrdenFacturacion");

            migrationBuilder.DropColumn(
                name: "Peso_Neto",
                table: "Detalles_OrdenFacturacion");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Detalles_OrdenFacturacion");
        }
    }
}
