using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CamposInMaqInternas_ServProduccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SvcProd_Valor",
                table: "Servicios_Produccion",
                newName: "SvcProd_ValorNoche");

            migrationBuilder.AddColumn<decimal>(
                name: "SvcProd_ValorDia",
                table: "Servicios_Produccion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SvcProd_ValorDomFest",
                table: "Servicios_Produccion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Impreso",
                table: "Maquilas_Internas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaqInt_Hora",
                table: "Maquilas_Internas",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Material_Id",
                table: "Maquilas_Internas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Turno_Id",
                table: "Maquilas_Internas",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Material_Id",
                table: "Maquilas_Internas",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Turno_Id",
                table: "Maquilas_Internas",
                column: "Turno_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Maquilas_Internas_Materiales_MatPrima_Material_Id",
                table: "Maquilas_Internas",
                column: "Material_Id",
                principalTable: "Materiales_MatPrima",
                principalColumn: "Material_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maquilas_Internas_Turnos_Turno_Id",
                table: "Maquilas_Internas",
                column: "Turno_Id",
                principalTable: "Turnos",
                principalColumn: "Turno_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquilas_Internas_Materiales_MatPrima_Material_Id",
                table: "Maquilas_Internas");

            migrationBuilder.DropForeignKey(
                name: "FK_Maquilas_Internas_Turnos_Turno_Id",
                table: "Maquilas_Internas");

            migrationBuilder.DropIndex(
                name: "IX_Maquilas_Internas_Material_Id",
                table: "Maquilas_Internas");

            migrationBuilder.DropIndex(
                name: "IX_Maquilas_Internas_Turno_Id",
                table: "Maquilas_Internas");

            migrationBuilder.DropColumn(
                name: "SvcProd_ValorDia",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "SvcProd_ValorDomFest",
                table: "Servicios_Produccion");

            migrationBuilder.DropColumn(
                name: "Impreso",
                table: "Maquilas_Internas");

            migrationBuilder.DropColumn(
                name: "MaqInt_Hora",
                table: "Maquilas_Internas");

            migrationBuilder.DropColumn(
                name: "Material_Id",
                table: "Maquilas_Internas");

            migrationBuilder.DropColumn(
                name: "Turno_Id",
                table: "Maquilas_Internas");

            migrationBuilder.RenameColumn(
                name: "SvcProd_ValorNoche",
                table: "Servicios_Produccion",
                newName: "SvcProd_Valor");
        }
    }
}
