using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Tinta_IdEnASGMPXTINTA4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Asignaciones_MatPrimasXTintas_AsigMPxTinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignaciones_MatPrimasXTintas",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            /*migrationBuilder.AlterColumn<DateTime>(
                name: "RecMp_FechaEntrega",
                table: "Recuperados_MatPrima",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");*/

            migrationBuilder.AddColumn<long>(
                name: "DtAsigMPxTinta_Codigo",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "Tinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignaciones_MatPrimasXTintas",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "DtAsigMPxTinta_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MatPrimasXTintas_AsigMPxTinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "AsigMPxTinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsignaciones_MatPrimasXTintas_Tinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Asignaciones_MatPrimasXTintas_AsigMPxTinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "AsigMPxTinta_Id",
                principalTable: "Asignaciones_MatPrimasXTintas",
                principalColumn: "AsigMPxTinta_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Tintas_Tinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Asignaciones_MatPrimasXTintas_AsigMPxTinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Tintas_Tinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesAsignaciones_MatPrimasXTintas",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesAsignaciones_MatPrimasXTintas_AsigMPxTinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesAsignaciones_MatPrimasXTintas_Tinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropColumn(
                name: "DtAsigMPxTinta_Codigo",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            migrationBuilder.DropColumn(
                name: "Tinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas");

            /*migrationBuilder.AlterColumn<DateTime>(
                name: "RecMp_FechaEntrega",
                table: "Recuperados_MatPrima",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);*/

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesAsignaciones_MatPrimasXTintas",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                columns: new[] { "AsigMPxTinta_Id", "MatPri_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Asignaciones_MatPrimasXTintas_AsigMPxTinta_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "AsigMPxTinta_Id",
                principalTable: "Asignaciones_MatPrimasXTintas",
                principalColumn: "AsigMPxTinta_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsignaciones_MatPrimasXTintas_Materias_Primas_MatPri_Id",
                table: "DetallesAsignaciones_MatPrimasXTintas",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
