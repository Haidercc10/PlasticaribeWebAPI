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
    public class DetSolicitud_MatPrimaExtrusionController : ControllerBase
    {
        private const long EmpresaPlasticaribeId = 800188732;

        private readonly dataContext _context;
        private readonly SolicitudesMaterialService _solicitudes;

        public DetSolicitud_MatPrimaExtrusionController(dataContext context, SolicitudesMaterialService solicitudes)
        {
            _context = context;
            _solicitudes = solicitudes;
        }

        // GET: api/DetSolicitud_MatPrimaExtrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetSolicitud_MatPrimaExtrusion>>> GetDetSolicitud_MatPrimaExtrusion()
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            return await _context.DetSolicitud_MatPrimaExtrusion.ToListAsync();
        }

        // GET: api/DetSolicitud_MatPrimaExtrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetSolicitud_MatPrimaExtrusion>> GetDetSolicitud_MatPrimaExtrusion(long id)
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            var detSolicitud_MatPrimaExtrusion = await _context.DetSolicitud_MatPrimaExtrusion.FindAsync(id);

            if (detSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }

            return detSolicitud_MatPrimaExtrusion;
        }

        
        /// <summary>
        /// Consulta una solicitud con sus detalles. Lo entregado, lo restante y el stock se calculan por subcategoría
        /// sumando materias primas y tintas.
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Una fila por detalle con los datos del encabezado y de la empresa; lista vacía si no existe.</returns>
        [HttpGet("getSolicitudMp_Extrusion/{id}")]
        public async Task<ActionResult> GetSolicitudMp_Extrusion(long id, CancellationToken cancellationToken)
        {
            var filas = await _context.DetSolicitud_MatPrimaExtrusion
                .AsNoTracking()
                .Where(d => d.SolMpExt_Id == id)
                .OrderBy(d => d.Codigo)
                .Select(d => new
                {
                    Id = d.SolMpExt_Id,
                    OT = d.SolMatPriExt!.SolMpExt_OT,
                    Maquina = d.SolMatPriExt.SolMpExt_Maquina,
                    Proceso = d.SolMatPriExt.Proceso_Id,
                    Observacion = d.SolMatPriExt.SolMpExt_Observacion,
                    Estado = d.SolMatPriExt.Estado_Id,
                    Nombre_Estado = d.SolMatPriExt.Estado!.Estado_Nombre,
                    Usuario = d.SolMatPriExt.Usua_Id,
                    Nombre_Usuario = d.SolMatPriExt.Usua!.Usua_Nombre,
                    Fecha = d.SolMatPriExt.SolMpExt_Fecha,
                    Hora = d.SolMatPriExt.SolMpExt_Hora,
                    Fecha_Entrega = d.SolMatPriExt.SolMpExt_FechaEstimadaEntrega,
                    d.Codigo,
                    d.SubCatMP_Id,
                    d.SubCatMP_Nombre,
                    d.DtSolMpExt_Cantidad,
                    d.UndMed_Id
                })
                .ToListAsync(cancellationToken);
            if (filas.Count == 0) return Ok(filas);

            var subcategorias = filas.Select(f => f.SubCatMP_Id).Distinct().ToList();
            var asignado = await _solicitudes.ObtenerAsignadoAsync([id], cancellationToken);
            var entregadoPorFila = filas
                .GroupBy(f => f.SubCatMP_Id)
                .SelectMany(g => SolicitudesMaterialService.DistribuirEntregado(
                    g.Select(f => (f.Codigo, f.DtSolMpExt_Cantidad)).ToList(),
                    asignado.GetValueOrDefault((id, g.Key))))
                .ToDictionary(x => x.Key, x => x.Value);

            var stockMp = await _context.Materias_Primas
                .AsNoTracking()
                .Where(m => m.SubCatMP_Id != null && subcategorias.Contains(m.SubCatMP_Id.Value))
                .GroupBy(m => m.SubCatMP_Id!.Value)
                .Select(g => new { SubCatMP_Id = g.Key, Stock = g.Sum(m => m.MatPri_Stock) })
                .ToListAsync(cancellationToken);
            var stockTinta = await _context.Tintas
                .AsNoTracking()
                .Where(t => t.SubCatMP_Id != null && subcategorias.Contains(t.SubCatMP_Id.Value))
                .GroupBy(t => t.SubCatMP_Id!.Value)
                .Select(g => new { SubCatMP_Id = g.Key, Stock = g.Sum(t => t.Tinta_Stock) })
                .ToListAsync(cancellationToken);
            var stock = stockMp.Concat(stockTinta)
                .GroupBy(s => s.SubCatMP_Id)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.Stock));

            var empresa = await _context.Empresas
                .AsNoTracking()
                .Where(e => e.Empresa_Id == EmpresaPlasticaribeId)
                .Select(e => new { e.Empresa_Id, e.Empresa_Nombre, e.Empresa_Direccion, e.Empresa_Telefono, e.Empresa_Ciudad, e.Empresa_Correo })
                .FirstOrDefaultAsync(cancellationToken);

            var result = filas.Select(f =>
            {
                var entregado = entregadoPorFila[f.Codigo];
                return new
                {
                    f.Id,
                    f.OT,
                    f.Maquina,
                    f.Proceso,
                    f.Observacion,
                    f.Estado,
                    f.Nombre_Estado,
                    f.Usuario,
                    f.Nombre_Usuario,
                    f.Fecha,
                    f.Hora,
                    f.Fecha_Entrega,
                    f.Codigo,
                    Id_Subcategoria = f.SubCatMP_Id,
                    Subcategoria = f.SubCatMP_Nombre,
                    Stock = stock.GetValueOrDefault(f.SubCatMP_Id),
                    Cantidad_Pedida = f.DtSolMpExt_Cantidad,
                    Cantidad_Entregada = entregado,
                    Cantidad_Restante = Math.Max(0m, f.DtSolMpExt_Cantidad - entregado),
                    Medida = f.UndMed_Id,
                    empresa?.Empresa_Id,
                    empresa?.Empresa_Nombre,
                    empresa?.Empresa_Direccion,
                    empresa?.Empresa_Telefono,
                    empresa?.Empresa_Ciudad,
                    empresa?.Empresa_Correo
                };
            });

            return Ok(result);
        }

        /// <summary>
        /// Obtiene los códigos de detalle de una subcategoría dentro de una solicitud.
        /// </summary>
        /// <param name="solicitud">Identificador de la solicitud.</param>
        /// <param name="subcategoria">Subcategoría a buscar.</param>
        /// <returns>Códigos de los detalles de esa subcategoría.</returns>
        [HttpGet("getSolicitudesConMatPrimas/{solicitud}/{subcategoria}")]
        public ActionResult GetSolicitudesConMatPrimas(long solicitud, int subcategoria)
        {
            var con = from sol in _context.Set<DetSolicitud_MatPrimaExtrusion>()
                      where sol.SolMpExt_Id == solicitud && sol.SubCatMP_Id == subcategoria
                      select sol.Codigo;
            return Ok(con);
        }
        
        
        // GET: api/DetSolicitud_MatPrimaExtrusion/5
        [HttpGet("getQuerySolicitudesMp_Extrusion/{fecha1}/{fecha2}")]
        public async Task<ActionResult> GetQuerySolicitudesMp_Extrusion(DateTime fecha1, DateTime fecha2, string? id = "", string? estado = "")
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null) return NotFound();

