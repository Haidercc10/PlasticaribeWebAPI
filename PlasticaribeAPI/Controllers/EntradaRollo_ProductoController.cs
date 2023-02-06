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
    public class EntradaRollo_ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        public EntradaRollo_ProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/EntradaRollo_Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntradaRollo_Producto>>> GetEntradasRollos_Productos()
        {
            return await _context.EntradasRollos_Productos.ToListAsync();
        }

        // GET: api/EntradaRollo_Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntradaRollo_Producto>> GetEntradaRollo_Producto(long id)
        {
            var entradaRollo_Producto = await _context.EntradasRollos_Productos.FindAsync(id);

            if (entradaRollo_Producto == null)
            {
                return NotFound();
            }

            return entradaRollo_Producto;
        }

        [HttpGet("UltumoID")]
        public ActionResult Get()
        {
            var con = _context.EntradasRollos_Productos.OrderByDescending(ent => ent.EntRolloProd_Id).First();
            return Ok(con);
        }

        // PUT: api/EntradaRollo_Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradaRollo_Producto(long id, EntradaRollo_Producto entradaRollo_Producto)
        {
            if (id != entradaRollo_Producto.EntRolloProd_Id)
            {
                return BadRequest();
            }

            _context.Entry(entradaRollo_Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradaRollo_ProductoExists(id))
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

        // POST: api/EntradaRollo_Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntradaRollo_Producto>> PostEntradaRollo_Producto(EntradaRollo_Producto entradaRollo_Producto)
        {
            _context.EntradasRollos_Productos.Add(entradaRollo_Producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntradaRollo_Producto", new { id = entradaRollo_Producto.EntRolloProd_Id }, entradaRollo_Producto);
        }

        // DELETE: api/EntradaRollo_Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntradaRollo_Producto(long id)
        {
            var entradaRollo_Producto = await _context.EntradasRollos_Productos.FindAsync(id);
            if (entradaRollo_Producto == null)
            {
                return NotFound();
            }

            _context.EntradasRollos_Productos.Remove(entradaRollo_Producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntradaRollo_ProductoExists(long id)
        {
            return _context.EntradasRollos_Productos.Any(e => e.EntRolloProd_Id == id);
        }
    }
}
