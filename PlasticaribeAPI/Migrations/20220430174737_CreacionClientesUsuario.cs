using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class CreacionClientesUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "Usuarios",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Clientes_Usuarios",
                columns: table => new
                {
                    CliUsu_Id = table.Column<int>(type: "int", nullable: false),
                    CliUsu_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes_Usuarios", x => x.CliUsu_Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_Clientes_Cli_Id",
                        column: x => x.Cli_Id,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Usuarios_Cli_Id",
                table: "Clientes_Usuarios",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Usuarios_Usua_Id",
                table: "Clientes_Usuarios",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clientes_Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "TipoIdentificacion_Id",
                table: "Usuarios",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id",
                principalTable: "TipoIdentificaciones",
                principalColumn: "TipoIdentificacion_Id");
        }
    }
}
