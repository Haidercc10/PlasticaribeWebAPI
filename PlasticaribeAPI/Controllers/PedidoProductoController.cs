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
    public class PedidoProductoController : ControllerBase
    {
        private readonly dataContext _context;
        
        public PedidoProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/PedidoProducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoProducto>>> GetPedidosExternos_Productos()
        {
            if (_context.PedidosExternos_Productos == null)
            {
                return NotFound();
            }
            return await _context.PedidosExternos_Productos.ToListAsync();
        }

        // GET: api/PedidoProducto/5
        [HttpGet("{id}")]
               
        public async Task<ActionResult<PedidoProducto>> GetPedidoProducto(int Prod_Id, long PedExt_Id)
        {
          if (_context.PedidosExternos_Productos == null)
          {
              return NotFound();
          }
            var pedidoProducto = await _context.PedidosExternos_Productos.FindAsync(Prod_Id, PedExt_Id);

            if (pedidoProducto == null)
            {
                return NotFound();
            } 

            return pedidoProducto;
        }

        // PUT: api/PedidoProducto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidoProducto(int Prod_Id, long PedExt_Id, PedidoProducto pedidoProducto)
        {
            if (Prod_Id != pedidoProducto.Prod_Id && PedExt_Id != pedidoProducto.PedExt_Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidoProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoProductoExists(Prod_Id))
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

        // POST: api/PedidoProducto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PedidoProducto>> PostPedidoProducto(PedidoProducto pedidoProducto)
        {
          if (_context.PedidosExternos_Productos == null)
          {
              return Problem("Entity set 'dataContext.PedidosExternos_Productos'  is null.");
          }
            _context.PedidosExternos_Productos.Add(pedidoProducto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PedidoProductoExists(pedidoProducto.Prod_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPedidoProducto", new { id = pedidoProducto.Prod_Id }, pedidoProducto);
        }

        // DELETE: api/PedidoProducto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoProducto(int Prod_Id, long PedExt_Id)
        {
            if (_context.PedidosExternos_Productos == null)
            {
                return NotFound();
            }
            var pedidoProducto = await _context.PedidosExternos_Productos.FindAsync(Prod_Id, PedExt_Id);
            if (pedidoProducto == null)
            {
                return NotFound();
            }

            _context.PedidosExternos_Productos.Remove(pedidoProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoProductoExists(int id)
        {
            return (_context.PedidosExternos_Productos?.Any(e => e.Prod_Id == id)).GetValueOrDefault();
        }
    }
}
