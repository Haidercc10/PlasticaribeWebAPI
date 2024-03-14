using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class adicionSedesClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sedes_Clientes",
                columns: table => new
                {
                    SedeCli_Id = table.Column<long>(type: "bigint", nullable: false),
                    SedeCli_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SedeCliente_Ciudad = table.Column<string>(type: "varchar(100)", nullable: false),
                    SedeCli_CodPostal = table.Column<long>(type: "bigint", nullable: false),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes_Clientes", x => x.SedeCli_Id);
                    table.ForeignKey(
                        name: "FK_Sedes_Clientes_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_Clientes_Cli_Id",
                table: "Sedes_Clientes",
                column: "Cli_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sedes_Clientes");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
