using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Diagnostics;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Detalles_AsignacionRollosOTController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_AsignacionRollosOTController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_AsignacionRollosOT
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_AsignacionRollosOT>>> GetDetalles_AsignacionRollosOT()
        {
            return await _context.Detalles_AsignacionRollosOT.ToListAsync();
        }

        // GET: api/Detalles_AsignacionRollosOT/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_AsignacionRollosOT>> GetDetalles_AsignacionRollosOT(long id)
        {
            var asg = await _context.Detalles_AsignacionRollosOT.FindAsync(id);

            if (asg == null)
            {
                return NotFound();
            }

            return asg;
        }

        // GET: Consulta para obtener las asignaciones por ID.
        [HttpGet("getAsignationsForId/{id}")]
        public ActionResult getAsignationsForId(long id)
        {
            var asg = from a in _context.Set<Asignacion_RollosOT>()
                      from d in _context.Set<Detalles_AsignacionRollosOT>()
                      where a.AsgRll_Id == d.AsgRll_Id
                      && d.AsgRll_Id == id 
                      select new { 
                        //Header
                        Id = d.AsgRll_Id,
                        Date = a.AsgRll_Fecha, 
                        Hour = a.AsgRll_Hora, 
                        UserId = a.Usua_Id, 
                        User = a.Usuario.Usua_Nombre,

                        //Details
                        Roll = d.Rollo_Id,
                        OT = d.DtAsgRll_OT,
                        Item = d.Prod_Id, 
                        Reference = d.Producto.Prod_Nombre,
                        Qty = d.DtAsgRll_Cantidad,
                        Und = d.UndMed_Id, 
                        ProcessId = d.Proceso_Id,
                        Process = d.Proceso.Proceso_Nombre, 
                      };

            return Ok(asg);
        }

        // PUT: api/Detalles_AsignacionRollosOT/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_AsignacionRollosOT(long id, Detalles_AsignacionRollosOT Detalles_AsignacionRollosOT)
        {
            if (id != Detalles_AsignacionRollosOT.DtlAsgRll_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(Detalles_AsignacionRollosOT).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_AsignacionRollosOTExists(id))
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

        // POST: api/Detalles_AsignacionRollosOT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_AsignacionRollosOT>> PostDetalles_AsignacionRollosOT(Detalles_AsignacionRollosOT Detalles_AsignacionRollosOT)
        {
            _context.Detalles_AsignacionRollosOT.Add(Detalles_AsignacionRollosOT);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_AsignacionRollosOT", new { id = Detalles_AsignacionRollosOT.DtlAsgRll_Codigo }, Detalles_AsignacionRollosOT);
        }

        // DELETE: api/Detalles_AsignacionRollosOT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_AsignacionRollosOT(long id)
        {
            var Detalles_AsignacionRollosOT = await _context.Detalles_AsignacionRollosOT.FindAsync(id);
            if (Detalles_AsignacionRollosOT == null)
            {
                return NotFound();
            }

            _context.Detalles_AsignacionRollosOT.Remove(Detalles_AsignacionRollosOT);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_AsignacionRollosOTExists(long id)
        {
            return _context.Detalles_AsignacionRollosOT.Any(e => e.DtlAsgRll_Codigo == id);
        }
    }
}
