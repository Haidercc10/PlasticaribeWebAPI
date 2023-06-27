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
    public class Detalles_SolicitudRollosController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_SolicitudRollosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_SolicitudRollos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_SolicitudRollos>>> GetDetalles_SolicitudRollos()
        {
          if (_context.Detalles_SolicitudRollos == null)
          {
              return NotFound();
          }
            return await _context.Detalles_SolicitudRollos.ToListAsync();
        }

        // GET: api/Detalles_SolicitudRollos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_SolicitudRollos>> GetDetalles_SolicitudRollos(long id)
        {
          if (_context.Detalles_SolicitudRollos == null)
          {
              return NotFound();
          }
            var detalles_SolicitudRollos = await _context.Detalles_SolicitudRollos.FindAsync(id);

            if (detalles_SolicitudRollos == null)
            {
                return NotFound();
            }

            return detalles_SolicitudRollos;
        }

        //Consulta que devolverá la información de una solicitud realizada
        [HttpGet("getInformacionSolicitud/{solicitud}")]
        public ActionResult GetInformacionSolicitud(long solicitud)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from sol in _context.Set<Detalles_SolicitudRollos>()
                      from Emp in _context.Set<Empresa>()
                      where sol.SolRollo_Id == solicitud
                      select new
                      {
                          Solicitud = sol.SolRollo_Id,
                          Usuario = sol.SolicitudRollos.Usuario.Usua_Nombre,
                          Fecha_Solicitud = sol.SolicitudRollos.SolRollo_FechaSolicitud,
                          Hora_Solicitud = sol.SolicitudRollos.SolRollo_HoraSolicitud,
                          Bodega_Solicitante = sol.Bodega_Solicitante.Proceso_Nombre,
                          Bodega_Solicitada = sol.Bodega_Solicitada.Proceso_Nombre,
                          Observacion = sol.SolicitudRollos.SolRollo_Observacion,
                          Tipo_Solicitud = sol.SolicitudRollos.Tipo_solicitud.TpSol_Nombre,
                          Estado = sol.SolicitudRollos.Estado.Estado_Nombre,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,

                          Orden_Trabajo = sol.DtSolRollo_OrdenTrabajo,
                          Maquina = sol.DtSolRollo_Maquina,
                          Item = sol.Prod_Id,
                          Referencia = sol.Producto.Prod_Nombre,
                          Rollo = sol.DtSolRollo_Rollo,
                          Cantidad = sol.DtSolRollo_Cantidad,
                          Presentacion = sol.UndMed_Id,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return con.Any() ? Ok(con) : BadRequest($"No hay información de la solicitud {solicitud}");
        }

        //Consulta que devolverá todas las solicitudes de rollos que se han realizado dependiendo de los filtros que le sean pasados
        [HttpGet("getSolicitudesRealizadas/{tpSol}/{fechaInicio}/{fechaFin}")]
        public ActionResult GetSolicitudesRealizadas(int tpSol, DateTime fechaInicio, DateTime fechaFin, string solRollos = "", string bgSolicitada = "", string bgSolicitante = "", string estado = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from sol in _context.Set<Detalles_SolicitudRollos>()
                      where sol.SolicitudRollos.TpSol_Id == tpSol &&
                            sol.SolicitudRollos.SolRollo_FechaSolicitud >= fechaInicio &&
                            sol.SolicitudRollos.SolRollo_FechaSolicitud <= fechaFin &&
                            Convert.ToString(sol.SolRollo_Id).Contains(solRollos) &&
                            Convert.ToString(sol.DtSolRollo_BodegaSolicitada).Contains(bgSolicitada) &&
                            Convert.ToString(sol.DtSolRollo_BodegaSolicitante).Contains(bgSolicitante) &&
                            Convert.ToString(sol.SolicitudRollos.Estado_Id).Contains(estado)
                      group sol by new
                      {
                          sol.SolRollo_Id,
                          sol.SolicitudRollos.Usuario.Usua_Nombre,
                          sol.SolicitudRollos.SolRollo_FechaSolicitud,
                          sol.SolicitudRollos.SolRollo_HoraSolicitud,
                          sol.SolicitudRollos.SolRollo_FechaRespuesta,
                          sol.SolicitudRollos.SolRollo_HoraRespuesta,
                          Bodega_Solicitante = sol.Bodega_Solicitante.Proceso_Nombre,
                          Bodega_Solicitada = sol.Bodega_Solicitada.Proceso_Nombre,
                          sol.SolicitudRollos.SolRollo_Observacion,
                          sol.SolicitudRollos.Tipo_solicitud.TpSol_Nombre,
                          sol.SolicitudRollos.Estado.Estado_Nombre

                      } into sol
                      select new
                      {
                          Solicitud = sol.Key.SolRollo_Id,
                          Usuario = sol.Key.Usua_Nombre,
                          Fecha_Solicitud = sol.Key.SolRollo_FechaSolicitud,
                          Hora_Solicitud = sol.Key.SolRollo_HoraSolicitud,
                          Fecha_Respuesta = sol.Key.SolRollo_FechaRespuesta,
                          Hora_Respuesta = sol.Key.SolRollo_HoraRespuesta,
                          Bodega_Solicitante = sol.Key.Bodega_Solicitante,
                          Bodega_Solicitada = sol.Key.Bodega_Solicitada,
                          Observacion = sol.Key.SolRollo_Observacion,
                          Tipo_Solicitud = sol.Key.TpSol_Nombre,
                          Estado = sol.Key.Estado_Nombre,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return con.Any() ? Ok(con) : BadRequest("No se encontró información");
        }

        //Consulta que devolverá la información de los detalles de una solcitud
        [HttpGet("getDetallesSolicitud/{solRollos}")]
        public ActionResult GetDetallesSolicitud(long solRollos)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from sol in _context.Set<Detalles_SolicitudRollos>()
                      where sol.SolRollo_Id == solRollos
                      select new
                      {
                          Solicitud = sol.SolRollo_Id,
                          Usuario = sol.SolicitudRollos.Usuario.Usua_Nombre,
                          Orden_Trabajo = sol.DtSolRollo_OrdenTrabajo,
                          Maquina = sol.DtSolRollo_Maquina,
                          Item = sol.Prod_Id,
                          Referencia = sol.Producto.Prod_Nombre,
                          Rollo = sol.DtSolRollo_Rollo,
                          Cantidad = sol.DtSolRollo_Cantidad,
                          Presentacion = sol.UndMed_Id,
                          Bodega_Solicitante = sol.Bodega_Solicitante.Proceso_Id,
                          Bodega_Solicitada = sol.Bodega_Solicitada.Proceso_Id,
                          TipoSolicitud = sol.SolicitudRollos.TpSol_Id,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return con.Any() ? Ok(con) : BadRequest("No se encontró información");
        }

        //Consulta que devolverá el numero de solicitudes que hay dependiendo de un estado

        // PUT: api/Detalles_SolicitudRollos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_SolicitudRollos(long id, Detalles_SolicitudRollos detalles_SolicitudRollos)
        {
            if (id != detalles_SolicitudRollos.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(detalles_SolicitudRollos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_SolicitudRollosExists(id))
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

        // POST: api/Detalles_SolicitudRollos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_SolicitudRollos>> PostDetalles_SolicitudRollos(Detalles_SolicitudRollos detalles_SolicitudRollos)
        {
          if (_context.Detalles_SolicitudRollos == null)
          {
              return Problem("Entity set 'dataContext.Detalles_SolicitudRollos'  is null.");
          }
            _context.Detalles_SolicitudRollos.Add(detalles_SolicitudRollos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_SolicitudRollos", new { id = detalles_SolicitudRollos.Codigo }, detalles_SolicitudRollos);
        }

        // DELETE: api/Detalles_SolicitudRollos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_SolicitudRollos(long id)
        {
            if (_context.Detalles_SolicitudRollos == null)
            {
                return NotFound();
            }
            var detalles_SolicitudRollos = await _context.Detalles_SolicitudRollos.FindAsync(id);
            if (detalles_SolicitudRollos == null)
            {
                return NotFound();
            }

            _context.Detalles_SolicitudRollos.Remove(detalles_SolicitudRollos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_SolicitudRollosExists(long id)
        {
            return (_context.Detalles_SolicitudRollos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
