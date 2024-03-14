using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Base_Datos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Hora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorSeverity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorProcedure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Errores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Transacciones",
                columns: table => new
                {
                    Transac_Codigo = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Transac_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_Tabla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_LlavePK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_Campo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_ValorOriginal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_valorNuevo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transac_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Transac_Hora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transac_Usuario = table.Column<long>(type: "bigint", nullable: false, defaultValue: 123456789),
                    Transac_BaseDatos = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
