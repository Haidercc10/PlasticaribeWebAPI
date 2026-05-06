using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SupervisorIn_ProduccionProcesos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Supervisor_Id",
                table: "Produccion_Procesos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produccion_Procesos_Supervisor_Id",
                table: "Produccion_Procesos",
                column: "Supervisor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produccion_Procesos_Usuarios_Supervisor_Id",
                table: "Produccion_Procesos",
                column: "Supervisor_Id",
                principalTable: "Usuarios",
                principalColumn: "Usua_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produccion_Procesos_Usuarios_Supervisor_Id",
                table: "Produccion_Procesos");

            migrationBuilder.DropIndex(
                name: "IX_Produccion_Procesos_Supervisor_Id",
                table: "Produccion_Procesos");

            migrationBuilder.DropColumn(
                name: "Supervisor_Id",
                table: "Produccion_Procesos");
        }
    }
}
