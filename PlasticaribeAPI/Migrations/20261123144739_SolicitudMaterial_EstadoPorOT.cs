using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticaribeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SolicitudMaterial_EstadoPorOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Tipo de estado y estados propios de la solicitud y asignación de materia prima, con IDs fijos (el código
            //    los usa como constantes en EstadosSolicitudMaterial). Si los IDs ya están ocupados, se detiene.
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM Tipos_Estados WHERE TpEstado_Id = 10)
    THROW 50001, 'El TpEstado_Id 10 ya existe. Ajuste los IDs de EstadosSolicitudMaterial y de esta migración.', 1;
IF EXISTS (SELECT 1 FROM Estados WHERE Estado_Id BETWEEN 56 AND 61)
    THROW 50002, 'Algún Estado_Id entre 56 y 61 ya existe. Ajuste los IDs de EstadosSolicitudMaterial y de esta migración.', 1;

SET IDENTITY_INSERT Tipos_Estados ON;
INSERT INTO Tipos_Estados (TpEstado_Id, TpEstado_Nombre, TpEstado_Descripcion)
VALUES (10, 'Solicitud y asignación MP', 'Estados de las solicitudes de material de producción, de la solicitud de cada OT y de su asignación de materia prima.');
SET IDENTITY_INSERT Tipos_Estados OFF;

