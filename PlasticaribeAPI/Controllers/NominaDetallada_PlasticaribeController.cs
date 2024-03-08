using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class NominaDetallada_PlasticaribeController : ControllerBase
    {
        private readonly dataContext _context;

        public NominaDetallada_PlasticaribeController(dataContext context)
        {
            _context = context;
        }

        // GET: api/NominaDetallada_Plasticaribe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NominaDetallada_Plasticaribe>>> GetNominaDetallada_Plasticaribe()
        {
            return await _context.NominaDetallada_Plasticaribe.ToListAsync();
        }

        // GET: api/NominaDetallada_Plasticaribe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NominaDetallada_Plasticaribe>> GetNominaDetallada_Plasticaribe(int id)
        {
            var nominaDetallada_Plasticaribe = await _context.NominaDetallada_Plasticaribe.FindAsync(id);

            if (nominaDetallada_Plasticaribe == null)
            {
                return NotFound();
            }

            return nominaDetallada_Plasticaribe;
        }

        //Función que consultará la nomina en un rango de fechas con parametros opcionales 
        [HttpGet("getReportPayroll/{date1}/{date2}")]
        public ActionResult getReportPayroll(DateTime date1, DateTime date2, string? id = "", string? name = "", string? area = "")
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var payRoll = from pr in _context.Set<NominaDetallada_Plasticaribe>()
                          from u in _context.Set<Usuario>()
                          from a in _context.Set<Area>()
                          where pr.Fecha >= date1 &&
                          pr.Fecha <= date2 &&
                          pr.Id_Trabajador == u.Usua_Id &&
                          u.Area_Id == a.Area_Id &&
                          Convert.ToString(pr.Id_Trabajador).Contains(id) &&
                          Convert.ToString(u.Usua_Nombre).Contains(name) &&
                          Convert.ToString(u.Area_Id).Contains(area)
                          select new
                          {
                              PayRoll = pr,
                              User = u,
                              Role = u.RolUsu,
                              Areas = a,
                              Status = pr.Estado,
                              payRollType = pr.TiposNomina,

                              /*IdEmployee = u.Usua_Id,
                              CardEmployee = u.Usua_Cedula,
                              Employee = u.Usua_Nombre,
                              Rol = u.RolUsu_Id,
                              Ocupation = u.RolUsu.RolUsu_Nombre,
                              IdArea = u.Area_Id,
                              Area = a.Area_Nombre,
                              Days_Labor = pr.DiasPagar,
                              Value_Inability = (pr.ValorIncapEG + pr.ValorIncapAT + pr.ValorIncapPATMAT),
                              Value_HoursExtras = pr.ValorTotalADCComp, //(pr.ValorADCDiurnas + pr.ValorNoctDom + pr.ValorExtDiurnasDom + pr.ValorRecargo035 + pr.ValorExtNocturnasDom + pr.ValorRecargo100 + pr.ValorRecargo075 + pr.TarifaADC)
                              Value_AuxTransport = pr.AuxTransporte,
                              Remuneration = pr.Devengado,
                              Eps = pr.EPS,
                              Afp = pr.AFP,
                              Saving = pr.Ahorro,
                              Loan = pr.Prestamo,
                              Advance = pr.Anticipo,
                              TotalPay = pr.Devengado - (pr.EPS + pr.AFP + pr.Ahorro + pr.Prestamo + pr.Anticipo),*/
                          };
#pragma warning restore CS8604 // Possible null reference argument.

            return Ok(payRoll);
        }

        [HttpGet("getDebtAdviceByWorker/{worker}")]
        public ActionResult GetDebtAdviceByWorker(long worker)
        {
            var advice = from ad in _context.Set<NominaDetallada_Plasticaribe>()
                         join u in _context.Set<Usuario>() on ad.Id_Trabajador equals u.Usua_Id
                         where ad.TipoNomina == 4 &&
                               ad.Estado_Nomina == 11
                         select new
                         {
                             Worker = u.Usua_Nombre,
                             StartDate = ad.PeriodoInicio,
                             EndDate = ad.PeriodoFin,
                             ValueAdvance = ad.TotalPagar,
                         };

            return advice.Any() ? Ok(advice) : BadRequest();
        }

        // PUT: api/NominaDetallada_Plasticaribe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNominaDetallada_Plasticaribe(int id, NominaDetallada_Plasticaribe nominaDetallada_Plasticaribe)
        {
            if (id != nominaDetallada_Plasticaribe.Id)
            {
                return BadRequest();
            }

            _context.Entry(nominaDetallada_Plasticaribe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaDetallada_PlasticaribeExists(id))
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

        // POST: api/NominaDetallada_Plasticaribe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NominaDetallada_Plasticaribe>> PostNominaDetallada_Plasticaribe(NominaDetallada_Plasticaribe nominaDetallada_Plasticaribe)
        {
            _context.NominaDetallada_Plasticaribe.Add(nominaDetallada_Plasticaribe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNominaDetallada_Plasticaribe", new { id = nominaDetallada_Plasticaribe.Id }, nominaDetallada_Plasticaribe);
        }

        // DELETE: api/NominaDetallada_Plasticaribe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNominaDetallada_Plasticaribe(int id)
        {
            var nominaDetallada_Plasticaribe = await _context.NominaDetallada_Plasticaribe.FindAsync(id);
            if (nominaDetallada_Plasticaribe == null)
            {
                return NotFound();
            }

            _context.NominaDetallada_Plasticaribe.Remove(nominaDetallada_Plasticaribe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NominaDetallada_PlasticaribeExists(int id)
        {
            return _context.NominaDetallada_Plasticaribe.Any(e => e.Id == id);
        }
    }
}
