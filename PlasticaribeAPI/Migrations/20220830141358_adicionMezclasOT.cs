using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class adicionMezclasOT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Mezcla_Id",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Mezcla_Id",
                table: "Orden_Trabajo",
                column: "Mezcla_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_Trabajo_Mezclas_Mezcla_Id",
                table: "Orden_Trabajo",
                column: "Mezcla_Id",
                principalTable: "Mezclas",
                principalColumn: "Mezcla_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orden_Trabajo_Mezclas_Mezcla_Id",
                table: "Orden_Trabajo");

            migrationBuilder.DropIndex(
                name: "IX_Orden_Trabajo_Mezcla_Id",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Mezcla_Id",
                table: "Orden_Trabajo");
        }
    }
}
