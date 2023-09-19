using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class UsuariosRoles4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropTable(
                 name: "Usuarios");*/

            /*migrationBuilder.DropTable(
                name: "Roles");*/

            /*migrationBuilder.DropTable(
                name: "TipoUsuarios");*/

            migrationBuilder.CreateTable(
                name: "Roles_Usuarios",
                columns: table => new
                {
                    RolUsu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolUsu_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    RolUsu_Descripcion = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_Usuarios", x => x.RolUsu_Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosSistema",
                columns: table => new
                {
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Usua_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion_Id = table.Column<string>(type: "varchar(10)", nullable: true),
                    Usua_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Area_Id = table.Column<long>(type: "bigint", nullable: false),
                    tpUsu_Id = table.Column<int>(type: "int", nullable: false),
                    RolUsu_Id = table.Column<int>(type: "int", nullable: false),
                    Empresa_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Usua_Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Usua_Telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    Usua_Contrasena = table.Column<string>(type: "varchar(100)", nullable: false),
                    cajComp_Id = table.Column<long>(type: "bigint", nullable: false),
                    eps_Id = table.Column<long>(type: "bigint", nullable: false),
                    fPen_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosSistema", x => x.Usua_Id);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_Areas_Area_Id",
                        column: x => x.Area_Id,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_Cajas_Compensaciones_cajComp_Id",
                        column: x => x.cajComp_Id,
                        principalTable: "Cajas_Compensaciones",
                        principalColumn: "cajComp_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_Empresas_Empresa_Id",
                        column: x => x.Empresa_Id,
                        principalTable: "Empresas",
                        principalColumn: "Empresa_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_EPS_eps_Id",
                        column: x => x.eps_Id,
                        principalTable: "EPS",
                        principalColumn: "eps_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_FondosPensiones_fPen_Id",
                        column: x => x.fPen_Id,
                        principalTable: "FondosPensiones",
                        principalColumn: "fPen_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_Roles_Usuarios_RolUsu_Id",
                        column: x => x.RolUsu_Id,
                        principalTable: "Roles_Usuarios",
                        principalColumn: "RolUsu_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_TipoIdentificaciones_TipoIdentificacion_Id",
                        column: x => x.TipoIdentificacion_Id,
                        principalTable: "TipoIdentificaciones",
                        principalColumn: "TipoIdentificacion_Id");
                    table.ForeignKey(
                        name: "FK_UsuariosSistema_Tipos_Usuarios_tpUsu_Id",
                        column: x => x.tpUsu_Id,
                        principalTable: "Tipos_Usuarios",
                        principalColumn: "tpUsu_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_Area_Id",
                table: "UsuariosSistema",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_cajComp_Id",
                table: "UsuariosSistema",
                column: "cajComp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_Empresa_Id",
                table: "UsuariosSistema",
                column: "Empresa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_eps_Id",
                table: "UsuariosSistema",
                column: "eps_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_Estado_Id",
                table: "UsuariosSistema",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_fPen_Id",
                table: "UsuariosSistema",
                column: "fPen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_RolUsu_Id",
                table: "UsuariosSistema",
                column: "RolUsu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_TipoIdentificacion_Id",
                table: "UsuariosSistema",
                column: "TipoIdentificacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_tpUsu_Id",
                table: "UsuariosSistema",
                column: "tpUsu_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosSistema");

            migrationBuilder.DropTable(
                name: "Roles_Usuarios");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Rol_Id = table.Column<int>(type: "int", nullable: false),
                    Rol_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol_Descripcion = table.Column<string>(type: "varchar(200)", nullable: true),
                    Rol_Nombre = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Rol_Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    TipoUsuario_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    TipoUsuario_Descripcion = table.Column<string>(type: "varchar(200)", nullable: true),
                    TipoUsuario_Nombre = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.TipoUsuario_Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usu_Id = table.Column<long>(type: "bigint", nullable: false),
                    Area_Id = table.Column<long>(type: "bigint", nullable: false),
                    cajComp_Id = table.Column<long>(type: "bigint", nullable: false),
                    Empresa_Id = table.Column<long>(type: "bigint", nullable: false),
                    eps_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    fPen_Id = table.Column<long>(type: "bigint", nullable: false),
                    Rol_Id = table.Column<int>(type: "int", nullable: false),
                    TipoIdentificacion_Id = table.Column<string>(type: "varchar(10)", nullable: true),
                    TipoUsuario_Id = table.Column<string>(type: "varchar(10)", nullable: true),
                    Usu_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usu_Contrasena = table.Column<string>(type: "varchar(100)", nullable: false),
                    Usu_Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Usu_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Usu_Telefono = table.Column<string>(type: "varchar(50)", nullable: false)
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
                        name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                        column: x => x.cajComp_Id,
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
                        name: "FK_Usuarios_FondosPensiones_fPen_Id",
                        column: x => x.fPen_Id,
                        principalTable: "FondosPensiones",
                        principalColumn: "fPen_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_Rol_Id",
                        column: x => x.Rol_Id,
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
                name: "IX_Usuarios_cajComp_Id",
                table: "Usuarios",
                column: "cajComp_Id");

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
                name: "IX_Usuarios_fPen_Id",
                table: "Usuarios",
                column: "fPen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Rol_Id",
                table: "Usuarios",
                column: "Rol_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoIdentificacion_Id",
                table: "Usuarios",
                column: "TipoIdentificacion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuario_Id",
                table: "Usuarios",
                column: "TipoUsuario_Id");
        }
    }
}
