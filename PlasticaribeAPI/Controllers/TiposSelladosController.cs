using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TiposSelladosController : ControllerBase
    {
        private readonly dataContext _context;

        public TiposSelladosController(dataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipos_Sellados>>> GetTipos_Sellados()
        {
            if (_context.Sedes_Clientes == null)
            {
                return NotFound();
            }
            return await _context.Tipos_Sellados.ToListAsync();
        }

        // GET: api/SedesClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipos_Sellados>> GetTipos_Sellados(int id)
        {
            if (_context.Sedes_Clientes == null)
            {
                return NotFound();
            }
            var sedesClientes = await _context.Tipos_Sellados.FindAsync(id);

            if (sedesClientes == null)
            {
                return NotFound();
            }

            return sedesClientes;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipos_Sellados(int id, Tipos_Sellados TiposSellados)
        {
            if (id != TiposSellados.TpSellado_Id)
            {
                return BadRequest();
            }

            _context.Entry(TiposSellados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipos_SelladosExists(id))
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

        // POST: api/SedesClientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipos_Sellados>> PostTipos_Sellados(Tipos_Sellados TiposSellados)
        {
            if (_context.Tipos_Sellados == null)
            {
                return Problem("Entity set 'dataContext.Sedes_Clientes'  is null.");
            }
            _context.Tipos_Sellados.Add(TiposSellados);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Tipos_SelladosExists(TiposSellados.TpSellado_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSedesClientes", new { id = TiposSellados.TpSellado_Id }, TiposSellados);
        }

        // DELETE: api/SedesClientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipos_Sellados(int id)
        {
            if (_context.Tipos_Sellados == null)
            {
                return NotFound();
            }
            var TiposSellados = await _context.Tipos_Sellados.FindAsync(id);
            if (TiposSellados == null)
            {
                return NotFound();
            }

            _context.Tipos_Sellados.Remove(TiposSellados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipos_SelladosExists(int id)
        {
            return (_context.Tipos_Sellados?.Any(e => e.TpSellado_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
