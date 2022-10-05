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
    public class Existencia_ProductosController : ControllerBase
    {
        private readonly dataContext _context;

        public Existencia_ProductosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Existencia_Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencia_Productos>>> GetExistencias_Productos()
        {
          if (_context.Existencias_Productos == null)
          {
              return NotFound();
          }
            return await _context.Existencias_Productos.ToListAsync();
        }

        // GET: api/Existencia_Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Existencia_Productos>> GetExistencia_Productos(long id)
        {
          if (_context.Existencias_Productos == null)
          {
              return NotFound();
          }
            var existencia_Productos = await _context.Existencias_Productos.FindAsync(id);

            if (existencia_Productos == null)
            {
                return NotFound();
            }

            return existencia_Productos;
        }

        [HttpGet("IdProducto/{Prod_Id}")]
        public ActionResult<Existencia_Productos> GetNombreCliente(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == Prod_Id)
                .Select(p => new
                {
                    p.ExProd_Id,
                    p.Prod_Id,
                    p.Prod.Prod_Nombre,
                    p.Prod.Prod_Ancho,
                    p.Prod.Prod_Fuelle,
                    p.Prod.Prod_Calibre,
                    p.Prod.Prod_Largo,
                    p.Prod.UndMedACF,
                    p.Prod.TpProd.TpProd_Nombre,
                    p.Prod.MaterialMP.Material_Nombre,
                    p.Prod.Pigmt.Pigmt_Nombre,
                    p.UndMed_Id,
                    p.ExProd_PrecioVenta,
                    p.ExProd_Cantidad,
                    p.Prod.Prod_Descripcion,
                    p.TpMoneda_Id,
                    p.Prod.Prod_Peso_Millar,
                    p.Prod.Prod_Peso,
                    p.Prod.UndMedPeso,
                    p.TpBod_Id,
                    p.Prod.TiposSellados.TpSellados_Nombre,
                    p.Prod.Prod_CantBolsasPaquete,
                    p.Prod.Prod_CantBolsasBulto,
                    p.ExProd_CantMinima

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

        /** Compara ID Producto de PBDD con Codigo Articulo de Zeus. */
        [HttpGet("IdProductoPBDDXCodigoArticuloZeus/{Prod_Id}")]
        public ActionResult<Existencia_Productos> GetCompararProductos(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == Prod_Id)
                .Select(p => new
                {
                    p.Prod_Id,
                    p.Prod.Prod_Nombre,
                    p.ExProd_Cantidad,
                    p.UndMed_Id,                  
                    p.ExProd_Id,
                    p.ExProd_Precio,
                    p.ExProd_PrecioExistencia,
                    p.ExProd_PrecioSinInflacion,
                    p.TpMoneda_Id,
                    p.ExProd_PrecioVenta,
                    p.ExProd_CantMinima
                    
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

        [HttpGet("IdProductoPresentacion/{Prod_Id}/{UndMed_Id}")]
        public ActionResult<Existencia_Productos> GetProductoPresentacion(int Prod_Id, string UndMed_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == Prod_Id && p.UndMed_Id == UndMed_Id)
                .Select(p => new
                {
                    p.ExProd_Id,
                    p.Prod_Id,
                    p.Prod.Prod_Nombre,
                    p.Prod.Prod_Ancho,
                    p.Prod.Prod_Fuelle,
                    p.Prod.Prod_Calibre,
                    p.Prod.Prod_Largo,
                    p.Prod.UndMedACF,
                    p.Prod.TpProd.TpProd_Nombre,
                    p.Prod.MaterialMP.Material_Nombre,
                    p.Prod.Pigmt.Pigmt_Nombre,
                    p.UndMed_Id,
                    p.ExProd_PrecioVenta,
                    p.ExProd_Cantidad,
                    p.Prod.Prod_Descripcion,
                    p.TpMoneda_Id,
                    p.Prod.Prod_Peso_Millar,
                    p.Prod.Prod_Peso,
                    p.Prod.UndMedPeso,
                    p.TpBod_Id,

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


        [HttpGet("InventarioProductos")]
        public ActionResult<Existencia_Productos> GetInventarioProductos()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var producto = _context.Existencias_Productos.Where(p => p.Prod_Id == p.Prod.Prod_Id && p.ExProd_Cantidad >= 1)
                .Include(p => p.Prod)
                .Select(p => new
                {                    
                    p.Prod_Id,
                    p.Prod.Prod_Nombre, 
                    p.ExProd_PrecioVenta,
                    p.ExProd_Cantidad,
                    p.UndMed_Id,
                    p.ExProd_CantMinima
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

        // PUT: api/Existencia_Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExistencia_Productos(long id, Existencia_Productos existencia_Productos)
        {
            if (id != existencia_Productos.ExProd_Id)
            {
                return BadRequest();
            }

            _context.Entry(existencia_Productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(id))
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

        [HttpPut("ActualizacionProducto/{Prod_Id}/{UndMed_Id}")]
        public IActionResult PutEstadoClientesOt(int Prod_Id, string UndMed_Id, Existencia_Productos existencia_Productos)
        {

            if (Prod_Id != existencia_Productos.Prod_Id && UndMed_Id != existencia_Productos.UndMed_Id)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.Existencias_Productos.Where(x => x.Prod_Id == Prod_Id && x.UndMed_Id == UndMed_Id).First<Existencia_Productos>();
                Actualizado.ExProd_PrecioVenta = existencia_Productos.ExProd_PrecioVenta;
                Actualizado.ExProd_Precio = existencia_Productos.ExProd_Precio;
                Actualizado.TpMoneda_Id = existencia_Productos.TpMoneda_Id;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(Prod_Id, UndMed_Id))
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

        [HttpPut("ActualizacionExistencia/{exProd_Id}")]
        public IActionResult Put(int ExProd_Id, Existencia_Productos existencia_Productos)
        {

            if (ExProd_Id != existencia_Productos.ExProd_Id)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.Existencias_Productos.Where(x => x.ExProd_Id == ExProd_Id).First<Existencia_Productos>();
                Actualizado.ExProd_PrecioVenta = existencia_Productos.ExProd_PrecioVenta;
                Actualizado.ExProd_Cantidad = existencia_Productos.ExProd_Cantidad;
                Actualizado.ExProd_PrecioExistencia = existencia_Productos.ExProd_PrecioExistencia;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(ExProd_Id))
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

        [HttpPut("ActualizacionCantMinima/{Prod_Id}")]
        public IActionResult PutCantMinima(int Prod_Id, Existencia_Productos existencia_Productos)
        {
            if (Prod_Id != existencia_Productos.Prod_Id)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.Existencias_Productos.Where(x => x.Prod_Id == Prod_Id).First<Existencia_Productos>();
                Actualizado.ExProd_CantMinima = existencia_Productos.ExProd_CantMinima;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Existencia_ProductosExists(Prod_Id))
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

        // POST: api/Existencia_Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Existencia_Productos>> PostExistencia_Productos(Existencia_Productos existencia_Productos)
        {
          if (_context.Existencias_Productos == null)
          {
              return Problem("Entity set 'dataContext.Existencias_Productos'  is null.");
          }
            _context.Existencias_Productos.Add(existencia_Productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExistencia_Productos", new { id = existencia_Productos.ExProd_Id }, existencia_Productos);
        }

        // DELETE: api/Existencia_Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExistencia_Productos(long id)
        {
            if (_context.Existencias_Productos == null)
            {
                return NotFound();
            }
            var existencia_Productos = await _context.Existencias_Productos.FindAsync(id);
            if (existencia_Productos == null)
            {
                return NotFound();
            }

            _context.Existencias_Productos.Remove(existencia_Productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Existencia_ProductosExists(long id)
        {
            return (_context.Existencias_Productos?.Any(e => e.ExProd_Id == id)).GetValueOrDefault();
        }

        //Actualizacion de existencia segun el id del producto y la presentacion de este mismo
        private bool Existencia_ProductosExists(int prod_Id, string undMed_Id)
        {
            return (_context.Existencias_Productos?.Any(e => e.Prod_Id == prod_Id && e.UndMed_Id == undMed_Id)).GetValueOrDefault();
        }
    }
}
