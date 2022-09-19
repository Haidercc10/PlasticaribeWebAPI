using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class BOPPSerial_CambioTipoDatoALong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BOPP_Serial",
                table: "BOPP",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BOPP_Serial",
                table: "BOPP",
                type: "varchar(MAX)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
