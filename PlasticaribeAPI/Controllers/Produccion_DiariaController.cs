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
                      select new
                      {
                          Id = pd.Prd_Id,
                          Machine = pd.Prd_Maquina,
                          Process = pd.Proceso_Id,
                          ProcessName = pd.Procesos.Proceso_Nombre, 
                          Date = pd.Prd_Fecha,
                          Weight = pd.Prd_Peso,
                          Percentage = pd.Prd_Porcentaje,
                          Goal = pd.Prd_Meta,
                      };
            return Ok(con);
        }

        //Función para actualizar la meta de produccion por maquina. 
        [HttpPut("putGoalForMachine/{id}/{goal}")]
        public async Task<IActionResult> putGoalForMachine(int id, decimal goal)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            var machine = (from pd in _context.Set<Produccion_Diaria>() where pd.Prd_Id == id select pd).FirstOrDefault();
            
            machine.Prd_Meta = goal;
            machine.Prd_Porcentaje = machine.Prd_Peso == 0 ? 0 : (machine.Prd_Peso * 100) / goal;

            _context.Entry(machine).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(machine);
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
