using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class EventosCalendariosController : ControllerBase
    {
        private readonly dataContext _context;

        public EventosCalendariosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/EventosCalendarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventosCalendario>>> GetEventosCalendario()
        {
            if (_context.EventosCalendario == null)
            {
                return NotFound();
            }
            return await _context.EventosCalendario.ToListAsync();
        }

        // GET: api/EventosCalendarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventosCalendario>> GetEventosCalendario(long id)
        {
            if (_context.EventosCalendario == null)
            {
                return NotFound();
            }
            var eventosCalendario = await _context.EventosCalendario.FindAsync(id);

            if (eventosCalendario == null)
            {
                return NotFound();
            }

            return eventosCalendario;
        }

        //Get que devolverá los eventos de un usuario y los eventos para su rol y para todos
        [HttpGet("getEventosUsuario/{id}/{rol}/{fechaInicio}/{fechaFin}")]
        public ActionResult GetEventosUsuario(long id, string rol, DateTime fechaInicio, DateTime fechaFin)
        {
            var con = from ev in _context.Set<EventosCalendario>()
                      where ev.EventoCal_FechaInicial.Month >= (fechaInicio.Month - 3)
                            && ev.EventoCal_FechaFinal.Month <= (fechaFin.Month + 3)
                            && (ev.Usua_Id == id || ev.EventoCal_Visibilidad.Contains("|" + rol + "|"))
                      select ev;
            return con.Count() > 0 ? Ok(con) : NotFound("No se encontraron eventos");
        }

        //Get que devolverá la cantidad de eventos que hay para el mes actual
        [HttpGet("getCantidadEventos/{id}/{rol}/{fechaInicio}/{fechaFin}")]
        public ActionResult GetCantidadEventos(long id, string rol, DateTime fechaInicio, DateTime fechaFin)
        {
            var con = from ev in _context.Set<EventosCalendario>()
                      where ev.EventoCal_FechaInicial >= fechaInicio
                            && ev.EventoCal_FechaFinal <= fechaFin
                            && (ev.Usua_Id == id || ev.EventoCal_Visibilidad.Contains("|" + rol + "|"))
                      select ev;
            return Ok(con.Count());
        }

        //Get que devolverá la cantidad de eventos que hay para el dia actual
        [HttpGet("getEventosDia/{id}/{rol}")]
        public ActionResult GetEventosDia(long id, string rol)
        {
            var con = from ev in _context.Set<EventosCalendario>()
                      where ev.EventoCal_FechaInicial == DateTime.Today
                            && (ev.Usua_Id == id || ev.EventoCal_Visibilidad.Contains("|" + rol + "|"))
                      select ev;
            return Ok(con);
        }


        //Get que devolverá la cantidad de eventos que hay para el mes actual
        [HttpGet("getEventosMes/{id}/{rol}")]
        public ActionResult GetEventosMes(long id, string rol)
        {
            var con = from ev in _context.Set<EventosCalendario>()
                      where ev.EventoCal_FechaInicial.Month == DateTime.Today.Month
                            && ev.EventoCal_FechaInicial >= DateTime.Today
                            && (ev.Usua_Id == id || ev.EventoCal_Visibilidad.Contains("|" + rol + "|"))
                      select ev;
            return Ok(con);
        }

        // PUT: api/EventosCalendarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventosCalendario(long id, EventosCalendario eventosCalendario)
        {
            if (id != eventosCalendario.EventoCal_Id)
            {
                return BadRequest();
            }

            _context.Entry(eventosCalendario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventosCalendarioExists(id))
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

        // POST: api/EventosCalendarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventosCalendario>> PostEventosCalendario(EventosCalendario eventosCalendario)
        {
            if (_context.EventosCalendario == null)
            {
                return Problem("Entity set 'dataContext.EventosCalendario'  is null.");
            }
            _context.EventosCalendario.Add(eventosCalendario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventosCalendario", new { id = eventosCalendario.EventoCal_Id }, eventosCalendario);
        }

        // DELETE: api/EventosCalendarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventosCalendario(long id)
        {
            if (_context.EventosCalendario == null)
            {
                return NotFound();
            }
            var eventosCalendario = await _context.EventosCalendario.FindAsync(id);
            if (eventosCalendario == null)
            {
                return NotFound();
            }

            _context.EventosCalendario.Remove(eventosCalendario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventosCalendarioExists(long id)
        {
            return (_context.EventosCalendario?.Any(e => e.EventoCal_Id == id)).GetValueOrDefault();
        }
    }
}
