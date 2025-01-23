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
    public class Detalles_PlanillaDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_PlanillaDespachoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Detalles_PlanillaDespacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_PlanillaDespacho>>> GetDetalles_PlanillaDespacho()
        {
            return await _context.Detalles_PlanillaDespacho.ToListAsync();
        }

        // GET: api/Detalles_PlanillaDespacho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_PlanillaDespacho>> GetDetalles_PlanillaDespacho(int id)
        {
            var Detalles_PlanillaDespacho = await _context.Detalles_PlanillaDespacho.FindAsync(id);

            if (Detalles_PlanillaDespacho == null)
            {
                return NotFound();
            }

            return Detalles_PlanillaDespacho;
        }

        // GET: api/Detalles_PlanillaDespacho/5
        [HttpGet("getSpreadSheetforId/{id}")]
        public ActionResult getSpreadSheetforId(int id)
        {
            var SpreadSheet = from pla in _context.Set<Planillas_Despacho>()
                              from det in _context.Set<Detalles_PlanillaDespacho>()
                              where pla.Pla_Id == id
                              && pla.Pla_Id == det.Pla_Id
                              select new
                              {
                                Planilla = pla,
                                UserName = pla.Usuario.Usua_Nombre,
                                Driver = pla.Conductor.Usua_Nombre,
                                Status = pla.Estado.Estado_Nombre, 
                                Details = det,
                                Client = det.Cli.Cli_Nombre
                              };

            if (SpreadSheet == null)
            {
                return NotFound();
            }
            return Ok(SpreadSheet);
        }

        // PUT: api/Detalles_PlanillaDespacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_PlanillaDespacho(int id, Detalles_PlanillaDespacho Detalles_PlanillaDespacho)
        {
            if (id != Detalles_PlanillaDespacho.DtPla_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(Detalles_PlanillaDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_PlanillaDespachoExists(id))
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

        // POST: api/Detalles_PlanillaDespacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detalles_PlanillaDespacho>> PostDetalles_PlanillaDespacho(Detalles_PlanillaDespacho Detalles_PlanillaDespacho)
        {
            _context.Detalles_PlanillaDespacho.Add(Detalles_PlanillaDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_PlanillaDespacho", new { id = Detalles_PlanillaDespacho.DtPla_Codigo }, Detalles_PlanillaDespacho);
        }

        // DELETE: api/Detalles_PlanillaDespacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_PlanillaDespacho(int id)
        {
            var Detalles_PlanillaDespacho = await _context.Detalles_PlanillaDespacho.FindAsync(id);
            if (Detalles_PlanillaDespacho == null)
            {
                return NotFound();
            }

            _context.Detalles_PlanillaDespacho.Remove(Detalles_PlanillaDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Detalles_PlanillaDespachoExists(int id)
        {
            return _context.Detalles_PlanillaDespacho.Any(e => e.DtPla_Codigo == id);
        }
    }
}
