using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Asignacion_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Asignacion_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Asignacion_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion_MatPrima>>> GetAsignaciones_MatPrima()
        {
            if (_context.Asignaciones_MatPrima == null)
            {
                return NotFound();
            }
            return await _context.Asignaciones_MatPrima.ToListAsync();
        }

        // GET: api/Asignacion_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion_MatPrima>> GetAsignacion_MatPrima(long id)
        {
            if (_context.Asignaciones_MatPrima == null)
            {
                return NotFound();
            }
            var asignacion_MatPrima = await _context.Asignaciones_MatPrima.FindAsync(id);

            if (asignacion_MatPrima == null)
            {
                return NotFound();
            }

            return asignacion_MatPrima;
        }

        /// <summary>
        /// Datos del PDF de una asignación: encabezado y materiales (materias primas y tintas) agrupados por subcategoría.
        /// Si la asignación viene de una solicitud, agrega la distribución por máquina y, por subcategoría y material,
        /// lo solicitado, lo entregado en las asignaciones anteriores de la solicitud y lo que falta después de esta.
        /// Los saldos se cuentan hasta esta asignación (incluida), así el PDF muestra lo mismo cada vez que se reimprime.
        /// </summary>
        /// <param name="id">Número de la asignación.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Los datos del PDF; 404 si la asignación no existe.</returns>
        [HttpGet("{id:long}/pdf")]
        public async Task<IActionResult> ObtenerDatosPdf(long id, CancellationToken cancellationToken)
        {
            var asignacion = await _context.Asignaciones_MatPrima
                .AsNoTracking()
                .Where(a => a.AsigMp_Id == id)
                .Select(a => new
                {
                    a.AsigMp_Id,
                    a.AsigMP_OrdenTrabajo,
                    a.AsigMp_Maquina,
                    a.AsigMp_FechaEntrega,
                    a.AsigMp_Hora,
                    a.AsigMp_FechaRealEntrega,
                    Usuario = a.Usua!.Usua_Nombre,
                    a.AsigMp_Observacion,
                    a.SolMpExt_Id
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (asignacion == null) return NotFound($"La asignación N° {id} no existe.");

            // Sin solicitud o con la genérica (asignación manual): solo los materiales de esta asignación.
            long? solicitudId = asignacion.SolMpExt_Id is long s && s != EstadosSolicitudMaterial.SolicitudGenericaId ? s : null;

            // Materiales entregados: de esta asignación, o (con solicitud) de las asignaciones de la solicitud hasta esta.
            var mp = _context.DetallesAsignaciones_MateriasPrimas.AsNoTracking()
                .Where(d => d.MatPri_Id != SolicitudesMaterialService.MateriaPrimaGenericaId);
            var tintas = _context.DetalleAsignaciones_Tintas.AsNoTracking()
                .Where(d => d.Tinta_Id != SolicitudesMaterialService.TintaGenericaId);
            if (solicitudId is long sol)
            {
                mp = mp.Where(d => d.AsigMp!.SolMpExt_Id == sol && d.AsigMp_Id <= id);
                tintas = tintas.Where(d => d.AsigMp!.SolMpExt_Id == sol && d.AsigMp_Id <= id);
            }
            else
            {
                mp = mp.Where(d => d.AsigMp_Id == id);
                tintas = tintas.Where(d => d.AsigMp_Id == id);
            }

            var entregadoMp = await mp
                .Select(d => new FilaAsignadaPdf(d.AsigMp_Id, d.AsigMp!.AsigMp_Maquina, TipoMaterialInventario.MateriaPrima, d.MatPri_Id,
                    d.MatPri!.MatPri_Nombre, d.UndMed_Id, d.MatPri.SubCatMP_Id, d.DtAsigMp_Cantidad))
                .ToListAsync(cancellationToken);
            var entregadoTintas = await tintas
                .Select(d => new FilaAsignadaPdf(d.AsigMp_Id, d.AsigMp!.AsigMp_Maquina, TipoMaterialInventario.Tinta, d.Tinta_Id,
                    d.Tinta!.Tinta_Nombre, d.UndMed_Id, d.Tinta.SubCatMP_Id, d.DtAsigTinta_Cantidad))
                .ToListAsync(cancellationToken);
            List<FilaAsignadaPdf> entregados = [.. entregadoMp, .. entregadoTintas];
            var deEsta = entregados.Where(e => e.AsigMp_Id == id).ToList();

            // Lo solicitado por subcategoría y por material (vacío en las asignaciones manuales).
            var solicitadoSub = new Dictionary<int, (string Nombre, string UndMed_Id, decimal Cantidad)>();
            var solicitadoMaterial = new Dictionary<(TipoMaterialInventario, long), decimal>();
            object? solicitud = null;
            if (solicitudId is long idSol)
            {
                var encabezado = await _context.Solicitud_MatPrimaExtrusion
                    .AsNoTracking()
                    .Where(x => x.SolMpExt_Id == idSol)
                    .Select(x => new { x.SolMpExt_Maquina, Estado = x.Estado!.Estado_Nombre })
                    .FirstOrDefaultAsync(cancellationToken);

                solicitadoSub = (await _context.DetSolicitud_MatPrimaExtrusion
                    .AsNoTracking()
                    .Where(d => d.SolMpExt_Id == idSol)
                    .Select(d => new { d.SubCatMP_Id, d.SubCatMP_Nombre, d.UndMed_Id, d.DtSolMpExt_Cantidad })
                    .ToListAsync(cancellationToken))
                    .GroupBy(d => d.SubCatMP_Id)
                    .ToDictionary(g => g.Key, g => (g.First().SubCatMP_Nombre.Trim(), g.First().UndMed_Id, g.Sum(x => x.DtSolMpExt_Cantidad)));

                solicitadoMaterial = (await _context.DetSolicitud_Materiales
                    .AsNoTracking()
                    .Where(m => m.SolMpExt_Id == idSol)
                    .Select(m => new { m.DtSolMat_TipoMaterial, MaterialId = m.MatPri_Id ?? m.Tinta_Id ?? 0, m.DtSolMat_Cantidad })
                    .ToListAsync(cancellationToken))
                    .GroupBy(m => (m.DtSolMat_TipoMaterial, m.MaterialId))
                    .ToDictionary(g => g.Key, g => g.Sum(x => x.DtSolMat_Cantidad));

                var maquinas = await _context.Solicitud_Maquinas
                    .AsNoTracking()
                    .Where(m => m.SolMpExt_Id == idSol)
                    .OrderBy(m => m.SolMaq_Id)
                    .Select(m => new MaquinaSolicitud(m.SolMaq_Maquina, m.SolMaq_Porcentaje))
                    .ToListAsync(cancellationToken);
                if (maquinas.Count == 0 && encabezado != null) maquinas.Add(new MaquinaSolicitud(encabezado.SolMpExt_Maquina, 100m));

                // Lo entregado por máquina cuenta solo materiales con subcategoría (misma regla que el detalle de la solicitud).
                var totalSolicitado = solicitadoSub.Values.Sum(v => v.Cantidad);
                solicitud = new
                {
                    Id = idSol,
                    Estado = encabezado?.Estado,
                    Maquinas = maquinas.Select(m => new
                    {
                        m.Maquina,
                        m.Porcentaje,
                        Objetivo = Math.Round(totalSolicitado * m.Porcentaje / 100m, 2),
                        Entregado = entregados.Where(e => e.Maquina == m.Maquina && e.SubCatMP_Id != null).Sum(e => e.Cantidad)
                    })
                };
            }

            // Subcategorías de los materiales de esta asignación (las de la solicitud también, para su nombre).
            var idsSubcategorias = deEsta.Where(e => e.SubCatMP_Id != null).Select(e => e.SubCatMP_Id!.Value).Distinct().ToList();
            var nombresSubcategorias = await _context.Subcategorias_MatPrima
                .AsNoTracking()
                .Where(sc => idsSubcategorias.Contains(sc.SubCatMP_Id))
                .ToDictionaryAsync(sc => sc.SubCatMP_Id, sc => sc.SubCatMP_Nombre.Trim(), cancellationToken);

            var antes = entregados.Where(e => e.AsigMp_Id < id).ToList();
            var subcategorias = deEsta
                .GroupBy(e => e.SubCatMP_Id ?? 0)
                .Select(g =>
                {
                    solicitadoSub.TryGetValue(g.Key, out var pedido);
                    var solicitadoSubcategoria = pedido.Cantidad;
                    var antesSubcategoria = g.Key == 0 ? 0m : antes.Where(e => e.SubCatMP_Id == g.Key).Sum(e => e.Cantidad);
                    var estaSubcategoria = g.Sum(e => e.Cantidad);
                    var materiales = g
                        .GroupBy(e => (e.Tipo, e.MaterialId))
                        .Select(m =>
                        {
                            var primero = m.First();
                            var solicitadoMat = solicitadoMaterial.GetValueOrDefault(m.Key);
                            return new
                            {
                                TipoMaterial = m.Key.Tipo,
                                MaterialId = m.Key.MaterialId,
                                Nombre = primero.Nombre.Trim(),
                                primero.UndMed_Id,
                                Solicitado = solicitadoMat,
                                EntregadoAntes = antes.Where(e => e.Tipo == m.Key.Tipo && e.MaterialId == m.Key.MaterialId).Sum(e => e.Cantidad),
                                EstaAsignacion = m.Sum(e => e.Cantidad),
                                // Entregado en lugar de lo pedido: la subcategoría se pidió con materiales y este no estaba.
                                EsSustituto = solicitadoMat == 0 && solicitadoMaterial.Count > 0 && solicitadoSub.ContainsKey(g.Key)
                            };
                        })
                        .OrderBy(m => m.Nombre)
                        .ToList();
                    return new
                    {
                        SubCatMP_Id = g.Key,
                        Nombre = g.Key == 0 ? "SIN SUBCATEGORÍA" : nombresSubcategorias.GetValueOrDefault(g.Key) ?? pedido.Nombre ?? $"SUBCATEGORÍA {g.Key}",
                        UndMed_Id = pedido.UndMed_Id ?? g.First().UndMed_Id,
                        Solicitado = solicitadoSubcategoria,
                        EntregadoAntes = antesSubcategoria,
                        EstaAsignacion = estaSubcategoria,
                        Faltante = Math.Max(0m, solicitadoSubcategoria - antesSubcategoria - estaSubcategoria),
                        Materiales = materiales
                    };
                })
                .OrderBy(s => s.Nombre)
                .ToList();

            return Ok(new
            {
                Id = asignacion.AsigMp_Id,
                OT = asignacion.AsigMP_OrdenTrabajo,
                Maquina = asignacion.AsigMp_Maquina,
                Fecha = asignacion.AsigMp_FechaEntrega,
                Hora = asignacion.AsigMp_Hora,
                FechaReal = asignacion.AsigMp_FechaRealEntrega,
                asignacion.Usuario,
                Observacion = asignacion.AsigMp_Observacion,
                Solicitud = solicitud,
                Subcategorias = subcategorias
            });
        }

        /// <summary>Material entregado en una asignación, para el PDF.</summary>
        private sealed record FilaAsignadaPdf(
            long AsigMp_Id,
            int Maquina,
            TipoMaterialInventario Tipo,
            long MaterialId,
            string Nombre,
            string UndMed_Id,
            int? SubCatMP_Id,
            decimal Cantidad);

        //Consulta por orden de trabajo
        [HttpGet("ot/{AsigMP_OrdenTrabajo}")]
        public ActionResult<Asignacion_MatPrima> GetOt(long AsigMP_OrdenTrabajo)
        {
            var asignacion_MatPrima = _context.Asignaciones_MatPrima.Where(asg => asg.AsigMP_OrdenTrabajo == AsigMP_OrdenTrabajo).ToList();

            if (asignacion_MatPrima == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(asignacion_MatPrima);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion_MatPrima(long id, Asignacion_MatPrima asignacion_MatPrima)
        {
            if (id != asignacion_MatPrima.AsigMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacion_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asignacion_MatPrimaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Asignacion_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignacion_MatPrima>> PostAsignacion_MatPrima(Asignacion_MatPrima asignacion_MatPrima)
        {
            if (_context.Asignaciones_MatPrima == null)
            {
                return Problem("Entity set 'dataContext.Asignaciones_MatPrima'  is null.");
            }
            _context.Asignaciones_MatPrima.Add(asignacion_MatPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion_MatPrima", new { id = asignacion_MatPrima.AsigMp_Id }, asignacion_MatPrima);
        }

        // DELETE: api/Asignacion_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion_MatPrima(long id)
        {
            if (_context.Asignaciones_MatPrima == null)
            {
                return NotFound();
            }
            var asignacion_MatPrima = await _context.Asignaciones_MatPrima.FindAsync(id);
            if (asignacion_MatPrima == null)
            {
                return NotFound();
            }

            _context.Asignaciones_MatPrima.Remove(asignacion_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Asignacion_MatPrimaExists(long id)
        {
            return (_context.Asignaciones_MatPrima?.Any(e => e.AsigMp_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
