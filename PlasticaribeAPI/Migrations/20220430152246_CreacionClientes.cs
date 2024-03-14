using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class CreacionClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Cli_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Cli_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cli_Direccion = table.Column<string>(type: "varchar(60)", nullable: false),
                    Cli_Telefono = table.Column<long>(type: "bigint", nullable: false),
                    Cli_Email = table.Column<string>(type: "varchar(60)", nullable: false),
                    TPCli_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Cli_Id);
                    table.ForeignKey(
                        name: "FK_Clientes_TipoIdentificaciones_TipoIdentificacion_Id",
                        column: x => x.TipoIdentificacion_Id,
                        principalTable: "TipoIdentificaciones",
                        principalColumn: "TipoIdentificacion_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Tipos_Clientes_TPCli_Id",
                        column: x => x.TPCli_Id,
                        principalTable: "Tipos_Clientes",
                        principalColumn: "TPCli_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoIdentificacion_Id",
                table: "Clientes",
                column: "TipoIdentificacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TPCli_Id",
                table: "Clientes",
                column: "TPCli_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
