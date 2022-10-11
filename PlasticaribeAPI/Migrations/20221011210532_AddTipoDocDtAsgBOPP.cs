using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddTipoDocDtAsgBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TpDoc_Id",
                table: "DetallesAsignaciones_BOPP",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "ASIGBOPP");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_BOPP_TpDoc_Id",
                table: "DetallesAsignaciones_BOPP",
                column: "TpDoc_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_BOPP_Tipos_Documentos_TpDoc_Id",
                table: "DetallesAsignaciones_BOPP",
                column: "TpDoc_Id",
                principalTable: "Tipos_Documentos",
                principalColumn: "TpDoc_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_BOPP_Tipos_Documentos_TpDoc_Id",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.DropIndex(
                name: "IX_DetallesAsignaciones_BOPP_TpDoc_Id",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.DropColumn(
                name: "TpDoc_Id",
                table: "DetallesAsignaciones_BOPP");
        }
    }
}
