using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DetallesIngRollos_ExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallesIngRollos_ExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetallesIngRollos_Extrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallesIngRollos_Extrusion>>> GetDetallesIngRollos_Extrusion()
        {
            return await _context.DetallesIngRollos_Extrusion.ToListAsync();
        }

        // GET: api/DetallesIngRollos_Extrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallesIngRollos_Extrusion>> GetDetallesIngRollos_Extrusion(int id)
        {
            var detallesIngRollos_Extrusion = await _context.DetallesIngRollos_Extrusion.FindAsync(id);

            if (detallesIngRollos_Extrusion == null)
            {
                return NotFound();
            }

            return detallesIngRollos_Extrusion;
        }

        //Funcion que servirá para consultar y devolver la informacion de un rollo en especifico
        [HttpGet("consultaRollo/{rollo}")]
        public ActionResult consultaRollo(int rollo)
        {
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      where ing.Rollo_Id == rollo
                      select ing;
            return Ok(con);
        }

        //Funcion que servirá para consultar y devolver la informacion necesaria para crear un pdf
        [HttpGet("getCrearPDFUltimoId/{id}")]
        public ActionResult getCrearPDFUltimoId(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetallesIngRollos_Extrusion>()
                      from emp in _context.Set<Empresa>()
                      where rollo.IngRollo_Id == id
                            && emp.Empresa_Id == 800188732
                      orderby rollo.IngRollo_Id
                      select new
                      {
                          rollo.DtIngRollo_OT,
                          rollo.IngRollo_Id,
                          rollo.Rollo_Id,
                          rollo.Prod_Id,
                          rollo.Producto.Prod_Nombre,
                          rollo.UndMed_Id,
                          rollo.DtIngRollo_Cantidad,
                          rollo.IngresoRollos_Extrusion.IngRollo_Fecha,
                          rollo.IngresoRollos_Extrusion.IngRollo_Hora,
                          Creador = rollo.IngresoRollos_Extrusion.Usua_Id,
                          NombreCreador = rollo.IngresoRollos_Extrusion.Usua.Usua_Nombre,
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

        //Funcion que consultará y devolverá los rollos ingresados el dia de hoy y con estado disponible
        [HttpGet("getRollosDisponibles/{hoy}")]
        public ActionResult getRollosDisponibles(DateTime hoy)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      where ing.IngresoRollos_Extrusion.IngRollo_Fecha == hoy && ing.Estado_Id == 19
                      select new
                      {
                          ing.DtIngRollo_OT,
                          ing.Rollo_Id,
                          ing.Prod_Id,
                          ing.Producto.Prod_Nombre,
                          ing.DtIngRollo_Cantidad,
                          ing.UndMed_Id,
                          ing.IngresoRollos_Extrusion.IngRollo_Fecha,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        /** Función que Consulta todos los rollos disponibles */
        [HttpGet("getTodosRollosDisponibles")]
        public ActionResult getRollosDisponibles2()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      where ing.Estado_Id == 19
                      select new
                      {
                          ing.DtIngRollo_OT,
                          ing.Rollo_Id,
                          ing.Prod_Id,
                          ing.Producto.Prod_Nombre,
                          ing.DtIngRollo_Cantidad,
                          ing.UndMed_Id,
                          ing.IngresoRollos_Extrusion.IngRollo_Fecha,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Funcion que va a consultar y agrupar por OT los rollos disponibles 
        [HttpGet("getTodosRollosDisponiblesAgrupados")]
        public ActionResult getTodosRollosDisponiblesAgrupados()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      where ing.Estado_Id == 19
                      group ing by new
                      {
                          ing.DtIngRollo_OT,
                          ing.Prod_Id,
                          ing.Producto.Prod_Nombre,
                          ing.UndMed_Id
                      } into ing
                      select new
                      {
                          ing.Key.DtIngRollo_OT,
                          ing.Key.Prod_Id,
                          ing.Key.Prod_Nombre,
                          Suma = ing.Sum(x => x.DtIngRollo_Cantidad),
                          ing.Key.UndMed_Id,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // Funcion que consultará y deolverá los rollos ingresados de una OT especifica y que tengan un estado disponieble
        [HttpGet("getRollosDisponiblesOT/{ot}")]
        public ActionResult getRollosDisponiblesOT(long ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      where ing.DtIngRollo_OT == ot && ing.Estado_Id == 19
                      select new
                      {
                          ing.DtIngRollo_OT,
                          ing.Rollo_Id,
                          ing.Prod_Id,
                          ing.Producto.Prod_Nombre,
                          ing.DtIngRollo_Cantidad,
                          ing.UndMed_Id,
                          ing.IngresoRollos_Extrusion.IngRollo_Fecha,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // Funcion que servirá para consultar y devolverá un rollo en especifico con estado disponible
        [HttpGet("getRollosDisponiblesRollo/{rollo}")]
        public ActionResult getRollosDisponiblesRollo(int rollo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      where ing.Rollo_Id == rollo && ing.Estado_Id == 19
                      select new
                      {
                          ing.DtIngRollo_OT,
                          ing.Rollo_Id,
                          ing.Prod_Id,
                          ing.Producto.Prod_Nombre,
                          ing.DtIngRollo_Cantidad,
                          ing.UndMed_Id,
                          ing.IngresoRollos_Extrusion.IngRollo_Fecha,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Funcion que consultará y devolverá los rollos ingresado en un rango de fechas que tengan estado disponible
        [HttpGet("getRollosDisponiblesFechas/{fechaInicial}/{fechaFinal}")]
        public ActionResult getRollosDisponiblesFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                      where ing.IngresoRollos_Extrusion.IngRollo_Fecha >= fechaInicial
                            && ing.IngresoRollos_Extrusion.IngRollo_Fecha <= fechaFinal
                            && ing.Estado_Id == 19
                      select new
                      {
                          ing.DtIngRollo_OT,
                          ing.Rollo_Id,
                          ing.Prod_Id,
                          ing.Producto.Prod_Nombre,
                          ing.DtIngRollo_Cantidad,
                          ing.UndMed_Id,
                          ing.IngresoRollos_Extrusion.IngRollo_Fecha,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        /* Funcion que consultará y devolverá un consolidado de los ingresos y las salidas de rollos realizadas,
         * esto servirá para el reporte de la bodega de extrusion*/
        [HttpGet("getconsultaRollosFechas/{fechaInicial}/{fechaFinal}")]
        public ActionResult getconsultaRollosFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ingreso = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                          where ing.IngresoRollos_Extrusion.IngRollo_Fecha >= fechaInicial
                                && ing.IngresoRollos_Extrusion.IngRollo_Fecha <= fechaFinal
                          group ing by new
                          {
                              ing.IngRollo_Id,
                              ing.IngresoRollos_Extrusion.IngRollo_Fecha,
                              ing.IngresoRollos_Extrusion.Usua.Usua_Nombre,
                          } into ing
                          select new
                          {
                              OT = ing.Key.IngRollo_Id,
                              Fecha = ing.Key.IngRollo_Fecha,
                              Tipo = "Ingreso de Rollos",
                              Usuario = ing.Key.Usua_Nombre,
                          };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ingreso);
        }

        [HttpPost("getRollos")]
        public IActionResult getRollos([FromBody] List<long> rollos)
        {
            return Ok(from e in _context.Set<DetallesIngRollos_Extrusion>() where rollos.Contains(e.Rollo_Id) select new { e.Rollo_Id, e.Proceso_Id });
        }

        /* Funcion que consultará y devolverá un consolidado de los ingresos y las salidas de rollos realizadas,
         * esto servirá para el reporte de la bodega de extrusion*/
        [HttpGet("getconsultaRollosOT/{ot}")]
        public ActionResult getconsultaRollosOT(int ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ingreso = from ing in _context.Set<DetallesIngRollos_Extrusion>()
                          where ing.DtIngRollo_OT == ot
                          group ing by new
                          {
                              ing.IngRollo_Id,
                              ing.IngresoRollos_Extrusion.IngRollo_Fecha,
                              ing.IngresoRollos_Extrusion.Usua.Usua_Nombre,
                          } into ing
                          select new
                          {
                              OT = ing.Key.IngRollo_Id,
                              Fecha = ing.Key.IngRollo_Fecha,
                              Tipo = "Ingreso de Rollos",
                              Usuario = ing.Key.Usua_Nombre,
                          };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ingreso);
        }

        //Funcion que va a consultar la información de la asignacion consultada
        [HttpGet("getCrearPdfEntrada/{id}")]
        public ActionResult getCrearPdfSalida(int id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetallesIngRollos_Extrusion>()
                      where rollo.IngRollo_Id == id
                      group rollo by new
                      {
                          rollo.IngRollo_Id,
                          rollo.DtIngRollo_OT,
                          rollo.Prod_Id,
                          rollo.Producto.Prod_Nombre,
                      } into rollo
                      select new
                      {
                          rollo.Key.IngRollo_Id,
                          rollo.Key.DtIngRollo_OT,
                          rollo.Key.Prod_Id,
                          rollo.Key.Prod_Nombre,
                          CantidadRollos = rollo.Count()
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(con);
        }

        // PUT: api/DetallesIngRollos_Extrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesIngRollos_Extrusion(int id, DetallesIngRollos_Extrusion detallesIngRollos_Extrusion)
        {
            if (id != detallesIngRollos_Extrusion.DtIngRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(detallesIngRollos_Extrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesIngRollos_ExtrusionExists(id))
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

        // POST: api/DetallesIngRollos_Extrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallesIngRollos_Extrusion>> PostDetallesIngRollos_Extrusion(DetallesIngRollos_Extrusion detallesIngRollos_Extrusion)
        {
            _context.DetallesIngRollos_Extrusion.Add(detallesIngRollos_Extrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallesIngRollos_Extrusion", new { id = detallesIngRollos_Extrusion.DtIngRollo_Id }, detallesIngRollos_Extrusion);
        }

        // DELETE: api/DetallesIngRollos_Extrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesIngRollos_Extrusion(int id)
        {
            var detallesIngRollos_Extrusion = await _context.DetallesIngRollos_Extrusion.FindAsync(id);
            if (detallesIngRollos_Extrusion == null)
            {
                return NotFound();
            }

            _context.DetallesIngRollos_Extrusion.Remove(detallesIngRollos_Extrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Eliminar Rollo del ingreso
        [HttpDelete("EliminarRolloIngresados/{rollo}")]
        public ActionResult EliminarRolloIngresados(int rollo)
        {
            var x = (from y in _context.Set<DetallesIngRollos_Extrusion>()
                     where y.Rollo_Id == rollo
                     orderby y.Rollo_Id descending
                     select y).FirstOrDefault();

            if (x == null)
            {
                return NotFound();
            }

            _context.DetallesIngRollos_Extrusion.Remove(x);
            _context.SaveChanges();

            return NoContent();
        }

        private bool DetallesIngRollos_ExtrusionExists(int id)
        {
            return _context.DetallesIngRollos_Extrusion.Any(e => e.DtIngRollo_Id == id);
        }
    }
}
