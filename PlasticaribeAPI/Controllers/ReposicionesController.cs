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
    public class ReposicionesController : ControllerBase
    {

        private readonly dataContext _context;

        public ReposicionesController(dataContext context)
        {
            _context = context;
        }
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reposiciones>>> GetReposiciones()
        {
            return await _context.Reposiciones.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Reposiciones>> GetReposiciones(long id)
        {
            var Reposiciones = await _context.Reposiciones.FindAsync(id);

            if (Reposiciones == null)
            {
                return NotFound();
            }

            return Reposiciones;
        }

        [HttpGet("GetIdReposiciones/{id}")]
        public ActionResult GetIdReposiciones(long id)
        {
            var Reposiciones = (from pre in _context.Set<Reposiciones>()
                                where pre.Estado_Id == 39
                                && pre.Rep_Id == id
                                select pre).FirstOrDefault();

            if (Reposiciones == null) return NotFound();
            return Ok(Reposiciones);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReposiciones(long id, Reposiciones Reposiciones)
        {
            if (id != Reposiciones.Rep_Id)
            {
                return BadRequest();
            }

            _context.Entry(Reposiciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReposicionesExists(id))
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

        [HttpPut("putPreloadDispatch/{id}")]
        public async Task<IActionResult> putPreloadDispatch(long id, List<Preload> Preload)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            int count = 0;
            var preload = (from pre in _context.Set<Reposiciones>() where pre.Rep_Id == id select pre).FirstOrDefault();

            foreach (var load in Preload)
            {
                preload.Estado_Id = load.status;
                preload.Rep_FechaSalida = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                preload.Rep_HoraSalida = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
                preload.Rep_Observacion = load.observation;
                preload.Usua_Salida = load.user;

                _context.Entry(preload).State = EntityState.Modified;
                _context.SaveChanges();

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                count++;
                if (count == Preload.Count) return NoContent();
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //
        [HttpPost]
        public async Task<ActionResult<Reposiciones>> PostReposiciones(Reposiciones Reposiciones)
        {
            _context.Reposiciones.Add(Reposiciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReposiciones", new { id = Reposiciones.Rep_Id }, Reposiciones);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReposiciones(long id)
        {
            var Reposiciones = await _context.Reposiciones.FindAsync(id);
            if (Reposiciones == null)
            {
                return NotFound();
            }

            _context.Reposiciones.Remove(Reposiciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool ReposicionesExists(long id)
        {
            return _context.Reposiciones.Any(e => e.Rep_Id == id);
        }

    }

}

public class Repo
{
    public int user { get; set; }
    public string observation { get; set; }
    public int status { get; set; }
}
