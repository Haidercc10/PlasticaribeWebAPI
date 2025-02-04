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
    public class Detalles_PlanillaDespachoController : ControllerBase
    {
        private readonly dataContext _context;
        private readonly IPlanillas_Despacho _planillasDespacho;
        public Detalles_PlanillaDespachoController(dataContext context, IPlanillas_Despacho planillasDespacho)
        {
            _context = context;
            _planillasDespacho = planillasDespacho;
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
                              orderby Convert.ToInt32(det.DtPla_Factura) ascending
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

        //Función que actualiza la planilla de una factura que se despachó y por alguna razón volvió  a la empresa y debe ser agregada en otra planilla. 
        [HttpPut("putSpreadSheetForFact/{code}/{newSpreadSheet}")]
        public async Task<IActionResult> putSpreadSheetForFact(int code, int newSpreadSheet, [FromBody] List<long> codes)
        {
            var dispatchs = (from pl in _context.Set<Detalles_PlanillaDespacho>() where pl.DtPla_Codigo == code select pl).FirstOrDefault();
            if (dispatchs != null)
            {
                var valueCounted = dispatchs.DtPla_FormaPago == "CONTADO" ? dispatchs.DtPla_ValorFactura : Convert.ToDecimal(0m);
                var old_Id = dispatchs.Pla_Id;
                var valueFact = dispatchs.DtPla_ValorFactura;
                var weight = dispatchs.DtPla_PesoBruto;

                dispatchs.Pla_Id = newSpreadSheet;
                _context.Entry(dispatchs).State = EntityState.Modified;
                _context.SaveChanges();
               
                try
                {
                    await _context.SaveChangesAsync();
                    await _planillasDespacho.putHeaderSpreadSheet(newSpreadSheet, old_Id, valueFact, valueCounted, weight, codes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            else
            {
                return NotFound("No se encontró el detalle de la planilla " + code);
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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
