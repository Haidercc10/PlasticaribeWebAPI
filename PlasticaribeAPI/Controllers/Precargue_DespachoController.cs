using Intercom.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPut("putPreloadDispatch/{id}/")]
        public async Task<IActionResult> putPreloadDispatch(long id, List<Preload> Preload)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var preload = (from pre in _context.Set<Precargue_Despacho>() where pre.Pcd_Id == id select pre).FirstOrDefault();

            foreach (var load in Preload)
            {
                preload.Estado_Id = load.status;
                preload.Pcd_FechaModifica = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                preload.Pcd_HoraModifica = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
                preload.OF_Id = load.of;
                preload.Pcd_Observacion = load.observation;
                preload.Usua_Crea = load.user;
            
                _context.Entry(preload).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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
