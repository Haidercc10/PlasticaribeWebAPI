using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class EstadoOTEnDetallesAsigBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_BOPP_Estado_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP",
                column: "Estado_OrdenTrabajo");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_BOPP_Estados_Estado_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP",
                column: "Estado_OrdenTrabajo",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_BOPP_Estados_Estado_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.DropIndex(
                name: "IX_DetallesAsignaciones_BOPP_Estado_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.DropColumn(
                name: "Estado_OrdenTrabajo",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.AlterColumn<int>(
                name: "Estado_OrdenTrabajo",
                table: "Asignaciones_MatPrima",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
