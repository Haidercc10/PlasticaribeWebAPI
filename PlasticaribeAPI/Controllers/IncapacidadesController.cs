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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class IncapacidadesController : ControllerBase
    {
        private readonly dataContext _context;

        public IncapacidadesController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Incapacidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incapacidades>>> GetIncapacidades()
        {
            return await _context.Incapacidades.ToListAsync();
        }

        // GET: api/Incapacidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Incapacidades>> GetIncapacidades(int id)
        {
            var incapacidades = await _context.Incapacidades.FindAsync(id);

            if (incapacidades == null)
            {
                return NotFound();
            }

            return incapacidades;
        }

        [HttpGet("getDisablityByWorker/{worker}/{start}/{end}")]
        public ActionResult GetDisablityByWorker(long worker, DateTime start, DateTime end)
        {
            var disabilities = from d in _context.Set<Incapacidades>()
                               join u in _context.Set<Usuario>() on d.Id_Trabajador equals u.Usua_Id
                               join td in _context.Set<TipoIncapacidad>() on d.Id_TipoIncapacidad equals td.Id
                               join st in _context.Set<SalariosTrabajadores>() on d.Id_Trabajador equals st.Id_Trabajador
                               where ((d.FechaInicio < start && d.FechaFin >= start) ||
                                       d.FechaInicio >= start && d.FechaInicio <= end) &&
                                     d.Id_Trabajador == worker
                               select new
                               {
                                    Worker = u.Usua_Nombre,
                                    BaseSalary = st.SalarioBase,
                                    ValueDay = st.SalarioBase / 30,
                                    IdTypeDisability = d.Id_TipoIncapacidad,
                                    TypeDisanility = td.Nombre,
                                    StartDate = d.FechaInicio,
                                    EndDate = d.FechaFin,
                                    TotalDays = d.CantDias,
                                    TotalToPayThisPayroll = 0,
                                    TotalToPayPreviusPayrolls = 0,
                                    TotalToPayNextPayroll = 0,
                                    TotalToPay = d.TotalPagar,
                               };

            return disabilities.Any() ? Ok(disabilities) : NotFound();
        }

        // PUT: api/Incapacidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncapacidades(int id, Incapacidades incapacidades)
        {
            if (id != incapacidades.Id)
            {
                return BadRequest();
            }

            _context.Entry(incapacidades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncapacidadesExists(id))
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

        // POST: api/Incapacidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Incapacidades>> PostIncapacidades(Incapacidades incapacidades)
        {
            _context.Incapacidades.Add(incapacidades);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncapacidades", new { id = incapacidades.Id }, incapacidades);
        }

        // DELETE: api/Incapacidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncapacidades(int id)
        {
            var incapacidades = await _context.Incapacidades.FindAsync(id);
            if (incapacidades == null)
            {
                return NotFound();
            }

            _context.Incapacidades.Remove(incapacidades);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncapacidadesExists(int id)
        {
            return _context.Incapacidades.Any(e => e.Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
