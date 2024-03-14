using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Desperdicio3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desperdicios_Areas_Area_Id",
                table: "Desperdicios");

            migrationBuilder.DropIndex(
                name: "IX_Desperdicios_Area_Id",
                table: "Desperdicios");

            migrationBuilder.DropColumn(
                name: "Area_Id",
                table: "Desperdicios");

            migrationBuilder.RenameColumn(
                name: "Desp_Maquina",
                table: "Desperdicios",
                newName: "Actv_Id");

            migrationBuilder.AddColumn<string>(
                name: "Proceso_Id",
                table: "Desperdicios",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Actv_Id",
                table: "Desperdicios",
                column: "Actv_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Proceso_Id",
                table: "Desperdicios",
                column: "Proceso_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Desperdicios_Activos_Actv_Id",
                table: "Desperdicios",
                column: "Actv_Id",
                principalTable: "Activos",
                principalColumn: "Actv_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Desperdicios_Procesos_Proceso_Id",
                table: "Desperdicios",
                column: "Proceso_Id",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desperdicios_Activos_Actv_Id",
                table: "Desperdicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Desperdicios_Procesos_Proceso_Id",
                table: "Desperdicios");

            migrationBuilder.DropIndex(
                name: "IX_Desperdicios_Actv_Id",
                table: "Desperdicios");

            migrationBuilder.DropIndex(
                name: "IX_Desperdicios_Proceso_Id",
                table: "Desperdicios");

            migrationBuilder.DropColumn(
                name: "Proceso_Id",
                table: "Desperdicios");

            migrationBuilder.RenameColumn(
                name: "Actv_Id",
                table: "Desperdicios",
                newName: "Desp_Maquina");

            migrationBuilder.AddColumn<long>(
                name: "Area_Id",
                table: "Desperdicios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Area_Id",
                table: "Desperdicios",
                column: "Area_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Desperdicios_Areas_Area_Id",
                table: "Desperdicios",
                column: "Area_Id",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
