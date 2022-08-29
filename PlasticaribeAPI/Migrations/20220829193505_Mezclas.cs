using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Mezclas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mezclas",
                columns: table => new
                {
                    Mezcla_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mezcla_Nombre = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Mezcla_NroCapas = table.Column<int>(type: "int", nullable: false),
                    Material_Id = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeCapa1 = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id1xCapa1 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial1_Capa1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id2xCapa1 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial2_Capa1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id3xCapa1 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial3_Capa1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id4xCapa1 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial4_Capa1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezPigmto_Id1xCapa1 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajePigmto1_Capa1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezPigmto_Id2xCapa1 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajePigmto2_Capa1 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Mezcla_PorcentajeCapa2 = table.Column<int>(type: "int", nullable: false),
                    MezMaterial_Id1xCapa2 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial1_Capa2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id2xCapa2 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial2_Capa2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id3xCapa2 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial3_Capa2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id4xCapa2 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial4_Capa2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezPigmto_Id1xCapa2 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajePigmto1_Capa2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezPigmto_Id2xCapa2 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajePigmto2_Capa2 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Mezcla_PorcentajeCapa3 = table.Column<int>(type: "int", nullable: false),
                    MezMaterial_Id1xCapa3 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial1_Capa3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id2xCapa3 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial2_Capa3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id3xCapa3 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial3_Capa3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezMaterial_Id4xCapa3 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajeMaterial4_Capa3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezPigmto_Id1xCapa3 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajePigmto1_Capa3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MezPigmto_Id2xCapa3 = table.Column<int>(type: "int", nullable: false),
                    Mezcla_PorcentajePigmto2_Capa3 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    Mezcla_FechaIngreso = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mezclas", x => x.Mezcla_Id);
                    table.ForeignKey(
                        name: "FK_Mezclas_Materiales_MatPrima_Material_Id",
                        column: x => x.Material_Id,
                        principalTable: "Materiales_MatPrima",
                        principalColumn: "Material_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id1xCapa1",
                        column: x => x.MezMaterial_Id1xCapa1,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id1xCapa2",
                        column: x => x.MezMaterial_Id1xCapa2,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id1xCapa3",
                        column: x => x.MezMaterial_Id1xCapa3,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id2xCapa1",
                        column: x => x.MezMaterial_Id2xCapa1,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id2xCapa2",
                        column: x => x.MezMaterial_Id2xCapa2,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id2xCapa3",
                        column: x => x.MezMaterial_Id2xCapa3,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id3xCapa1",
                        column: x => x.MezMaterial_Id3xCapa1,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id3xCapa2",
                        column: x => x.MezMaterial_Id3xCapa2,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id3xCapa3",
                        column: x => x.MezMaterial_Id3xCapa3,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id4xCapa1",
                        column: x => x.MezMaterial_Id4xCapa1,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id4xCapa2",
                        column: x => x.MezMaterial_Id4xCapa2,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Materiales_MezMaterial_Id4xCapa3",
                        column: x => x.MezMaterial_Id4xCapa3,
                        principalTable: "Mezclas_Materiales",
                        principalColumn: "MezMaterial_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Pigmentos_MezPigmto_Id1xCapa1",
                        column: x => x.MezPigmto_Id1xCapa1,
                        principalTable: "Mezclas_Pigmentos",
                        principalColumn: "MezPigmto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Pigmentos_MezPigmto_Id1xCapa2",
                        column: x => x.MezPigmto_Id1xCapa2,
                        principalTable: "Mezclas_Pigmentos",
                        principalColumn: "MezPigmto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Pigmentos_MezPigmto_Id1xCapa3",
                        column: x => x.MezPigmto_Id1xCapa3,
                        principalTable: "Mezclas_Pigmentos",
                        principalColumn: "MezPigmto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Pigmentos_MezPigmto_Id2xCapa1",
                        column: x => x.MezPigmto_Id2xCapa1,
                        principalTable: "Mezclas_Pigmentos",
                        principalColumn: "MezPigmto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Pigmentos_MezPigmto_Id2xCapa2",
                        column: x => x.MezPigmto_Id2xCapa2,
                        principalTable: "Mezclas_Pigmentos",
                        principalColumn: "MezPigmto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Mezclas_Pigmentos_MezPigmto_Id2xCapa3",
                        column: x => x.MezPigmto_Id2xCapa3,
                        principalTable: "Mezclas_Pigmentos",
                        principalColumn: "MezPigmto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mezclas_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_Material_Id",
                table: "Mezclas",
                column: "Material_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id1xCapa1",
                table: "Mezclas",
                column: "MezMaterial_Id1xCapa1");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id1xCapa2",
                table: "Mezclas",
                column: "MezMaterial_Id1xCapa2");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id1xCapa3",
                table: "Mezclas",
                column: "MezMaterial_Id1xCapa3");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id2xCapa1",
                table: "Mezclas",
                column: "MezMaterial_Id2xCapa1");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id2xCapa2",
                table: "Mezclas",
                column: "MezMaterial_Id2xCapa2");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id2xCapa3",
                table: "Mezclas",
                column: "MezMaterial_Id2xCapa3");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id3xCapa1",
                table: "Mezclas",
                column: "MezMaterial_Id3xCapa1");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id3xCapa2",
                table: "Mezclas",
                column: "MezMaterial_Id3xCapa2");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id3xCapa3",
                table: "Mezclas",
                column: "MezMaterial_Id3xCapa3");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id4xCapa1",
                table: "Mezclas",
                column: "MezMaterial_Id4xCapa1");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id4xCapa2",
                table: "Mezclas",
                column: "MezMaterial_Id4xCapa2");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezMaterial_Id4xCapa3",
                table: "Mezclas",
                column: "MezMaterial_Id4xCapa3");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezPigmto_Id1xCapa1",
                table: "Mezclas",
                column: "MezPigmto_Id1xCapa1");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezPigmto_Id1xCapa2",
                table: "Mezclas",
                column: "MezPigmto_Id1xCapa2");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezPigmto_Id1xCapa3",
                table: "Mezclas",
                column: "MezPigmto_Id1xCapa3");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezPigmto_Id2xCapa1",
                table: "Mezclas",
                column: "MezPigmto_Id2xCapa1");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezPigmto_Id2xCapa2",
                table: "Mezclas",
                column: "MezPigmto_Id2xCapa2");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_MezPigmto_Id2xCapa3",
                table: "Mezclas",
                column: "MezPigmto_Id2xCapa3");

            migrationBuilder.CreateIndex(
                name: "IX_Mezclas_Usua_Id",
                table: "Mezclas",
                column: "Usua_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mezclas");
        }
    }
}
