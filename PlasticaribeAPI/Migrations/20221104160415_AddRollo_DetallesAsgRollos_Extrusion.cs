using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AddRollo_DetallesAsgRollos_Extrusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Rollo_Id",
                table: "DetallesAsgRollos_Extrusion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rollo_Id",
                table: "DetallesAsgRollos_Extrusion");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
