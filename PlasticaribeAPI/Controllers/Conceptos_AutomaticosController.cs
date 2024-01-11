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
    public class Conceptos_AutomaticosController : ControllerBase
    {
        private readonly dataContext _context;

        public Conceptos_AutomaticosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Conceptos_Automaticos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conceptos_Automaticos>>> GetConceptos_Automaticos()
        {
            return await _context.Conceptos_Automaticos.ToListAsync();
        }

        // GET: api/Conceptos_Automaticos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conceptos_Automaticos>> GetConceptos_Automaticos(int id)
        {
            var conceptos_Automaticos = await _context.Conceptos_Automaticos.FindAsync(id);

            if (conceptos_Automaticos == null)
            {
                return NotFound();
            }

            return conceptos_Automaticos;
        }

        // PUT: api/Conceptos_Automaticos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConceptos_Automaticos(int id, Conceptos_Automaticos conceptos_Automaticos)
        {
            if (id != conceptos_Automaticos.Id)
            {
                return BadRequest();
            }

            _context.Entry(conceptos_Automaticos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Conceptos_AutomaticosExists(id))
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

        // POST: api/Conceptos_Automaticos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conceptos_Automaticos>> PostConceptos_Automaticos(Conceptos_Automaticos conceptos_Automaticos)
        {
            _context.Conceptos_Automaticos.Add(conceptos_Automaticos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConceptos_Automaticos", new { id = conceptos_Automaticos.Id }, conceptos_Automaticos);
        }

        // DELETE: api/Conceptos_Automaticos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConceptos_Automaticos(int id)
        {
            var conceptos_Automaticos = await _context.Conceptos_Automaticos.FindAsync(id);
            if (conceptos_Automaticos == null)
            {
                return NotFound();
            }

            _context.Conceptos_Automaticos.Remove(conceptos_Automaticos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Conceptos_AutomaticosExists(int id)
        {
            return _context.Conceptos_Automaticos.Any(e => e.Id == id);
        }
    }
}
