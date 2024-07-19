using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Dynamic;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

        //Consulta que validará que un rollo existe en una bodega y devolverá toda su información
        [HttpGet("getRollo/{rollo}/{bodega}")]
        public ActionResult getRollo(long rollo, string bodega)
        {
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.DtBgRollo_Rollo == rollo &&
                            bg.BgRollo_BodegaInicial == bodega
                      select bg;
            return Ok(con);
        }

        //Consulta que validará que un rollo existe en una bodega y devolverá toda su información
        [HttpGet("getIdRollo/{rollo}")]
        public ActionResult GetIdRollo(long rollo)
        {
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.DtBgRollo_Rollo == rollo
                      select bg;
            return con.Any() ? Ok(con) : NotFound();
        }

        //Consulta que obtendrá el rollo solicitado si se encuentra en estado disponible.
        [HttpGet("getRollForOut/{roll}")]
        public ActionResult getRollForOut(long roll)
        {
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.DtBgRollo_Rollo == roll &&
                      bg.Estado_Id == 19 
                      select new
                      {
                          Ot = bg.BgRollo_OrdenTrabajo,
                          Rollo = bg.DtBgRollo_Rollo,
                          Item = bg.Prod_Id,
                          Referencia = bg.Producto.Prod_Nombre,
                          Cantidad = bg.DtBgRollo_Cantidad,
                          Presentacion = bg.UndMed_Id,
                          Ubicacion = bg.DtBgRollo_Ubicacion,
                          Bodega = bg.Bodega_Actual.Proceso_Nombre
                      };

            return Ok(con);
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
                            && bg.Estado_Id == 19
                      select new
                      {
                          Ot = bg.BgRollo_OrdenTrabajo,
                          Rollo = bg.DtBgRollo_Rollo,
                          Item = bg.Prod_Id,
                          Referencia = bg.Producto.Prod_Nombre,
                          Cantidad = bg.DtBgRollo_Cantidad,
                          Presentacion = bg.UndMed_Id,
                          Ubicacion = bg.DtBgRollo_Ubicacion,
                          Bodega = bg.Bodega_Actual.Proceso_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8604 // Posible argumento de referencia nulo
            if (con.Count() > 0) return Ok(con);
            else return BadRequest("No hay rollos disponIbles en la bodega solicitada");
        }

        //Consulta que devolverá la información del inventario de cada una de las bodegas de rollos
        [HttpGet("getInventarioRollos")]
        public ActionResult GetInventarioRollos()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.Estado_Id == 19 &&
                      bg.BgRollo_BodegaActual == Convert.ToString("BGPI")
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
                            bg.BgRollo_BodegaActual == bodega &&
                            bg.Estado_Id == 19
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
                          bg.DtBgRollo_Ubicacion,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta que devolverá todos los rollos disponibles
        [HttpGet("getInventoryAvailable")]
        public ActionResult getInventoryAvailable()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.Estado_Id == 19 && 
                      bg.BgRollo_BodegaActual == Convert.ToString("BGPI")
                      select new
                      {
                          Roll = bg.DtBgRollo_Rollo,
                          OT = bg.BgRollo_OrdenTrabajo,
                          Item = bg.Prod_Id,
                          Reference = bg.Producto.Prod_Nombre,
                          Qty = bg.DtBgRollo_Cantidad,
                          Presentation = bg.UndMed_Id,
                          DateIn = bg.Bodegas_Rollos.BgRollo_FechaEntrada,
                          HourIn = bg.Bodegas_Rollos.BgRollo_HoraEntrada,
                          Ubication = bg.DtBgRollo_Ubicacion,
                          InitialWarehouse = bg.Bodega_Inicial.Proceso_Nombre,
                          ActualWarehouse = bg.Bodega_Actual.Proceso_Nombre,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta para obtener todos los rollos de una OT consultada.
        [HttpGet("getRollsForOT/{order}")]
        public ActionResult getRollsForOT(long order)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from bg in _context.Set<Detalles_BodegasRollos>()
                      where bg.BgRollo_OrdenTrabajo == order
                      select bg.DtBgRollo_Rollo;
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
                          Usuario = bg.Bodegas_Rollos.Usuario.Usua_Nombre,

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
                          Ubicacion = bg.DtBgRollo_Ubicacion,
                          Bodega_Inicial = bg.BgRollo_BodegaInicial
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return con.Any() ? Ok(con) : BadRequest();
        }

        //Consulta para obtener los movimientos de entrada y salida de la bodega.
        [HttpGet("getMovementsStore/{date1}/{date2}")]
        public ActionResult getMovementsStore(DateTime date1, DateTime date2, string? ot = "", string? roll = "", string? item = "", string? typeMov = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var entries = from bg in _context.Set<Detalles_BodegasRollos>()
                      from b in _context.Set<Bodegas_Rollos>()
                      where
                      b.BgRollo_FechaEntrada >= date1 &&
                      b.BgRollo_FechaEntrada <= date2 &&
                      b.BgRollo_Id == bg.BgRollo_Id &&
                      (item != "" ? bg.Prod_Id == Convert.ToInt64(item) : bg.Prod_Id.ToString().Contains(item)) &&
                      (ot != "" ? bg.BgRollo_OrdenTrabajo == Convert.ToInt64(ot) : bg.BgRollo_OrdenTrabajo.ToString().Contains(ot)) &&
                      (roll != "" ? bg.DtBgRollo_Rollo == Convert.ToInt64(roll) : bg.DtBgRollo_Rollo.ToString().Contains(roll)) &&
                      ("ENTRADA".Contains(typeMov) ? true : false)
                      select new
                      {
                          Movement = bg.BgRollo_Id,
                          Date = b.BgRollo_FechaEntrada,
                          Hour = b.BgRollo_HoraEntrada,
                          Observation = b.BgRollo_Observacion,
                          User = b.Usuario.Usua_Nombre,

                          OT = bg.BgRollo_OrdenTrabajo,
                          Item = bg.Prod_Id,
                          Reference = bg.Producto.Prod_Nombre,
                          Roll = bg.DtBgRollo_Rollo,
                          Quantity = bg.DtBgRollo_Cantidad,
                          Presentation = bg.UndMed_Id,
                          typeMov = Convert.ToString("ENTRADA"),
                          Status = bg.Estados.Estado_Nombre
                      };

            var outputs = from s in _context.Set<Solicitud_Rollos_Areas>()
                      from d in _context.Set<Detalles_SolicitudRollos>()
                      where
                      s.SolRollo_FechaSolicitud >= date1 &&
                      s.SolRollo_FechaSolicitud <= date2 &&
                      s.SolRollo_Id == d.SolRollo_Id &&
                      (item != "" ? d.Prod_Id == Convert.ToInt64(item) : d.Prod_Id.ToString().Contains(item)) &&
                      (ot != "" ? d.DtSolRollo_OrdenTrabajo == Convert.ToInt64(ot) : d.DtSolRollo_OrdenTrabajo.ToString().Contains(ot)) &&
                      (roll != "" ? d.DtSolRollo_Rollo == Convert.ToInt64(roll) : d.DtSolRollo_Rollo.ToString().Contains(roll)) &&
                      ("SALIDA".Contains(typeMov) ? true : false)
                      select new
                      {
                          Movement = s.SolRollo_Id,
                          Date = s.SolRollo_FechaSolicitud,
                          Hour = s.SolRollo_HoraSolicitud,
                          Observation = s.SolRollo_Observacion,
                          User = s.Usuario.Usua_Nombre,

                          OT = d.DtSolRollo_OrdenTrabajo,
                          Item = d.Prod_Id,
                          Reference = d.Producto.Prod_Nombre,
                          Roll = d.DtSolRollo_Rollo,
                          Quantity = d.DtSolRollo_Cantidad,
                          Presentation = d.UndMed_Id,
                          typeMov = Convert.ToString("SALIDA"),
                          Status = s.Estado.Estado_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(entries.Concat(outputs));
        }

        //Consulta que actualiza el estado de rollos de la bodega
        [HttpPut("putRollsStore/{status}/{process}")]
        public async Task<IActionResult> putRollsStore(int status, string process, List<rollsStore> rollsStore)
        {
            string[] asociatedProcess = { "EMP", "CORTE" }; 
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foreach (var roll in rollsStore)
            {
                var dataStore = (from dbr in _context.Set<Detalles_BodegasRollos>() where dbr.DtBgRollo_Rollo == roll.Rollo && dbr.BgRollo_OrdenTrabajo == roll.OT select dbr).FirstOrDefault();
                dataStore.Estado_Id = status;
                dataStore.BgRollo_BodegaActual = process;
                if (process == "EXT") dataStore.DtBgRollo_Extrusion = true;
                else if (process == "IMP") dataStore.DtBgRollo_Impresion = true;
                else if (process == "ROT") dataStore.DtBgRollo_Rotograbado = true;
                else if (process == "SELLA") dataStore.DtBgRollo_Sellado = true;
                else if (process == "DESP") dataStore.DtBgRollo_Despacho = true;
                else if (asociatedProcess.Contains(process)) dataStore.DtBgRollo_Corte = true;
                _context.Entry(dataStore).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Consulta que actualiza el estado de rollos de la bodega
        [HttpPut("putUbicationRoll/{ubication}/{observation}")]
        public async Task<IActionResult> putUbicationRoll(string ubication, string observation, [FromBody] List<int> rolls)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            int count = 0;
            foreach (var roll in rolls)
            {
                var dataStore = (from dbr in _context.Set<Detalles_BodegasRollos>() where dbr.DtBgRollo_Rollo == roll select dbr).FirstOrDefault();
                dataStore.DtBgRollo_Ubicacion = ubication;
                dataStore.DtBgRollo_Observacion = observation;
                _context.Entry(dataStore).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                if (count == rolls.Count()) return NoContent();
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

public class rollsStore
{
    public long Rollo { get; set; }
    public int OT { get; set; }
}