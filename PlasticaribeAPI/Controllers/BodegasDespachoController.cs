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
    public class BodegasDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public BodegasDespachoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/BodegasDespacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodegasDespacho>>> GetBodegasDespacho()
        {
            return await _context.BodegasDespacho.ToListAsync();
        }

        // GET: api/BodegasDespacho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BodegasDespacho>> GetBodegasDespacho(int id)
        {
            var bodegasDespacho = await _context.BodegasDespacho.FindAsync(id);

            if (bodegasDespacho == null)
            {
                return NotFound();
            }

            return bodegasDespacho;
        }

        [HttpGet("getBodegas")]
        public ActionResult GetBodegas()
        {
            return Ok (from bg in _context.Set<BodegasDespacho>()
                       group bg by new
                       {
                           bg.IdBodega
                       } into bg
                       select new
                       {
                           Id = bg.Key.IdBodega,
                           Nombre = "Bodega " + bg.Key.IdBodega,
                       });
        }

        [HttpGet("getUbicacionesPorBodegas/{bodega}")]
        public ActionResult GetUbicacionesPorBodegas(string bodega)
        {
            return Ok(from bg in _context.Set<BodegasDespacho>()
                      where bg.IdBodega == bodega
                      group bg by new
                      {
                          bg.IdBodega,
                          bg.IdUbicacion,
                          bg.NombreUbicacion,
                      } into bg
                      select new
                      {
                          bg.Key.IdBodega,
                          bg.Key.IdUbicacion,
                          bg.Key.NombreUbicacion,
                          NombreCompleto = bg.Key.NombreUbicacion + " " + bg.Key.IdUbicacion
                      });
        }

        [HttpGet("getSubUbicacionesPorUbicacion/{bodega}/{ubicacion}/{nombreUbicacion}")]
        public ActionResult GetSubUbicacionesPorUbicaciones(string bodega, string ubicacion, string nombreUbicacion)
        {
            return Ok(from bg in _context.Set<BodegasDespacho>()
                      where bg.IdBodega == bodega &&
                            bg.IdUbicacion == ubicacion &&
                            bg.NombreUbicacion == nombreUbicacion
                      group bg by new
                      {
                          bg.IdBodega,
                          bg.IdUbicacion,
                          bg.NombreUbicacion,
                          bg.IdSubUbicacion,
                          bg.NombreSubUbicacion
                      } into bg
                      select new
                      {
                          bg.Key.IdBodega,
                          bg.Key.IdUbicacion,
                          bg.Key.NombreUbicacion,
                          bg.Key.IdSubUbicacion,
                          bg.Key.NombreSubUbicacion,
                          NombreCompleto = bg.Key.NombreSubUbicacion + " " + bg.Key.IdSubUbicacion
                      });
        }

        [HttpGet("getCubosPorSubUbicacion/{bodega}/{ubicacion}/{nombreUbicacion}/{subUbicacion}")]
        public ActionResult GetCubosPorSubUbicacione(string bodega, string ubicacion, string nombreUbicacion, string subUbicacion)
        {
            return Ok(from bg in _context.Set<BodegasDespacho>()
                      where bg.IdBodega == bodega &&
                            bg.IdUbicacion == ubicacion &&
                            bg.NombreUbicacion == nombreUbicacion && 
                            bg.IdSubUbicacion == subUbicacion &&
                            bg.IdCubo != ""
                      group bg by new
                      {
                          bg.IdBodega,
                          bg.IdUbicacion,
                          bg.NombreUbicacion,
                          bg.IdSubUbicacion,
                          bg.NombreSubUbicacion,
                          bg.IdCubo
                      } into bg
                      select new
                      {
                          bg.Key.IdBodega,
                          bg.Key.IdUbicacion,
                          bg.Key.NombreUbicacion,
                          bg.Key.IdSubUbicacion,
                          bg.Key.NombreSubUbicacion,
                          bg.Key.IdCubo
                      });
        }

        [HttpGet("getCantidadRollosPorUbicacion")]
        public ActionResult GetCantidadRollosPorUbicacion()
        {
            var con = from p in _context.Set<Producto>()
                      join dt in _context.Set<DetalleEntradaRollo_Producto>() on p.Prod_Id equals dt.Prod_Id
                      join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                      join pp in _context.Set<Produccion_Procesos>() on dt.Rollo_Id equals pp.Numero_Rollo
                      where e.EntRolloProd_Observacion != null &&
                            e.EntRolloProd_Observacion != "" &&
                            e.EntRolloProd_Observacion != "Ingreso inicial de inventario de productos por rollos" &&
                            pp.Estado_Rollo == 19 && 
                            pp.Envio_Zeus == true
                      group e by new
                      {
                          e.EntRolloProd_Observacion
                      } into e
                      select new
                      {
                          Ubicacion = e.Key.EntRolloProd_Observacion,
                          CantTotalRollos = e.Count(),
                      };

            return Ok(con);
        }

        [HttpGet("getInventarioPorUbicacionYProductos")]
        public ActionResult GetInventarioPorUbicacionYProductos()
        {
            var con = from p in _context.Set<Producto>()
                      join dt in _context.Set<DetalleEntradaRollo_Producto>() on p.Prod_Id equals dt.Prod_Id
                      join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                      join pp in _context.Set<Produccion_Procesos>() on dt.Rollo_Id equals pp.Numero_Rollo
                      where e.EntRolloProd_Observacion != null &&
                            e.EntRolloProd_Observacion != "" &&
                            e.EntRolloProd_Observacion != "Ingreso inicial de inventario de productos por rollos" &&
                            pp.Estado_Rollo == 19 &&
                            pp.Envio_Zeus == true
                      group new { p, dt, e, pp } by new
                      {
                          p.Prod_Id,
                          p.Prod_Nombre,
                          e.EntRolloProd_Observacion,
                          dt.UndMed_Prod,
                          pp.PrecioVenta_Producto
                      } into data
                      select new
                      {
                          data.Key.Prod_Id,
                          data.Key.Prod_Nombre,
                          Ubicacion = data.Key.EntRolloProd_Observacion,
                          CantTotal = data.Sum(x => x.dt.DtEntRolloProd_Cantidad),
                          Presentacion = data.Key.UndMed_Prod,
                          CantRollos = data.Count(),
                          data.Key.PrecioVenta_Producto
                      };

            return Ok(con);
        }

        [HttpGet("getInventarioPorUbicacionYProducto")]
        public ActionResult GetInventarioPorUbicacionYProducto(string? producto = "", string? numeroRollo = "")
        {
            try
            {
                var con = from p in _context.Set<Producto>()
                          join dt in _context.Set<DetalleEntradaRollo_Producto>() on p.Prod_Id equals dt.Prod_Id
                          join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                          join pp in _context.Set<Produccion_Procesos>() on dt.Rollo_Id equals pp.Numero_Rollo
                          where e.EntRolloProd_Observacion != null &&
                                e.EntRolloProd_Observacion != "" &&
                                e.EntRolloProd_Observacion != "Ingreso inicial de inventario de productos por rollos" &&
                                pp.Estado_Rollo == 19 &&
                                pp.Envio_Zeus == true &&
                                (producto != "" ? p.Prod_Id == Convert.ToInt64(producto) : true) &&
                                (numeroRollo != "" ? pp.NumeroRollo_BagPro == Convert.ToInt64(numeroRollo) : true)
                          group new { p, dt, e, pp } by new
                          {
                              p.Prod_Id,
                              p.Prod_Nombre,
                              e.EntRolloProd_Observacion,
                              dt.UndMed_Prod,
                              pp.PrecioVenta_Producto
                          } into data
                          select new
                          {
                              data.Key.Prod_Id,
                              data.Key.Prod_Nombre,
                              Ubicacion = data.Key.EntRolloProd_Observacion,
                              CantTotal = data.Sum(x => x.dt.DtEntRolloProd_Cantidad),
                              Presentacion = data.Key.UndMed_Prod,
                              CantRollos = data.Count(),
                              data.Key.PrecioVenta_Producto
                          };

                return con.Any() ? Ok(con) : NotFound();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getInventarioPorUbicacion/{ubicacion}")]
        public ActionResult GetInventarioPorUbicacion(string ubicacion)
        {
            try
            {
                var con = from p in _context.Set<Producto>()
                          join dt in _context.Set<DetalleEntradaRollo_Producto>() on p.Prod_Id equals dt.Prod_Id
                          join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                          join pp in _context.Set<Produccion_Procesos>() on dt.Rollo_Id equals pp.Numero_Rollo
                          where e.EntRolloProd_Observacion == ubicacion &&
                                pp.Estado_Rollo == 19 &&
                                pp.Envio_Zeus == true
                          select new
                          {
                              p.Prod_Id,
                              p.Prod_Nombre,
                              Ubicacion = e.EntRolloProd_Observacion,
                              pp.OT,
                              pp.Numero_Rollo,
                              pp.NumeroRollo_BagPro,
                              Peso = pp.Peso_Neto,
                              CantTotal = pp.Presentacion == "Kg" ? pp.Peso_Neto : pp.Cantidad,
                              pp.Presentacion,
                              pp.PrecioVenta_Producto,
                              SubTotal = dt.DtEntRolloProd_Cantidad * pp.PrecioVenta_Producto,
                              pp.Fecha,
                              pp.Hora,
                          };


                return con.Any() ? Ok(con) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/BodegasDespacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodegasDespacho(int id, BodegasDespacho bodegasDespacho)
        {
            if (id != bodegasDespacho.Id)
            {
                return BadRequest();
            }

            _context.Entry(bodegasDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodegasDespachoExists(id))
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

        // POST: api/BodegasDespacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BodegasDespacho>> PostBodegasDespacho(BodegasDespacho bodegasDespacho)
        {
            _context.BodegasDespacho.Add(bodegasDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBodegasDespacho", new { id = bodegasDespacho.Id }, bodegasDespacho);
        }

        // DELETE: api/BodegasDespacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodegasDespacho(int id)
        {
            var bodegasDespacho = await _context.BodegasDespacho.FindAsync(id);
            if (bodegasDespacho == null)
            {
                return NotFound();
            }

            _context.BodegasDespacho.Remove(bodegasDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BodegasDespachoExists(int id)
        {
            return _context.BodegasDespacho.Any(e => e.Id == id);
        }
    }
}
