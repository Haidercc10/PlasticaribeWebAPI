using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Maquilas_Internas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maquilas_Internas",
                columns: table => new
                {
                    MaqInt_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaqInt_OT = table.Column<int>(type: "int", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Cono_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Ancho_Cono = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tara_Cono = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Peso_Bruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Peso_Neto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Presentacion = table.Column<string>(type: "varchar(10)", nullable: false),
                    MaqInt_Medida = table.Column<string>(type: "varchar(10)", nullable: true),
                    Maquina = table.Column<int>(type: "int", nullable: false),
                    Operario_Id = table.Column<long>(type: "bigint", nullable: false),
                    MaqInt_Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    SvcProd_Id = table.Column<int>(type: "int", nullable: false),
                    MaqInt_FechaRegistro = table.Column<DateTime>(type: "date", nullable: false),
                    MaqInt_HoraRegistro = table.Column<string>(type: "varchar(10)", nullable: false),
                    Creador_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquilas_Internas", x => x.MaqInt_Id);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Conos_Cono_Id",
                        column: x => x.Cono_Id,
                        principalTable: "Conos",
                        principalColumn: "Cono_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Servicios_Produccion_SvcProd_Id",
                        column: x => x.SvcProd_Id,
                        principalTable: "Servicios_Produccion",
                        principalColumn: "SvcProd_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Unidades_Medidas_Presentacion",
                        column: x => x.Presentacion,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Usuarios_Creador_Id",
                        column: x => x.Creador_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maquilas_Internas_Usuarios_Operario_Id",
                        column: x => x.Operario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Cono_Id",
                table: "Maquilas_Internas",
                column: "Cono_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Creador_Id",
                table: "Maquilas_Internas",
                column: "Creador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Estado_Id",
                table: "Maquilas_Internas",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Operario_Id",
                table: "Maquilas_Internas",
                column: "Operario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Presentacion",
                table: "Maquilas_Internas",
                column: "Presentacion");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Proceso_Id",
                table: "Maquilas_Internas",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_Prod_Id",
                table: "Maquilas_Internas",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_Internas_SvcProd_Id",
                table: "Maquilas_Internas",
                column: "SvcProd_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maquilas_Internas");
        }
    }
}
