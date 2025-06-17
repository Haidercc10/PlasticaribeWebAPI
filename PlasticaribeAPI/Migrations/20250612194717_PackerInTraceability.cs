using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class PackerInTraceability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Empacador_Id",
                table: "Trazabilidad_Produccion",
                type: "bigint",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Turno_Id",
                table: "Trazabilidad_Produccion",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "NE");

            migrationBuilder.AddColumn<long>(
                name: "Empacador_Id",
                table: "Produccion_Procesos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Empacador_Id",
                table: "Trazabilidad_Produccion",
                column: "Empacador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trazabilidad_Produccion_Turno_Id",
                table: "Trazabilidad_Produccion",
                column: "Turno_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Empacador_Id",
                table: "Produccion_Procesos",
                column: "Empacador_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produccion_Procesos_Usuarios_Empacador_Id",
                table: "Produccion_Procesos",
                column: "Empacador_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidad_Produccion_Turnos_Turno_Id",
                table: "Trazabilidad_Produccion",
                column: "Turno_Id",
                principalTable: "Turnos",
                principalColumn: "Turno_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trazabilidad_Produccion_Usuarios_Empacador_Id",
                table: "Trazabilidad_Produccion",
                column: "Empacador_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produccion_Procesos_Usuarios_Empacador_Id",
                table: "Produccion_Procesos");

            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Produccion_Turnos_Turno_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Trazabilidad_Produccion_Usuarios_Empacador_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Trazabilidad_Produccion_Empacador_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Trazabilidad_Produccion_Turno_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Produccion_Procesos_Empacador_Id",
                table: "Produccion_Procesos");

            migrationBuilder.DropColumn(
                name: "Empacador_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropColumn(
                name: "Turno_Id",
                table: "Trazabilidad_Produccion");

            migrationBuilder.DropColumn(
                name: "Empacador_Id",
                table: "Produccion_Procesos");
        }
    }
}
