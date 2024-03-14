using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class EstadoOTEnAsigMatPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_MatPrima_Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima",
                column: "Estado_OrdenTrabajo");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_MatPrima_Estados_Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima",
                column: "Estado_OrdenTrabajo",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_MatPrima_Estados_Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_MatPrima_Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima");

            migrationBuilder.DropColumn(
                name: "Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
