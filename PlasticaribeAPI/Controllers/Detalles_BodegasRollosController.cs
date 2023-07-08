using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Detalles_BodegasRollosController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_BodegasRollosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_BodegasRollos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_BodegasRollos>>> GetDetalles_BodegasRollos()
        {
          if (_context.Detalles_BodegasRollos == null)
          {
              return NotFound();
          }
            return await _context.Detalles_BodegasRollos.ToListAsync();
        }

        // GET: api/Detalles_BodegasRollos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_BodegasRollos>> GetDetalles_BodegasRollos(long id)
        {
          if (_context.Detalles_BodegasRollos == null)
          {
              return NotFound();
          }
            var detalles_BodegasRollos = await _context.Detalles_BodegasRollos.FindAsync(id);

            if (detalles_BodegasRollos == null)
            {
                return NotFound();
            }

            return detalles_BodegasRollos;
        }

        // Consulta que validará que los rollos que le sean pasado en el array estén en la base de datos, retornará los rollos que estén en la base de datos.
        [HttpPost("getRollos")]
        public IActionResult GetRollos([FromBody] List<long> rollos)
        {
            return Ok(from e in _context.Set<Detalles_BodegasRollos>()
                      where rollos.Contains(e.DtBgRollo_Rollo)
                      select new { e.DtBgRollo_Rollo, e.BgRollo_BodegaActual });
        }

        //Consulta que validará que un rollo existe en una bodega y devolverá toda su información
        [HttpGet("getInfoRollo/{rollo}/{bodega}")]
        public ActionResult GetInfoRollo(long rollo, string bodega)
        {
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.DtBgRollo_Rollo == rollo &&
                            bg.BgRollo_BodegaActual == bodega
                      select bg;
            return con.Any() ? Ok(con) : NotFound();
        }

        //
        [HttpGet("getRollosDisponibles/{bodega}/{ot}")]
        public ActionResult GetRollosDisponibles(string bodega, long ot, string? rollo = "")
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.BgRollo_BodegaActual == bodega
                            && bg.BgRollo_OrdenTrabajo == ot
                            && Convert.ToString(bg.DtBgRollo_Rollo).Contains(rollo)
                      select new
                      {
                          Ot = bg.BgRollo_OrdenTrabajo,
                          Rollo = bg.DtBgRollo_Rollo,
                          Item = bg.Prod_Id,
                          Referencia = bg.Producto.Prod_Nombre,
                          Cantidad = bg.DtBgRollo_Cantidad,
                          Presentacion = bg.UndMed_Id,
                          Bodega = bg.Bodega_Actual.Proceso_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8604 // Posible argumento de referencia nulo
            if (con.Count() > 0) return Ok(con);
            else return BadRequest("No hay rollos disponobles en la bodega solicitada");
        }

        //Consulta que devolverá la información del inventario de cada una de las bodegas de rollos
        [HttpGet("getInventarioRollos")]
        public ActionResult GetInventarioRollos()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      group bg by new
                      {
                          bg.BgRollo_OrdenTrabajo,
                          bg.Prod_Id,
                          bg.Producto.Prod_Nombre,
                          bg.UndMed_Id,
                          bg.BgRollo_BodegaActual,
                          bg.Bodega_Actual.Proceso_Nombre,
                      } into bg
                      select new
                      {
                          bg.Key.BgRollo_OrdenTrabajo,
                          bg.Key.Prod_Id,
                          bg.Key.Prod_Nombre,
                          Cantidad = bg.Sum(x => x.DtBgRollo_Cantidad),
                          bg.Key.UndMed_Id,
                          bg.Key.BgRollo_BodegaActual,
                          bg.Key.Proceso_Nombre,
                          Rollos = bg.Count(),
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta que devolverá los rollos que hay en una bodega, dependiendo de la orden de trabajo
        [HttpGet("getInventarioRollos_OrdenTrabajo/{orden}/{bodega}")]
        public ActionResult GetInventarioRollos_OrdenTrabajo(long orden, string bodega)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.BgRollo_OrdenTrabajo == orden &&
                            bg.BgRollo_BodegaActual == bodega
                      select new
                      {
                          bg.DtBgRollo_Rollo,
                          bg.BgRollo_OrdenTrabajo,
                          bg.Prod_Id,
                          bg.Producto.Prod_Nombre,
                          bg.DtBgRollo_Cantidad,
                          bg.UndMed_Id,
                          bg.BgRollo_BodegaActual,
                          bg.Bodegas_Rollos.BgRollo_FechaEntrada,
                          bg.DtBgRollo_Extrusion,
                          bg.DtBgRollo_ProdIntermedio,
                          bg.DtBgRollo_Impresion,
                          bg.DtBgRollo_Rotograbado,
                          bg.DtBgRollo_Sellado,
                          bg.DtBgRollo_Despacho,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //
        [HttpGet("getInformacionIngreso/{id}")]
        public ActionResult GetInformacionIngreso(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      from Emp in _context.Set<Empresa>()
                      where bg.BgRollo_Id == id
                      select new
                      {
                          Ingreso = bg.BgRollo_Id,
                          Fecha = bg.Bodegas_Rollos.BgRollo_FechaEntrada,
                          Hora = bg.Bodegas_Rollos.BgRollo_HoraEntrada,
                          Observacion = bg.Bodegas_Rollos.BgRollo_Observacion,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,

                          Orden_Trabajo = bg.BgRollo_OrdenTrabajo,
                          Item = bg.Prod_Id,
                          Referencia = bg.Producto.Prod_Nombre,
                          Rollo = bg.DtBgRollo_Rollo,
                          Cantidad = bg.DtBgRollo_Cantidad,
                          Presentacion = bg.UndMed_Id,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return con.Any() ? Ok(con) : BadRequest();
        }

        // PUT: api/Detalles_BodegasRollos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_BodegasRollos(long id, Detalles_BodegasRollos detalles_BodegasRollos)
        {
            if (id != detalles_BodegasRollos.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalles_BodegasRollos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_BodegasRollosExists(id))
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

        // POST: api/Detalles_BodegasRollos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_BodegasRollos>> PostDetalles_BodegasRollos(Detalles_BodegasRollos detalles_BodegasRollos)
        {
          if (_context.Detalles_BodegasRollos == null)
          {
              return Problem("Entity set 'dataContext.Detalles_BodegasRollos'  is null.");
          }
            _context.Detalles_BodegasRollos.Add(detalles_BodegasRollos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_BodegasRollos", new { id = detalles_BodegasRollos.Codigo }, detalles_BodegasRollos);
        }

        // DELETE: api/Detalles_BodegasRollos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_BodegasRollos(long id)
        {
            if (_context.Detalles_BodegasRollos == null)
            {
                return NotFound();
            }
            var detalles_BodegasRollos = await _context.Detalles_BodegasRollos.FindAsync(id);
            if (detalles_BodegasRollos == null)
            {
                return NotFound();
            }

            _context.Detalles_BodegasRollos.Remove(detalles_BodegasRollos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_BodegasRollosExists(long id)
        {
            return (_context.Detalles_BodegasRollos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
