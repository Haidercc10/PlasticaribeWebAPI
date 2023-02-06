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
    public class Asignacion_BOPPController : ControllerBase
    {
        private readonly dataContext _context;

        public Asignacion_BOPPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Asignacion_BOPP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion_BOPP>>> GetAsignaciones_BOPP()
        {
          if (_context.Asignaciones_BOPP == null)
          {
              return NotFound();
          }
            return await _context.Asignaciones_BOPP.ToListAsync();
        }

        // GET: api/Asignacion_BOPP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion_BOPP>> GetAsignacion_BOPP(long id)
        {
          if (_context.Asignaciones_BOPP == null)
          {
              return NotFound();
          }
            var asignacion_BOPP = await _context.Asignaciones_BOPP.FindAsync(id);

            if (asignacion_BOPP == null)
            {
                return NotFound();
            }

            return asignacion_BOPP;
        }

        // GET: api/Asignacion_BOPP/5
        [HttpGet("fecha/{AsigBOPP_FechaEntrega}")]
        public ActionResult<Asignacion_BOPP> Getfecha(DateTime AsigBOPP_FechaEntrega)
        {
            if (_context.Asignaciones_BOPP == null)
            {
                return NotFound();
            }
            var asignacion_BOPP = _context.Asignaciones_BOPP.Where(asgBopp => asgBopp.AsigBOPP_FechaEntrega == AsigBOPP_FechaEntrega).ToList();

            if (asignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(asignacion_BOPP);
            }
        }

        [HttpGet("fechas/")]
        public ActionResult<Asignacion_BOPP> Getfechas(DateTime AsigBOPP_FechaEntrega1, DateTime AsigBOPP_FechaEntrega2)
        {
            if (_context.Asignaciones_BOPP == null)
            {
                return NotFound();
            }
            var asignacion_BOPP = _context.Asignaciones_BOPP.Where(asgBopp => asgBopp.AsigBOPP_FechaEntrega >= AsigBOPP_FechaEntrega1 && asgBopp.AsigBOPP_FechaEntrega <= AsigBOPP_FechaEntrega2).ToList();

            if (asignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(asignacion_BOPP);
            }
        }

        //Obtener el ultimo ID de asignacion de BOPP


        [HttpGet("ultimoId/")]
        public ActionResult<Asignacion_BOPP> GetUltimoId()
        {
            if (_context.Asignaciones_BOPP == null)
            {
                return NotFound();
            }
            var asignacion_BOPP = _context.Asignaciones_BOPP.OrderBy(asg => asg.AsigBOPP_Id).Last();

            if (asignacion_BOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(asignacion_BOPP);
            }
        }

        // PUT: api/Asignacion_BOPP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion_BOPP(long id, Asignacion_BOPP asignacion_BOPP)
        {
            if (id != asignacion_BOPP.AsigBOPP_Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacion_BOPP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asignacion_BOPPExists(id))
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

        // POST: api/Asignacion_BOPP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignacion_BOPP>> PostAsignacion_BOPP(Asignacion_BOPP asignacion_BOPP)
        {
          if (_context.Asignaciones_BOPP == null)
          {
              return Problem("Entity set 'dataContext.Asignaciones_BOPP'  is null.");
          }
            _context.Asignaciones_BOPP.Add(asignacion_BOPP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion_BOPP", new { id = asignacion_BOPP.AsigBOPP_Id }, asignacion_BOPP);
        }

        // DELETE: api/Asignacion_BOPP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion_BOPP(long id)
        {
            if (_context.Asignaciones_BOPP == null)
            {
                return NotFound();
            }
            var asignacion_BOPP = await _context.Asignaciones_BOPP.FindAsync(id);
            if (asignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.Asignaciones_BOPP.Remove(asignacion_BOPP);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool Asignacion_BOPPExists(long id)
        {
            return (_context.Asignaciones_BOPP?.Any(e => e.AsigBOPP_Id == id)).GetValueOrDefault();
        }
    }
}
