using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DetalleAsignacion_MatPrimaXTintaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_MatPrimaXTintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_MatPrimaXTinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_MatPrimaXTinta>>> GetDetallesAsignaciones_MatPrimasXTintas()
        {
            if (_context.DetallesAsignaciones_MatPrimasXTintas == null)
            {
                return NotFound();
            }
            return await _context.DetallesAsignaciones_MatPrimasXTintas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_MatPrimaXTinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_MatPrimaXTinta>> GetDetalleAsignacion_MatPrimaXTinta(long id)
        {
            if (_context.DetallesAsignaciones_MatPrimasXTintas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_MatPrimaXTinta = await _context.DetallesAsignaciones_MatPrimasXTintas.FindAsync(id);

            if (detalleAsignacion_MatPrimaXTinta == null)
            {
                return NotFound();
            }

            return detalleAsignacion_MatPrimaXTinta;
        }

        [HttpGet("getTintasCreadasMes/{fecha1}/{fecha2}")]
        public ActionResult GetTintasCreadasMes(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from creacion in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                      where creacion.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                            && creacion.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2
                      group creacion by new { creacion.AsigMPxTinta.Tinta_Id, creacion.AsigMPxTinta.Tinta.Tinta_Nombre }
                       into creacion
                      select new
                      {
                          creacion.Key.Tinta_Id,
                          creacion.Key.Tinta_Nombre,
                          cantidad = creacion.Sum(x => x.AsigMPxTinta.AsigMPxTinta_Cantidad),
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("getMateriasPrimasCrearTintasMes/{fecha1}/{fecha2}")]
        public ActionResult getMateriasPrimasCrearTintasMes(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from mp in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                      where mp.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                            && mp.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2
                      group mp by new
                      {
                          mp.Tinta_Id,
                          mp.TintasDAMPxT.Tinta_Nombre,
                      } into mp
                      select new
                      {
                          mp.Key.Tinta_Id,
                          mp.Key.Tinta_Nombre,
                          cantidad = mp.Sum(x => x.DetAsigMPxTinta_Cantidad),
                          asignaciones = mp.Count(),
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // PUT: api/DetalleAsignacion_MatPrimaXTinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_MatPrimaXTinta(long id, DetalleAsignacion_MatPrimaXTinta detalleAsignacion_MatPrimaXTinta)
        {
            if (id != detalleAsignacion_MatPrimaXTinta.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_MatPrimaXTinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_MatPrimaXTintaExists(id))
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


        // POST: api/DetalleAsignacion_MatPrimaXTinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_MatPrimaXTinta>> PostDetalleAsignacion_MatPrimaXTinta(DetalleAsignacion_MatPrimaXTinta detalleAsignacion_MatPrimaXTinta)
        {
            if (_context.DetallesAsignaciones_MatPrimasXTintas == null)
            {
                return Problem("Entity set 'dataContext.DetallesAsignaciones_MatPrimasXTintas'  is null.");
            }
            _context.DetallesAsignaciones_MatPrimasXTintas.Add(detalleAsignacion_MatPrimaXTinta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_MatPrimaXTintaExists(detalleAsignacion_MatPrimaXTinta.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_MatPrimaXTinta", new { id = detalleAsignacion_MatPrimaXTinta.Codigo }, detalleAsignacion_MatPrimaXTinta);
        }

        // DELETE: api/DetalleAsignacion_MatPrimaXTinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_MatPrimaXTinta(long id)
        {
            if (_context.DetallesAsignaciones_MatPrimasXTintas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_MatPrimaXTinta = await _context.DetallesAsignaciones_MatPrimasXTintas.FindAsync(id);
            if (detalleAsignacion_MatPrimaXTinta == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_MatPrimasXTintas.Remove(detalleAsignacion_MatPrimaXTinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_MatPrimaXTintaExists(long id)
        {
            return (_context.DetallesAsignaciones_MatPrimasXTintas?.Any(e => e.AsigMPxTinta_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
