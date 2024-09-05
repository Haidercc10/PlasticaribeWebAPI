using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProcesosXAreas_EnServiciosProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Produccion_Areas_Area_Realiza",
                table: "Servicios_Produccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Produccion_Areas_Area_Solicita",
                table: "Servicios_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_Produccion_Area_Realiza",
                table: "Servicios_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_Produccion_Area_Solicita",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "Area_Realiza",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "Area_Solicita",
                table: "Servicios_Produccion");

            migrationBuilder.AddColumn<string>(
                name: "Proceso_Crea",
                table: "Servicios_Produccion",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "N/A");

            migrationBuilder.AddColumn<string>(
                name: "Proceso_Solicita",
                table: "Servicios_Produccion",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "N/A");

            migrationBuilder.AlterColumn<string>(
                name: "Presentacion",
                table: "Maquilas_Internas",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Produccion_Proceso_Crea",
                table: "Servicios_Produccion",
                column: "Proceso_Crea");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Produccion_Proceso_Solicita",
                table: "Servicios_Produccion",
                column: "Proceso_Solicita");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Produccion_Procesos_Proceso_Crea",
                table: "Servicios_Produccion",
                column: "Proceso_Crea",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Produccion_Procesos_Proceso_Solicita",
                table: "Servicios_Produccion",
                column: "Proceso_Solicita",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Produccion_Procesos_Proceso_Crea",
                table: "Servicios_Produccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Produccion_Procesos_Proceso_Solicita",
                table: "Servicios_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_Produccion_Proceso_Crea",
                table: "Servicios_Produccion");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_Produccion_Proceso_Solicita",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "Proceso_Crea",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "Proceso_Solicita",
                table: "Servicios_Produccion");

            migrationBuilder.AddColumn<long>(
                name: "Area_Realiza",
                table: "Servicios_Produccion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Area_Solicita",
                table: "Servicios_Produccion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Presentacion",
                table: "Maquilas_Internas",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Produccion_Area_Realiza",
                table: "Servicios_Produccion",
                column: "Area_Realiza");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Produccion_Area_Solicita",
                table: "Servicios_Produccion",
                column: "Area_Solicita");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Produccion_Areas_Area_Realiza",
                table: "Servicios_Produccion",
                column: "Area_Realiza",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Produccion_Areas_Area_Solicita",
                table: "Servicios_Produccion",
                column: "Area_Solicita",
                principalTable: "Areas",
                principalColumn: "Area_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
