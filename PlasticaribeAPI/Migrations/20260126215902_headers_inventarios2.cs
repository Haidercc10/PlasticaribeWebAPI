using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class headers_inventarios2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Tipos_Bodegas_TpBod_Id",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Toma_Fisica_Inventario_Tipos_Bodegas_TpBod_Id",
                table: "Toma_Fisica_Inventario");

            migrationBuilder.DropColumn(
                name: "Tipo_Inventario",
                table: "Toma_Fisica_Inventario");

            migrationBuilder.DropColumn(
                name: "Tipo_Inventario",
                table: "Inventarios");

            migrationBuilder.RenameColumn(
                name: "TpBod_Id",
                table: "Toma_Fisica_Inventario",
                newName: "Toma_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Toma_Fisica_Inventario_TpBod_Id",
                table: "Toma_Fisica_Inventario",
                newName: "IX_Toma_Fisica_Inventario_Toma_Id");

            migrationBuilder.RenameColumn(
                name: "TpBod_Id",
                table: "Inventarios",
                newName: "InvSnap_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Inventarios_TpBod_Id",
                table: "Inventarios",
                newName: "IX_Inventarios_InvSnap_Id");

            migrationBuilder.CreateTable(
                name: "Inventarios_Snapshot",
                columns: table => new
                {
                    InvSnap_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvSnap_Descripcion = table.Column<string>(type: "varchar(200)", nullable: false),
                    Tipo_Inventario = table.Column<string>(type: "varchar(20)", nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false),
                    InvSnap_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    InvSnap_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    InvSnap_Observacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios_Snapshot", x => x.InvSnap_Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_Snapshot_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Snapshot_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Toma_Fisica",
                columns: table => new
                {
                    Toma_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Toma_Descripcion = table.Column<string>(type: "varchar(200)", nullable: false),
                    Tipo_Inventario = table.Column<string>(type: "varchar(20)", nullable: false),
                    TpBod_Id = table.Column<int>(type: "int", nullable: false),
                    Toma_FechaCreacion = table.Column<DateTime>(type: "date", nullable: false),
                    Toma_HoraCreacion = table.Column<string>(type: "varchar(10)", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Toma_FechaCierre = table.Column<DateTime>(type: "date", nullable: true),
                    Toma_HoraCierre = table.Column<string>(type: "varchar(10)", nullable: true),
                    Usua_Cierre = table.Column<long>(type: "bigint", nullable: false),
                    Toma_Observacion = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toma_Fisica", x => x.Toma_Id);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Tipos_Bodegas_TpBod_Id",
                        column: x => x.TpBod_Id,
                        principalTable: "Tipos_Bodegas",
                        principalColumn: "TpBod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Usuarios_Usua_Cierre",
                        column: x => x.Usua_Cierre,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Toma_Fisica_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Snapshot_TpBod_Id",
                table: "Inventarios_Snapshot",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Snapshot_Usua_Id",
                table: "Inventarios_Snapshot",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Estado_Id",
                table: "Toma_Fisica",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_TpBod_Id",
                table: "Toma_Fisica",
                column: "TpBod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Usua_Cierre",
                table: "Toma_Fisica",
                column: "Usua_Cierre");

            migrationBuilder.CreateIndex(
                name: "IX_Toma_Fisica_Usua_Id",
                table: "Toma_Fisica",
                column: "Usua_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Inventarios_Snapshot_InvSnap_Id",
                table: "Inventarios",
                column: "InvSnap_Id",
                principalTable: "Inventarios_Snapshot",
                principalColumn: "InvSnap_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Toma_Fisica_Inventario_Toma_Fisica_Toma_Id",
                table: "Toma_Fisica_Inventario",
                column: "Toma_Id",
                principalTable: "Toma_Fisica",
                principalColumn: "Toma_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Inventarios_Snapshot_InvSnap_Id",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Toma_Fisica_Inventario_Toma_Fisica_Toma_Id",
                table: "Toma_Fisica_Inventario");

            migrationBuilder.DropTable(
                name: "Inventarios_Snapshot");

            migrationBuilder.DropTable(
                name: "Toma_Fisica");

            migrationBuilder.RenameColumn(
                name: "Toma_Id",
                table: "Toma_Fisica_Inventario",
                newName: "TpBod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Toma_Fisica_Inventario_Toma_Id",
                table: "Toma_Fisica_Inventario",
                newName: "IX_Toma_Fisica_Inventario_TpBod_Id");

            migrationBuilder.RenameColumn(
                name: "InvSnap_Id",
                table: "Inventarios",
                newName: "TpBod_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Inventarios_InvSnap_Id",
                table: "Inventarios",
                newName: "IX_Inventarios_TpBod_Id");

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Inventario",
                table: "Toma_Fisica_Inventario",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tipo_Inventario",
                table: "Inventarios",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Tipos_Bodegas_TpBod_Id",
                table: "Inventarios",
                column: "TpBod_Id",
                principalTable: "Tipos_Bodegas",
                principalColumn: "TpBod_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Toma_Fisica_Inventario_Tipos_Bodegas_TpBod_Id",
                table: "Toma_Fisica_Inventario",
                column: "TpBod_Id",
                principalTable: "Tipos_Bodegas",
                principalColumn: "TpBod_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
