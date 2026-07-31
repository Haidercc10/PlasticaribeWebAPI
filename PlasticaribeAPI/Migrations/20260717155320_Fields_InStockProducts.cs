using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fields_InStockProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExProd_Asesor",
                table: "Existencias_Productos",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExProd_Cliente",
                table: "Existencias_Productos",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Usua_Asesor",
                table: "Existencias_Productos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_Productos_Usua_Asesor",
                table: "Existencias_Productos",
                column: "Usua_Asesor");

            migrationBuilder.AddForeignKey(
                name: "FK_Existencias_Productos_Usuarios_Usua_Asesor",
                table: "Existencias_Productos",
                column: "Usua_Asesor",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Existencias_Productos_Usuarios_Usua_Asesor",
                table: "Existencias_Productos");

            migrationBuilder.DropIndex(
                name: "IX_Existencias_Productos_Usua_Asesor",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "ExProd_Asesor",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "ExProd_Cliente",
                table: "Existencias_Productos");

            migrationBuilder.DropColumn(
                name: "Usua_Asesor",
                table: "Existencias_Productos");
        }
    }
}
