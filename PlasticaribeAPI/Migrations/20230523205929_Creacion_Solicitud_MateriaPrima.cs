using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class Creacion_Solicitud_MateriaPrima : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitud_MateriaPrima",
                columns: table => new
                {
                    Solicitud_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Solicitud_Observacion = table.Column<string>(type: "varchar(max)", nullable: false, defaultValue: ""),
                    Solicitud_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Solicitud_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud_MateriaPrima", x => x.Solicitud_Id);
                    table.ForeignKey(
                        name: "FK_Solicitud_MateriaPrima_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitud_MateriaPrima_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_SolicitudMateriaPrima",
                columns: table => new
                {
                    DtSolicitud_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solicitud_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta_Id = table.Column<long>(type: "bigint", nullable: false),
                    Bopp_Id = table.Column<long>(type: "bigint", nullable: false),
                    DtSolicitud_Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_SolicitudMateriaPrima", x => x.DtSolicitud_Id);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudMateriaPrima_Bopp_Generico_Bopp_Id",
                        column: x => x.Bopp_Id,
                        principalTable: "Bopp_Generico",
                        principalColumn: "BoppGen_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudMateriaPrima_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudMateriaPrima_Materias_Primas_MatPri_Id",
                        column: x => x.MatPri_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudMateriaPrima_Solicitud_MateriaPrima_Solicitud_Id",
                        column: x => x.Solicitud_Id,
                        principalTable: "Solicitud_MateriaPrima",
                        principalColumn: "Solicitud_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudMateriaPrima_Tintas_Tinta_Id",
                        column: x => x.Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_SolicitudMateriaPrima_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesMP_OrdenesCompra",
                columns: table => new
                {
                    Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oc_Id = table.Column<long>(type: "bigint", nullable: false),
                    Solicitud_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesMP_OrdenesCompra", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_SolicitudesMP_OrdenesCompra_Ordenes_Compras_Oc_Id",
                        column: x => x.Oc_Id,
                        principalTable: "Ordenes_Compras",
                        principalColumn: "Oc_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudesMP_OrdenesCompra_Solicitud_MateriaPrima_Solicitud_Id",
                        column: x => x.Solicitud_Id,
                        principalTable: "Solicitud_MateriaPrima",
                        principalColumn: "Solicitud_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudMateriaPrima_Bopp_Id",
                table: "Detalles_SolicitudMateriaPrima",
                column: "Bopp_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudMateriaPrima_Estado_Id",
                table: "Detalles_SolicitudMateriaPrima",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudMateriaPrima_MatPri_Id",
                table: "Detalles_SolicitudMateriaPrima",
                column: "MatPri_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudMateriaPrima_Solicitud_Id",
                table: "Detalles_SolicitudMateriaPrima",
                column: "Solicitud_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudMateriaPrima_Tinta_Id",
                table: "Detalles_SolicitudMateriaPrima",
                column: "Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_SolicitudMateriaPrima_UndMed_Id",
                table: "Detalles_SolicitudMateriaPrima",
                column: "UndMed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_MateriaPrima_Estado_Id",
                table: "Solicitud_MateriaPrima",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_MateriaPrima_Usua_Id",
                table: "Solicitud_MateriaPrima",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesMP_OrdenesCompra_Oc_Id",
                table: "SolicitudesMP_OrdenesCompra",
                column: "Oc_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesMP_OrdenesCompra_Solicitud_Id",
                table: "SolicitudesMP_OrdenesCompra",
                column: "Solicitud_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_SolicitudMateriaPrima");

            migrationBuilder.DropTable(
                name: "SolicitudesMP_OrdenesCompra");

            migrationBuilder.DropTable(
                name: "Solicitud_MateriaPrima");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
