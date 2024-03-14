using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class ActualizacionUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajas_CompensacioncajComp_Id",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "cajas_CompensacioncajComp_Id",
                table: "Usuarios",
                newName: "cajComp_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_cajas_CompensacioncajComp_Id",
                table: "Usuarios",
                newName: "IX_Usuarios_cajComp_Id");

            migrationBuilder.AlterColumn<long>(
                name: "Area_Id",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Area_Id",
                table: "Areas",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                table: "Usuarios",
                column: "cajComp_Id",
                principalTable: "Cajas_Compensaciones",
                principalColumn: "cajComp_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajComp_Id",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "cajComp_Id",
                table: "Usuarios",
                newName: "cajas_CompensacioncajComp_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_cajComp_Id",
                table: "Usuarios",
                newName: "IX_Usuarios_cajas_CompensacioncajComp_Id");

            migrationBuilder.AlterColumn<int>(
                name: "Area_Id",
                table: "Usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Area_Id",
                table: "Areas",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cajas_Compensaciones_cajas_CompensacioncajComp_Id",
                table: "Usuarios",
                column: "cajas_CompensacioncajComp_Id",
                principalTable: "Cajas_Compensaciones",
                principalColumn: "cajComp_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
