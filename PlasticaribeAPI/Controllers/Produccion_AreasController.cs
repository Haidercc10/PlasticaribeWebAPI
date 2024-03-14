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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Produccion_AreasController : ControllerBase
    {
        private readonly dataContext _context;

        public Produccion_AreasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Produccion_Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produccion_Areas>>> GetProduccion_Areas()
        {
          if (_context.Produccion_Areas == null)
          {
              return NotFound();
          }
            return await _context.Produccion_Areas.ToListAsync();
        }

        // GET: api/Produccion_Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produccion_Areas>> GetProduccion_Areas(int id)
        {
          if (_context.Produccion_Areas == null)
          {
              return NotFound();
          }
            var produccion_Areas = await _context.Produccion_Areas.FindAsync(id);

            if (produccion_Areas == null)
            {
                return NotFound();
            }

            return produccion_Areas;
        }

        [HttpGet("getProduccionAreas_Mes/{anio}")]
        public ActionResult GetProduccionAreas_Mes(int anio)
        {
            var con = from pa in _context.Set<Produccion_Areas>()
                      join pro in _context.Set<Proceso>() on pa.Proceso_Id equals pro.Proceso_Id
                      where pa.Anio_Produccion == anio
                      select new
                      {
                          pa.Id,
                          pa.Proceso_Id,
                          pro.Proceso_Nombre,
                          pa.Anio_Produccion,
                          pa.Meta_Enero,
                          pa.Producido_Enero,
                          pa.Meta_Febrero,
                          pa.Producido_Febrero,
                          pa.Meta_Marzo,
                          pa.Producido_Marzo,
                          pa.Meta_Abril,
                          pa.Producido_Abril,
                          pa.Meta_Mayo,
                          pa.Producido_Mayo,
                          pa.Meta_Junio,
                          pa.Producido_Junio,
                          pa.Meta_Julio,
                          pa.Producido_Julio,
                          pa.Meta_Agosto,
                          pa.Producido_Agosto,
                          pa.Meta_Septiembre,
                          pa.Producido_Septiembre,
                          pa.Meta_Octubre,
                          pa.Producido_OCtubre,
                          pa.Meta_Noviembre,
                          pa.Producido_Noviembre,
                          pa.Meta_Diciembre,
                          pa.Producido_Diciembre,
                      };
            return Ok(con);
        }

        // PUT: api/Produccion_Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduccion_Areas(int id, Produccion_Areas produccion_Areas)
        {
            if (id != produccion_Areas.Id)
            {
                return BadRequest();
            }

            _context.Entry(produccion_Areas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Produccion_AreasExists(id))
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

        [HttpPut("putMetaProduccionMes/{id}")]
        public async Task<IActionResult> PutMetaProduccionMes(int id, decimal meta)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            var produccion = (from pa in _context.Set<Produccion_Areas>() where pa.Id == id select pa).FirstOrDefault();
            int mesActual = DateTime.Today.Month;

            if (mesActual == 1) produccion.Meta_Enero = meta;
            else if (mesActual == 2) produccion.Meta_Febrero = meta;
            else if (mesActual == 3) produccion.Meta_Marzo = meta;
            else if (mesActual == 4) produccion.Meta_Abril = meta;
            else if (mesActual == 5) produccion.Meta_Mayo = meta;
            else if (mesActual == 6) produccion.Meta_Junio = meta;
            else if (mesActual == 7) produccion.Meta_Julio = meta;
            else if (mesActual == 8) produccion.Meta_Agosto = meta;
            else if (mesActual == 9) produccion.Meta_Septiembre = meta;
            else if (mesActual == 10) produccion.Meta_Octubre = meta;
            else if (mesActual == 11) produccion.Meta_Noviembre = meta;
            else if (mesActual == 12) produccion.Meta_Diciembre = meta;

            _context.Entry(produccion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(produccion);
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // POST: api/Produccion_Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produccion_Areas>> PostProduccion_Areas(Produccion_Areas produccion_Areas)
        {
          if (_context.Produccion_Areas == null)
          {
              return Problem("Entity set 'dataContext.Produccion_Areas'  is null.");
          }
            _context.Produccion_Areas.Add(produccion_Areas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccion_Areas", new { id = produccion_Areas.Id }, produccion_Areas);
        }

        // DELETE: api/Produccion_Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccion_Areas(int id)
        {
            if (_context.Produccion_Areas == null)
            {
                return NotFound();
            }
            var produccion_Areas = await _context.Produccion_Areas.FindAsync(id);
            if (produccion_Areas == null)
            {
                return NotFound();
            }

            _context.Produccion_Areas.Remove(produccion_Areas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Produccion_AreasExists(int id)
        {
            return (_context.Produccion_Areas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
