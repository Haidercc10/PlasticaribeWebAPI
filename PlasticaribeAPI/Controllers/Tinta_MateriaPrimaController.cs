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
    public class Tinta_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Tinta_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tinta_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tinta_MateriaPrima>>> GetTintas_MateriasPrimas()
        {
          if (_context.Tintas_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.Tintas_MateriasPrimas.ToListAsync();
        }

        // GET: api/Tinta_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tinta_MateriaPrima>> GetTinta_MateriaPrima(long id)
        {
          if (_context.Tintas_MateriasPrimas == null)
          {
              return NotFound();
          }
            var tinta_MateriaPrima = await _context.Tintas_MateriasPrimas.FindAsync(id);

            if (tinta_MateriaPrima == null)
            {
                return NotFound();
            }

            return tinta_MateriaPrima;
        }

        // PUT: api/Tinta_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTinta_MateriaPrima(long id, Tinta_MateriaPrima tinta_MateriaPrima)
        {
            if (id != tinta_MateriaPrima.Tinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(tinta_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tinta_MateriaPrimaExists(id))
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

        // POST: api/Tinta_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tinta_MateriaPrima>> PostTinta_MateriaPrima(Tinta_MateriaPrima tinta_MateriaPrima)
        {
          if (_context.Tintas_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.Tintas_MateriasPrimas'  is null.");
          }
            _context.Tintas_MateriasPrimas.Add(tinta_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Tinta_MateriaPrimaExists(tinta_MateriaPrima.Tinta_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTinta_MateriaPrima", new { id = tinta_MateriaPrima.Tinta_Id }, tinta_MateriaPrima);
        }

        // DELETE: api/Tinta_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTinta_MateriaPrima(long id)
        {
            if (_context.Tintas_MateriasPrimas == null)
            {
                return NotFound();
            }
            var tinta_MateriaPrima = await _context.Tintas_MateriasPrimas.FindAsync(id);
            if (tinta_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.Tintas_MateriasPrimas.Remove(tinta_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tinta_MateriaPrimaExists(long id)
        {
            return (_context.Tintas_MateriasPrimas?.Any(e => e.Tinta_Id == id)).GetValueOrDefault();
        }
    }
}
