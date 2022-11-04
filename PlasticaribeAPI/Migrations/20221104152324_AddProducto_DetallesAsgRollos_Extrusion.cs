using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AddProducto_DetallesAsgRollos_Extrusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prod_Id",
                table: "DetallesAsgRollos_Extrusion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsgRollos_Extrusion_Prod_Id",
                table: "DetallesAsgRollos_Extrusion",
                column: "Prod_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesAsgRollos_Extrusion_Productos_Prod_Id",
                table: "DetallesAsgRollos_Extrusion",
                column: "Prod_Id",
                principalTable: "Productos",
                principalColumn: "Prod_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesAsgRollos_Extrusion_Productos_Prod_Id",
                table: "DetallesAsgRollos_Extrusion");

            migrationBuilder.DropIndex(
                name: "IX_DetallesAsgRollos_Extrusion_Prod_Id",
                table: "DetallesAsgRollos_Extrusion");

            migrationBuilder.DropColumn(
                name: "Prod_Id",
                table: "DetallesAsgRollos_Extrusion");
        }
    }
}
