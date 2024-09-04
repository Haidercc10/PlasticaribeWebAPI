using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Servicios_Produccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servicios_Produccion",
                columns: table => new
                {
                    SvcProd_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SvcProd_Nombre = table.Column<string>(type: "varchar(150)", nullable: false),
                    SvcProd_Descripcion = table.Column<string>(type: "varchar(max)", nullable: true),
                    SvcProd_Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Area_Realiza = table.Column<long>(type: "bigint", nullable: false),
                    Area_Solicita = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios_Produccion", x => x.SvcProd_Id);
                    table.ForeignKey(
                        name: "FK_Servicios_Produccion_Areas_Area_Realiza",
                        column: x => x.Area_Realiza,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_Produccion_Areas_Area_Solicita",
                        column: x => x.Area_Solicita,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Produccion_Area_Realiza",
                table: "Servicios_Produccion",
                column: "Area_Realiza");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_Produccion_Area_Solicita",
                table: "Servicios_Produccion",
                column: "Area_Solicita");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servicios_Produccion");
        }
    }
}
