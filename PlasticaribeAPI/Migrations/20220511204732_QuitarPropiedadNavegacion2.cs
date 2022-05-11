using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class QuitarPropiedadNavegacion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                 table: "Estados"); */

            migrationBuilder.RenameColumn(
                name: "TpEstado_Id1",
                table: "Estados",
                newName: "TpEstado_Id");

            /* migrationBuilder.RenameIndex(
                 name: "IX_Estados_TpEstado_Id1",
                 table: "Estados",
                 newName: "IX_Estados_TpEstado_Id"); */

            /*migrationBuilder.AddForeignKey(
                 name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                 table: "Estados",
                 column: "TpEstado_Id",
                 principalTable: "Tipos_Estados",
                 principalColumn: "TpEstado_Id",
                 onDelete: ReferentialAction.Restrict);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_Estados_Tipos_Estados_TpEstado_Id",
                 table: "Estados");*/

            migrationBuilder.RenameColumn(
                name: "TpEstado_Id",
                table: "Estados",
                newName: "TpEstado_Id1");

            /* migrationBuilder.RenameIndex(
                 name: "IX_Estados_TpEstado_Id",
                 table: "Estados",
                 newName: "IX_Estados_TpEstado_Id1"); */

            /*migrationBuilder.AddForeignKey(
                name: "FK_Estados_Tipos_Estados_TpEstado_Id1",
                table: "Estados",
                column: "TpEstado_Id1",
                principalTable: "Tipos_Estados",
                principalColumn: "TpEstado_Id",
                onDelete: ReferentialAction.Restrict);*/
        }
    }
}