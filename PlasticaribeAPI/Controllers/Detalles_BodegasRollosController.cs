using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Detalles_BodegasRollosController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_BodegasRollosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_BodegasRollos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_BodegasRollos>>> GetDetalles_BodegasRollos()
        {
          if (_context.Detalles_BodegasRollos == null)
          {
              return NotFound();
          }
            return await _context.Detalles_BodegasRollos.ToListAsync();
        }

        // GET: api/Detalles_BodegasRollos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_BodegasRollos>> GetDetalles_BodegasRollos(long id)
        {
          if (_context.Detalles_BodegasRollos == null)
          {
              return NotFound();
          }
            var detalles_BodegasRollos = await _context.Detalles_BodegasRollos.FindAsync(id);

            if (detalles_BodegasRollos == null)
            {
                return NotFound();
            }

            return detalles_BodegasRollos;
        }

        // Consulta que validará que los rollos que le sean pasado en el array estén en la base de datos, retornará los rollos que estén en la base de datos.
        [HttpPost("getRollos")]
        public IActionResult GetRollos([FromBody] List<long> rollos)
        {
            return Ok(from e in _context.Set<Detalles_BodegasRollos>()
                      where rollos.Contains(e.DtBgRollo_Rollo)
                      select new { e.DtBgRollo_Rollo, e.BgRollo_BodegaActual });
        }

        //Consulta que validará que un rollo existe en una bodega y devolverá toda su información
        [HttpGet("getInfoRollo/{rollo}/{bodega}")]
        public ActionResult GetInfoRollo(long rollo, string bodega)
        {
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.DtBgRollo_Rollo == rollo &&
                            bg.BgRollo_BodegaActual == bodega
                      select bg;
            return con.Any() ? Ok(con) : NotFound();
        }

        //
        [HttpGet("getRollosDisponibles/{bodega}/{fechaInicial}/{fechaFinal}/{ot}")]
        public ActionResult GetRollosDisponibles(string bodega, DateTime fechaInicial, DateTime fechaFinal, long ot, string? rollo = "")
        {
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.BgRollo_BodegaActual == bodega
                            && bg.Bodegas_Rollos.BgRollo_FechaModifica >= fechaInicial
                            && bg.Bodegas_Rollos.BgRollo_FechaModifica <= fechaFinal
                            && bg.BgRollo_OrdenTrabajo == ot
                            && Convert.ToString(bg.DtBgRollo_Rollo).Contains(rollo)
                      select new
                      {
                          Ot = bg.BgRollo_OrdenTrabajo,
                          Rollo = bg.DtBgRollo_Rollo,
                          Item = bg.Prod_Id,
                          Referencia = bg.Producto.Prod_Nombre,
                          Cantidad = bg.DtBgRollo_Cantidad,
                          Presentacion = bg.UndMed_Id,
                          Bodega = bg.Bodega_Actual.Proceso_Nombre
                      };
            if (con.Count() > 0) return Ok(con);
            else return BadRequest("No hay rollos disponobles en la bodega solicitada");
        }

        // PUT: api/Detalles_BodegasRollos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_BodegasRollos(long id, Detalles_BodegasRollos detalles_BodegasRollos)
        {
            if (id != detalles_BodegasRollos.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalles_BodegasRollos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_BodegasRollosExists(id))
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

        // POST: api/Detalles_BodegasRollos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_BodegasRollos>> PostDetalles_BodegasRollos(Detalles_BodegasRollos detalles_BodegasRollos)
        {
          if (_context.Detalles_BodegasRollos == null)
          {
              return Problem("Entity set 'dataContext.Detalles_BodegasRollos'  is null.");
          }
            _context.Detalles_BodegasRollos.Add(detalles_BodegasRollos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_BodegasRollos", new { id = detalles_BodegasRollos.Codigo }, detalles_BodegasRollos);
        }

        // DELETE: api/Detalles_BodegasRollos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_BodegasRollos(long id)
        {
            if (_context.Detalles_BodegasRollos == null)
            {
                return NotFound();
            }
            var detalles_BodegasRollos = await _context.Detalles_BodegasRollos.FindAsync(id);
            if (detalles_BodegasRollos == null)
            {
                return NotFound();
            }

            _context.Detalles_BodegasRollos.Remove(detalles_BodegasRollos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_BodegasRollosExists(long id)
        {
            return (_context.Detalles_BodegasRollos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
