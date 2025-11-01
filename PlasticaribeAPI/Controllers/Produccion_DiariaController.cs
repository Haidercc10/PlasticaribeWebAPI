using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Produccion_DiariaController : ControllerBase
    {
        private readonly dataContext _context;

        public Produccion_DiariaController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produccion_Diaria>>> GetProduccion_Diaria()
        {
            return await _context.Produccion_Diaria.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Produccion_Diaria>> GetProduccion_Diaria(long id)
        {
            var Produccion_Diaria = await _context.Produccion_Diaria.FindAsync(id);

            if (Produccion_Diaria == null)
            {
                return NotFound();
            }

            return Produccion_Diaria;
        }

        //Función para obtener la producción diaria de las maquinas.
        [HttpGet("getProductionDay/{date1}/{date2}")]
        public ActionResult getProductionDay(DateTime date1, DateTime date2)
        {
            var con = from pd in _context.Set<Produccion_Diaria>()
                      where pd.Prd_Fecha >= date1 &&
                      pd.Prd_Fecha <= date2
                      group pd by new { pd.Prd_Maquina, pd.Proceso_Id, pd.Procesos.Proceso_Nombre, pd.Prd_Fecha } into g
                      select new
                      {
                          Machine = g.Key.Prd_Maquina,
                          Process = g.Key.Proceso_Id,
                          ProcessName = g.Key.Proceso_Nombre, 
                          Date = g.Key.Prd_Fecha,
                          Weight = g.Sum(x => x.Prd_Peso),
                          Percentage = Math.Round(g.Sum(x => x.Prd_Porcentaje)),
                          Goal = g.Sum(x => x.Prd_Meta) > Convert.ToDecimal(0) ? (g.Sum(x => x.Prd_Meta) / 2) : Convert.ToDecimal(0),
                          WeightNight = g.Where(x => x.Turno_Id == "NOCHE")
                                       .Sum(x => (decimal?)x.Prd_Peso) ?? 0,
                          weightDay = g.Where(x => x.Turno_Id == "DIA")
                                       .Sum(x => (decimal?)x.Prd_Peso) ?? 0
                      };
            return Ok(con);
        }

        //Función para actualizar la meta de produccion por maquina. 
        [HttpPut("putGoalForMachine/{machine}/{process}/{date}/{goal}")]
        public async Task<IActionResult> putGoalForMachine(int machine, string process, DateTime date, decimal goal)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            var production = from pd in _context.Set<Produccion_Diaria>() 
                             where pd.Prd_Maquina == machine 
                             && pd.Prd_Fecha == date.Date 
                             && pd.Proceso_Id == process
                             select pd;

            if (!production.Any())
            {
                return NotFound("No se encontró producción para la máquina y proceso especificados.");
            }
            foreach (var prod in production)
            {
                prod.Prd_Meta = goal;
                prod.Prd_Porcentaje = prod.Prd_Peso == 0 ? 0 : (prod.Prd_Peso * 100) / goal;
                _context.Entry(prod).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

       

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduccion_Diaria(long id, Produccion_Diaria Produccion_Diaria)
        {
            if (id != Produccion_Diaria.Prd_Id)
            {
                return BadRequest();
            }

            _context.Entry(Produccion_Diaria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Produccion_DiariaExists(id))
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

        //
        [HttpPost]
        public async Task<ActionResult<Produccion_Diaria>> PostProduccion_Diaria(Produccion_Diaria Produccion_Diaria)
        {
            _context.Produccion_Diaria.Add(Produccion_Diaria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccion_Diaria", new { id = Produccion_Diaria.Prd_Id }, Produccion_Diaria);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduccion_Diaria(long id)
        {
            var Produccion_Diaria = await _context.Produccion_Diaria.FindAsync(id);
            if (Produccion_Diaria == null)
            {
                return NotFound();
            }

            _context.Produccion_Diaria.Remove(Produccion_Diaria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Produccion_DiariaExists(long id)
        {
            return _context.Produccion_Diaria.Any(e => e.Prd_Id == id);
        }
    }
}
