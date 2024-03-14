using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class actualizacionEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Empresa_Id = table.Column<long>(type: "bigint", nullable: false),
                    Empresa_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Empresa_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Empresa_Direccion = table.Column<string>(type: "varchar(100)", nullable: false),
                    Empresa_Ciudad = table.Column<string>(type: "varchar(50)", nullable: false),
                    Empresa_Telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    Empresa_Correo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Empresa_COdigoPostal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Empresa_Id);
                    table.ForeignKey(
                        name: "FK_Empresas_TipoIdentificaciones_TipoIdentificacion_Id",
                        column: x => x.TipoIdentificacion_Id,
                        principalTable: "TipoIdentificaciones",
                        principalColumn: "TipoIdentificacion_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_TipoIdentificacion_Id",
                table: "Empresas",
                column: "TipoIdentificacion_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
