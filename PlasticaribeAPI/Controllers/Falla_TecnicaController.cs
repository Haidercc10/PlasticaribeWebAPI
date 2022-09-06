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
    public class Falla_TecnicaController : ControllerBase
    {
        private readonly dataContext _context;

        public Falla_TecnicaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Falla_Tecnica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Falla_Tecnica>>> GetFallas_Tecnicas()
        {
          if (_context.Fallas_Tecnicas == null)
          {
              return NotFound();
          }
            return await _context.Fallas_Tecnicas.ToListAsync();
        }

        // GET: api/Falla_Tecnica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Falla_Tecnica>> GetFalla_Tecnica(int id)
        {
          if (_context.Fallas_Tecnicas == null)
          {
              return NotFound();
          }
            var falla_Tecnica = await _context.Fallas_Tecnicas.FindAsync(id);

            if (falla_Tecnica == null)
            {
                return NotFound();
            }

            return falla_Tecnica;
        }

        // PUT: api/Falla_Tecnica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFalla_Tecnica(int id, Falla_Tecnica falla_Tecnica)
        {
            if (id != falla_Tecnica.Falla_Id)
            {
                return BadRequest();
            }

            _context.Entry(falla_Tecnica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Falla_TecnicaExists(id))
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

        // POST: api/Falla_Tecnica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Falla_Tecnica>> PostFalla_Tecnica(Falla_Tecnica falla_Tecnica)
        {
          if (_context.Fallas_Tecnicas == null)
          {
              return Problem("Entity set 'dataContext.Fallas_Tecnicas'  is null.");
          }
            _context.Fallas_Tecnicas.Add(falla_Tecnica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFalla_Tecnica", new { id = falla_Tecnica.Falla_Id }, falla_Tecnica);
        }

        // DELETE: api/Falla_Tecnica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFalla_Tecnica(int id)
        {
            if (_context.Fallas_Tecnicas == null)
            {
                return NotFound();
            }
            var falla_Tecnica = await _context.Fallas_Tecnicas.FindAsync(id);
            if (falla_Tecnica == null)
            {
                return NotFound();
            }

            _context.Fallas_Tecnicas.Remove(falla_Tecnica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Falla_TecnicaExists(int id)
        {
            return (_context.Fallas_Tecnicas?.Any(e => e.Falla_Id == id)).GetValueOrDefault();
        }
    }
}
