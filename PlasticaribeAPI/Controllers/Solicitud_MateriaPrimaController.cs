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
    public class Solicitud_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Solicitud_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Solicitud_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud_MateriaPrima>>> GetSolicitud_MateriaPrima()
        {
          if (_context.Solicitud_MateriaPrima == null)
          {
              return NotFound();
          }
            return await _context.Solicitud_MateriaPrima.ToListAsync();
        }

        // GET: api/Solicitud_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud_MateriaPrima>> GetSolicitud_MateriaPrima(long id)
        {
          if (_context.Solicitud_MateriaPrima == null)
          {
              return NotFound();
          }
            var solicitud_MateriaPrima = await _context.Solicitud_MateriaPrima.FindAsync(id);

            if (solicitud_MateriaPrima == null)
            {
                return NotFound();
            }

            return solicitud_MateriaPrima;
        }

        //Consulta que devolverá consecutivo que tendrá la nueva solicitud
        [HttpGet("getNuevoConsecutivo")]
        public ActionResult GetSiguienteConsecutivo()
        {
            var con = (from sol in _context.Set<Solicitud_MateriaPrima>() orderby sol.Solicitud_Id descending select sol.Solicitud_Id + 1).FirstOrDefault();
            if (con == 0) return Ok(1);
            else return Ok(con);
        }

        /** Consultas para obtener solicitudes de mat. prima por fechas y estados */
        [HttpGet("getFechasEstado/{fecha1}/{fecha2}/{estado}")]
        public ActionResult GetSolicitudes_FechasEstados(DateTime fecha1, DateTime fecha2, int estado)
        {
            var query = from sol in _context.Set<Solicitud_MateriaPrima>()
                        where sol.Solicitud_Fecha >= fecha1 &&
                        sol.Solicitud_Fecha <= fecha2 &&
                        sol.Estado_Id == estado
                        select new
                        {
                            Consecutivo = sol.Solicitud_Id,
                            Usuario_Id = sol.Usua_Id,
                            Usuario = sol.Usuario.Usua_Nombre,
                            Fecha = sol.Solicitud_Fecha,
                            Estado_Solicitud_Id = sol.Estado_Id,
                            Estado_Solicitud = sol.Estado.Estado_Nombre
                        };
            return Ok(query);
        }

        /** Consultas para obtener solicitudes de mat. prima por fechas */
        [HttpGet("getFechas/{fecha1}/{fecha2}")]
        public ActionResult GetSolicitudes_Fechas(DateTime fecha1, DateTime fecha2)
        {
            var query = from sol in _context.Set<Solicitud_MateriaPrima>()
                        where sol.Solicitud_Fecha >= fecha1 &&
                        sol.Solicitud_Fecha <= fecha2
                        select new
                        {
                            Consecutivo = sol.Solicitud_Id,
                            Usuario_Id = sol.Usua_Id,
                            Usuario = sol.Usuario.Usua_Nombre,
                            Fecha = sol.Solicitud_Fecha,
                            Estado_Solicitud_Id = sol.Estado_Id,
                            Estado_Solicitud = sol.Estado.Estado_Nombre
                        };
            return Ok(query);
        }

        /** Consultas para obtener solicitudes de mat. prima por estados */
        [HttpGet("getEstados/{estado}")]
        public ActionResult GetSolicitudes_Estado(int estado)
        {
            var query = from sol in _context.Set<Solicitud_MateriaPrima>()
                        where sol.Estado_Id == estado
                        select new
                        {
                            Consecutivo = sol.Solicitud_Id,
                            Usuario_Id = sol.Usua_Id,
                            Usuario = sol.Usuario.Usua_Nombre,
                            Fecha = sol.Solicitud_Fecha,
                            Estado_Solicitud_Id = sol.Estado_Id,
                            Estado_Solicitud = sol.Estado.Estado_Nombre
                        };
            return Ok(query);
        }

        // PUT: api/Solicitud_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitud_MateriaPrima(long id, Solicitud_MateriaPrima solicitud_MateriaPrima)
        {
            if (id != solicitud_MateriaPrima.Solicitud_Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitud_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Solicitud_MateriaPrimaExists(id))
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

        // POST: api/Solicitud_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitud_MateriaPrima>> PostSolicitud_MateriaPrima(Solicitud_MateriaPrima solicitud_MateriaPrima)
        {
          if (_context.Solicitud_MateriaPrima == null)
          {
              return Problem("Entity set 'dataContext.Solicitud_MateriaPrima'  is null.");
          }
            _context.Solicitud_MateriaPrima.Add(solicitud_MateriaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitud_MateriaPrima", new { id = solicitud_MateriaPrima.Solicitud_Id }, solicitud_MateriaPrima);
        }

        // DELETE: api/Solicitud_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud_MateriaPrima(long id)
        {
            if (_context.Solicitud_MateriaPrima == null)
            {
                return NotFound();
            }
            var solicitud_MateriaPrima = await _context.Solicitud_MateriaPrima.FindAsync(id);
            if (solicitud_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.Solicitud_MateriaPrima.Remove(solicitud_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Solicitud_MateriaPrimaExists(long id)
        {
            return (_context.Solicitud_MateriaPrima?.Any(e => e.Solicitud_Id == id)).GetValueOrDefault();
        }
    }
}
