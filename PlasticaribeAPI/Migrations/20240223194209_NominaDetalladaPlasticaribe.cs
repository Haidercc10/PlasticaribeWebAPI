using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <inheritdoc />
    public partial class NominaDetalladaPlasticaribe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NominaDetallada_Plasticaribe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Trabajador = table.Column<long>(type: "bigint", nullable: false),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PeriodoInicio = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodoFin = table.Column<DateTime>(type: "date", nullable: false),
                    DiasAusente = table.Column<int>(type: "int", nullable: false),
                    DiasPagar = table.Column<int>(type: "int", nullable: false),
                    HorasPagar = table.Column<int>(type: "int", nullable: false),
                    ValorDiasPagar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiasIncapEG = table.Column<int>(type: "int", nullable: false),
                    ValorIncapEG = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiasIncapAT = table.Column<int>(type: "int", nullable: false),
                    ValorIncapAT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiasIncapPATMAT = table.Column<int>(type: "int", nullable: false),
                    ValorIncapPATMAT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HorasADCDiurnas = table.Column<int>(type: "int", nullable: false),
                    ValorADCDiurnas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HorasNoctDom = table.Column<int>(type: "int", nullable: false),
                    ValorNoctDom = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HorasExtDiurnasDom = table.Column<int>(type: "int", nullable: false),
                    ValorExtDiurnasDom = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HorasRecargo035 = table.Column<int>(type: "int", nullable: false),
                    ValorRecargo035 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HorasExtNocturnasDom = table.Column<int>(type: "int", nullable: false),
                    ValorExtNocturnasDom = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HorasRecargo075 = table.Column<int>(type: "int", nullable: false),
                    ValorRecargo075 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HorasRecargo100 = table.Column<int>(type: "int", nullable: false),
                    ValorRecargo100 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TarifaADC = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotalADCComp = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AuxTransporte = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductividadSella = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductividadExt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductividadMontaje = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Devengado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EPS = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AFP = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ahorro = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Prestamo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Anticipo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalDcto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PagoPTESemanaAnt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Dctos = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Deducciones = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPagar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Novedades = table.Column<string>(type: "varchar(max)", nullable: false),
                    TipoNomina = table.Column<int>(type: "int", nullable: false),
                    Estado_Nomina = table.Column<int>(type: "int", nullable: false),
                    Creador_Id = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Hora = table.Column<string>(type: "varchar(20)", precision: 18, scale: 2, nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaDetallada_Plasticaribe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominaDetallada_Plasticaribe_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id");
                    table.ForeignKey(
                        name: "FK_NominaDetallada_Plasticaribe_Tipos_Nomina_Estado_Nomina",
                        column: x => x.Estado_Nomina,
                        principalTable: "Tipos_Nomina",
                        principalColumn: "TpNomina_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaDetallada_Plasticaribe_Usuarios_Creador_Id",
                        column: x => x.Creador_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NominaDetallada_Plasticaribe_Usuarios_Id_Trabajador",
                        column: x => x.Id_Trabajador,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Ptm_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Ptm_Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ptm_ValorDeuda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ptm_ValorCancelado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ptm_ValorCuota = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ptm_PctjeCuota = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Ptm_FechaPlazo = table.Column<DateTime>(type: "date", nullable: false),
                    Ptm_FechaUltCuota = table.Column<DateTime>(type: "date", nullable: false),
                    Ptm_Observacion = table.Column<string>(type: "varchar(max)", nullable: false),
                    Creador_Id = table.Column<long>(type: "bigint", nullable: false),
                    Ptm_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Ptm_Hora = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Ptm_Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_Creador_Id",
                        column: x => x.Creador_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalariosTrabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Trabajador = table.Column<long>(type: "bigint", nullable: false),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AuxilioTransp = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EPSMensual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AFPMensual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AhorroTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalariosTrabajadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalariosTrabajadores_Usuarios_Id_Trabajador",
                        column: x => x.Id_Trabajador,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoIncapacidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIncapacidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incapacidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Trabajador = table.Column<long>(type: "bigint", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false),
                    CantDias = table.Column<int>(type: "int", nullable: false),
                    TotalPagar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Id_TipoIncapacidad = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "date", nullable: false),
                    HoraRegistro = table.Column<string>(type: "varchar(50)", nullable: false),
                    Creador_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incapacidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incapacidades_TipoIncapacidad_Id_TipoIncapacidad",
                        column: x => x.Id_TipoIncapacidad,
                        principalTable: "TipoIncapacidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incapacidades_Usuarios_Creador_Id",
                        column: x => x.Creador_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incapacidades_Usuarios_Id_Trabajador",
                        column: x => x.Id_Trabajador,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incapacidades_Creador_Id",
                table: "Incapacidades",
                column: "Creador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Incapacidades_Id_TipoIncapacidad",
                table: "Incapacidades",
                column: "Id_TipoIncapacidad");

            migrationBuilder.CreateIndex(
                name: "IX_Incapacidades_Id_Trabajador",
                table: "Incapacidades",
                column: "Id_Trabajador");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetallada_Plasticaribe_Creador_Id",
                table: "NominaDetallada_Plasticaribe",
                column: "Creador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetallada_Plasticaribe_Estado_Id",
                table: "NominaDetallada_Plasticaribe",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetallada_Plasticaribe_Estado_Nomina",
                table: "NominaDetallada_Plasticaribe",
                column: "Estado_Nomina");

            migrationBuilder.CreateIndex(
                name: "IX_NominaDetallada_Plasticaribe_Id_Trabajador",
                table: "NominaDetallada_Plasticaribe",
                column: "Id_Trabajador");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_Creador_Id",
                table: "Prestamos",
                column: "Creador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_Estado_Id",
                table: "Prestamos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_Usua_Id",
                table: "Prestamos",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SalariosTrabajadores_Id_Trabajador",
                table: "SalariosTrabajadores",
                column: "Id_Trabajador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incapacidades");

            migrationBuilder.DropTable(
                name: "NominaDetallada_Plasticaribe");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "SalariosTrabajadores");

            migrationBuilder.DropTable(
                name: "TipoIncapacidad");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
