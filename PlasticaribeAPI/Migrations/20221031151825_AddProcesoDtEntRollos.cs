using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AddProcesoDtEntRollos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Proceso_Id",
                table: "DetallesEntradasRollos_Productos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesEntradasRollos_Productos_Proceso_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "Proceso_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Procesos_Proceso_Id",
                table: "DetallesEntradasRollos_Productos",
                column: "Proceso_Id",
                principalTable: "Procesos",
                principalColumn: "Proceso_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesEntradasRollos_Productos_Procesos_Proceso_Id",
                table: "DetallesEntradasRollos_Productos");

            migrationBuilder.DropIndex(
                name: "IX_DetallesEntradasRollos_Productos_Proceso_Id",
                table: "DetallesEntradasRollos_Productos");
            migrationBuilder.DropColumn(
                name: "Proceso_Id",
                table: "DetallesEntradasRollos_Productos");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
