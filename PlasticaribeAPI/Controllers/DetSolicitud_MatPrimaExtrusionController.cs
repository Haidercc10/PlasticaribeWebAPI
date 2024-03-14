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
    public class DetSolicitud_MatPrimaExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public DetSolicitud_MatPrimaExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetSolicitud_MatPrimaExtrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetSolicitud_MatPrimaExtrusion>>> GetDetSolicitud_MatPrimaExtrusion()
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            return await _context.DetSolicitud_MatPrimaExtrusion.ToListAsync();
        }

        // GET: api/DetSolicitud_MatPrimaExtrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetSolicitud_MatPrimaExtrusion>> GetDetSolicitud_MatPrimaExtrusion(long id)
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            var detSolicitud_MatPrimaExtrusion = await _context.DetSolicitud_MatPrimaExtrusion.FindAsync(id);

            if (detSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }

            return detSolicitud_MatPrimaExtrusion;
        }

        // GET: api/DetSolicitud_MatPrimaExtrusion/5
        [HttpGet("getSolicitudMp_Extrusion/{id}")]
        public ActionResult GetSolicitudMp_Extrusion(long id)
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null) return NotFound();

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var detSolicitud_MatPrimaExtrusion = from smpe in _context.Set<Solicitud_MatPrimaExtrusion>()
                                                 from dsmpe in _context.Set<DetSolicitud_MatPrimaExtrusion>()
                                                 from emp in _context.Set<Empresa>()
                                                 where smpe.SolMpExt_Id == dsmpe.SolMpExt_Id &&
                                                 dsmpe.SolMpExt_Id == id &&
                                                 emp.Empresa_Id == 800188732
                                                 select new
                                                 {
                                                     //Encabezado
                                                     Id = smpe.SolMpExt_Id,
                                                     OT = smpe.SolMpExt_OT,
                                                     Maquina = smpe.SolMpExt_Maquina,
                                                     Proceso = smpe.Proceso_Id,
                                                     Observacion = smpe.SolMpExt_Observacion,
                                                     Estado = smpe.Estado_Id,
                                                     Nombre_Estado = smpe.Estado.Estado_Nombre,
                                                     Usuario = smpe.Usua_Id,
                                                     Nombre_Usuario = smpe.Usua.Usua_Nombre,
                                                     Fecha = smpe.SolMpExt_Fecha,
                                                     Hora = smpe.SolMpExt_Hora,
                                                     //Detalle
                                                     MatPrima_Id = dsmpe.MatPri_Id,
                                                     MatPrima = dsmpe.MatPrima.MatPri_Nombre,
                                                     Stock_Mp = dsmpe.MatPrima.MatPri_Stock,
                                                     Tinta_Id = dsmpe.Tinta_Id,
                                                     Tinta = dsmpe.Tinta.Tinta_Nombre,
                                                     Stock_Tinta = dsmpe.Tinta.Tinta_Stock,
                                                     Cantidad = dsmpe.DtSolMpExt_Cantidad,
                                                     Medida = dsmpe.UndMed_Id,
                                                     //Empresa
                                                     emp.Empresa_Id,
                                                     emp.Empresa_Nombre,
                                                     emp.Empresa_Direccion,
                                                     emp.Empresa_Telefono,
                                                     emp.Empresa_Ciudad,
                                                     emp.Empresa_Correo
                                                 };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (detSolicitud_MatPrimaExtrusion == null) return NotFound();
            else return Ok(detSolicitud_MatPrimaExtrusion);
        }

        /** Obtener solicitudes y materias primas asociadas para actualizar detalles  */
        [HttpGet("getSolicitudesConMatPrimas/{solicitud}/{mp}")]
        public ActionResult GetSolicitudesConMatPrimas(long solicitud, long mp)
        {
            var con = from sol in _context.Set<DetSolicitud_MatPrimaExtrusion>()
                      where sol.SolMpExt_Id == solicitud
                            && (sol.MatPri_Id == mp || sol.Tinta_Id == mp)
                      select sol.Codigo;
            return Ok(con);
        }

        // GET: api/DetSolicitud_MatPrimaExtrusion/5
        [HttpGet("getQuerySolicitudesMp_Extrusion/{fecha1}/{fecha2}")]
        public ActionResult GetQuerySolicitudesMp_Extrusion(DateTime fecha1, DateTime fecha2, string? id = "", string? estado = "")
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null) return NotFound();

