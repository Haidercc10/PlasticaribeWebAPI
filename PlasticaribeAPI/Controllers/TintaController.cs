using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TintaController : ControllerBase
    {
        private readonly dataContext _context;

        public TintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tinta>>> GetTintas()
        {
          if (_context.Tintas == null)
          {
              return NotFound();
          }
            return await _context.Tintas.ToListAsync();
        }

        // GET: api/Tinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tinta>> GetTinta(long id)
        {
          if (_context.Tintas == null)
          {
              return NotFound();
          }
            var tinta = await _context.Tintas.FindAsync(id);

            if (tinta == null)
            {
                return NotFound();
            }

            return tinta;
        }

        [HttpGet("TintasColores")]
        public ActionResult GetTintaColor()
        {
            if (_context.Tintas == null)
            {
                return NotFound();
            }
            var tinta = _context.Tintas.ToList();

            if (tinta == null)
            {
                return NotFound();
            } else
            {
                return Ok(tinta);
            }

            //return tinta;
        }

        [HttpGet("consultaImpresion/{Tinta_Nombre1}/{Tinta_Nombre2}/{Tinta_Nombre3}/{Tinta_Nombre4}/{Tinta_Nombre5}/{Tinta_Nombre6}/{Tinta_Nombre7}/{Tinta_Nombre8}")]
        public ActionResult GetConsultaImpresion(string Tinta_Nombre1, string Tinta_Nombre2, string Tinta_Nombre3, string Tinta_Nombre4, string Tinta_Nombre5, string Tinta_Nombre6, string Tinta_Nombre7, string Tinta_Nombre8)
        {
            var con = from t1 in _context.Set<Tinta>()
                      from t2 in _context.Set<Tinta>()
                      from t3 in _context.Set<Tinta>()
                      from t4 in _context.Set<Tinta>()
                      from t5 in _context.Set<Tinta>()
                      from t6 in _context.Set<Tinta>()
                      from t7 in _context.Set<Tinta>()
                      from t8 in _context.Set<Tinta>()
                      where t1.Tinta_Nombre == Tinta_Nombre1
                            && t2.Tinta_Nombre == Tinta_Nombre2
                            && t3.Tinta_Nombre == Tinta_Nombre3
                            && t4.Tinta_Nombre == Tinta_Nombre4
                            && t5.Tinta_Nombre == Tinta_Nombre5
                            && t6.Tinta_Nombre == Tinta_Nombre6
                            && t7.Tinta_Nombre == Tinta_Nombre7
                            && t8.Tinta_Nombre == Tinta_Nombre8
                      select new
                      {
                          Tinta_Id1 = t1.Tinta_Id,
                          Tinta_Id2 = t2.Tinta_Id,
                          Tinta_Id3 = t3.Tinta_Id,
                          Tinta_Id4 = t4.Tinta_Id,
                          Tinta_Id5 = t5.Tinta_Id,
                          Tinta_Id6 = t6.Tinta_Id,
                          Tinta_Id7 = t7.Tinta_Id,
                          Tinta_Id8 = t8.Tinta_Id,
                      };

            return Ok(con);
        }

        // PUT: api/Tinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTinta(long id, Tinta tinta)
        {
            if (id != tinta.Tinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(tinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TintaExists(id))
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

        // POST: api/Tinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tinta>> PostTinta(Tinta tinta)
        {
          if (_context.Tintas == null)
          {
              return Problem("Entity set 'dataContext.Tintas'  is null.");
          }
            _context.Tintas.Add(tinta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTinta", new { id = tinta.Tinta_Id }, tinta);
        }

        // DELETE: api/Tinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTinta(long id)
        {
            if (_context.Tintas == null)
            {
                return NotFound();
            }
            var tinta = await _context.Tintas.FindAsync(id);
            if (tinta == null)
            {
                return NotFound();
            }

            _context.Tintas.Remove(tinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TintaExists(long id)
        {
            return (_context.Tintas?.Any(e => e.Tinta_Id == id)).GetValueOrDefault();
        }
    }
}
