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
    public class DetalleEntradaRollo_ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleEntradaRollo_ProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleEntradaRollo_Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleEntradaRollo_Producto>>> GetDetallesEntradasRollos_Productos()
        {
            return await _context.DetallesEntradasRollos_Productos.ToListAsync();
        }

        // GET: api/DetalleEntradaRollo_Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleEntradaRollo_Producto>> GetDetalleEntradaRollo_Producto(long id)
        {
            var detalleEntradaRollo_Producto = await _context.DetallesEntradasRollos_Productos.FindAsync(id);

            if (detalleEntradaRollo_Producto == null)
            {
                return NotFound();
            }

            return detalleEntradaRollo_Producto;
        }

        [HttpGet("VerificarRollo/{id}")]
        public ActionResult Get (long id)
        {
            var con = _context.DetallesEntradasRollos_Productos.Where(x => x.Rollo_Id == id).ToList();
            return Ok(con);
        }

        [HttpGet("consultarProducto/{id}")]
        public ActionResult GetConsultarProd (long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesEntradasRollos_Productos
                .Where(x => x.EntRollo_Producto.Prod_Id == id)
                .Select(x => new
                {
                    x.EntRolloProd_Id,
                    x.EntRollo_Producto.Prod_Id,
                    x.EntRollo_Producto.Prod.Prod_Nombre,
                    x.Estado_Id,
                    x.Rollo_Id,
                    x.DtEntRolloProd_Cantidad,
                    x.UndMed_Id
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // PUT: api/DetalleEntradaRollo_Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleEntradaRollo_Producto(long id, DetalleEntradaRollo_Producto detalleEntradaRollo_Producto)
        {
            if (id != detalleEntradaRollo_Producto.DtEntRolloProd_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalleEntradaRollo_Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleEntradaRollo_ProductoExists(id))
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

        // POST: api/DetalleEntradaRollo_Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleEntradaRollo_Producto>> PostDetalleEntradaRollo_Producto(DetalleEntradaRollo_Producto detalleEntradaRollo_Producto)
        {
            _context.DetallesEntradasRollos_Productos.Add(detalleEntradaRollo_Producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleEntradaRollo_Producto", new { id = detalleEntradaRollo_Producto.DtEntRolloProd_Codigo }, detalleEntradaRollo_Producto);
        }

        // DELETE: api/DetalleEntradaRollo_Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleEntradaRollo_Producto(long id)
        {
            var detalleEntradaRollo_Producto = await _context.DetallesEntradasRollos_Productos.FindAsync(id);
            if (detalleEntradaRollo_Producto == null)
            {
                return NotFound();
            }

            _context.DetallesEntradasRollos_Productos.Remove(detalleEntradaRollo_Producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleEntradaRollo_ProductoExists(long id)
        {
            return _context.DetallesEntradasRollos_Productos.Any(e => e.DtEntRolloProd_Codigo == id);
        }
    }
}
