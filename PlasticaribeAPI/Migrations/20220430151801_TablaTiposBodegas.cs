using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class TablaTiposBodegas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipos_Bodegas",
                columns: table => new
                {
                    TpBod_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpBod_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    TpBod_Descripcion = table.Column<string>(type: "text", nullable: false),
                    Area_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Bodegas", x => x.TpBod_Id);
                    table.ForeignKey(
                        name: "FK_Tipos_Bodegas_Areas_Area_Id",
                        column: x => x.Area_Id,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_Bodegas_Area_Id",
                table: "Tipos_Bodegas",
                column: "Area_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos_Bodegas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
