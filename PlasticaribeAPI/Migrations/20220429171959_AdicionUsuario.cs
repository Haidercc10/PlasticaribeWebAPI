using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class AdicionUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_fondosPensiones",
                table: "fondosPensiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cajas_Compensaciones",
                table: "cajas_Compensaciones");

            migrationBuilder.RenameTable(
                name: "fondosPensiones",
                newName: "FondosPensiones");

            migrationBuilder.RenameTable(
                name: "cajas_Compensaciones",
                newName: "Cajas_Compensaciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FondosPensiones",
                table: "FondosPensiones",
                column: "fPen_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cajas_Compensaciones",
                table: "Cajas_Compensaciones",
                column: "cajComp_Id");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usu_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usu_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usu_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Area_Id = table.Column<int>(type: "bigint", nullable: false),
                    TipoUsuario_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    RolesRol_Id = table.Column<int>(type: "int", nullable: false),
                    Empresa_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Usu_Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Usu_Telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    Usu_Contrasena = table.Column<string>(type: "varchar(100)", nullable: false),
                    cajas_CompensacioncajComp_Id = table.Column<long>(type: "bigint", nullable: false),
                    eps_Id = table.Column<long>(type: "bigint", nullable: false),
                    fondosPensionfPen_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usu_Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Areas_Area_Id",
                        column: x => x.Area_Id,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Cajas_Compensaciones_cajas_CompensacioncajComp_Id",
                        column: x => x.cajas_CompensacioncajComp_Id,
                        principalTable: "Cajas_Compensaciones",
                        principalColumn: "cajComp_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empresas_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresas",
                        principalColumn: "Empresa_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_EPS_eps_Id",
                        column: x => x.eps_Id,
                        principalTable: "EPS",
                        principalColumn: "eps_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_FondosPensiones_fondosPensionfPen_Id",
                        column: x => x.fondosPensionfPen_Id,
                        principalTable: "FondosPensiones",
                        principalColumn: "fPen_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolesRol_Id",
                        column: x => x.RolesRol_Id,
                        principalTable: "Roles",
                        principalColumn: "Rol_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoIdentificaciones_TipoIdentificacion_Id",
                        column: x => x.TipoIdentificacion_Id,
                        principalTable: "TipoIdentificaciones",
                        principalColumn: "TipoIdentificacion_Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoUsuarios_TipoUsuario_Id",
                        column: x => x.TipoUsuario_Id,
                        principalTable: "TipoUsuarios",
                        principalColumn: "TipoUsuario_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Area_Id",
                table: "Usuarios",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_cajas_CompensacioncajComp_Id",
                table: "Usuarios",
                column: "cajas_CompensacioncajComp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Empresa_Id",
                table: "Usuarios",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_eps_Id",
                table: "Usuarios",
                column: "eps_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Estado_Id",
                table: "Usuarios",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_fondosPensionfPen_Id",
                table: "Usuarios",
                column: "fondosPensionfPen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolesRol_Id",
                table: "Usuarios",
                column: "RolesRol_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuario_Id",
                table: "Usuarios",
                column: "TipoUsuario_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FondosPensiones",
                table: "FondosPensiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cajas_Compensaciones",
                table: "Cajas_Compensaciones");

            migrationBuilder.RenameTable(
                name: "FondosPensiones",
                newName: "fondosPensiones");

            migrationBuilder.RenameTable(
                name: "Cajas_Compensaciones",
                newName: "cajas_Compensaciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fondosPensiones",
                table: "fondosPensiones",
                column: "fPen_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cajas_Compensaciones",
                table: "cajas_Compensaciones",
                column: "cajComp_Id");
        }
    }
}
