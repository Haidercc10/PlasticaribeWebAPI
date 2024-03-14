using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DetallesAsgRollos_ExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallesAsgRollos_ExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetallesAsgRollos_Extrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallesAsgRollos_Extrusion>>> GetDetallesAsgRollos_Extrusion()
        {
            return await _context.DetallesAsgRollos_Extrusion.ToListAsync();
        }

        // GET: api/DetallesAsgRollos_Extrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallesAsgRollos_Extrusion>> GetDetallesAsgRollos_Extrusion(int id)
        {
            var detallesAsgRollos_Extrusion = await _context.DetallesAsgRollos_Extrusion.FindAsync(id);

            if (detallesAsgRollos_Extrusion == null)
            {
                return NotFound();
            }

            return detallesAsgRollos_Extrusion;
        }

        //Funcion que serivrá para consultar y devolver la informacion necesaria para crear un PDF
        [HttpGet("getCrearPDFUltimoId/{id}")]
        public ActionResult getCrearPDFUltimoId(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetallesAsgRollos_Extrusion>()
                      from emp in _context.Set<Empresa>()
                      where rollo.AsgRollos_Id == id
                            && emp.Empresa_Id == 800188732
                      orderby rollo.AsgRollos_Id
                      select new
                      {
                          rollo.DtAsgRollos_OT,
                          rollo.AsgRollos_Id,
                          rollo.Rollo_Id,
                          rollo.Prod_Id,
                          rollo.Producto.Prod_Nombre,
                          rollo.UndMed_Id,
                          rollo.DtAsgRollos_Cantidad,
                          rollo.AsignacionRollos.AsgRollos_Fecha,
                          rollo.AsignacionRollos.AsgRollos_Hora,
                          Creador = rollo.AsignacionRollos.Usua_Id,
                          NombreCreador = rollo.AsignacionRollos.Usuario.Usua_Nombre,
                          rollo.Proceso.Proceso_Nombre,
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

        /* Funcion que consultará y devolverá un consolidado de los ingresos y las salidas de rollos realizadas,
         * esto servirá para el reporte de la bodega de extrusion*/
        [HttpGet("getconsultaRollosFechas/{fechaInicial}/{fechaFinal}")]
        public ActionResult getconsultaRollosFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var salida = from sal in _context.Set<DetallesAsgRollos_Extrusion>()
                         where sal.AsignacionRollos.AsgRollos_Fecha >= fechaInicial
                               && sal.AsignacionRollos.AsgRollos_Fecha <= fechaFinal
                         group sal by new
                         {
                             sal.AsgRollos_Id,
                             sal.AsignacionRollos.AsgRollos_Fecha,
                             sal.AsignacionRollos.Usuario.Usua_Nombre,
                         } into sal
                         select new
                         {
                             OT = sal.Key.AsgRollos_Id,
                             Fecha = sal.Key.AsgRollos_Fecha,
                             Tipo = "Salida de Rollos",
                             Usuario = sal.Key.Usua_Nombre,
                         };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(salida);
        }

        /* Funcion que consultará y devolverá un consolidado de los ingresos y las salidas de rollos realizadas,
         * esto servirá para el reporte de la bodega de extrusion*/
        [HttpGet("getconsultaRollosOT/{ot}")]
        public ActionResult getconsultaRollosOT(int ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var salida = from sal in _context.Set<DetallesAsgRollos_Extrusion>()
                         where sal.DtAsgRollos_OT == ot
                         group sal by new
                         {
                             sal.AsgRollos_Id,
                             sal.AsignacionRollos.AsgRollos_Fecha,
                             sal.AsignacionRollos.Usuario.Usua_Nombre,
                         } into sal
                         select new
                         {
                             OT = sal.Key.AsgRollos_Id,
                             Fecha = sal.Key.AsgRollos_Fecha,
                             Tipo = "Salida de Rollos",
                             Usuario = sal.Key.Usua_Nombre,
                         };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(salida);
        }

        // PUT: api/DetallesAsgRollos_Extrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesAsgRollos_Extrusion(int id, DetallesAsgRollos_Extrusion detallesAsgRollos_Extrusion)
        {
            if (id != detallesAsgRollos_Extrusion.DtAsgRollos_Id)
            {
                return BadRequest();
            }

            _context.Entry(detallesAsgRollos_Extrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesAsgRollos_ExtrusionExists(id))
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

        // POST: api/DetallesAsgRollos_Extrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallesAsgRollos_Extrusion>> PostDetallesAsgRollos_Extrusion(DetallesAsgRollos_Extrusion detallesAsgRollos_Extrusion)
        {
            _context.DetallesAsgRollos_Extrusion.Add(detallesAsgRollos_Extrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallesAsgRollos_Extrusion", new { id = detallesAsgRollos_Extrusion.DtAsgRollos_Id }, detallesAsgRollos_Extrusion);
        }

        // DELETE: api/DetallesAsgRollos_Extrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesAsgRollos_Extrusion(int id)
        {
            var detallesAsgRollos_Extrusion = await _context.DetallesAsgRollos_Extrusion.FindAsync(id);
            if (detallesAsgRollos_Extrusion == null)
            {
                return NotFound();
            }

            _context.DetallesAsgRollos_Extrusion.Remove(detallesAsgRollos_Extrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallesAsgRollos_ExtrusionExists(int id)
        {
            return _context.DetallesAsgRollos_Extrusion.Any(e => e.DtAsgRollos_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
