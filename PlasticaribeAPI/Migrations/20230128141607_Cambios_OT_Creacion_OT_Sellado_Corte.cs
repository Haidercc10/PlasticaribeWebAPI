using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambiosOTCreacionOTSelladoCorte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ot_CantidadUnidades_Margen",
                table: "Orden_Trabajo",
                newName: "Ot_ValorUnidad");

            migrationBuilder.RenameColumn(
                name: "Ot_CantidadUnidades",
                table: "Orden_Trabajo",
                newName: "Ot_ValorOT");

            migrationBuilder.RenameColumn(
                name: "Ot_CantidadKilos_Margen",
                table: "Orden_Trabajo",
                newName: "Ot_ValorKg");

            migrationBuilder.RenameColumn(
                name: "Ot_CantidadKilos",
                table: "Orden_Trabajo",
                newName: "Ot_PesoNetoKg");

            migrationBuilder.AddColumn<decimal>(
                name: "Ot_CantidadPedida",
                table: "Orden_Trabajo",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "OT_Sellado_Corte",
                columns: table => new
                {
                    SelladoCorteId = table.Column<long>(name: "SelladoCorte_Id", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OtId = table.Column<long>(name: "Ot_Id", type: "bigint", nullable: false),
                    Corte = table.Column<bool>(type: "bit", nullable: false),
                    Sellado = table.Column<bool>(type: "bit", nullable: false),
                    FormatoId = table.Column<int>(name: "Formato_Id", type: "int", nullable: false),
                    SelladoCorteAncho = table.Column<decimal>(name: "SelladoCorte_Ancho", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    SelladoCorteLargo = table.Column<decimal>(name: "SelladoCorte_Largo", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    SelladoCorteFuelle = table.Column<decimal>(name: "SelladoCorte_Fuelle", type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    SelladoCortePesoMillar = table.Column<decimal>(name: "SelladoCorte_PesoMillar", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    TpSelladoId = table.Column<int>(name: "TpSellado_Id", type: "int", nullable: false),
                    SelladoCortePrecioSelladoDia = table.Column<decimal>(name: "SelladoCorte_PrecioSelladoDia", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    SelladoCortePrecioSelladoNoche = table.Column<decimal>(name: "SelladoCorte_PrecioSelladoNoche", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    SelladoCorteCantBolsasPaquete = table.Column<decimal>(name: "SelladoCorte_CantBolsasPaquete", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    SelladoCorteCantBolsasBulto = table.Column<decimal>(name: "SelladoCorte_CantBolsasBulto", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    SelladoCortePesoPaquete = table.Column<decimal>(name: "SelladoCorte_PesoPaquete", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    SelladoCortePesoBulto = table.Column<decimal>(name: "SelladoCorte_PesoBulto", type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Sellado_Corte", x => x.SelladoCorteId);
                    table.ForeignKey(
                        name: "FK_OT_Sellado_Corte_Orden_Trabajo_Ot_Id",
                        column: x => x.OtId,
                        principalTable: "Orden_Trabajo",
                        principalColumn: "Ot_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Sellado_Corte_Tipos_Productos_Formato_Id",
                        column: x => x.FormatoId,
                        principalTable: "Tipos_Productos",
                        principalColumn: "TpProd_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Sellado_Corte_Tipos_Sellados_TpSellado_Id",
                        column: x => x.TpSelladoId,
                        principalTable: "Tipos_Sellados",
                        principalColumn: "TpSellado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OT_Sellado_Corte_Formato_Id",
                table: "OT_Sellado_Corte",
                column: "Formato_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Sellado_Corte_Ot_Id",
                table: "OT_Sellado_Corte",
                column: "Ot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Sellado_Corte_TpSellado_Id",
                table: "OT_Sellado_Corte",
                column: "TpSellado_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OT_Sellado_Corte");

            migrationBuilder.DropColumn(
                name: "Ot_CantidadPedida",
                table: "Orden_Trabajo");

            migrationBuilder.RenameColumn(
                name: "Ot_ValorUnidad",
                table: "Orden_Trabajo",
                newName: "Ot_CantidadUnidades_Margen");

            migrationBuilder.RenameColumn(
                name: "Ot_ValorOT",
                table: "Orden_Trabajo",
                newName: "Ot_CantidadUnidades");

            migrationBuilder.RenameColumn(
                name: "Ot_ValorKg",
                table: "Orden_Trabajo",
                newName: "Ot_CantidadKilos_Margen");

            migrationBuilder.RenameColumn(
                name: "Ot_PesoNetoKg",
                table: "Orden_Trabajo",
                newName: "Ot_CantidadKilos");
        }
    }
}
