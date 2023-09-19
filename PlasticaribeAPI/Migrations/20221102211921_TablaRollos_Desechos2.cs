using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class TablaRollos_Desechos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cono_Descripcion",
                table: "Conos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Rollos_Desechos",
                columns: table => new
                {
                    Rollo_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rollo_OT = table.Column<long>(type: "bigint", nullable: false),
                    Rollo_Cliente = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Rollo_TotalPedidoOT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Rollo_Id = table.Column<long>(type: "bigint", nullable: false),
                    Rollo_Ancho = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Rollo_Largo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Rollo_Fuelle = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UndMed_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Rollo_PesoBruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Rollo_PesoNeto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Rollo_Tara = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cono_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Material_Id = table.Column<int>(type: "int", nullable: false),
                    Rollo_Calibre = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Rollo_FechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    Rollo_Hora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Rollo_Maquina = table.Column<int>(type: "int", nullable: false),
                    Rollo_Operario = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Proceso_Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Turno_Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rollos_Desechos", x => x.Rollo_Codigo);
                    table.ForeignKey(
                        name: "FK_Rollos_Desechos_Conos_Cono_Id",
                        column: x => x.Cono_Id,
                        principalTable: "Conos",
                        principalColumn: "Cono_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rollos_Desechos_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rollos_Desechos_Materiales_MatPrima_Material_Id",
                        column: x => x.Material_Id,
                        principalTable: "Materiales_MatPrima",
                        principalColumn: "Material_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rollos_Desechos_Procesos_Proceso_Id",
                        column: x => x.Proceso_Id,
                        principalTable: "Procesos",
                        principalColumn: "Proceso_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rollos_Desechos_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rollos_Desechos_Turnos_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turnos",
                        principalColumn: "Turno_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rollos_Desechos_Unidades_Medidas_UndMed_Id",
                        column: x => x.UndMed_Id,
                        principalTable: "Unidades_Medidas",
                        principalColumn: "UndMed_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_Cono_Id",
                table: "Rollos_Desechos",
                column: "Cono_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_Estado_Id",
                table: "Rollos_Desechos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_Material_Id",
                table: "Rollos_Desechos",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_Proceso_Id",
                table: "Rollos_Desechos",
                column: "Proceso_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_Prod_Id",
                table: "Rollos_Desechos",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_Turno_Id",
                table: "Rollos_Desechos",
                column: "Turno_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rollos_Desechos_UndMed_Id",
                table: "Rollos_Desechos",
                column: "UndMed_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rollos_Desechos");

            migrationBuilder.AlterColumn<string>(
                name: "Cono_Descripcion",
                table: "Conos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
