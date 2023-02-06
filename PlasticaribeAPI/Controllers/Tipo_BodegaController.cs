#nullable disable
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
    public class Tipo_BodegaController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_BodegaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Bodega
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Bodega>>> GetTipos_Bodegas()
        {
            return await _context.Tipos_Bodegas.ToListAsync();
        }

        // GET: api/Tipo_Bodega/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Bodega>> GetTipo_Bodega(int id)
        {
            var tipo_Bodega = await _context.Tipos_Bodegas.FindAsync(id);

            if (tipo_Bodega == null)
            {
                return NotFound();
            }

            return tipo_Bodega;
        }

        // PUT: api/Tipo_Bodega/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Bodega(int id, Tipo_Bodega tipo_Bodega)
        {
            if (id != tipo_Bodega.TpBod_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Bodega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_BodegaExists(id))
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

        // POST: api/Tipo_Bodega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Bodega>> PostTipo_Bodega(Tipo_Bodega tipo_Bodega)
        {
            _context.Tipos_Bodegas.Add(tipo_Bodega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Bodega", new { id = tipo_Bodega.TpBod_Id }, tipo_Bodega);
        }

        // DELETE: api/Tipo_Bodega/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Bodega(int id)
        {
            var tipo_Bodega = await _context.Tipos_Bodegas.FindAsync(id);
            if (tipo_Bodega == null)
            {
                return NotFound();
            }

            _context.Tipos_Bodegas.Remove(tipo_Bodega);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_BodegaExists(int id)
        {
            return _context.Tipos_Bodegas.Any(e => e.TpBod_Id == id);
        }
    }
}
