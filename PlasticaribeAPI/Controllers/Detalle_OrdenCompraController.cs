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
    public class Detalle_OrdenCompraController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalle_OrdenCompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalle_OrdenCompra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle_OrdenCompra>>> GetDetalles_OrdenesCompras()
        {
            return await _context.Detalles_OrdenesCompras.ToListAsync();
        }

        // GET: api/Detalle_OrdenCompra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalle_OrdenCompra>> GetDetalle_OrdenCompra(long id)
        {
            var detalle_OrdenCompra = await _context.Detalles_OrdenesCompras.FindAsync(id);

            if (detalle_OrdenCompra == null)
            {
                return NotFound();
            }

            return detalle_OrdenCompra;
        }

        // PUT: api/Detalle_OrdenCompra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle_OrdenCompra(long id, Detalle_OrdenCompra detalle_OrdenCompra)
        {
            if (id != detalle_OrdenCompra.Doc_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalle_OrdenCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_OrdenCompraExists(id))
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

        // POST: api/Detalle_OrdenCompra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalle_OrdenCompra>> PostDetalle_OrdenCompra(Detalle_OrdenCompra detalle_OrdenCompra)
        {
            _context.Detalles_OrdenesCompras.Add(detalle_OrdenCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalle_OrdenCompra", new { id = detalle_OrdenCompra.Doc_Codigo }, detalle_OrdenCompra);
        }

        // DELETE: api/Detalle_OrdenCompra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle_OrdenCompra(long id)
        {
            var detalle_OrdenCompra = await _context.Detalles_OrdenesCompras.FindAsync(id);
            if (detalle_OrdenCompra == null)
            {
                return NotFound();
            }

            _context.Detalles_OrdenesCompras.Remove(detalle_OrdenCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalle_OrdenCompraExists(long id)
        {
            return _context.Detalles_OrdenesCompras.Any(e => e.Doc_Codigo == id);
        }
    }
}
