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
    public class Tipo_MonedaController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_MonedaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Moneda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Moneda>>> GetTipos_Moneda()
        {
            return await _context.Tipos_Moneda.ToListAsync();
        }

        // GET: api/Tipo_Moneda/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Moneda>> GetTipo_Moneda(string id)
        {
            var tipo_Moneda = await _context.Tipos_Moneda.FindAsync(id);

            if (tipo_Moneda == null)
            {
                return NotFound();
            }

            return tipo_Moneda;
        }

        // PUT: api/Tipo_Moneda/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Moneda(string id, Tipo_Moneda tipo_Moneda)
        {
            if (id != tipo_Moneda.TpMoneda_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Moneda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_MonedaExists(id))
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

        // POST: api/Tipo_Moneda
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Moneda>> PostTipo_Moneda(Tipo_Moneda tipo_Moneda)
        {
            _context.Tipos_Moneda.Add(tipo_Moneda);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Tipo_MonedaExists(tipo_Moneda.TpMoneda_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipo_Moneda", new { id = tipo_Moneda.TpMoneda_Id }, tipo_Moneda);
        }

        // DELETE: api/Tipo_Moneda/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Moneda(string id)
        {
            var tipo_Moneda = await _context.Tipos_Moneda.FindAsync(id);
            if (tipo_Moneda == null)
            {
                return NotFound();
            }

            _context.Tipos_Moneda.Remove(tipo_Moneda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_MonedaExists(string id)
        {
            return _context.Tipos_Moneda.Any(e => e.TpMoneda_Id == id);
        }
    }
}
