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
    public class ProveedorController : ControllerBase
    {
        private readonly dataContext _context;

        public ProveedorController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Proveedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
          if (_context.Proveedores == null)
          {
              return NotFound();
          }
            return await _context.Proveedores.ToListAsync();
        }

        // GET: api/Proveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(long id)
        {
          if (_context.Proveedores == null)
          {
              return NotFound();
          }
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        //
        [HttpGet("getProveedorLike/{nombre}")]
        public ActionResult getProveedorLike(string nombre)
        {
            var con = _context.Proveedores
                .Where(x => x.Prov_Nombre.StartsWith(nombre) || Convert.ToString(x.Prov_Id).StartsWith(nombre))
                .Select(x => new
                {
                    x.Prov_Id,
                    x.Prov_Nombre,
                });
            return Ok(con);
        }

        // PUT: api/Proveedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(long id, Proveedor proveedor)
        {
            if (id != proveedor.Prov_Id)
            {
                return BadRequest();
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        // POST: api/Proveedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
          if (_context.Proveedores == null)
          {
              return Problem("Entity set 'dataContext.Proveedores'  is null.");
          }
            _context.Proveedores.Add(proveedor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProveedorExists(proveedor.Prov_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProveedor", new { id = proveedor.Prov_Id }, proveedor);
        }

        // DELETE: api/Proveedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(long id)
        {
            if (_context.Proveedores == null)
            {
                return NotFound();
            }
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(long id)
        {
            return (_context.Proveedores?.Any(e => e.Prov_Id == id)).GetValueOrDefault();
        }
    }
}
