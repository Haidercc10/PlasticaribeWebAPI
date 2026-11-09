using Intercom.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Security.Claims;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Precargue_DespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public Precargue_DespachoController(dataContext context)
        {
            _context = context;
        }
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Precargue_Despacho>>> GetPrecargue_Despacho()
        {
            return await _context.Precargue_Despacho.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Precargue_Despacho>> GetPrecargue_Despacho(long id)
        {
            var Precargue_Despacho = await _context.Precargue_Despacho.FindAsync(id);

            if (Precargue_Despacho == null)
            {
                return NotFound();
            }

            return Precargue_Despacho;
        }

        [HttpGet("GetIdPrecargue_Despacho/{id}")]
        public ActionResult GetIdPrecargue_Despacho(long id)
        {   
            var Precargue_Despacho = (from pre in _context.Set<Precargue_Despacho>()
                                      where pre.Estado_Id == 26
                                      && pre.Pcd_Id == id
                                      select pre).FirstOrDefault();

            if (Precargue_Despacho == null) return NotFound();
            return Ok(Precargue_Despacho);
        }

        [HttpGet("getLastPreload")]
        public async Task<ActionResult<Precargue_Despacho>> GetLastPreload(long id)
        {
            var lastReposition = _context.Precargue_Despacho.Select(p => p.Pcd_Id).DefaultIfEmpty().Max() + 1;
            return Ok(lastReposition);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrecargue_Despacho(long id, Precargue_Despacho Precargue_Despacho)
        {
            if (id != Precargue_Despacho.Pcd_Id)
            {
                return BadRequest();
            }

            _context.Entry(Precargue_Despacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Precargue_DespachoExists(id))
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

        /// <summary>
        /// Actualiza estado, OF asociada, observación, usuario y fecha/hora de modificación de un precargue.
        /// Cada elemento del cuerpo se aplica y guarda en orden (en la práctica el front envía uno solo).
        /// </summary>
        /// <param name="id">Número del precargue.</param>
        /// <param name="Preload">Datos a aplicar (OF, usuario, observación y estado).</param>
        /// <returns>204 si se actualiza; 404 si el precargue no existe.</returns>
        [HttpPut("putPreloadDispatch/{id}")]
        public async Task<IActionResult> putPreloadDispatch(long id, List<Preload> Preload)
        {
            var preload = await _context.Set<Precargue_Despacho>().FirstOrDefaultAsync(pre => pre.Pcd_Id == id);
            if (preload == null) return NotFound();

            foreach (var load in Preload)
            {
                preload.Estado_Id = load.status;
                preload.Pcd_FechaModifica = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                preload.Pcd_HoraModifica = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
                preload.OF_Id = load.of;
                preload.Pcd_Observacion = load.observation;
                preload.Usua_Modifica = load.user;

                _context.Entry(preload).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        /// <summary>
        /// Anula un precargue de despacho pendiente (11) o en proceso (2) y libera sus rollos,
        /// devolviéndolos del estado 50 (en precargue) al estado 19 (disponible). Solo el rol 1 puede anular.
        /// Todos los cambios se guardan en una única transacción.
        /// </summary>
        /// <param name="id">Número del precargue a anular.</param>
        /// <returns>200 con la cantidad de rollos liberados; 400 si el estado no permite anular;
        /// 401 si el token no trae el usuario; 403 si el rol no es 1; 404 si el precargue no existe.</returns>
        [HttpPut("anular/{id}")]
        public async Task<IActionResult> AnularPrecargue(long id)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();
            if (User.FindFirstValue(ClaimTypes.Role) != "1")
                return Forbid();

            var precargue = await _context.Precargue_Despacho.FirstOrDefaultAsync(p => p.Pcd_Id == id);
            if (precargue == null) return NotFound("El precargue no fue encontrado.");
            if (precargue.Estado_Id != 11 && precargue.Estado_Id != 2)
                return BadRequest("Solo se pueden anular precargues pendientes o en proceso.");

            // Una sola consulta: rollos del precargue que siguen en estado 50 (mismo producto y número de rollo).
            var rollos = (await (from d in _context.Detalles_PrecargueDespacho
                                 join pp in _context.Produccion_Procesos
                                     on new { Rollo = d.DtlPcd_Rollo, d.Prod_Id } equals new { Rollo = pp.NumeroRollo_BagPro, pp.Prod_Id }
                                 where d.Pcd_Id == id && pp.Estado_Rollo == 50
                                 select pp).ToListAsync())
                         .Distinct()
                         .ToList();

            foreach (var rollo in rollos) rollo.Estado_Rollo = 19;

            precargue.Estado_Id = 3;
            precargue.Usua_Modifica = usuarioId;
            precargue.Pcd_FechaModifica = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            precargue.Pcd_HoraModifica = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));

            await _context.SaveChangesAsync();
            return Ok(new { rollosLiberados = rollos.Count });
        }

        /// <summary>
        /// Actualiza todos los precargues asociados a una orden de facturación con el estado, observación,
        /// usuario y OF del primer elemento del cuerpo.
        /// </summary>
        /// <param name="order">Número de la orden de facturación asociada a los precargues.</param>
        /// <param name="Preload">Datos a aplicar; solo se usa el primer elemento.</param>
        /// <returns>204 siempre que no ocurra un error (también si no hay precargues asociados).</returns>
        [HttpPut("putPreloadForOrderFact/{order}")]
        public async Task<IActionResult> putPreloadForOrderFact(long order, List<Preload> Preload)
        {
            var preload = await _context.Set<Precargue_Despacho>().Where(pre => pre.OF_Id == order).ToListAsync();

            foreach (var load in preload)
            {
                load.Estado_Id = Preload[0].status;
                load.Pcd_FechaModifica = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                load.Pcd_HoraModifica = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
                load.Pcd_Observacion = Preload[0].observation;
                load.Usua_Modifica = Preload[0].user;
                load.OF_Id = Preload[0].of;

                _context.Entry(load).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        //
        [HttpPost]
        public async Task<ActionResult<Precargue_Despacho>> PostPrecargue_Despacho(Precargue_Despacho Precargue_Despacho)
        {
            _context.Precargue_Despacho.Add(Precargue_Despacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrecargue_Despacho", new { id = Precargue_Despacho.Pcd_Id }, Precargue_Despacho);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrecargue_Despacho(long id)
        {
            var Precargue_Despacho = await _context.Precargue_Despacho.FindAsync(id);
            if (Precargue_Despacho == null)
            {
                return NotFound();
            }

            _context.Precargue_Despacho.Remove(Precargue_Despacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Precargue_DespachoExists(long id)
        {
            return _context.Precargue_Despacho.Any(e => e.Pcd_Id == id);
        }

    }
}

public class Preload
{
    public int of { get; set; }
    public int user { get; set; }
    public string observation { get; set; }
    public int status { get; set; }
}
