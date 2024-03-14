using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class AddRolloEnDevolucionesPF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Rollo_Id",
                table: "DetallesEntradasRollos_Productos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<long>(
                name: "Rollo_Id",
                table: "DetallesDevoluciones_ProductosFacturados",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rollo_Id",
                table: "DetallesDevoluciones_ProductosFacturados");

            migrationBuilder.AlterColumn<long>(
                name: "Rollo_Id",
                table: "DetallesEntradasRollos_Productos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Relational:ColumnOrder", 3);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
