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
    public class DetalleDevolucion_ProductoFacturadoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleDevolucion_ProductoFacturadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleDevolucion_ProductoFacturado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleDevolucion_ProductoFacturado>>> GetDetallesDevoluciones_ProductosFacturados()
        {
            return await _context.DetallesDevoluciones_ProductosFacturados.ToListAsync();
        }

        // GET: api/DetalleDevolucion_ProductoFacturado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleDevolucion_ProductoFacturado>> GetDetalleDevolucion_ProductoFacturado(long id)
        {
            var detalleDevolucion_ProductoFacturado = await _context.DetallesDevoluciones_ProductosFacturados.FindAsync(id);

            if (detalleDevolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            return detalleDevolucion_ProductoFacturado;
        }

        // PUT: api/DetalleDevolucion_ProductoFacturado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleDevolucion_ProductoFacturado(long id, DetalleDevolucion_ProductoFacturado detalleDevolucion_ProductoFacturado)
        {
            if (id != detalleDevolucion_ProductoFacturado.DevProdFact_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleDevolucion_ProductoFacturado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleDevolucion_ProductoFacturadoExists(id))
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

        // POST: api/DetalleDevolucion_ProductoFacturado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleDevolucion_ProductoFacturado>> PostDetalleDevolucion_ProductoFacturado(DetalleDevolucion_ProductoFacturado detalleDevolucion_ProductoFacturado)
        {
            _context.DetallesDevoluciones_ProductosFacturados.Add(detalleDevolucion_ProductoFacturado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleDevolucion_ProductoFacturadoExists(detalleDevolucion_ProductoFacturado.DevProdFact_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleDevolucion_ProductoFacturado", new { id = detalleDevolucion_ProductoFacturado.DevProdFact_Id }, detalleDevolucion_ProductoFacturado);
        }

        // DELETE: api/DetalleDevolucion_ProductoFacturado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleDevolucion_ProductoFacturado(long id)
        {
            var detalleDevolucion_ProductoFacturado = await _context.DetallesDevoluciones_ProductosFacturados.FindAsync(id);
            if (detalleDevolucion_ProductoFacturado == null)
            {
                return NotFound();
            }

            _context.DetallesDevoluciones_ProductosFacturados.Remove(detalleDevolucion_ProductoFacturado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleDevolucion_ProductoFacturadoExists(long id)
        {
            return _context.DetallesDevoluciones_ProductosFacturados.Any(e => e.DevProdFact_Id == id);
        }
    }
}
