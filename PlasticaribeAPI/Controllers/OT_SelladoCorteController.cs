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
    public class OT_SelladoCorteController : ControllerBase
    {
        private readonly dataContext _context;

        public OT_SelladoCorteController(dataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OT_Sellado_Corte>>> GetOT_SelladoCorte()
        {
          if (_context.OT_Sellado_Corte == null)
          {
              return NotFound();
          }
            return await _context.OT_Sellado_Corte.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OT_Sellado_Corte>> GetOT_Sellado_Corte(int id)
        {
          if (_context.OT_Sellado_Corte == null)
          {
              return NotFound();
          }
            var OT_Sellado_Corte = await _context.OT_Sellado_Corte.FindAsync(id);

            if (OT_Sellado_Corte == null)
            {
                return NotFound();
            }

            return OT_Sellado_Corte;
        }

        // PUT: api/OT_Impresion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOT_Sellado_Corte(int id, OT_Sellado_Corte OT_Sellado_Corte)
        {
            if (id != OT_Sellado_Corte.SelladoCorte_Id)
            {
                return BadRequest();
            }

            _context.Entry(OT_Sellado_Corte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OT_Sellado_CorteExists(id))
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

        [HttpPost]
        public async Task<ActionResult<OT_Impresion>> PostOT_Sellado_Corte(OT_Sellado_Corte OT_Sellado_Corte)
        {
          if (_context.OT_Sellado_Corte == null)
          {
              return Problem("Entity set 'dataContext.OT_Impresion'  is null.");
          }
            _context.OT_Sellado_Corte.Add(OT_Sellado_Corte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOT_Sellado_Corte", new { id = OT_Sellado_Corte.Sellado }, OT_Sellado_Corte);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOT_Sellado_Corte(int id)
        {
            if (_context.OT_Sellado_Corte == null)
            {
                return NotFound();
            }
            var OT_Sellado_Corte = await _context.OT_Sellado_Corte.FindAsync(id);
            if (OT_Sellado_Corte == null)
            {
                return NotFound();
            }

            _context.OT_Sellado_Corte.Remove(OT_Sellado_Corte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OT_Sellado_CorteExists(long id)
        {
            return (_context.OT_Sellado_Corte?.Any(e => e.SelladoCorte_Id == id)).GetValueOrDefault();
        }
    }
}
