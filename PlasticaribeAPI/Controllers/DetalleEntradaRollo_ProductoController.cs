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

        //Consulta que va a traer la informacion de un Rollo
        [HttpGet("VerificarRollo/{id}")]
        public ActionResult Get(long id)
        {
            var con = _context.DetallesEntradasRollos_Productos.Where(x => x.Rollo_Id == id).ToList();
            return Ok(con);
        }

        //Consulta que va a traer la información de los rollos ingresados en el proceso que sea consultado.
        [HttpGet("GetRollosProceso/{proceso}")]
        public ActionResult GetRollosProceso(string proceso)
        {
            var con = _context.DetallesEntradasRollos_Productos.Where(x => x.Proceso_Id == proceso).Select(x => x.Rollo_Id).ToList();
            return Ok(con);
        }

        //Consulta que va a traer la informacion de los rollos que esten asociados a un producto, el cual será consultado
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

        //Consulta que nos permitirá crear un archivo pdf a base de dala informacion pedida
        [HttpGet("CrearPdf/{ot}")]
        public ActionResult GetCrearPdf(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetalleEntradaRollo_Producto>()
                      from emp in _context.Set<Empresa>()
                      where rollo.EntRolloProd_Id == ot
                            && emp.Empresa_Id == 800188732
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

        [HttpGet("CrearPdf2/{ot}")]
        public ActionResult GetCrearPdf2(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesEntradasRollos_Productos.Where(x => x.EntRolloProd_Id == ot)
                    .GroupBy(x => new
                    {
                        x.Prod_Id,
                        x.Prod.Prod_Nombre,
                        x.UndMed_Prod
                    }).Select(x => new
                    {
                        x.Key.Prod_Id,
                        x.Key.Prod_Nombre,
                        suma = x.Sum(x => x.DtEntRolloProd_Cantidad),
                        x.Key.UndMed_Prod,
                        cantRollos = x.Count()
                    }).ToList();
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
                            && emp.Empresa_Id == 800188732
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

        [HttpGet("ConsultarOTReporteProcesosActual/{ot}")]
        public ActionResult GetConsultaActual(long ot)
        {
            var con1 = from Entrada in _context.Set<DetalleEntradaRollo_Producto>()
                       where Entrada.DtEntRolloProd_OT == ot
                       group Entrada by Entrada.DtEntRolloProd_OT
                       into Entrada
                       select new
                       {
                           Sum = Entrada.Sum(x => x.DtEntRolloProd_Cantidad)
                       };

            return Ok(con1);
        }

        [HttpGet("ConsultarOTReporteProcesosSalidas/{ot}")]
        public ActionResult GetConsultaSalidas(long ot)
        {

            var con2 = from Entrada in _context.Set<DetalleEntradaRollo_Producto>()
                       from Salida in _context.Set<DetallesAsignacionProducto_FacturaVenta>()
                       where Entrada.DtEntRolloProd_OT == ot && Entrada.Rollo_Id == Salida.Rollo_Id
                       group Entrada by Entrada.DtEntRolloProd_OT
                       into Salida
                       select new
                       {
                           SumTotal = Salida.Sum(x => x.DtEntRolloProd_Cantidad),
                           SumPqt = Salida.Sum(x => x.DtEntRolloProd_Cantidad) - Salida.Sum(x => x.Prod_CantPaquetesRestantes)
                       };

            return Ok(con2);
        }

        [HttpPost("getRollos")]
        public IActionResult getRollos([FromBody] List<long> rollos)
        {
            return Ok(from e in _context.Set<DetalleEntradaRollo_Producto>() where rollos.Contains(e.Rollo_Id) select new { e.Rollo_Id, e.Proceso_Id });
        }

        // PUT: api/DetalleEntradaRollo_Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleEntradaRollo_Producto(long id, DetalleEntradaRollo_Producto detalleEntradaRollo_Producto)
        {
            if (id != detalleEntradaRollo_Producto.Codigo)
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

            return CreatedAtAction("GetDetalleEntradaRollo_Producto", new { id = detalleEntradaRollo_Producto.Codigo }, detalleEntradaRollo_Producto);
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

        //Eliminar Rollo de la tabla
        [HttpDelete("EliminarRollo/{rollo}")]
        public ActionResult EliminarRollo(long rollo)
        {
            var x = (from y in _context.Set<DetalleEntradaRollo_Producto>()
                     where y.Rollo_Id == rollo
                     select y).FirstOrDefault();
            
            if (x == null)
            {
                return NoContent();
            }
                _context.DetallesEntradasRollos_Productos.Remove(x);
                _context.SaveChanges();

                return NoContent();
            
        }

        private bool DetalleEntradaRollo_ProductoExists(long id)
        {
            return _context.DetallesEntradasRollos_Productos.Any(e => e.Codigo == id);
        }
    }
}
