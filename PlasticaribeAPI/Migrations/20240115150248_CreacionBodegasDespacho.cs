using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class CreacionBodegasDespacho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodegasDespacho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBodega = table.Column<string>(type: "varchar(20)", nullable: false),
                    IdUbicacion = table.Column<string>(type: "varchar(20)", nullable: false),
                    NombreUbicacion = table.Column<string>(type: "varchar(50)", nullable: false),
                    IdSubUbicacion = table.Column<string>(type: "varchar(20)", nullable: false),
                    NombreSubUbicacion = table.Column<string>(type: "varchar(50)", nullable: false),
                    IdCubo = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodegasDespacho", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodegasDespacho");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
