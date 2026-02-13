using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class machine_in_events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Maq_Id",
                table: "Eventos_Maquinas",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Maquinas_Maq_Id",
                table: "Eventos_Maquinas",
                column: "Maq_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Maquinas_Maquinas_Maq_Id",
                table: "Eventos_Maquinas",
                column: "Maq_Id",
                principalTable: "Maquinas",
                principalColumn: "Maq_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Maquinas_Maquinas_Maq_Id",
                table: "Eventos_Maquinas");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_Maquinas_Maq_Id",
                table: "Eventos_Maquinas");

            migrationBuilder.DropColumn(
                name: "Maq_Id",
                table: "Eventos_Maquinas");
        }
    }
}
