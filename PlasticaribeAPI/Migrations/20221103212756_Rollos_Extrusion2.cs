using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Rollos_Extrusion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "DetallesIngRollos_Extrusion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesIngRollos_Extrusion_Prod_Id",
                table: "DetallesIngRollos_Extrusion",
                column: "Prod_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesIngRollos_Extrusion_Productos_Prod_Id",
                table: "DetallesIngRollos_Extrusion",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesIngRollos_Extrusion_Productos_Prod_Id",
                table: "DetallesIngRollos_Extrusion");

            migrationBuilder.DropIndex(
                name: "IX_DetallesIngRollos_Extrusion_Prod_Id",
                table: "DetallesIngRollos_Extrusion");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "DetallesIngRollos_Extrusion");
        }
    }
}
