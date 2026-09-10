using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SubcategoriasEnSolicitudesMaterialProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetSolicitud_MatPrimaExtrusion_Materias_Primas_MatPri_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetSolicitud_MatPrimaExtrusion_Tintas_Tinta_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_MatPri_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_Tinta_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "MatPri_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "Tinta_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.AddColumn<int>(
                name: "SubCatMP_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubCatMP_Nombre",
                table: "DetSolicitud_MatPrimaExtrusion",
                type: "varchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_SubCatMP_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "SubCatMP_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetSolicitud_MatPrimaExtrusion_Subcategorias_MatPrima_SubCatMP_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "SubCatMP_Id",
                principalTable: "Subcategorias_MatPrima",
                principalColumn: "SubCatMP_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetSolicitud_MatPrimaExtrusion_Subcategorias_MatPrima_SubCatMP_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_SubCatMP_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "SubCatMP_Id",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.DropColumn(
                name: "SubCatMP_Nombre",
                table: "DetSolicitud_MatPrimaExtrusion");

            migrationBuilder.AddColumn<long>(
                name: "MatPri_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Tinta_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_MatPri_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetSolicitud_MatPrimaExtrusion_Tinta_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "Tinta_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetSolicitud_MatPrimaExtrusion_Materias_Primas_MatPri_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "MatPri_Id",
                principalTable: "Materias_Primas",
                principalColumn: "MatPri_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetSolicitud_MatPrimaExtrusion_Tintas_Tinta_Id",
                table: "DetSolicitud_MatPrimaExtrusion",
                column: "Tinta_Id",
                principalTable: "Tintas",
                principalColumn: "Tinta_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