#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.

            var detSolicitud_MatPrimaExtrusion = from smpe in _context.Set<Solicitud_MatPrimaExtrusion>()
                                                 join dsmpe in _context.Set<DetSolicitud_MatPrimaExtrusion>() on smpe.SolMpExt_Id equals dsmpe.SolMpExt_Id
                                                 where smpe.SolMpExt_Id == dsmpe.SolMpExt_Id &&
                                                 smpe.SolMpExt_Fecha >= fecha1 &&
                                                 smpe.SolMpExt_Fecha <= fecha2 &&
                                                 Convert.ToString(smpe.SolMpExt_Id).Contains(id) &&
                                                 Convert.ToString(smpe.Estado_Id).Contains(estado)
                                                 select new
                                                 {
                                                     //Encabezado
                                                     Id = smpe.SolMpExt_Id,
                                                     OT = smpe.SolMpExt_OT,
                                                     Maquina = smpe.SolMpExt_Maquina,
                                                     Proceso = smpe.Proceso_Id,
                                                     Observacion = smpe.SolMpExt_Observacion,
                                                     Estado = smpe.Estado_Id,
                                                     Nombre_Estado = smpe.Estado.Estado_Nombre,
                                                     Usuario = smpe.Usua_Id,
                                                     Nombre_Usuario = smpe.Usua.Usua_Nombre,
                                                     Fecha = smpe.SolMpExt_Fecha,
                                                     Hora = smpe.SolMpExt_Hora,
                                                     Fecha_Entrega = smpe.SolMpExt_FechaEstimadaEntrega.Value,
                                                     //Detalle
                                                     Subcat_Id = dsmpe.SubCatMP_Id,
                                                     Subcategoria = dsmpe.SubCatMP_Nombre,
                                                     Stock = 0,
                                                     Cantidad = dsmpe.DtSolMpExt_Cantidad,
                                                     Cantidad_Faltante = dsmpe.DtSolMpExt_CantidadFaltante ?? dsmpe.DtSolMpExt_Cantidad,
                                                     Cantidad_Entregada = dsmpe.DtSolMpExt_CantidadEntregada,
                                                     Medida = dsmpe.UndMed_Id,
                                                 };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            var result = await detSolicitud_MatPrimaExtrusion.ToListAsync();

            if (result == null) return NotFound();
            else return Ok(result);
        }

        // PUT: api/DetSolicitud_MatPrimaExtrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetSolicitud_MatPrimaExtrusion(long id, DetSolicitud_MatPrimaExtrusion detSolicitud_MatPrimaExtrusion)
        {
            if (id != detSolicitud_MatPrimaExtrusion.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detSolicitud_MatPrimaExtrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetSolicitud_MatPrimaExtrusionExists(id))
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

        // POST: api/DetSolicitud_MatPrimaExtrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetSolicitud_MatPrimaExtrusion>> PostDetSolicitud_MatPrimaExtrusion(DetSolicitud_MatPrimaExtrusion detSolicitud_MatPrimaExtrusion)
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return Problem("Entity set 'dataContext.DetSolicitud_MatPrimaExtrusion'  is null.");
            }
            _context.DetSolicitud_MatPrimaExtrusion.Add(detSolicitud_MatPrimaExtrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetSolicitud_MatPrimaExtrusion", new { id = detSolicitud_MatPrimaExtrusion.Codigo }, detSolicitud_MatPrimaExtrusion);
        }

        // DELETE: api/DetSolicitud_MatPrimaExtrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetSolicitud_MatPrimaExtrusion(long id)
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            var detSolicitud_MatPrimaExtrusion = await _context.DetSolicitud_MatPrimaExtrusion.FindAsync(id);
            if (detSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }

            _context.DetSolicitud_MatPrimaExtrusion.Remove(detSolicitud_MatPrimaExtrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetSolicitud_MatPrimaExtrusionExists(long id)
        {
            return (_context.DetSolicitud_MatPrimaExtrusion?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
