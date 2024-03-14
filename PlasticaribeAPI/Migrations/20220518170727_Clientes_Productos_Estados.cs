using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Clientes_Productos_Estados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado_Id",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Estado_Id",
                table: "Productos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Estado_Id",
                table: "Clientes",
                column: "Estado_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Estados_Estado_Id",
                table: "Clientes",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Estados_Estado_Id",
                table: "Productos",
                column: "Estado_Id",
                principalTable: "Estados",
                principalColumn: "Estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Estados_Estado_Id",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Estados_Estado_Id",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Estado_Id",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_Estado_Id",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Estado_Id",
                table: "Clientes");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
