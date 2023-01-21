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
    public class ProductoController : ControllerBase
    {
        private readonly dataContext _context;

        public ProductoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
          if (_context.Productos == null)
          {
              return NotFound();
          }
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
          if (_context.Productos == null)
          {
              return NotFound();
          }
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }


        [HttpGet("consultaNombreItem/{letras}")]
        public ActionResult GetItem(string letras)
        {
            var productos = _context.Productos.Where(p => p.Prod_Nombre.StartsWith(letras))
                                              .Select(p => new {p.Prod_Id,  p.Prod_Nombre })
                                              .Take(30)
                                              .ToList();
                            
            return Ok(productos);
        }


        [HttpGet("IdProducto/{Prod_Id}")]
        public ActionResult<Producto> GetNombreCliente(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Productos.Where(p => p.Prod_Id == Prod_Id)
                .Select(p => new
                {
                    p.Prod_Id,
                    p.Prod_Nombre,
                    p.Prod_Ancho,
                    p.Prod_Fuelle,
                    p.Prod_Calibre,
                    p.Prod_Largo,
                    p.UndMedACF,
                    p.TpProd.TpProd_Nombre,
                    p.MaterialMP.Material_Nombre,
                    p.Pigmt.Pigmt_Nombre,
                    p.Prod_Descripcion,
                    p.Prod_Peso_Millar,
                    p.Prod_Peso,
                    p.UndMedPeso,

                })
                .ToList();

            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(producto);
            }
        }


        [HttpGet("consultaGeneral")]
        public ActionResult Get()
        {
            var productos = from p in _context.Set<Producto>()
                            select new
                            {
                                p.Prod_Id,
                                p.Prod_Nombre,
                            };
            return Ok(productos);
        }


        [HttpGet("consultaNombreProducto/{Id}")]
        public ActionResult GetNombreProducto(int Id)
        {
            var productos = from p in _context.Set<Producto>()
                            where p.Prod_Id == Id
                            select p.Prod_Nombre;

            return Ok(productos);
        }


        [HttpGet("ConsultaProductoExistencia/{Prod_Id}")]
        public ActionResult<Producto> GetProductoPresentacion(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = (from E in _context.Set<Existencia_Productos>()
                            from P in _context.Set<Producto>()
                            where P.Prod_Id == Prod_Id 
                            && E.Prod_Id == Prod_Id
                            && E.Prod_Id == P.Prod_Id
                            select new
                            {
                                E.Prod_Id,
                                P.Prod_Nombre,
                                E.ExProd_Cantidad, 
                                E.ExProd_PrecioVenta,
                                E.TpMoneda_Id,
                                E.UndMed_Id
                            })
                            .ToList();

            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(producto);
            }

        }

        //Funcion que va a consultar el id del ultimo producto creado
        [HttpGet("getIdUltimoProducto")]
        public ActionResult GetIdUltimoProducto()
        {
            var con = (from prod in _context.Set<Producto>()
                       orderby prod.Prod_Id descending
                       select prod.Prod_Id).FirstOrDefault();
            return Ok(con);
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Prod_Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
          if (_context.Productos == null)
          {
              return Problem("Entity set 'dataContext.Productos'  is null.");
          }
            _context.Productos.Add(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductoExists(producto.Prod_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProducto", new { id = producto.Prod_Id }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.Prod_Id == id)).GetValueOrDefault();
        }
    }
}
