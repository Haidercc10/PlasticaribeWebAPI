using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class QuitarNullTipoEstadoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                table: "Estados");

            migrationBuilder.AlterColumn<int>(
                name: "TpEstado_Id",
                table: "Estados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                table: "Estados",
                column: "TpEstado_Id",
                principalTable: "Tipos_Estados",
                principalColumn: "TpEstado_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                table: "Estados");

            migrationBuilder.AlterColumn<int>(
                name: "TpEstado_Id",
                table: "Estados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                table: "Estados",
                column: "TpEstado_Id",
                principalTable: "Tipos_Estados",
                principalColumn: "TpEstado_Id");
        }
    }
}
