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
    public class DetalleAsignacion_BOPPController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_BOPPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_BOPP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_BOPP>>> GetDetallesAsignaciones_BOPP()
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            return await _context.DetallesAsignaciones_BOPP.ToListAsync();
        }

        // GET: api/DetalleAsignacion_BOPP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_BOPP>> GetDetalleAsignacion_BOPP(long id)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP = await _context.DetallesAsignaciones_BOPP.FindAsync(id);

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            return detalleAsignacion_BOPP;
        }

        [HttpGet("OT/{DtAsigBOPP_OrdenTrabajo}")]
        public ActionResult<DetalleAsignacion_BOPP> GetOT(long DtAsigBOPP_OrdenTrabajo)
        {
            var detalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP.Where(dtAsg => dtAsg.DtAsigBOPP_OrdenTrabajo == DtAsigBOPP_OrdenTrabajo).ToList();

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(detalleAsignacion_BOPP);
            }
        }

        //Consulta que traerá la cantidad de materia prima asignada, teniendo en cuenta la materia prima devuelta
        [HttpGet("getBiorientadoAsignado/{ot}")]
        public ActionResult GetBiorientadoAsignado(int ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.            
            var asgBopp = (from asgbopp in _context.Set<DetalleAsignacion_BOPP>()
                           where asgbopp.DtAsigBOPP_OrdenTrabajo == ot
                           select asgbopp.DtAsigBOPP_Cantidad).Sum();

            var devol = (from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                         where dev.DevMatPri.DevMatPri_OrdenTrabajo == ot &&
                               dev.BOPP_Id != 449
                         select dev.DtDevMatPri_CantidadDevuelta).Sum();

            var asigs = asgBopp - devol;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(asigs);
        }

        // PUT: api/DetalleAsignacion_BOPP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_BOPP(long id, DetalleAsignacion_BOPP detalleAsignacion_BOPP)
        {
            if (id != detalleAsignacion_BOPP.AsigBOPP_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_BOPP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_BOPPExists(id))
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

        [HttpPut("{id}/{ot}/{bopp}")]
        public IActionResult PutDetalleAsignacion_BOPP(long id, long ot, int bopp, DetalleAsignacion_BOPP detalleAsignacion_BOPP)
        {
            if (id != detalleAsignacion_BOPP.AsigBOPP_Id && ot != detalleAsignacion_BOPP.DtAsigBOPP_OrdenTrabajo && bopp != detalleAsignacion_BOPP.BOPP_Id)
            {
                return BadRequest();
            }

            try
            {
                var actualizado = _context.DetallesAsignaciones_BOPP
                    .Where(x => x.DtAsigBOPP_OrdenTrabajo == ot
                           && x.BOPP_Id == bopp
                           && x.AsigBOPP_Id == id)
                    .First<DetalleAsignacion_BOPP>();
                actualizado.DtAsigBOPP_Cantidad = detalleAsignacion_BOPP.DtAsigBOPP_Cantidad;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_BOPPExists(id))
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

        // POST: api/DetalleAsignacion_BOPP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_BOPP>> PostDetalleAsignacion_BOPP(DetalleAsignacion_BOPP detalleAsignacion_BOPP)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return Problem("Entity set 'dataContext.DetallesAsignaciones_BOPP'  is null.");
            }
            _context.DetallesAsignaciones_BOPP.Add(detalleAsignacion_BOPP);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_BOPPExists(detalleAsignacion_BOPP.AsigBOPP_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_BOPP", new { id = detalleAsignacion_BOPP.AsigBOPP_Id }, detalleAsignacion_BOPP);
        }

        // DELETE: api/DetalleAsignacion_BOPP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_BOPP(long id)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP = await _context.DetallesAsignaciones_BOPP.FindAsync(id);
            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_BOPP.Remove(detalleAsignacion_BOPP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: Eliminar Detalle de asignación de BOPP por OT y asignación. 
        [HttpDelete("EliminarXOT_AsignacionBOPP")]
        public ActionResult DeleteIdDetalleAsignacion_BOPP(long AsigBOPP_Id, long DtAsigBOPP_OrdenTrabajo)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP.Where(x => x.AsigBOPP_Id == AsigBOPP_Id &&
                                                                                   x.DtAsigBOPP_OrdenTrabajo == DtAsigBOPP_OrdenTrabajo
                                                                                   ).ToList();

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_BOPP.RemoveRange(detalleAsignacion_BOPP);
            _context.SaveChangesAsync();

            return NoContent();
        }


        /** Eliminar Detalle de asignación de BOPP por Asignación y BOPP */
        [HttpDelete("EliminarXAsignacion_BOPP")]
        public ActionResult DeleteEnDetalleAsignacion_BOPP(long AsigBOPP_Id, long BOPP_Id)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP = _context.DetallesAsignaciones_BOPP.Where(x => x.AsigBOPP_Id == AsigBOPP_Id &&
                                                                                  x.BOPP_Id == BOPP_Id
                                                                                  ).ToList();

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_BOPP.RemoveRange(detalleAsignacion_BOPP);
            _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DetalleAsignacion_BOPPExists(long id)
        {
            return (_context.DetallesAsignaciones_BOPP?.Any(e => e.AsigBOPP_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
