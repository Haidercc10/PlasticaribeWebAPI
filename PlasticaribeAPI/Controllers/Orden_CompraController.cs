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
    public class Orden_CompraController : ControllerBase
    {
        private readonly dataContext _context;

        public Orden_CompraController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Orden_Compra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden_Compra>>> GetOrdenes_Compras()
        {
            return await _context.Ordenes_Compras.ToListAsync();
        }

        //FUNCION QUE CONSULTARÁ Y RETURNARÁ EL ULTIMO ID CREADO 
        [HttpGet("GetUltimoId")]
        public ActionResult GetUltimoId()
        {
            var con = _context.Ordenes_Compras.OrderBy(x => x.Oc_Id).Select(x => x.Oc_Id).Last();
            return Ok(con);
        }

        // GET: api/Orden_Compra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden_Compra>> GetOrden_Compra(long id)
        {
            var orden_Compra = await _context.Ordenes_Compras.FindAsync(id);

            if (orden_Compra == null)
            {
                return NotFound();
            }

            return orden_Compra;
        }

        // PUT: api/Orden_Compra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrden_Compra(long id, Orden_Compra orden_Compra)
        {
            if (id != orden_Compra.Oc_Id)
            {
                return BadRequest();
            }

            _context.Entry(orden_Compra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Orden_CompraExists(id))
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

        // POST: api/Orden_Compra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orden_Compra>> PostOrden_Compra(Orden_Compra orden_Compra)
        {
            _context.Ordenes_Compras.Add(orden_Compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrden_Compra", new { id = orden_Compra.Oc_Id }, orden_Compra);
        }

        // DELETE: api/Orden_Compra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden_Compra(long id)
        {
            var orden_Compra = await _context.Ordenes_Compras.FindAsync(id);
            if (orden_Compra == null)
            {
                return NotFound();
            }

            _context.Ordenes_Compras.Remove(orden_Compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Orden_CompraExists(long id)
        {
            return _context.Ordenes_Compras.Any(e => e.Oc_Id == id);
        }
    }
}
