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
    public class Devolucion_MatPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Devolucion_MatPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Devolucion_MatPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Devolucion_MatPrima>>> GetDevoluciones_MatPrima()
        {
          if (_context.Devoluciones_MatPrima == null)
          {
              return NotFound();
          }
            return await _context.Devoluciones_MatPrima.ToListAsync();
        }

        // GET: api/Devolucion_MatPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Devolucion_MatPrima>> GetDevolucion_MatPrima(long id)
        {
          if (_context.Devoluciones_MatPrima == null)
          {
              return NotFound();
          }
            var devolucion_MatPrima = await _context.Devoluciones_MatPrima.FindAsync(id);

            if (devolucion_MatPrima == null)
            {
                return NotFound();
            }

            return devolucion_MatPrima;
        }

        [HttpGet("fecha/{DevMatPri_Fecha}")]
        public ActionResult<Devolucion_MatPrima> Getfecha(DateTime DevMatPri_Fecha)
        {
            var devolucion_MatPrima = _context.Devoluciones_MatPrima.Where(dev => dev.DevMatPri_Fecha == DevMatPri_Fecha).ToList(); 

            if (devolucion_MatPrima == null)
            {
                return NotFound();
            }

            return Ok(devolucion_MatPrima);
        }

        [HttpGet("fechas/")]
        public ActionResult<Devolucion_MatPrima> Getfechas(DateTime DevMatPri_Fecha1, DateTime DevMatPri_Fecha2)
        {
            var devolucion_MatPrima = _context.Devoluciones_MatPrima.Where(dev => dev.DevMatPri_Fecha >= DevMatPri_Fecha1 && dev.DevMatPri_Fecha <= DevMatPri_Fecha2).ToList();

            if (devolucion_MatPrima == null)
            {
                return NotFound();
            }

            return Ok(devolucion_MatPrima);
        }

        [HttpGet("OT/{DevMatPri_OrdenTrabajo}")]
        public ActionResult<Devolucion_MatPrima> GetOT(long DevMatPri_OrdenTrabajo)
        {
            var devolucion_MatPrima = _context.Devoluciones_MatPrima.Where(dev => dev.DevMatPri_OrdenTrabajo == DevMatPri_OrdenTrabajo).ToList();

            if (devolucion_MatPrima == null)
            {
                return NotFound();
            }

            return Ok(devolucion_MatPrima);
        }

        // PUT: api/Devolucion_MatPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevolucion_MatPrima(long id, Devolucion_MatPrima devolucion_MatPrima)
        {
            if (id != devolucion_MatPrima.DevMatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(devolucion_MatPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Devolucion_MatPrimaExists(id))
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

        // POST: api/Devolucion_MatPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Devolucion_MatPrima>> PostDevolucion_MatPrima(Devolucion_MatPrima devolucion_MatPrima)
        {
          if (_context.Devoluciones_MatPrima == null)
          {
              return Problem("Entity set 'dataContext.Devoluciones_MatPrima'  is null.");
          }
            _context.Devoluciones_MatPrima.Add(devolucion_MatPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevolucion_MatPrima", new { id = devolucion_MatPrima.DevMatPri_Id }, devolucion_MatPrima);
        }

        // DELETE: api/Devolucion_MatPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevolucion_MatPrima(long id)
        {
            if (_context.Devoluciones_MatPrima == null)
            {
                return NotFound();
            }
            var devolucion_MatPrima = await _context.Devoluciones_MatPrima.FindAsync(id);
            if (devolucion_MatPrima == null)
            {
                return NotFound();
            }

            _context.Devoluciones_MatPrima.Remove(devolucion_MatPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Devolucion_MatPrimaExists(long id)
        {
            return (_context.Devoluciones_MatPrima?.Any(e => e.DevMatPri_Id == id)).GetValueOrDefault();
        }
    }
}
