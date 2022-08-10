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
    public class Cliente_ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        public Cliente_ProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Cliente_Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente_Producto>>> GetClientes_Productos()
        {
          if (_context.Clientes_Productos == null)
          {
              return NotFound();
          }
            return await _context.Clientes_Productos.ToListAsync();
        }

        [HttpGet("IdCliente/{Cli_Id}")]
        public ActionResult<Cliente_Producto> GetNombreCliente(long Cli_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var clientes = _context.Clientes_Productos.Where(pp => pp.Cli.Cli_Id == Cli_Id).ToList();

            if (clientes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientes);
            }
        }

        // GET: api/Cliente_Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente_Producto>> GetCliente_Producto(long Cli_Id, int Prod_Id)
        {
          if (_context.Clientes_Productos == null)
          {
              return NotFound();
          }
            var cliente_Producto = await _context.Clientes_Productos.FindAsync(Prod_Id, Cli_Id);

            if (cliente_Producto == null)
            {
                return NotFound();
            }

            return cliente_Producto;
        }

        // PUT: api/Cliente_Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente_Producto(int id, Cliente_Producto cliente_Producto)
        {
            if (id != cliente_Producto.Prod_Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente_Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cliente_ProductoExists(id))
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

        // POST: api/Cliente_Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente_Producto>> PostCliente_Producto(Cliente_Producto cliente_Producto)
        {
          if (_context.Clientes_Productos == null)
          {
              return Problem("Entity set 'dataContext.Clientes_Productos'  is null.");
          }
            _context.Clientes_Productos.Add(cliente_Producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Cliente_ProductoExists(cliente_Producto.Prod_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente_Producto", new { id = cliente_Producto.Prod_Id }, cliente_Producto);
        }

        // DELETE: api/Cliente_Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente_Producto(int id)
        {
            if (_context.Clientes_Productos == null)
            {
                return NotFound();
            }
            var cliente_Producto = await _context.Clientes_Productos.FindAsync(id);
            if (cliente_Producto == null)
            {
                return NotFound();
            }

            _context.Clientes_Productos.Remove(cliente_Producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cliente_ProductoExists(int id)
        {
            return (_context.Clientes_Productos?.Any(e => e.Prod_Id == id)).GetValueOrDefault();
        }
    }
}
