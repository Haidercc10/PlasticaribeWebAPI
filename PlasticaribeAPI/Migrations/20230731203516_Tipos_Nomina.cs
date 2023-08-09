using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Tipos_Nomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TpNomina_Id",
                table: "Nomina_Plasticaribe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tipos_Nomina",
                columns: table => new
                {
                    TpNomina_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpNomina_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TpNomina_Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Nomina", x => x.TpNomina_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_Plasticaribe_TpNomina_Id",
                table: "Nomina_Plasticaribe",
                column: "TpNomina_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nomina_Plasticaribe_Tipos_Nomina_TpNomina_Id",
                table: "Nomina_Plasticaribe",
                column: "TpNomina_Id",
                principalTable: "Tipos_Nomina",
                principalColumn: "TpNomina_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nomina_Plasticaribe_Tipos_Nomina_TpNomina_Id",
                table: "Nomina_Plasticaribe");

            migrationBuilder.DropTable(
                name: "Tipos_Nomina");

            migrationBuilder.DropIndex(
                name: "IX_Nomina_Plasticaribe_TpNomina_Id",
                table: "Nomina_Plasticaribe");

            migrationBuilder.DropColumn(
                name: "TpNomina_Id",
                table: "Nomina_Plasticaribe");
        }
    }
}
