using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Estado_DetallesOrdenFacturacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "Detalles_OrdenFacturacion",
                type: "int",
                nullable: false,
                defaultValue: 19);

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_OrdenFacturacion_Estado_Id",
                table: "Detalles_OrdenFacturacion",
                column: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalles_OrdenFacturacion_Estados_Estado_Id",
                table: "Detalles_OrdenFacturacion",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalles_OrdenFacturacion_Estados_Estado_Id",
                table: "Detalles_OrdenFacturacion");

            migrationBuilder.DropIndex(
                name: "IX_Detalles_OrdenFacturacion_Estado_Id",
                table: "Detalles_OrdenFacturacion");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "Detalles_OrdenFacturacion");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
