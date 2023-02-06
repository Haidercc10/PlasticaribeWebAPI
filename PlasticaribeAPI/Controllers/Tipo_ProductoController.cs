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
    public class Tipo_ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        public Tipo_ProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Producto>>> GetTipos_Productos()
        {
            return await _context.Tipos_Productos.ToListAsync();
        }

        // GET: api/Tipo_Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Producto>> GetTipo_Producto(int id)
        {
            var tipo_Producto = await _context.Tipos_Productos.FindAsync(id);

            if (tipo_Producto == null)
            {
                return NotFound();
            }

            return tipo_Producto;
        }

        [HttpGet("nombreTipoProducto/{TpProd_Nombre}")]
        public ActionResult<Tipo_Producto> GetProductoPedido(string TpProd_Nombre)
        {
            try
            {
                var tipoProducto = _context.Tipos_Productos.Where(tp => tp.TpProd_Nombre == TpProd_Nombre).ToList();


                if (tipoProducto == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(tipoProducto);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // PUT: api/Tipo_Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_Producto(int id, Tipo_Producto tipo_Producto)
        {
            if (id != tipo_Producto.TpProd_Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_ProductoExists(id))
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

        // POST: api/Tipo_Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_Producto>> PostTipo_Producto(Tipo_Producto tipo_Producto)
        {
            _context.Tipos_Productos.Add(tipo_Producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Producto", new { id = tipo_Producto.TpProd_Id }, tipo_Producto);
        }

        // DELETE: api/Tipo_Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_Producto(int id)
        {
            var tipo_Producto = await _context.Tipos_Productos.FindAsync(id);
            if (tipo_Producto == null)
            {
                return NotFound();
            }

            _context.Tipos_Productos.Remove(tipo_Producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_ProductoExists(int id)
        {
            return _context.Tipos_Productos.Any(e => e.TpProd_Id == id);
        }
    }
}
