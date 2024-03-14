using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Estado_VistasPermisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Vp_Estado",
                table: "Vistas_Permisos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Vistas_Permisos_Vp_Estado",
                table: "Vistas_Permisos",
                column: "Vp_Estado");

            migrationBuilder.AddForeignKey(
                name: "FK_Vistas_Permisos_Estados_Vp_Estado",
                table: "Vistas_Permisos",
                column: "Vp_Estado",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vistas_Permisos_Estados_Vp_Estado",
                table: "Vistas_Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Vistas_Permisos_Vp_Estado",
                table: "Vistas_Permisos");

            migrationBuilder.DropColumn(
                name: "Vp_Estado",
                table: "Vistas_Permisos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