SET IDENTITY_INSERT Estados ON;
INSERT INTO Estados (Estado_Id, Estado_Nombre, Estado_Descripcion, TpEstado_Id) VALUES
(56, 'PENDIENTE',  'Solicitud de material (o solicitud de la OT) sin material asignado.', 10),
(57, 'PARCIAL',    'Con material asignado que aún no cubre lo solicitado (o, en la OT, que no llega al tope con el adicional).', 10),
(58, 'COMPLETADA', 'La solicitud recibió todo lo que pidió o su OT se completó; la OT llegó al tope o se completó a mano tras llegar a sus kg. No admite más asignaciones.', 10),
(59, 'FINALIZADA', 'Se cerró la asignación de materiales de la OT.', 10),
(60, 'VERIFICADA', 'El solicitante comprobó físicamente el material recibido para la OT.', 10),
(61, 'CANCELADA',  'Solicitud de material cancelada.', 10);
SET IDENTITY_INSERT Estados OFF;
");

            // 2) Las solicitudes existentes pasan a los estados nuevos (la solicitud genérica N° 1 no se toca):
            //    11 PENDIENTE → 56, 12 PARCIAL → 57, 5 FINALIZADO → 59, 55 VERIFICADO → 60, 4 CANCELADO → 61.
            migrationBuilder.Sql(@"
UPDATE Solicitud_MatPrimaExtrusion
SET Estado_Id = CASE Estado_Id WHEN 11 THEN 56 WHEN 12 THEN 57 WHEN 5 THEN 59 WHEN 55 THEN 60 WHEN 4 THEN 61 END
WHERE SolMpExt_Id <> 1 AND Estado_Id IN (11, 12, 5, 55, 4);
");

            // 3) Las activas se recalculan con la regla nueva: por el total de kg (no por subcategoría), igual que
            //    SolicitudesMaterialService.CalcularEstado. Lo asignado cuenta solo en las subcategorías solicitadas.
            migrationBuilder.Sql(@"
;WITH Solicitado AS (
    SELECT SolMpExt_Id, SubCatMP_Id, SUM(DtSolMpExt_Cantidad) AS Cantidad
    FROM DetSolicitud_MatPrimaExtrusion
    GROUP BY SolMpExt_Id, SubCatMP_Id
), Asignado AS (
    SELECT a.SolMpExt_Id, m.SubCatMP_Id, d.DtAsigMp_Cantidad AS Cantidad
    FROM DetallesAsignaciones_MateriasPrimas d
    JOIN Asignaciones_MatPrima a ON a.AsigMp_Id = d.AsigMp_Id
    JOIN Materias_Primas m ON m.MatPri_Id = d.MatPri_Id
    WHERE a.SolMpExt_Id IS NOT NULL AND m.SubCatMP_Id IS NOT NULL
    UNION ALL
    SELECT a.SolMpExt_Id, t.SubCatMP_Id, d.DtAsigTinta_Cantidad
    FROM DetalleAsignaciones_Tintas d
    JOIN Asignaciones_MatPrima a ON a.AsigMp_Id = d.AsigMp_Id
    JOIN Tintas t ON t.Tinta_Id = d.Tinta_Id
    WHERE a.SolMpExt_Id IS NOT NULL AND t.SubCatMP_Id IS NOT NULL
), Totales AS (
    SELECT s.SolMpExt_Id, SUM(s.Cantidad) AS Solicitado, SUM(ISNULL(x.Cantidad, 0)) AS Asignado
    FROM Solicitado s
    LEFT JOIN (SELECT SolMpExt_Id, SubCatMP_Id, SUM(Cantidad) AS Cantidad FROM Asignado GROUP BY SolMpExt_Id, SubCatMP_Id) x
        ON x.SolMpExt_Id = s.SolMpExt_Id AND x.SubCatMP_Id = s.SubCatMP_Id
    GROUP BY s.SolMpExt_Id
)
UPDATE se
SET Estado_Id = CASE WHEN t.Asignado <= 0 THEN 56 WHEN t.Asignado >= t.Solicitado THEN 58 ELSE 57 END
FROM Solicitud_MatPrimaExtrusion se
JOIN Totales t ON t.SolMpExt_Id = se.SolMpExt_Id
WHERE se.SolMpExt_Id <> 1 AND se.Estado_Id IN (56, 57);
");

            migrationBuilder.CreateTable(
                name: "SolicitudMaterial_OT",
                columns: table => new
                {
                    SolOT_OrdenTrabajo = table.Column<long>(type: "bigint", nullable: false),
                    SolOT_KgOT = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                    Estado_Id = table.Column<int>(type: "int", nullable: false),
                    SolOT_FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SolOT_FechaCompletada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Usua_IdCompleta = table.Column<long>(type: "bigint", nullable: true),
                    SolOT_FechaFinalizada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Usua_IdFinaliza = table.Column<long>(type: "bigint", nullable: true),
                    SolOT_FechaVerificada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Usua_IdVerifica = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudMaterial_OT", x => x.SolOT_OrdenTrabajo);
                    table.ForeignKey(
                        name: "FK_SolicitudMaterial_OT_Estados_Estado_Id",
                        column: x => x.Estado_Id,
                        principalTable: "Estados",
                        principalColumn: "Estado_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudMaterial_OT_Usuarios_Usua_IdCompleta",
                        column: x => x.Usua_IdCompleta,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudMaterial_OT_Usuarios_Usua_IdFinaliza",
                        column: x => x.Usua_IdFinaliza,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudMaterial_OT_Usuarios_Usua_IdVerifica",
                        column: x => x.Usua_IdVerifica,
                        principalTable: "Usuarios",
                        principalColumn: "Usua_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudMaterial_OT_Estado_Id",
                table: "SolicitudMaterial_OT",
                column: "Estado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudMaterial_OT_Usua_IdCompleta",
                table: "SolicitudMaterial_OT",
                column: "Usua_IdCompleta");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudMaterial_OT_Usua_IdFinaliza",
                table: "SolicitudMaterial_OT",
                column: "Usua_IdFinaliza");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudMaterial_OT_Usua_IdVerifica",
                table: "SolicitudMaterial_OT",
                column: "Usua_IdVerifica");

            // 4) Una solicitud de OT por cada OT con solicitudes. Sus kg se desconocen (están en BagPro) y se guardan
            //    cuando el front consulta la OT. Sin solicitudes activas ni completadas, la OT queda verificada si todas
            //    sus solicitudes no canceladas lo están y si no, finalizada; si tiene activas, parcial si ya tiene
            //    asignaciones y pendiente si no.
            migrationBuilder.Sql(@"
INSERT INTO SolicitudMaterial_OT (SolOT_OrdenTrabajo, SolOT_KgOT, Estado_Id, SolOT_FechaRegistro)
SELECT s.SolMpExt_OT, NULL,
    CASE
        WHEN SUM(CASE WHEN s.Estado_Id IN (56, 57, 58) THEN 1 ELSE 0 END) = 0
             AND SUM(CASE WHEN s.Estado_Id IN (59, 60) THEN 1 ELSE 0 END) > 0
            THEN CASE WHEN SUM(CASE WHEN s.Estado_Id = 59 THEN 1 ELSE 0 END) = 0 THEN 60 ELSE 59 END
        WHEN EXISTS (SELECT 1 FROM Asignaciones_MatPrima a WHERE a.AsigMP_OrdenTrabajo = s.SolMpExt_OT) THEN 57
        ELSE 56
    END,
    MIN(s.SolMpExt_Fecha)
FROM Solicitud_MatPrimaExtrusion s
WHERE s.SolMpExt_Id <> 1
GROUP BY s.SolMpExt_OT;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudMaterial_OT");

            // Las solicitudes vuelven a los estados anteriores (completada pasa a finalizado, como antes).
            migrationBuilder.Sql(@"
UPDATE Solicitud_MatPrimaExtrusion
SET Estado_Id = CASE Estado_Id WHEN 56 THEN 11 WHEN 57 THEN 12 WHEN 58 THEN 5 WHEN 59 THEN 5 WHEN 60 THEN 55 WHEN 61 THEN 4 END
WHERE Estado_Id BETWEEN 56 AND 61;

DELETE FROM Estados WHERE Estado_Id BETWEEN 56 AND 61 AND TpEstado_Id = 10;
DELETE FROM Tipos_Estados WHERE TpEstado_Id = 10;
");
        }
    }
}
