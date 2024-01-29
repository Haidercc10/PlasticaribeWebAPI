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
using StackExchange.Redis;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ReImpresionEtiquetasController : ControllerBase
    {
        private readonly dataContext _context;

        public ReImpresionEtiquetasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/ReImpresionEtiquetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReImpresionEtiquetas>>> GetReImpresionEtiquetas()
        {
            return await _context.ReImpresionEtiquetas.ToListAsync();
        }

        // GET: api/ReImpresionEtiquetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReImpresionEtiquetas>> GetReImpresionEtiquetas(int id)
        {
            var reImpresionEtiquetas = await _context.ReImpresionEtiquetas.FindAsync(id);

            if (reImpresionEtiquetas == null)
            {
                return NotFound();
            }

            return reImpresionEtiquetas;
        }

        // Consulta que devolverá los datos de las reimpresiones de una etiqueta
        [HttpGet("getReImpresionesEtiquetaByRollo/{rollo}")]
        public ActionResult getReImpresionesEtiquetasByRollo(long rollo)
        {
            var con = from ri in _context.Set<ReImpresionEtiquetas>()
                      join usua in _context.Set<Usuario>() on ri.Usua_Id equals usua.Usua_Id
                      join proc in _context.Set<Proceso>() on ri.Proceso_Id equals proc.Proceso_Id
                      where ri.NumeroRollo_BagPro == rollo
                      select new
                      {
                          OrderProduction = ri.Orden_Trabajo,
                          NumberProduction = ri.NumeroRollo_BagPro,
                          ProcessProduction = proc.Proceso_Nombre,
                          DateRePrint = ri.Fecha,
                          HourRePrint = ri.Hora,
                          ri.Usua_Id,
                          UserRePrint = usua.Usua_Nombre,
                      };
            return con.Any() ? Ok(con) : NotFound();
        }

        // Consulta que devolverá la cantidad de veces que se ha reimpreso una etiqueta
        [HttpPost("getCantidadReImpresionesPorEtiqueta")]
        public IActionResult GetCantidadReImpresionPorEtiqueta([FromBody] List<long> Rollos)
        {
            var con = from ri in _context.Set<ReImpresionEtiquetas>()
                      where Rollos.Contains(ri.NumeroRollo_BagPro)
                      group ri by new
                      {
                          ri.NumeroRollo_BagPro,
                          ri.Orden_Trabajo,
                          ri.Proceso_Id
                      } into data
                      select new
                      {
                          data.Key.Orden_Trabajo,
                          data.Key.NumeroRollo_BagPro,
                          data.Key.Proceso_Id,
                          Cantidad = data.Count() + 1,
                      };
            return con.Any() ? Ok(con) : NotFound();
        }

        // PUT: api/ReImpresionEtiquetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReImpresionEtiquetas(int id, ReImpresionEtiquetas reImpresionEtiquetas)
        {
            if (id != reImpresionEtiquetas.Id)
            {
                return BadRequest();
            }

            _context.Entry(reImpresionEtiquetas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReImpresionEtiquetasExists(id))
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

        // POST: api/ReImpresionEtiquetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReImpresionEtiquetas>> PostReImpresionEtiquetas(ReImpresionEtiquetas reImpresionEtiquetas)
        {
            _context.ReImpresionEtiquetas.Add(reImpresionEtiquetas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReImpresionEtiquetas", new { id = reImpresionEtiquetas.Id }, reImpresionEtiquetas);
        }

        // DELETE: api/ReImpresionEtiquetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReImpresionEtiquetas(int id)
        {
            var reImpresionEtiquetas = await _context.ReImpresionEtiquetas.FindAsync(id);
            if (reImpresionEtiquetas == null)
            {
                return NotFound();
            }

            _context.ReImpresionEtiquetas.Remove(reImpresionEtiquetas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReImpresionEtiquetasExists(int id)
        {
            return _context.ReImpresionEtiquetas.Any(e => e.Id == id);
        }
    }
}
