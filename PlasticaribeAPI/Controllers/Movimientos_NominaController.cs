using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Movimientos_NominaController : ControllerBase
    {
        private readonly dataContext _context;

        public Movimientos_NominaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos_Nomina
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimientos_Nomina>>> GetMovimientos_Nomina()
        {
            return await _context.Movimientos_Nomina.ToListAsync();
        }

        // GET: api/Movimientos_Nomina/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimientos_Nomina>> GetMovimientos_Nomina(int id)
        {
            var movimientos_Nomina = await _context.Movimientos_Nomina.FindAsync(id);

            if (movimientos_Nomina == null)
            {
                return NotFound();
            }

            return movimientos_Nomina;
        }

        [HttpGet("getPendintDocumentsByTypeDocuAndWorker/{typeDoc}/{worker}")]
        public ActionResult GetPendintDocumentsByTypeDocAndWorker(string typeDoc, long worker)
        {
            var documents = from m in _context.Set<Movimientos_Nomina>()
                            where m.NombreMovimento == typeDoc &&
                                  m.Trabajador_Id == worker &&
                                  m.ValorFinalDeuda > 0 &&
                                  m.Estado_Id == 11 &&
                                  m.Id == (from m2 in _context.Set<Movimientos_Nomina>()
                                           where m2.NombreMovimento == typeDoc &&
                                                 m2.Trabajador_Id == worker &&
                                                 m2.ValorFinalDeuda > 0 &&
                                                 m2.Estado_Id == 11
                                           orderby m2.Id descending
                                           select m2.Id).FirstOrDefault()
                            select new
                            {
                            };

            return documents.Any() ? Ok(documents) : NotFound();
        }

        // PUT: api/Movimientos_Nomina/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientos_Nomina(int id, Movimientos_Nomina movimientos_Nomina)
        {
            if (id != movimientos_Nomina.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimientos_Nomina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Movimientos_NominaExists(id))
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

        // POST: api/Movimientos_Nomina
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimientos_Nomina>> PostMovimientos_Nomina(Movimientos_Nomina movimientos_Nomina)
        {
            if (ValidatePendintMovement(movimientos_Nomina)) return BadRequest($"No se pueden crear movimientos del {movimientos_Nomina.NombreMovimento} con el identificador {movimientos_Nomina.CodigoMovimento} porque el valor de la deuda es cero (0).");

            movimientos_Nomina = CalculateValues(movimientos_Nomina);
            if (movimientos_Nomina.ValorFinalDeuda <= 0 && movimientos_Nomina.NombreMovimento != "AHORRO")
            {
                await PutChangeStateMov(movimientos_Nomina.CodigoMovimento, movimientos_Nomina.NombreMovimento, movimientos_Nomina.Trabajador_Id);
                movimientos_Nomina.Estado_Id = 13;
            }
            _context.Movimientos_Nomina.Add(movimientos_Nomina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientos_Nomina", new { id = movimientos_Nomina.Id }, movimientos_Nomina);
        }

        private bool ValidatePendintMovement(Movimientos_Nomina movimientos_Nomina)
        {
            var movement = (from m in _context.Set<Movimientos_Nomina>()
                            where m.CodigoMovimento == movimientos_Nomina.CodigoMovimento &&
                                  m.NombreMovimento == movimientos_Nomina.NombreMovimento &&
                                  m.Trabajador_Id == movimientos_Nomina.Trabajador_Id
                            orderby m.Id descending
                            select m).FirstOrDefault();
            if (movement == null) return false;
            if (movement.Estado_Id != 11 && movement.NombreMovimento != "AHORRO") return true;
            return false;
        }

        private Movimientos_Nomina CalculateValues(Movimientos_Nomina movimientos_Nomina)
        {
            var movement = (from m in _context.Set<Movimientos_Nomina>()
                            where m.CodigoMovimento == movimientos_Nomina.CodigoMovimento &&
                                  m.NombreMovimento == movimientos_Nomina.NombreMovimento &&
                                  m.Trabajador_Id == movimientos_Nomina.Trabajador_Id
                            orderby m.Id descending
                            select m).FirstOrDefault();
            movimientos_Nomina.Fecha = DateTime.Now;
            movimientos_Nomina.Hora = DateTime.Now.ToString("HH:mm:ss");
            if (movement == null)
            {
                movimientos_Nomina.ValorPagado = movimientos_Nomina.ValorAbonado;
                if (movimientos_Nomina.NombreMovimento != "AHORRO")
                {
                    movimientos_Nomina.ValorDeuda = movimientos_Nomina.ValorTotal;
                    movimientos_Nomina.ValorFinalDeuda = movimientos_Nomina.ValorTotal - movimientos_Nomina.ValorAbonado;
                }
                else movimientos_Nomina.Estado_Id = 13;
                return movimientos_Nomina;
            }
            movimientos_Nomina.ValorPagado = movement.ValorPagado + movimientos_Nomina.ValorAbonado;
            if (movement.NombreMovimento != "AHORRO")
            {
                movimientos_Nomina.ValorDeuda = movement.ValorFinalDeuda;
                movimientos_Nomina.ValorFinalDeuda = movement.ValorFinalDeuda - movimientos_Nomina.ValorAbonado;
            }
            else movimientos_Nomina.Estado_Id = 13;
            return movimientos_Nomina;
        }

        [HttpPut("putChangeStateMov")]
        public async Task<IActionResult> PutChangeStateMov(int codMovement, string nameNomvement, long worker)
        {
            int count = 0;
            var movements = (from m in _context.Set<Movimientos_Nomina>()
                             where m.CodigoMovimento == codMovement &&
                                   m.NombreMovimento == nameNomvement &&
                                   m.Trabajador_Id == worker &&
                                   m.Estado_Id == 11
                             select m).ToList();

            foreach (var movement in movements)
            {
                movement.Estado_Id = 13;
                _context.Entry(movement).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Movimientos_NominaExists(movement.Id)) return NotFound();
                    else throw;
                }
                count++;
                if (count == movements.Count()) return NoContent();
            }
            return NoContent();
        }

        // DELETE: api/Movimientos_Nomina/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientos_Nomina(int id)
        {
            var movimientos_Nomina = await _context.Movimientos_Nomina.FindAsync(id);
            if (movimientos_Nomina == null)
            {
                return NotFound();
            }

            _context.Movimientos_Nomina.Remove(movimientos_Nomina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Movimientos_NominaExists(int id)
        {
            return _context.Movimientos_Nomina.Any(e => e.Id == id);
        }
    }
}
