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
    public class DetallePreEntrega_RolloDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallePreEntrega_RolloDespachoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetallePreEntrega_RolloDespacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePreEntrega_RolloDespacho>>> GetDetallesPreEntrega_RollosDespacho()
        {
            return await _context.DetallesPreEntrega_RollosDespacho.ToListAsync();
        }

        // GET: api/DetallePreEntrega_RolloDespacho/5
        [HttpGet("VerificarRollo/{id}")]
        public ActionResult GetVerificarRollo(long id)
        {
            var con = _context.DetallesPreEntrega_RollosDespacho.Where(x => x.Rollo_Id == id).ToList();
            return Ok(con);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePreEntrega_RolloDespacho>> GetDetallePreEntrega_RolloDespacho(long id)
        {
            var detallePreEntrega_RolloDespacho = await _context.DetallesPreEntrega_RollosDespacho.FindAsync(id);

            if (detallePreEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            return detallePreEntrega_RolloDespacho;
        }

        //Funcion que va a buscar la informacion que aparecerá en el PDF
        [HttpGet("crearPdf/{ot}/{proceso}")]
        public ActionResult crearPdf(long ot, string proceso)
        {
            var con = from rollo in _context.Set<DetallePreEntrega_RolloDespacho>()
                      from emp in _context.Set<Empresa>()
                      where rollo.DtlPreEntRollo_OT == ot && rollo.Proceso.Proceso_Nombre == proceso
                      group rollo by new {
                          rollo.PreEntregaRollo.PreEntRollo_Id,
                          rollo.Prod_Id,
                          rollo.Prod.Prod_Nombre,
                          rollo.Rollo_Id,
                          rollo.UndMed_Rollo,
                          rollo.DtlPreEntRollo_Cantidad,
                          rollo.PreEntregaRollo.PreEntRollo_Fecha,
                          rollo.PreEntregaRollo.Usua_Id,
                          rollo.PreEntregaRollo.Usuario.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre,
                      }
                      into rollos
                      select new
                      {
                          rollos.Key.PreEntRollo_Id,
                          rollos.Key.Prod_Id,
                          rollos.Key.Prod_Nombre,
                          rollos.Key.Rollo_Id,
                          rollos.Key.UndMed_Rollo,
                          rollos.Key.DtlPreEntRollo_Cantidad,
                          rollos.Key.PreEntRollo_Fecha,
                          Creador = rollos.Key.Usua_Id,
                          NombreCreador = rollos.Key.Usua_Nombre,
                          cantRollos = rollos.Count(),
                          rollos.Key.Empresa_Id,
                          rollos.Key.Empresa_Ciudad,
                          rollos.Key.Empresa_COdigoPostal,
                          rollos.Key.Empresa_Correo,
                          rollos.Key.Empresa_Direccion,
                          rollos.Key.Empresa_Telefono,
                          rollos.Key.Empresa_Nombre
                      };
            return Ok(con);
        }

        //Funcion que va a buscar la informacion que aparecerá en el PDF
        [HttpGet("crearPdf2/{ot}/{proceso}")]
        public ActionResult crearPdf2(long ot, string proceso)
        {
            var con = _context.DetallesPreEntrega_RollosDespacho.Where(x => x.DtlPreEntRollo_OT == ot && x.Proceso.Proceso_Nombre == proceso)
                .GroupBy(x => new
                {
                    x.Prod_Id,
                    x.Prod.Prod_Nombre,
                    x.UndMed_Producto
                })
                .Select(x => new
                {
                    x.Key.Prod_Id,
                    x.Key.Prod_Nombre,
                    x.Key.UndMed_Producto,
                    suma = x.Sum(x => x.DtlPreEntRollo_Cantidad),
                    cantRollos = x.Count()
                });
            return Ok(con);
        }

        //Funcion que va a buscar la informacion que aparecerá en el PDF
        [HttpGet("cantidadRollosPorOT/{ot}/{proceso}")]
        public ActionResult cantidadRollosPorOT(long ot, string proceso)
        {
            var con = from rollo in _context.Set<DetallePreEntrega_RolloDespacho>()
                      from emp in _context.Set<Empresa>()
                      where rollo.DtlPreEntRollo_OT == ot && rollo.Proceso.Proceso_Nombre == proceso
                      group rollo by new
                      {
                          rollo.Prod_Id,
                          rollo.Prod.Prod_Nombre,
                      }
                      into rollos
                      select new
                      {
                          cantRollos = rollos.Count(),
                      };
            return Ok(con);
        }

        [HttpGet("CrearPDFUltimoID/{id}")]
        public ActionResult Get(long id )
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dt in _context.Set<DetallePreEntrega_RolloDespacho>()
                      from emp in _context.Set<Empresa>()
                      where dt.PreEntregaRollo.PreEntRollo_Id == id
                      orderby dt.PreEntregaRollo.PreEntRollo_Id
                      select new {
                          dt.PreEntRollo_Id,
                          dt.PreEntregaRollo.PreEntRollo_Fecha,
                          dt.PreEntregaRollo.PreEntRollo_Observacion,
                          dt.PreEntregaRollo.Usua_Id,
                          dt.PreEntregaRollo.Usuario.Usua_Nombre,
                          dt.Rollo_Id,
                          dt.Prod_Id,
                          dt.Prod.Prod_Nombre,
                          dt.UndMed_Rollo,
                          dt.DtlPreEntRollo_Cantidad,
                          dt.DtlPreEntRollo_OT,
                          dt.Cli_Id,
                          dt.Cliente.Cli_Nombre,
                          dt.Proceso_Id,
                          dt.Proceso.Proceso_Nombre,
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

        //
        [HttpGet("getRollosPreEntregadosFechas/{fechaInicial}/{fechaFinal}/{proceso}")]
        public ActionResult getRollosPreEntregadosFechas(DateTime fechaInicial, DateTime fechaFinal, string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from pre in _context.Set<DetallePreEntrega_RolloDespacho>()
                      where pre.Proceso_Id == proceso 
                            && pre.PreEntregaRollo.PreEntRollo_Fecha >= fechaInicial
                            && pre.PreEntregaRollo.PreEntRollo_Fecha <= fechaFinal
                      select new
                      {
                          pre.DtlPreEntRollo_OT,
                          pre.Rollo_Id,
                          pre.Prod_Id,
                          pre.Prod.Prod_Nombre,
                          pre.DtlPreEntRollo_Cantidad,
                          pre.UndMed_Rollo,
                          pre.Proceso.Proceso_Nombre,
                          pre.PreEntregaRollo.PreEntRollo_Fecha
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //
        [HttpGet("getRollosPreEntregadosRollo/{rollo}/{proceso}")]
        public ActionResult getRollosPreEntregadosRollo(int rollo, string proceso)
        {
            var con = from pre in _context.Set<DetallePreEntrega_RolloDespacho>()
                      where pre.Rollo_Id == rollo
                            && pre.Proceso_Id == proceso
                      select new
                      {
                          pre.DtlPreEntRollo_OT,
                          pre.Rollo_Id,
                          pre.Prod_Id,
                          pre.Prod.Prod_Nombre,
                          pre.DtlPreEntRollo_Cantidad,
                          pre.UndMed_Rollo,
                          pre.Proceso.Proceso_Nombre,
                          pre.PreEntregaRollo.PreEntRollo_Fecha
                      };
            return Ok(con);
        }

        //
        [HttpGet("getRollosPreEntregadosOT/{ot}/{proceso}")]
        public ActionResult getRollosPreEntregadosOT(long ot, string proceso)
        {
            var con = from pre in _context.Set<DetallePreEntrega_RolloDespacho>()
                      where pre.DtlPreEntRollo_OT == ot
                            && pre.Proceso_Id == proceso
                      select new
                      {
                          pre.DtlPreEntRollo_OT,
                          pre.Rollo_Id,
                          pre.Prod_Id,
                          pre.Prod.Prod_Nombre,
                          pre.DtlPreEntRollo_Cantidad,
                          pre.UndMed_Rollo,
                          pre.Proceso.Proceso_Nombre,
                          pre.PreEntregaRollo.PreEntRollo_Fecha
                      };
            return Ok(con);
        }

        // PUT: api/DetallePreEntrega_RolloDespacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePreEntrega_RolloDespacho(long id, DetallePreEntrega_RolloDespacho detallePreEntrega_RolloDespacho)
        {
            if (id != detallePreEntrega_RolloDespacho.DtlPreEntRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(detallePreEntrega_RolloDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePreEntrega_RolloDespachoExists(id))
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

        // POST: api/DetallePreEntrega_RolloDespacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallePreEntrega_RolloDespacho>> PostDetallePreEntrega_RolloDespacho(DetallePreEntrega_RolloDespacho detallePreEntrega_RolloDespacho)
        {
            _context.DetallesPreEntrega_RollosDespacho.Add(detallePreEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallePreEntrega_RolloDespacho", new { id = detallePreEntrega_RolloDespacho.DtlPreEntRollo_Id }, detallePreEntrega_RolloDespacho);
        }

        // DELETE: api/DetallePreEntrega_RolloDespacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallePreEntrega_RolloDespacho(long id)
        {
            var detallePreEntrega_RolloDespacho = await _context.DetallesPreEntrega_RollosDespacho.FindAsync(id);
            if (detallePreEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            _context.DetallesPreEntrega_RollosDespacho.Remove(detallePreEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallePreEntrega_RolloDespachoExists(long id)
        {
            return _context.DetallesPreEntrega_RollosDespacho.Any(e => e.DtlPreEntRollo_Id == id);
        }
    }
}
