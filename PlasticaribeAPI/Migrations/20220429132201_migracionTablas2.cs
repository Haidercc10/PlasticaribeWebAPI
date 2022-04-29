using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class migracionTablas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cajas_Compensaciones",
                columns: table => new
                {
                    cajComp_Id = table.Column<long>(type: "bigint", nullable: false),
                    cajComp_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cajComp_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    cajComp_Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    cajComp_Telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    cajComp_CuentaBancaria = table.Column<long>(type: "bigint", nullable: false),
                    cajComp_Direccion = table.Column<string>(type: "varchar(100)", nullable: false),
                    cajComp_Ciudad = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cajas_Compensaciones", x => x.cajComp_Id);
                });

            migrationBuilder.CreateTable(
                name: "EPS",
                columns: table => new
                {
                    eps_Id = table.Column<long>(type: "bigint", nullable: false),
                    eps_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eps_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    eps_Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    eps_Telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    eps_CuentaBancaria = table.Column<long>(type: "bigint", nullable: false),
                    eps_Direccion = table.Column<string>(type: "varchar(100)", nullable: false),
                    eps_Ciudad = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPS", x => x.eps_Id);
                });

            migrationBuilder.CreateTable(
                name: "fondosPensiones",
                columns: table => new
                {
                    fPen_Id = table.Column<long>(type: "bigint", nullable: false),
                    fPen_Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fPen_Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    fPen_Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    fPen_Telefono = table.Column<string>(type: "varchar(50)", nullable: false),
                    fPen_CuentaBancaria = table.Column<long>(type: "bigint", nullable: false),
                    fPen_Direccion = table.Column<string>(type: "varchar(100)", nullable: false),
                    fPen_Ciudad = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fondosPensiones", x => x.fPen_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cajas_Compensaciones");

            migrationBuilder.DropTable(
                name: "EPS");

            migrationBuilder.DropTable(
                name: "fondosPensiones");
        }
    }
}
