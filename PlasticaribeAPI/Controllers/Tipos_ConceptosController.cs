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
    public class Tipos_ConceptosController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipos_ConceptosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipos_Conceptos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipos_Conceptos>>> GetTipos_Conceptos()
        {
            return await _context.Tipos_Conceptos.ToListAsync();
        }

        // GET: api/Tipos_Conceptos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipos_Conceptos>> GetTipos_Conceptos(int id)
        {
            var tipos_Conceptos = await _context.Tipos_Conceptos.FindAsync(id);

            if (tipos_Conceptos == null)
            {
                return NotFound();
            }

            return tipos_Conceptos;
        }

        // PUT: api/Tipos_Conceptos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipos_Conceptos(int id, Tipos_Conceptos tipos_Conceptos)
        {
            if (id != tipos_Conceptos.TpCcpto_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipos_Conceptos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipos_ConceptosExists(id))
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

        // POST: api/Tipos_Conceptos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipos_Conceptos>> PostTipos_Conceptos(Tipos_Conceptos tipos_Conceptos)
        {
            _context.Tipos_Conceptos.Add(tipos_Conceptos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipos_Conceptos", new { id = tipos_Conceptos.TpCcpto_Id }, tipos_Conceptos);
        }

        // DELETE: api/Tipos_Conceptos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipos_Conceptos(int id)
        {
            var tipos_Conceptos = await _context.Tipos_Conceptos.FindAsync(id);
            if (tipos_Conceptos == null)
            {
                return NotFound();
            }

            _context.Tipos_Conceptos.Remove(tipos_Conceptos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipos_ConceptosExists(int id)
        {
            return _context.Tipos_Conceptos.Any(e => e.TpCcpto_Id == id);
        }
    }
}
