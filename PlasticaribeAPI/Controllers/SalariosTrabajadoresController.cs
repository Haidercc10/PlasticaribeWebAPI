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
    public class SalariosTrabajadoresController : ControllerBase
    {
        private readonly dataContext _context;

        public SalariosTrabajadoresController(dataContext context)
        {
            _context = context;
        }

        // GET: api/SalariosTrabajadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalariosTrabajadores>>> GetSalariosTrabajadores()
        {
            return await _context.SalariosTrabajadores.ToListAsync();
        }

        // GET: api/SalariosTrabajadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalariosTrabajadores>> GetSalariosTrabajadores(int id)
        {
            var salariosTrabajadores = await _context.SalariosTrabajadores.FindAsync(id);

            if (salariosTrabajadores == null)
            {
                return NotFound();
            }

            return salariosTrabajadores;
        }

        [HttpGet("getSaveMoneyByWorker/{worker}")]
        public ActionResult GetSaveMoneyByWorker(long worker)
        {
            var saveMoney = from sm in _context.Set<SalariosTrabajadores>()
                            join pr in _context.Set<NominaDetallada_Plasticaribe>() on sm.Id_Trabajador equals pr.Id_Trabajador
                            join u in _context.Set<Usuario>() on sm.Id_Trabajador equals u.Usua_Id
                            where sm.Id_Trabajador == worker &&
                                  pr.Ahorro > 0 &&
                                  u.Estado_Id == 1 &&
                                  sm.AhorroTotal > 0
                            select new
                            {
                                Worker = u.Usua_Nombre,
                                SubTotalSaveMoney = sm.AhorroTotal,
                                StartPayroll = pr.PeriodoInicio,
                                EndPayroll = pr.PeriodoFin,
                                TotalSaveInPayroll = pr.Ahorro
                            };

            return saveMoney.Any() ? Ok(saveMoney) : BadRequest();
        }

        // PUT: api/SalariosTrabajadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalariosTrabajadores(int id, SalariosTrabajadores salariosTrabajadores)
        {
            if (id != salariosTrabajadores.Id)
            {
                return BadRequest();
            }

            _context.Entry(salariosTrabajadores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalariosTrabajadoresExists(id))
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

        [HttpPut("putMoneySave")]
        public async Task<IActionResult> PutMoneySave(List<DataMoneySave> datamoneySave)
        {
            int count = 0;

            if (datamoneySave.Count == 0) return BadRequest();

            foreach (var item in datamoneySave)
            {
                var salariosTrabajadores = (from st in _context.Set<SalariosTrabajadores>() where st.Id_Trabajador == item.IdWorker select st).FirstOrDefault();
                salariosTrabajadores.AhorroTotal += item.Value;

                _context.Entry(salariosTrabajadores).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalariosTrabajadoresExists(salariosTrabajadores.Id)) return NotFound();
                    else throw;
                }
            }

            return NoContent();
        }

        // POST: api/SalariosTrabajadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalariosTrabajadores>> PostSalariosTrabajadores(SalariosTrabajadores salariosTrabajadores)
        {
            _context.SalariosTrabajadores.Add(salariosTrabajadores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalariosTrabajadores", new { id = salariosTrabajadores.Id }, salariosTrabajadores);
        }

        // DELETE: api/SalariosTrabajadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalariosTrabajadores(int id)
        {
            var salariosTrabajadores = await _context.SalariosTrabajadores.FindAsync(id);
            if (salariosTrabajadores == null)
            {
                return NotFound();
            }

            _context.SalariosTrabajadores.Remove(salariosTrabajadores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalariosTrabajadoresExists(int id)
        {
            return _context.SalariosTrabajadores.Any(e => e.Id == id);
        }
    }

    public class DataMoneySave
    {
        public int IdWorker { get; set; }
        public decimal Value { get; set; }
    }
}
