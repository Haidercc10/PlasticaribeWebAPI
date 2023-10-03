using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVendedor_OT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id_Vendedor",
                table: "Orden_Trabajo",
                type: "bigint",
                nullable: false,
                defaultValue: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Proceso_Id",
                table: "Inventarios_Areas",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "vachar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "InvObservacion",
                table: "Inventarios_Areas",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Id_Vendedor",
                table: "Orden_Trabajo",
                column: "Id_Vendedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Orden_Trabajo_Usuarios_Id_Vendedor",
                table: "Orden_Trabajo",
                column: "Id_Vendedor",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orden_Trabajo_Usuarios_Id_Vendedor",
                table: "Orden_Trabajo");

            migrationBuilder.DropIndex(
                name: "IX_Orden_Trabajo_Id_Vendedor",
                table: "Orden_Trabajo");

            migrationBuilder.DropColumn(
                name: "Id_Vendedor",
                table: "Orden_Trabajo");

            migrationBuilder.AlterColumn<string>(
                name: "Proceso_Id",
                table: "Inventarios_Areas",
                type: "vachar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "InvObservacion",
                table: "Inventarios_Areas",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);
        }
    }
}
