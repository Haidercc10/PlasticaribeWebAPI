using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Control_Time_Machine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipos_Eventos",
                columns: table => new
                {
                    TpEvento_Id = table.Column<string>(type: "varchar(20)", nullable: false),
                    TpEvento_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpEvento_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TpEvento_Descripcion = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Eventos", x => x.TpEvento_Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos_Maquinas",
                columns: table => new
                {
                    Evmq_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Evmq_Codigo = table.Column<string>(type: "varchar(50)", nullable: false),
                    Evmq_Descripcion = table.Column<string>(type: "varchar(100)", nullable: false),
                    TpEvento_Id = table.Column<string>(type: "varchar(20)", nullable: false),
                    Evmq_Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos_Maquinas", x => x.Evmq_Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Maquinas_Tipos_Eventos_TpEvento_Id",
                        column: x => x.TpEvento_Id,
                        principalTable: "Tipos_Eventos",
                        principalColumn: "TpEvento_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogEventos_Maquinas",
                columns: table => new
                {
                    Lem_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maq_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Evmq_Id = table.Column<long>(type: "bigint", nullable: false),
                    Lem_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Lem_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Lem_Observacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEventos_Maquinas", x => x.Lem_Id);
                    table.ForeignKey(
                        name: "FK_LogEventos_Maquinas_Eventos_Maquinas_Evmq_Id",
                        column: x => x.Evmq_Id,
                        principalTable: "Eventos_Maquinas",
                        principalColumn: "Evmq_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogEventos_Maquinas_Maquinas_Maq_Id",
                        column: x => x.Maq_Id,
                        principalTable: "Maquinas",
                        principalColumn: "Maq_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogEventos_Maquinas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Maquinas_TpEvento_Id",
                table: "Eventos_Maquinas",
                column: "TpEvento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LogEventos_Maquinas_Evmq_Id",
                table: "LogEventos_Maquinas",
                column: "Evmq_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LogEventos_Maquinas_Maq_Id",
                table: "LogEventos_Maquinas",
                column: "Maq_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LogEventos_Maquinas_Usua_Id",
                table: "LogEventos_Maquinas",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEventos_Maquinas");

            migrationBuilder.DropTable(
                name: "Eventos_Maquinas");

            migrationBuilder.DropTable(
                name: "Tipos_Eventos");
        }
    }
}