#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var detSolicitud_MatPrimaExtrusion = from smpe in _context.Set<Solicitud_MatPrimaExtrusion>()
                                                 from dsmpe in _context.Set<DetSolicitud_MatPrimaExtrusion>()
                                                 from emp in _context.Set<Empresa>()
                                                 where smpe.SolMpExt_Id == dsmpe.SolMpExt_Id &&
                                                 smpe.SolMpExt_Fecha >= fecha1 &&
                                                 smpe.SolMpExt_Fecha <= fecha2 &&
                                                 Convert.ToString(smpe.SolMpExt_Id).Contains(id) &&
                                                 Convert.ToString(smpe.Estado_Id).Contains(estado) &&
                                                 emp.Empresa_Id == 800188732
                                                 select new
                                                 {
                                                     //Encabezado
                                                     Id = smpe.SolMpExt_Id,
                                                     OT = smpe.SolMpExt_OT,
                                                     Maquina = smpe.SolMpExt_Maquina,
                                                     Proceso = smpe.Proceso_Id,
                                                     Observacion = smpe.SolMpExt_Observacion,
                                                     Estado = smpe.Estado_Id,
                                                     Nombre_Estado = smpe.Estado.Estado_Nombre,
                                                     Usuario = smpe.Usua_Id,
                                                     Nombre_Usuario = smpe.Usua.Usua_Nombre,
                                                     Fecha = smpe.SolMpExt_Fecha,
                                                     Hora = smpe.SolMpExt_Hora,
                                                     //Detalle
                                                     MatPrima_Id = dsmpe.MatPri_Id,
                                                     MatPrima = dsmpe.MatPrima.MatPri_Nombre,
                                                     Stock_Mp = dsmpe.MatPrima.MatPri_Stock,
                                                     Tinta_Id = dsmpe.Tinta_Id,
                                                     Tinta = dsmpe.Tinta.Tinta_Nombre,
                                                     Stock_Tinta = dsmpe.Tinta.Tinta_Stock,
                                                     Cantidad = dsmpe.DtSolMpExt_Cantidad,
                                                     Medida = dsmpe.UndMed_Id,
                                                     //Empresa
                                                     emp.Empresa_Id,
                                                     emp.Empresa_Nombre,
                                                     emp.Empresa_Direccion,
                                                     emp.Empresa_Telefono,
                                                     emp.Empresa_Ciudad,
                                                     emp.Empresa_Correo
                                                 };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            if (detSolicitud_MatPrimaExtrusion == null) return NotFound();
            else return Ok(detSolicitud_MatPrimaExtrusion);
        }

        // PUT: api/DetSolicitud_MatPrimaExtrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetSolicitud_MatPrimaExtrusion(long id, DetSolicitud_MatPrimaExtrusion detSolicitud_MatPrimaExtrusion)
        {
            if (id != detSolicitud_MatPrimaExtrusion.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detSolicitud_MatPrimaExtrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetSolicitud_MatPrimaExtrusionExists(id))
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

        // POST: api/DetSolicitud_MatPrimaExtrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetSolicitud_MatPrimaExtrusion>> PostDetSolicitud_MatPrimaExtrusion(DetSolicitud_MatPrimaExtrusion detSolicitud_MatPrimaExtrusion)
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return Problem("Entity set 'dataContext.DetSolicitud_MatPrimaExtrusion'  is null.");
            }
            _context.DetSolicitud_MatPrimaExtrusion.Add(detSolicitud_MatPrimaExtrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetSolicitud_MatPrimaExtrusion", new { id = detSolicitud_MatPrimaExtrusion.Codigo }, detSolicitud_MatPrimaExtrusion);
        }

        // DELETE: api/DetSolicitud_MatPrimaExtrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetSolicitud_MatPrimaExtrusion(long id)
        {
            if (_context.DetSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }
            var detSolicitud_MatPrimaExtrusion = await _context.DetSolicitud_MatPrimaExtrusion.FindAsync(id);
            if (detSolicitud_MatPrimaExtrusion == null)
            {
                return NotFound();
            }

            _context.DetSolicitud_MatPrimaExtrusion.Remove(detSolicitud_MatPrimaExtrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetSolicitud_MatPrimaExtrusionExists(long id)
        {
            return (_context.DetSolicitud_MatPrimaExtrusion?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
