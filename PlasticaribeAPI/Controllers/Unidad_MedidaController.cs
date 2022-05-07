#nullable disable
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
    public class Unidad_MedidaController : ControllerBase
    {
        private readonly dataContext _context;

        public Unidad_MedidaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Unidad_Medida
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unidad_Medida>>> GetUnidades_Medidas()
        {
            return await _context.Unidades_Medidas.ToListAsync();
        }

        // GET: api/Unidad_Medida/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unidad_Medida>> GetUnidad_Medida(string id)
        {
            var unidad_Medida = await _context.Unidades_Medidas.FindAsync(id);

            if (unidad_Medida == null)
            {
                return NotFound();
            }

            return unidad_Medida;
        }

        // PUT: api/Unidad_Medida/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidad_Medida(string id, Unidad_Medida unidad_Medida)
        {
            if (id != unidad_Medida.UndMed_Id)
            {
                return BadRequest();
            }

            _context.Entry(unidad_Medida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Unidad_MedidaExists(id))
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

        // POST: api/Unidad_Medida
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unidad_Medida>> PostUnidad_Medida(Unidad_Medida unidad_Medida)
        {
            _context.Unidades_Medidas.Add(unidad_Medida);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Unidad_MedidaExists(unidad_Medida.UndMed_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUnidad_Medida", new { id = unidad_Medida.UndMed_Id }, unidad_Medida);
        }

        // DELETE: api/Unidad_Medida/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnidad_Medida(string id)
        {
            var unidad_Medida = await _context.Unidades_Medidas.FindAsync(id);
            if (unidad_Medida == null)
            {
                return NotFound();
            }

            _context.Unidades_Medidas.Remove(unidad_Medida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Unidad_MedidaExists(string id)
        {
            return _context.Unidades_Medidas.Any(e => e.UndMed_Id == id);
        }
    }
}
