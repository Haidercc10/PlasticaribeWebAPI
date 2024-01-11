using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    public partial class Inicio_OT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prod_Peso_Neto",
                table: "Productos",
                newName: "Prod_Peso_Millar");

            migrationBuilder.RenameColumn(
                name: "Prod_Peso_Bruto",
                table: "Productos",
                newName: "Prod_Peso");

            migrationBuilder.AlterColumn<decimal>(
                name: "Prod_Peso_Millar",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Prod_Peso",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasBulto",
                table: "Productos",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prod_CantBolsasPaquete",
                table: "Productos",
                type: "int",
                precision: 14,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TpSellado_Id",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Formato",
                columns: table => new
                {
                    Formato_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Formato_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Formato_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formato", x => x.Formato_Id);
                });

            migrationBuilder.CreateTable(
                name: "Laminado_Capa",
                columns: table => new
                {
                    LamCapa_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LamCapa_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    LamCapa_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laminado_Capa", x => x.LamCapa_Id);
                });

            migrationBuilder.CreateTable(
                name: "Orden_Trabajo",
                columns: table => new
                {
                    Ot_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SedeCli_Id = table.Column<long>(type: "bigint", nullable: false),
                    Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Ot_CantidadKilos = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Ot_CantidadUnidades = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Ot_MargenAdicional = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Ot_CantidadKilos_Margen = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Ot_CantidadUnidades_Margen = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Ot_FechaCreacion = table.Column<DateTime>(type: "Date", nullable: false),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    Usua_Id = table.Column<long>(type: "bigint", nullable: false),
                    PedExt_Id = table.Column<long>(type: "bigint", nullable: false),
                    Ot_Observacion = table.Column<string>(type: "text", nullable: false),
                    Ot_Cyrel = table.Column<string>(type: "varchar(10)", nullable: false),
                    Ot_Corte = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden_Trabajo", x => x.Ot_Id);
                    table.ForeignKey(
                        name: "FK_Orden_Trabajo_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orden_Trabajo_Pedidos_Externos_PedExt_Id",
                        column: x => x.PedExt_Id,
                        principalTable: "Pedidos_Externos",
                        principalColumn: "PedExt_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orden_Trabajo_Productos_Prod_Id",
                        column: x => x.Prod_Id,
                        principalTable: "Productos",
                        principalColumn: "Prod_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orden_Trabajo_Sedes_Clientes_SedeCli_Id",
                        column: x => x.SedeCli_Id,
                        principalTable: "Sedes_Clientes",
                        principalColumn: "SedeCli_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orden_Trabajo_Usuarios_Usua_Id",
                        column: x => x.Usua_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pistas",
                columns: table => new
                {
                    Pista_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pista_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Pista_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pistas", x => x.Pista_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rodillos",
                columns: table => new
                {
                    Rodillo_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rodillo_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Rodillo_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodillos", x => x.Rodillo_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos_Impresion",
                columns: table => new
                {
                    TpImpresion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpImpresion_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Tp_Impresion_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Impresion", x => x.TpImpresion_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos_Sellados",
                columns: table => new
                {
                    TpSellado_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpSellados_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TpSellado_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Sellados", x => x.TpSellado_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tratado",
                columns: table => new
                {
                    Tratado_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tratado_Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Tratado_Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratado", x => x.Tratado_Id);
                });

            migrationBuilder.CreateTable(
                name: "OT_Laminado",
                columns: table => new
                {
                    LamCapa_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OT_Id = table.Column<long>(type: "bigint", nullable: false),
                    Capa_Id1 = table.Column<int>(type: "int", nullable: false),
                    Capa_Id2 = table.Column<int>(type: "int", nullable: false),
                    Capa_Id3 = table.Column<int>(type: "int", nullable: false),
                    LamCapa_Calibre1 = table.Column<double>(type: "float(14)", precision: 14, scale: 2, nullable: false),
                    LamCapa_Calibre2 = table.Column<double>(type: "float(14)", precision: 14, scale: 2, nullable: false),
                    LamCapa_Calibre3 = table.Column<double>(type: "float(14)", precision: 14, scale: 2, nullable: false),
                    LamCapa_Cantidad1 = table.Column<double>(type: "float(14)", precision: 14, scale: 2, nullable: false),
                    LamCapa_Cantidad2 = table.Column<double>(type: "float(14)", precision: 14, scale: 2, nullable: false),
                    LamCapa_Cantidad3 = table.Column<double>(type: "float(14)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Laminado", x => x.LamCapa_Id);
                    table.ForeignKey(
                        name: "FK_OT_Laminado_Laminado_Capa_Capa_Id1",
                        column: x => x.Capa_Id1,
                        principalTable: "Laminado_Capa",
                        principalColumn: "LamCapa_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Laminado_Laminado_Capa_Capa_Id2",
                        column: x => x.Capa_Id2,
                        principalTable: "Laminado_Capa",
                        principalColumn: "LamCapa_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Laminado_Laminado_Capa_Capa_Id3",
                        column: x => x.Capa_Id3,
                        principalTable: "Laminado_Capa",
                        principalColumn: "LamCapa_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Laminado_Orden_Trabajo_OT_Id",
                        column: x => x.OT_Id,
                        principalTable: "Orden_Trabajo",
                        principalColumn: "Ot_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_Impresion",
                columns: table => new
                {
                    Impresion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ot_Id = table.Column<long>(type: "bigint", nullable: false),
                    TpImpresion_Id = table.Column<int>(type: "int", nullable: false),
                    Rodillo_Id = table.Column<int>(type: "int", nullable: false),
                    Pista_Id = table.Column<int>(type: "int", nullable: false),
                    Tinta1_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta2_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta3_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta4_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta5_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta6_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta7_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta8_Id = table.Column<long>(type: "bigint", nullable: false),
                    Tinta8Tinta_Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Impresion", x => x.Impresion_Id);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Orden_Trabajo_Ot_Id",
                        column: x => x.Ot_Id,
                        principalTable: "Orden_Trabajo",
                        principalColumn: "Ot_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Pistas_Pista_Id",
                        column: x => x.Pista_Id,
                        principalTable: "Pistas",
                        principalColumn: "Pista_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Rodillos_Rodillo_Id",
                        column: x => x.Rodillo_Id,
                        principalTable: "Rodillos",
                        principalColumn: "Rodillo_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta1_Id",
                        column: x => x.Tinta1_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta2_Id",
                        column: x => x.Tinta2_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta3_Id",
                        column: x => x.Tinta3_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta4_Id",
                        column: x => x.Tinta4_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta5_Id",
                        column: x => x.Tinta5_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta6_Id",
                        column: x => x.Tinta6_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta8_Id",
                        column: x => x.Tinta8_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tintas_Tinta8Tinta_Id",
                        column: x => x.Tinta8Tinta_Id,
                        principalTable: "Tintas",
                        principalColumn: "Tinta_Id");
                    table.ForeignKey(
                        name: "FK_OT_Impresion_Tipos_Impresion_TpImpresion_Id",
                        column: x => x.TpImpresion_Id,
                        principalTable: "Tipos_Impresion",
                        principalColumn: "TpImpresion_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_Extrusion",
                columns: table => new
                {
                    Extrusion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ot_Id = table.Column<long>(type: "bigint", nullable: false),
                    MatPri_Id = table.Column<int>(type: "int", nullable: false),
                    Formato_Id = table.Column<long>(type: "bigint", nullable: false),
                    Pigmt_Id = table.Column<int>(type: "int", nullable: false),
                    Extrusion_Calibre = table.Column<int>(type: "int", precision: 14, scale: 2, nullable: false),
                    Extrusion_Ancho1 = table.Column<int>(type: "int", precision: 14, scale: 2, nullable: false),
                    Extrusion_Ancho2 = table.Column<int>(type: "int", precision: 14, scale: 2, nullable: false),
                    Extrusion_Ancho3 = table.Column<int>(type: "int", precision: 14, scale: 2, nullable: false),
                    Tratado_Id = table.Column<int>(type: "int", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Extrusion", x => x.Extrusion_Id);
                    table.ForeignKey(
                        name: "FK_OT_Extrusion_Formato_Formato_Id",
                        column: x => x.Formato_Id,
                        principalTable: "Formato",
                        principalColumn: "Formato_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Extrusion_Materias_Primas_Formato_Id",
                        column: x => x.Formato_Id,
                        principalTable: "Materias_Primas",
                        principalColumn: "MatPri_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Extrusion_Orden_Trabajo_Ot_Id",
                        column: x => x.Ot_Id,
                        principalTable: "Orden_Trabajo",
                        principalColumn: "Ot_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Extrusion_Pigmentos_Pigmt_Id",
                        column: x => x.Pigmt_Id,
                        principalTable: "Pigmentos",
                        principalColumn: "Pigmt_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Extrusion_Tratado_Tratado_Id",
                        column: x => x.Tratado_Id,
                        principalTable: "Tratado",
                        principalColumn: "Tratado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_TpSellado_Id",
                table: "Productos",
                column: "TpSellado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Estado_Id",
                table: "Orden_Trabajo",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_PedExt_Id",
                table: "Orden_Trabajo",
                column: "PedExt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Prod_Id",
                table: "Orden_Trabajo",
                column: "Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_SedeCli_Id",
                table: "Orden_Trabajo",
                column: "SedeCli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Trabajo_Usua_Id",
                table: "Orden_Trabajo",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Extrusion_Formato_Id",
                table: "OT_Extrusion",
                column: "Formato_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Extrusion_Ot_Id",
                table: "OT_Extrusion",
                column: "Ot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Extrusion_Pigmt_Id",
                table: "OT_Extrusion",
                column: "Pigmt_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Extrusion_Tratado_Id",
                table: "OT_Extrusion",
                column: "Tratado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Ot_Id",
                table: "OT_Impresion",
                column: "Ot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Pista_Id",
                table: "OT_Impresion",
                column: "Pista_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Rodillo_Id",
                table: "OT_Impresion",
                column: "Rodillo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta1_Id",
                table: "OT_Impresion",
                column: "Tinta1_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta2_Id",
                table: "OT_Impresion",
                column: "Tinta2_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta3_Id",
                table: "OT_Impresion",
                column: "Tinta3_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta4_Id",
                table: "OT_Impresion",
                column: "Tinta4_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta5_Id",
                table: "OT_Impresion",
                column: "Tinta5_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta6_Id",
                table: "OT_Impresion",
                column: "Tinta6_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta8_Id",
                table: "OT_Impresion",
                column: "Tinta8_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_Tinta8Tinta_Id",
                table: "OT_Impresion",
                column: "Tinta8Tinta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Impresion_TpImpresion_Id",
                table: "OT_Impresion",
                column: "TpImpresion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Laminado_Capa_Id1",
                table: "OT_Laminado",
                column: "Capa_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Laminado_Capa_Id2",
                table: "OT_Laminado",
                column: "Capa_Id2");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Laminado_Capa_Id3",
                table: "OT_Laminado",
                column: "Capa_Id3");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Laminado_OT_Id",
                table: "OT_Laminado",
                column: "OT_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Tipos_Sellados_TpSellado_Id",
                table: "Productos",
                column: "TpSellado_Id",
                principalTable: "Tipos_Sellados",
                principalColumn: "TpSellado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Tipos_Sellados_TpSellado_Id",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "OT_Extrusion");

            migrationBuilder.DropTable(
                name: "OT_Impresion");

            migrationBuilder.DropTable(
                name: "OT_Laminado");

            migrationBuilder.DropTable(
                name: "Tipos_Sellados");

            migrationBuilder.DropTable(
                name: "Formato");

            migrationBuilder.DropTable(
                name: "Tratado");

            migrationBuilder.DropTable(
                name: "Pistas");

            migrationBuilder.DropTable(
                name: "Rodillos");

            migrationBuilder.DropTable(
                name: "Tipos_Impresion");

            migrationBuilder.DropTable(
                name: "Laminado_Capa");

            migrationBuilder.DropTable(
                name: "Orden_Trabajo");

            migrationBuilder.DropIndex(
                name: "IX_Productos_TpSellado_Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasBulto",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Prod_CantBolsasPaquete",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TpSellado_Id",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "Prod_Peso_Millar",
                table: "Productos",
                newName: "Prod_Peso_Neto");

            migrationBuilder.RenameColumn(
                name: "Prod_Peso",
                table: "Productos",
                newName: "Prod_Peso_Bruto");

            migrationBuilder.AlterColumn<decimal>(
                name: "Prod_Peso_Neto",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "Prod_Peso_Bruto",
                table: "Productos",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);
        }
    }
}
