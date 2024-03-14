using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Desperdicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Desperdicios",
                columns: table => new
                {
                    DespId = table.Column<long>(name: "Desp_Id", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DespOT = table.Column<long>(name: "Desp_OT", type: "bigint", nullable: false),
                    ProdId = table.Column<int>(name: "Prod_Id", type: "int", nullable: false),
                    MaterialId = table.Column<int>(name: "Material_Id", type: "int", nullable: false),
                    DespMaquina = table.Column<long>(name: "Desp_Maquina", type: "bigint", nullable: false),
                    UsuaOperario = table.Column<long>(name: "Usua_Operario", type: "bigint", nullable: false),
                    DespImpresion = table.Column<string>(name: "Desp_Impresion", type: "varchar(10)", nullable: false),
                    AreaId = table.Column<long>(name: "Area_Id", type: "bigint", nullable: false),
                    FallaId = table.Column<int>(name: "Falla_Id", type: "int", nullable: false),
                    DespPesoKg = table.Column<decimal>(name: "Desp_PesoKg", type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DespFecha = table.Column<DateTime>(name: "Desp_Fecha", type: "date", nullable: false),
                    DespObservacion = table.Column<string>(name: "Desp_Observacion", type: "text", nullable: true),
                    UsuaId = table.Column<long>(name: "Usua_Id", type: "bigint", nullable: false),
                    DespFechaRegistro = table.Column<DateTime>(name: "Desp_FechaRegistro", type: "date", nullable: true),
                    DespHoraRegistro = table.Column<string>(name: "Desp_HoraRegistro", type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desperdicios", x => x.DespId);
                    table.ForeignKey(
                        name: "FK_Desperdicios_Areas_Area_Id",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desperdicios_Fallas_Tecnicas_Falla_Id",
                        column: x => x.FallaId,
                        principalTable: "Fallas_Tecnicas",
                        principalColumn: "Falla_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desperdicios_Materiales_MatPrima_Material_Id",
                        column: x => x.MaterialId,
                        principalTable: "Materiales_MatPrima",
                        principalColumn: "Material_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desperdicios_Productos_Prod_Id",
                        column: x => x.ProdId,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desperdicios_Usuarios_Usua_Id",
                        column: x => x.UsuaId,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Desperdicios_Usuarios_Usua_Operario",
                        column: x => x.UsuaOperario,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Area_Id",
                table: "Desperdicios",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Falla_Id",
                table: "Desperdicios",
                column: "Falla_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Material_Id",
                table: "Desperdicios",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Prod_Id",
                table: "Desperdicios",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Usua_Id",
                table: "Desperdicios",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Desperdicios_Usua_Operario",
                table: "Desperdicios",
                column: "Usua_Operario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desperdicios");

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
