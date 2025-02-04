using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Interfaces;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Planillas_DespachoController : ControllerBase, IPlanillas_Despacho
    {
        private readonly dataContext _context;
        private readonly IAsignacionProducto_FacturaVenta _asignacionProducto_FacturaVenta;

        public Planillas_DespachoController(dataContext context, IAsignacionProducto_FacturaVenta asignacionProducto_FacturaVenta)
        {
            _context = context;
            _asignacionProducto_FacturaVenta = asignacionProducto_FacturaVenta;
        }

        // GET: api/Planillas_Despacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planillas_Despacho>>> GetPlanillas_Despacho()
        {
            return await _context.Planillas_Despacho.ToListAsync();
        }

        // GET: api/Planillas_Despacho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Planillas_Despacho>> GetPlanillas_Despacho(int id)
        {
            var Planillas_Despacho = await _context.Planillas_Despacho.FindAsync(id);

            if (Planillas_Despacho == null)
            {
                return NotFound();
            }

            return Planillas_Despacho;
        }

        [HttpPut("putSpreadSheetForId/{id}")]
        public async Task<IActionResult> PutPlanillas_Despacho(int id, SpreadSheet spreadSheet)
        {
            var planilla = (from pl in _context.Set<Planillas_Despacho>() where pl.Pla_Id == id select pl).FirstOrDefault();
            if (planilla != null) 
            {
                planilla.Pla_ValorRecibido = spreadSheet.counting;
                planilla.Estado_Id = spreadSheet.status;
                planilla.Pla_FechaRecepcion = spreadSheet.date;
                planilla.Pla_HoraRecepcion = spreadSheet.hour;
                planilla.Pla_Observacion = spreadSheet.observation;

                _context.Entry(planilla).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Planillas_DespachoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            } else
            {
                return NotFound("No se encontró La planilla " + id);
            }
            return NoContent();
        }

        [HttpPut("putHeaderSpreadSheet/{id}/{old_Id}/{totalValue}/{totalCounting}/{weight}")]
        public async Task<IActionResult> putHeaderSpreadSheet(int id, int old_Id, decimal totalValue, decimal totalCounting, decimal weight, [FromBody] List<long> codes)
        {
            var planilla = (from pl in _context.Set<Planillas_Despacho>() where pl.Pla_Id == id select pl).FirstOrDefault();
            
            if (planilla != null)
            {
                planilla.Pla_ValorTotal += totalValue;
                planilla.Pla_ValorContado += totalCounting;
                planilla.Pla_PesoTotal += weight;

                _context.Entry(planilla).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                    await putHeaderSpreadSheetNegativeValue(id, old_Id, totalValue, totalCounting, weight, codes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Planillas_DespachoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound("No se encontró La planilla " + id);
            }
            return NoContent();
        }

        [HttpPut("putHeaderSpreadSheetNegativeValue/{id}/{old_Id}/{totalValue}/{totalCounting}/{weight}")]
        public async Task<IActionResult> putHeaderSpreadSheetNegativeValue(int id, int old_Id, decimal totalValue, decimal totalCounting, decimal weight, [FromBody] List<long> codes)
        {
            var planilla = (from pl in _context.Set<Planillas_Despacho>() where pl.Pla_Id == old_Id select pl).FirstOrDefault();

            if (planilla != null)
            {
                planilla.Pla_ValorTotal -= totalValue;
                planilla.Pla_ValorContado -= totalCounting;
                planilla.Pla_PesoTotal -= weight;

                _context.Entry(planilla).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                    await _asignacionProducto_FacturaVenta.putMovementsDispatch(id, true, codes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Planillas_DespachoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound("No se encontró La planilla " + id);
            }
            return NoContent();
        }

        // PUT: api/Planillas_Despacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanillas_Despacho(int id, Planillas_Despacho Planillas_Despacho)
        {
            if (id != Planillas_Despacho.Pla_Id)
            {
                return BadRequest();
            }

            _context.Entry(Planillas_Despacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Planillas_DespachoExists(id))
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

        // POST: api/Planillas_Despacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Planillas_Despacho>> PostPlanillas_Despacho(Planillas_Despacho Planillas_Despacho)
        {
            _context.Planillas_Despacho.Add(Planillas_Despacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlanillas_Despacho", new { id = Planillas_Despacho.Pla_Id }, Planillas_Despacho);
        }

        // DELETE: api/Planillas_Despacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanillas_Despacho(int id)
        {
            var Planillas_Despacho = await _context.Planillas_Despacho.FindAsync(id);
            if (Planillas_Despacho == null)
            {
                return NotFound();
            }

            _context.Planillas_Despacho.Remove(Planillas_Despacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Planillas_DespachoExists(int id)
        {
            return _context.Planillas_Despacho.Any(e => e.Pla_Id == id);
        }
    }
}

public class SpreadSheet
{ 
    public DateTime date { get; set; }

    public string hour { get; set; }

    [Precision(18,2)]
    public decimal counting { get; set; }

    public int status { get; set; }

    public string? observation { get; set; }
}
