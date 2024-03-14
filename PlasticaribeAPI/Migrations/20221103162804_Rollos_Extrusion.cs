using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class Rollos_Extrusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsignacionRollos_Extrusion",
                columns: table => new
                {
                    AsgRollos_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsgRollos_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AsgRollos_Hora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AsgRollos_Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionRollos_Extrusion", x => x.AsgRollos_Id);
                    table.ForeignKey(
                        name: "FK_AsignacionRollos_Extrusion_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngresoRollos_Extrusion",
                columns: table => new
                {
                    IngRollo_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngRollo_Observacion = table.Column<string>(type: "text", nullable: true),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    IngRollo_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    IngRollo_Hora = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresoRollos_Extrusion", x => x.IngRollo_Id);
                    table.ForeignKey(
                        name: "FK_IngresoRollos_Extrusion_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesAsgRollos_Extrusion",
                columns: table => new
                {
                    DtAsgRollos_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsgRollos_Id = table.Column<int>(type: "int", nullable: false),
                    DtAsgRollos_OT = table.Column<long>(type: "bigint", nullable: false),
                    DtAsgRollos_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesAsgRollos_Extrusion", x => x.DtAsgRollos_Id);
                    table.ForeignKey(
                        name: "FK_DetallesAsgRollos_Extrusion_AsignacionRollos_Extrusion_AsgRollos_Id",
                        column: x => x.AsgRollos_Id,
                        principalTable: "AsignacionRollos_Extrusion",
                        principalColumn: "AsgRollos_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesAsgRollos_Extrusion_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesAsgRollos_Extrusion_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesIngRollos_Extrusion",
                columns: table => new
                {
                    DtIngRollo_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngRollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    Rollo_Id = table.Column<int>(type: "int", nullable: false),
                    DtIngRollo_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    DtIngRollo_OT = table.Column<int>(type: "int", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesIngRollos_Extrusion", x => x.DtIngRollo_Id);
                    table.ForeignKey(
                        name: "FK_DetallesIngRollos_Extrusion_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesIngRollos_Extrusion_IngresoRollos_Extrusion_IngRollo_Id",
                        column: x => x.IngRollo_Id,
                        principalTable: "IngresoRollos_Extrusion",
                        principalColumn: "IngRollo_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesIngRollos_Extrusion_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesIngRollos_Extrusion_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionRollos_Extrusion_Usua_Id",
                table: "AsignacionRollos_Extrusion",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsgRollos_Extrusion_AsgRollos_Id",
                table: "DetallesAsgRollos_Extrusion",
                column: "AsgRollos_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsgRollos_Extrusion_Proceso_Id",
                table: "DetallesAsgRollos_Extrusion",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesAsgRollos_Extrusion_UndMed_Id",
                table: "DetallesAsgRollos_Extrusion",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesIngRollos_Extrusion_Estado_Id",
                table: "DetallesIngRollos_Extrusion",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesIngRollos_Extrusion_IngRollo_Id",
                table: "DetallesIngRollos_Extrusion",
                column: "IngRollo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesIngRollos_Extrusion_Proceso_Id",
                table: "DetallesIngRollos_Extrusion",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesIngRollos_Extrusion_UndMed_Id",
                table: "DetallesIngRollos_Extrusion",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_IngresoRollos_Extrusion_Usua_Id",
                table: "IngresoRollos_Extrusion",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesAsgRollos_Extrusion");

            migrationBuilder.DropTable(
                name: "DetallesIngRollos_Extrusion");

            migrationBuilder.DropTable(
                name: "AsignacionRollos_Extrusion");

            migrationBuilder.DropTable(
                name: "IngresoRollos_Extrusion");

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
