using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Adicion_Proveedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Prov_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prov_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Prov_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    TpProv_Id = table.Column<int>(type: "int", nullable: false),
                    Prov_Ciudad = table.Column<string>(type: "varchar(100)", nullable: false),
                    Prov_Telefono = table.Column<string>(type: "varchar(100)", nullable: false),
                    Prov_Email = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Prov_Id);
                    table.ForeignKey(
                        name: "FK_Proveedores_TipoIdentificaciones_TipoIdentificacion_Id",
                        column: x => x.TipoIdentificacion_Id,
                        principalTable: "TipoIdentificaciones",
                        principalColumn: "TipoIdentificacion_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proveedores_Tipos_Proveedores_TpProv_Id",
                        column: x => x.TpProv_Id,
                        principalTable: "Tipos_Proveedores",
                        principalColumn: "TpProv_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_TipoIdentificacion_Id",
                table: "Proveedores",
                column: "TipoIdentificacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_TpProv_Id",
                table: "Proveedores",
                column: "TpProv_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
