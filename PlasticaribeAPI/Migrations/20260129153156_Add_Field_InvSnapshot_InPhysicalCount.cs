using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Add_Field_InvSnapshot_InPhysicalCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvSnap_Id",
                table: "Toma_Fisica",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_InvSnap_Id",
                table: "Toma_Fisica",
                column: "InvSnap_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Toma_Fisica_Inventarios_Snapshot_InvSnap_Id",
                table: "Toma_Fisica",
                column: "InvSnap_Id",
                principalTable: "Inventarios_Snapshot",
                principalColumn: "InvSnap_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toma_Fisica_Inventarios_Snapshot_InvSnap_Id",
                table: "Toma_Fisica");

            migrationBuilder.DropIndex(
                name: "IX_Toma_Fisica_InvSnap_Id",
                table: "Toma_Fisica");

            migrationBuilder.DropColumn(
                name: "InvSnap_Id",
                table: "Toma_Fisica");
        }
    }
}
