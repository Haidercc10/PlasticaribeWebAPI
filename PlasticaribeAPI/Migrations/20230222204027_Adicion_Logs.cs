using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Adicion_Logs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log_Errores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Base_Datos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Hora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorSeverity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorProcedure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Errores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Transacciones",
                columns: table => new
                {
                    Transac_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transac_Id = table.Column<long>(type: "bigint", nullable: false),
                    Transac_Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_Tabla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_LlavePK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_Campo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_ValorOriginal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_valorNuevo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Transac_Hora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_Usuario = table.Column<long>(type: "bigint", nullable: false, defaultValue: 123456789),
                    Transac_BaseDatos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Transacciones", x => x.Transac_Codigo);
                    table.ForeignKey(
                        name: "FK_Log_Transacciones_Usuarios_Transac_Usuario",
                        column: x => x.Transac_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_Transacciones_Transac_Usuario",
                table: "Log_Transacciones",
                column: "Transac_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log_Errores");

            migrationBuilder.DropTable(
                name: "Log_Transacciones");
        }
    }
}
