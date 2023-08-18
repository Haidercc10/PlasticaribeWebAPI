using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Certificados_CalidadController : ControllerBase
    {
        private readonly dataContext _context;

        public Certificados_CalidadController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Certificados_Calidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificados_Calidad>>> GetCertificados_Calidad()
        {
          if (_context.Certificados_Calidad == null)
          {
              return NotFound();
          }
            return await _context.Certificados_Calidad.ToListAsync();
        }

        // GET: api/Certificados_Calidad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Certificados_Calidad>> GetCertificados_Calidad(long id)
        {
          if (_context.Certificados_Calidad == null)
          {
              return NotFound();
          }
            var certificados_Calidad = await _context.Certificados_Calidad.FindAsync(id);

            if (certificados_Calidad == null)
            {
                return NotFound();
            }

            return certificados_Calidad;
        }


        //Consulta que devolverá los diferentes materiales utilizados en los certificados
        [HttpGet("getMateriales")]
        public ActionResult GetMateriales()
        {
            return Ok((from cc in _context.Set<Certificados_Calidad>() select cc.Material).Distinct());
        }

        // Funcion que retornará la información del ultimo certificado realizado a un Item
        [HttpGet("getUltCertificadoItem/{item}")]
        public ActionResult GetUltCertificadoItem(long item)
        {
            return Ok((from cc in _context.Set<Certificados_Calidad>() where cc.Item == item select cc).FirstOrDefault());
        } 

        // PUT: api/Certificados_Calidad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificados_Calidad(long id, Certificados_Calidad certificados_Calidad)
        {
            if (id != certificados_Calidad.Consecutivo)
            {
                return BadRequest();
            }

            _context.Entry(certificados_Calidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Certificados_CalidadExists(id))
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

        [HttpGet("GetCertificados/{fecha1}/{fecha2}")]
        public ActionResult GetCertificados(DateTime fecha1, DateTime fecha2, string? consec = "", string? ot = "", string? cliente = "", string? referencia = "")
        {
            var certificados_Calidad = from c in _context.Set<Certificados_Calidad>()
                                       where c.Fecha_Registro >= fecha1 &&
                                       c.Fecha_Registro <= fecha2 &&
                                       Convert.ToString(c.Consecutivo).Contains(consec) &&
                                       Convert.ToString(c.Orden_Trabajo).Contains(ot) &&
                                       Convert.ToString(c.Cliente).Contains(cliente) &&
                                       Convert.ToString(c.Referencia).Contains(referencia)
                                       select c;

            if (certificados_Calidad == null) return BadRequest("No se encontraron certificados con los datos consultados!");
            return Ok(certificados_Calidad);
        }

        // POST: api/Certificados_Calidad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Certificados_Calidad>> PostCertificados_Calidad(Certificados_Calidad certificados_Calidad)
        {
          if (_context.Certificados_Calidad == null)
          {
              return Problem("Entity set 'dataContext.Certificados_Calidad'  is null.");
          }
            _context.Certificados_Calidad.Add(certificados_Calidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificados_Calidad", new { id = certificados_Calidad.Consecutivo }, certificados_Calidad);
        }

        // DELETE: api/Certificados_Calidad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificados_Calidad(long id)
        {
            if (_context.Certificados_Calidad == null)
            {
                return NotFound();
            }
            var certificados_Calidad = await _context.Certificados_Calidad.FindAsync(id);
            if (certificados_Calidad == null)
            {
                return NotFound();
            }

            _context.Certificados_Calidad.Remove(certificados_Calidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Certificados_CalidadExists(long id)
        {
            return (_context.Certificados_Calidad?.Any(e => e.Consecutivo == id)).GetValueOrDefault();
        }
    }
}
