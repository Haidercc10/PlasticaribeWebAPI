using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class Mantenimiento3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Pedidos_Mantenimientos",
                columns: table => new
                {
                    PedMttoId = table.Column<long>(name: "PedMtto_Id", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuaId = table.Column<long>(name: "Usua_Id", type: "bigint", nullable: false),
                    PedMttoFecha = table.Column<DateTime>(name: "PedMtto_Fecha", type: "date", nullable: true),
                    PedMttoHora = table.Column<string>(name: "PedMtto_Hora", type: "varchar(10)", nullable: true),
                    EstadoId = table.Column<int>(name: "Estado_Id", type: "int", nullable: false),
                    PedMttoObservacion = table.Column<string>(name: "PedMtto_Observacion", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos_Mantenimientos", x => x.PedMttoId);
                    table.ForeignKey(
                        name: "FK_Pedidos_Mantenimientos_Estados_Estado_Id",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Mantenimientos_Usuarios_Usua_Id",
                        column: x => x.UsuaId,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tipos_Activos",
                columns: table => new
                {
                    TpActvId = table.Column<int>(name: "TpActv_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpActvNombre = table.Column<string>(name: "TpActv_Nombre", type: "varchar(100)", nullable: false),
                    TpActvDescripcion = table.Column<string>(name: "TpActv_Descripcion", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Activos", x => x.TpActvId);
                });

            migrationBuilder.CreateTable(
                name: "Tipos_Mantenimientos",
                columns: table => new
                {
                    TpMttoId = table.Column<int>(name: "TpMtto_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TpMttoNombre = table.Column<string>(name: "TpMtto_Nombre", type: "varchar(100)", nullable: false),
                    TpMttoDescripcion = table.Column<string>(name: "TpMtto_Descripcion", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Mantenimientos", x => x.TpMttoId);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    MttoId = table.Column<long>(name: "Mtto_Id", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedMttoId = table.Column<long>(name: "PedMtto_Id", type: "bigint", nullable: false),
                    ProvId = table.Column<long>(name: "Prov_Id", type: "bigint", nullable: false),
                    MttoFechaInicio = table.Column<DateTime>(name: "Mtto_FechaInicio", type: "date", nullable: false),
                    MttoFechaFin = table.Column<DateTime>(name: "Mtto_FechaFin", type: "date", nullable: false),
                    MttoPrecioTotal = table.Column<decimal>(name: "Mtto_PrecioTotal", type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    EstadoId = table.Column<int>(name: "Estado_Id", type: "int", nullable: false),
                    MttoObservacion = table.Column<string>(name: "Mtto_Observacion", type: "text", nullable: true),
                    MttoCantDias = table.Column<int>(name: "Mtto_CantDias", type: "int", nullable: true),
                    UsuaId = table.Column<long>(name: "Usua_Id", type: "bigint", nullable: false),
                    MttoFechaRegistro = table.Column<DateTime>(name: "Mtto_FechaRegistro", type: "date", nullable: true),
                    MttoHoraRegistro = table.Column<string>(name: "Mtto_HoraRegistro", type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.MttoId);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Estados_Estado_Id",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Pedidos_Mantenimientos_PedMtto_Id",
                        column: x => x.PedMttoId,
                        principalTable: "Pedidos_Mantenimientos",
                        principalColumn: "PedMtto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Proveedores_Prov_Id",
                        column: x => x.ProvId,
                        principalTable: "Proveedores",
                        principalColumn: "Prov_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Usuarios_Usua_Id",
                        column: x => x.UsuaId,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activos",
                columns: table => new
                {
                    ActvId = table.Column<long>(name: "Actv_Id", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActvSerial = table.Column<string>(name: "Actv_Serial", type: "varchar(100)", nullable: false),
                    ActvNombre = table.Column<string>(name: "Actv_Nombre", type: "varchar(100)", nullable: false),
                    ActvMarca = table.Column<string>(name: "Actv_Marca", type: "varchar(100)", nullable: false),
                    ActvModelo = table.Column<string>(name: "Actv_Modelo", type: "varchar(100)", nullable: true),
                    TpActvId = table.Column<int>(name: "TpActv_Id", type: "int", nullable: false),
                    EstadoId = table.Column<int>(name: "Estado_Id", type: "int", nullable: false),
                    AreaId = table.Column<long>(name: "Area_Id", type: "bigint", nullable: false),
                    ActvFechaUltimoMtto = table.Column<DateTime>(name: "Actv_FechaUltimoMtto", type: "date", nullable: true),
                    ActvObservacion = table.Column<string>(name: "Actv_Observacion", type: "text", nullable: true),
                    ActvFechaCompra = table.Column<DateTime>(name: "Actv_FechaCompra", type: "date", nullable: true),
                    ActvPrecioCompra = table.Column<decimal>(name: "Actv_PrecioCompra", type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ActvDepreciacion = table.Column<decimal>(name: "Actv_Depreciacion", type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ActvFechaCreacion = table.Column<DateTime>(name: "Actv_FechaCreacion", type: "date", nullable: true),
                    ActvHoraCreacion = table.Column<string>(name: "Actv_HoraCreacion", type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activos", x => x.ActvId);
                    table.ForeignKey(
                        name: "FK_Activos_Areas_Area_Id",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Area_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activos_Estados_Estado_Id",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activos_Tipos_Activos_TpActv_Id",
                        column: x => x.TpActvId,
                        principalTable: "Tipos_Activos",
                        principalColumn: "TpActv_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalles_Mantenimientos",
                columns: table => new
                {
                    DtMttoCodigo = table.Column<long>(name: "DtMtto_Codigo", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MttoId = table.Column<long>(name: "Mtto_Id", type: "bigint", nullable: false),
                    ActvId = table.Column<long>(name: "Actv_Id", type: "bigint", nullable: false),
                    TpMttoId = table.Column<int>(name: "TpMtto_Id", type: "int", nullable: false),
                    DtMttoPrecio = table.Column<decimal>(name: "DtMtto_Precio", type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DtMttoDescripcion = table.Column<string>(name: "DtMtto_Descripcion", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles_Mantenimientos", x => x.DtMttoCodigo);
                    table.ForeignKey(
                        name: "FK_Detalles_Mantenimientos_Activos_Actv_Id",
                        column: x => x.ActvId,
                        principalTable: "Activos",
                        principalColumn: "Actv_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_Mantenimientos_Mantenimientos_Mtto_Id",
                        column: x => x.MttoId,
                        principalTable: "Mantenimientos",
                        principalColumn: "Mtto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalles_Mantenimientos_Tipos_Mantenimientos_TpMtto_Id",
                        column: x => x.TpMttoId,
                        principalTable: "Tipos_Mantenimientos",
                        principalColumn: "TpMtto_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPedidos_Mantenimientos",
                columns: table => new
                {
                    DtPedMttoCodigo = table.Column<long>(name: "DtPedMtto_Codigo", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedMttoId = table.Column<long>(name: "PedMtto_Id", type: "bigint", nullable: false),
                    ActvId = table.Column<long>(name: "Actv_Id", type: "bigint", nullable: false),
                    TpMttoId = table.Column<int>(name: "TpMtto_Id", type: "int", nullable: false),
                    DtPedMttoFechaFalla = table.Column<DateTime>(name: "DtPedMtto_FechaFalla", type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPedidos_Mantenimientos", x => x.DtPedMttoCodigo);
                    table.ForeignKey(
                        name: "FK_DetallesPedidos_Mantenimientos_Activos_Actv_Id",
                        column: x => x.ActvId,
                        principalTable: "Activos",
                        principalColumn: "Actv_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPedidos_Mantenimientos_Pedidos_Mantenimientos_PedMtto_Id",
                        column: x => x.PedMttoId,
                        principalTable: "Pedidos_Mantenimientos",
                        principalColumn: "PedMtto_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPedidos_Mantenimientos_Tipos_Mantenimientos_TpMtto_Id",
                        column: x => x.TpMttoId,
                        principalTable: "Tipos_Mantenimientos",
                        principalColumn: "TpMtto_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activos_Area_Id",
                table: "Activos",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_Estado_Id",
                table: "Activos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_TpActv_Id",
                table: "Activos",
                column: "TpActv_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Mantenimientos_Actv_Id",
                table: "Detalles_Mantenimientos",
                column: "Actv_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Mantenimientos_Mtto_Id",
                table: "Detalles_Mantenimientos",
                column: "Mtto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Mantenimientos_TpMtto_Id",
                table: "Detalles_Mantenimientos",
                column: "TpMtto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_Mantenimientos_Actv_Id",
                table: "DetallesPedidos_Mantenimientos",
                column: "Actv_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_Mantenimientos_PedMtto_Id",
                table: "DetallesPedidos_Mantenimientos",
                column: "PedMtto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_Mantenimientos_TpMtto_Id",
                table: "DetallesPedidos_Mantenimientos",
                column: "TpMtto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_Estado_Id",
                table: "Mantenimientos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_PedMtto_Id",
                table: "Mantenimientos",
                column: "PedMtto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_Prov_Id",
                table: "Mantenimientos",
                column: "Prov_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_Usua_Id",
                table: "Mantenimientos",
                column: "Usua_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Mantenimientos_Estado_Id",
                table: "Pedidos_Mantenimientos",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Mantenimientos_Usua_Id",
                table: "Pedidos_Mantenimientos",
                column: "Usua_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles_Mantenimientos");

            migrationBuilder.DropTable(
                name: "DetallesPedidos_Mantenimientos");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Activos");

            migrationBuilder.DropTable(
                name: "Tipos_Mantenimientos");

            migrationBuilder.DropTable(
                name: "Pedidos_Mantenimientos");

            migrationBuilder.DropTable(
                name: "Tipos_Activos");


        }
    }
}
