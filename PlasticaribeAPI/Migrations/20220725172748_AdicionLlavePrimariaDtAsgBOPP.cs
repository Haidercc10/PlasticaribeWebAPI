using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionLlavePrimariaDtAsgBOPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignaciones_BOPP",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.AddColumn<long>(
                name: "DtAsigBOPP_Id",
                table: "DetallesAsignaciones_BOPP",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignaciones_BOPP",
                table: "DetallesAsignaciones_BOPP",
                column: "DtAsigBOPP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_BOPP_AsigBOPP_Id",
                table: "DetallesAsignaciones_BOPP",
                column: "AsigBOPP_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignaciones_BOPP",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.DropIndex(
                name: "IX_DetallesAsignaciones_BOPP_AsigBOPP_Id",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.DropColumn(
                name: "DtAsigBOPP_Id",
                table: "DetallesAsignaciones_BOPP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignaciones_BOPP",
                table: "DetallesAsignaciones_BOPP",
                columns: new[] { "AsigBOPP_Id", "BOPP_Id" });
        }
    }
}
