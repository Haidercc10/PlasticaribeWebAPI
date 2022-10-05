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
        public ActionResult Get(long id)
        {
            var con = _context.DetallesEntradasRollos_Productos.Where(x => x.Rollo_Id == id).ToList();
            return Ok(con);
        }

        [HttpGet("consultarProducto/{id}")]
        public ActionResult GetConsultarProd(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesEntradasRollos_Productos
                .Where(x => x.Prod_Id == id)
                .Select(x => new
                {
                    x.EntRolloProd_Id,
                    x.Prod_Id,
                    x.Prod.Prod_Nombre,
                    x.Estado_Id,
                    x.Rollo_Id,
                    x.DtEntRolloProd_Cantidad,
                    x.UndMed_Rollo,
                    x.Prod_CantBolsasPaquete,
                    x.Prod_CantBolsasRestates,
                    x.Prod_CantPaquetesRestantes
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("CrearPdf/{ot}")]
        public ActionResult GetCrearPdf(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from emp in _context.Set<Empresa>()
                      where rollo.DtEntRolloProd_OT == ot
                      select new
                      {
                          rollo.EntRollo_Producto.EntRolloProd_Id,
                          rollo.Prod_Id,
                          rollo.Prod.Prod_Nombre,
                          rollo.Rollo_Id,
                          rollo.UndMed_Rollo,
                          rollo.DtEntRolloProd_Cantidad,
                          rollo.EntRollo_Producto.EntRolloProd_Fecha,
                          Creador = rollo.EntRollo_Producto.Usua_Id,
                          NombreCreador = rollo.EntRollo_Producto.Usua.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("CrearPDFUltimoID/{id}")]
        public ActionResult GetCrearPDF(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from emp in _context.Set<Empresa>()
                      where rollo.EntRollo_Producto.EntRolloProd_Id == id
                      orderby rollo.EntRollo_Producto.EntRolloProd_Id
                      select new
                      {
                          rollo.DtEntRolloProd_OT,
                          rollo.EntRollo_Producto.EntRolloProd_Id,
                          rollo.Prod_Id,
                          rollo.Prod.Prod_Nombre,
                          rollo.Rollo_Id,
                          rollo.UndMed_Rollo,
                          rollo.DtEntRolloProd_Cantidad,
                          rollo.EntRollo_Producto.EntRolloProd_Fecha,
                          Creador = rollo.EntRollo_Producto.Usua_Id,
                          NombreCreador = rollo.EntRollo_Producto.Usua.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };
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
