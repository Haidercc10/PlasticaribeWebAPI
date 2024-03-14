using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class ProcesoEnDetalleDevolucion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Proceso_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_Proceso_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "Proceso_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Procesos_Proceso_Id",
                table: "DetallesDevoluciones_MateriasPrimas",
                column: "Proceso_Id",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesDevoluciones_MateriasPrimas_Procesos_Proceso_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesDevoluciones_MateriasPrimas_Proceso_Id",
                table: "DetallesDevoluciones_MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "Proceso_Id",
                table: "DetallesDevoluciones_MateriasPrimas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
