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
    public class Tipo_ProveedorController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_ProveedorController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Proveedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Proveedor>>> GetTipos_Proveedores()
        {
          if (_context.Tipos_Proveedores == null)
          {
              return NotFound();
          }
            return await _context.Tipos_Proveedores.ToListAsync();
        }

        // GET: api/Tipo_Proveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Proveedor>> GetTipo_Proveedor(int id)
        {
          if (_context.Tipos_Proveedores == null)
          {
              return NotFound();
          }
            var tipo_Proveedor = await _context.Tipos_Proveedores.FindAsync(id);

            if (tipo_Proveedor == null)
            {
                return NotFound();
            }

            return tipo_Proveedor;
        }

        // PUT: api/Tipo_Proveedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Proveedor(int id, Tipo_Proveedor tipo_Proveedor)
        {
            if (id != tipo_Proveedor.TpProv_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_ProveedorExists(id))
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

        // POST: api/Tipo_Proveedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Proveedor>> PostTipo_Proveedor(Tipo_Proveedor tipo_Proveedor)
        {
          if (_context.Tipos_Proveedores == null)
          {
              return Problem("Entity set 'dataContext.Tipos_Proveedores'  is null.");
          }
            _context.Tipos_Proveedores.Add(tipo_Proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Proveedor", new { id = tipo_Proveedor.TpProv_Id }, tipo_Proveedor);
        }

        // DELETE: api/Tipo_Proveedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Proveedor(int id)
        {
            if (_context.Tipos_Proveedores == null)
            {
                return NotFound();
            }
            var tipo_Proveedor = await _context.Tipos_Proveedores.FindAsync(id);
            if (tipo_Proveedor == null)
            {
                return NotFound();
            }

            _context.Tipos_Proveedores.Remove(tipo_Proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_ProveedorExists(int id)
        {
            return (_context.Tipos_Proveedores?.Any(e => e.TpProv_Id == id)).GetValueOrDefault();
        }
    }
}
