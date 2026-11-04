using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Service;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Solicitud_MatPrimaExtrusionController : ControllerBase
    {
        private readonly dataContext _context;
        private readonly SolicitudesMaterialService _solicitudes;

        public Solicitud_MatPrimaExtrusionController(dataContext context, SolicitudesMaterialService solicitudes)
        {
            _context = context;
            _solicitudes = solicitudes;
        }

        // GET: api/Solicitud_MatPrimaExtrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud_MatPrimaExtrusion>>> GetSolicitud_MatPrimaExtrusion()
        {
            if (_context.Solicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            return await _context.Solicitud_MatPrimaExtrusion.ToListAsync();
        }

        // GET: api/Solicitud_MatPrimaExtrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud_MatPrimaExtrusion>> GetSolicitud_MatPrimaExtrusion(long id)
        {
            if (_context.Solicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            var solicitud_MatPrimaExtrusion = await _context.Solicitud_MatPrimaExtrusion.FindAsync(id);

            if (solicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }

            return solicitud_MatPrimaExtrusion;
        }

        /** Obtener las ultimas 100 solicitudes para mostrar en los estados */
        [HttpGet("getEstadosSolicitudes")]
        public async Task<ActionResult<Solicitud_MatPrimaExtrusion>> getEstadosSolicitudes()
        {
            if (_context.Solicitud_MatPrimaExtrusion == null) return NotFound();

            var todo = await (from s in _context.Set<Solicitud_MatPrimaExtrusion>().AsNoTracking()
                              group s by new {
                                  Estado_Id = s.Estado_Id, 
                                  Estado = s.Estado.Estado_Nombre
                              } into g
                              select new
                              {
                                  Estado_Id = g.Key.Estado_Id,
                                  Estado = g.Key.Estado,
                                  Suma = g.Count()
                              }).ToListAsync();

            if (todo != null) return Ok(todo);
            else return BadRequest("No se encontraron registros de solicitudes de material de producción");
        }

        /** Obtener ultima solicitud */
        [HttpGet("getUltimaSolicitud")]
        public ActionResult GetUltimaSolicitud()
        {
            if (_context.Solicitud_MatPrimaExtrusion == null) return NotFound();

            var ultima = _context.Solicitud_MatPrimaExtrusion.Max(p => p.SolMpExt_Id);

            if (ultima != 0) return Ok(ultima);
            else return BadRequest("No se encontraron registros de solicitudes de material de producción");
        }

        /// <summary>
        /// Recalcula el estado de la solicitud y las cantidades entregadas de sus detalles según lo asignado
        /// (materias primas y tintas).
        /// </summary>
        /// <param name="id">Identificador de la solicitud.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>204 si se recalculó; 404 si no existe.</returns>
        [HttpPut("PutEstadoSolicitud/{id}")]
        public async Task<IActionResult> PutEstadoSolicitud(long id, CancellationToken cancellationToken)
        {
            var estado = await _solicitudes.RecalcularEstadoAsync(id, cancellationToken);
            return estado == null ? NotFound() : NoContent();
        }

        // PUT: api/Solicitud_MatPrimaExtrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitud_MatPrimaExtrusion(long id, Solicitud_MatPrimaExtrusion solicitud_MatPrimaExtrusion)
        {
            if (id != solicitud_MatPrimaExtrusion.SolMpExt_Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitud_MatPrimaExtrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Solicitud_MatPrimaExtrusionExists(id))
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

        // POST: api/Solicitud_MatPrimaExtrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitud_MatPrimaExtrusion>> PostSolicitud_MatPrimaExtrusion(Solicitud_MatPrimaExtrusion solicitud_MatPrimaExtrusion)
        {
            if (_context.Solicitud_MatPrimaExtrusion == null)
            {
                return Problem("Entity set 'dataContext.Solicitud_MatPrimaExtrusion'  is null.");
            }
            _context.Solicitud_MatPrimaExtrusion.Add(solicitud_MatPrimaExtrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitud_MatPrimaExtrusion", new { id = solicitud_MatPrimaExtrusion.SolMpExt_Id }, solicitud_MatPrimaExtrusion);
        }

        // DELETE: api/Solicitud_MatPrimaExtrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud_MatPrimaExtrusion(long id)
        {
            if (_context.Solicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            var solicitud_MatPrimaExtrusion = await _context.Solicitud_MatPrimaExtrusion.FindAsync(id);
            if (solicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }

            _context.Solicitud_MatPrimaExtrusion.Remove(solicitud_MatPrimaExtrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Solicitud_MatPrimaExtrusionExists(long id)
        {
            return (_context.Solicitud_MatPrimaExtrusion?.Any(e => e.SolMpExt_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
