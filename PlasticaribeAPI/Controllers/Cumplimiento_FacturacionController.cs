using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Cumplimiento_FacturacionController : ControllerBase
    {
        private readonly dataContext _context;

        public Cumplimiento_FacturacionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Cumplimiento_Facturacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cumplimiento_Facturacion>>> GetCumplimiento_Facturacion()
        {
            return await _context.Cumplimiento_Facturacion.ToListAsync();
        }

        // GET: api/Cumplimiento_Facturacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cumplimiento_Facturacion>> GetCumplimiento_Facturacion(long id)
        {
            var Cumplimiento_Facturacion = await _context.Cumplimiento_Facturacion.FindAsync(id);

            if (Cumplimiento_Facturacion == null)
            {
                return NotFound();
            }

            return Cumplimiento_Facturacion;
        }

        //Función para consultar el cumplimiento de facturación al día actual
        [HttpGet("ComplianceToday")]
        public async Task<ActionResult<Cumplimiento_Facturacion>> ComplianceToday()
        {

            var today = DateTime.Today;
            var fact = from c in _context.Cumplimiento_Facturacion
                       where c.Cufa_Fecha == today
                       select c;

            return Ok(fact);
        }

        // Función para actualizar la meta de facturación actual
        [HttpPut("actual-goal")]
        public async Task<IActionResult> UpdateActualGoal([FromBody] UpdateGoalDto dto)
        {
            if (dto.Goal < 0)
                return BadRequest("La meta no puede ser negativa.");

            var actualRow = await _context.Cumplimiento_Facturacion
                .OrderByDescending(x => x.Cufa_Id)
                .FirstOrDefaultAsync();

            if (actualRow == null)
                return NotFound("No existe registro de cumplimiento.");

            switch (dto.Type)
            {
                case GoalType.Dia:
                    actualRow.Cufa_MetaDia = dto.Goal;
                    break;

                case GoalType.Mes:
                    actualRow.Cufa_MetaMes = dto.Goal;
                    break;

                case GoalType.Anual:
                    actualRow.Cufa_MetaAnual = dto.Goal;
                    break;

                default:
                    return BadRequest("Tipo de meta inválido.");
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Función para actualizar la facturacion actual
        [HttpPut("currentBilling")]
        public async Task<IActionResult> currentBilling([FromBody] UpdateBillingDto dto)
        {
            if (dto.Billing_Day < 0 || dto.Billing_Month < 0 || dto.Billing_Year < 0)
                return BadRequest("Los valores de facturación deben ser mayores a cero.");

            var actualRow = await _context.Cumplimiento_Facturacion
                .OrderByDescending(x => x.Cufa_Id)
                .FirstOrDefaultAsync();

            if (actualRow == null)
                return NotFound("No existe registro de cumplimiento.");

                actualRow.Cufa_FacturadoDia = dto.Billing_Day;
                actualRow.Cufa_FacturadoMes = dto.Billing_Month;
                actualRow.Cufa_FacturadoAnual = dto.Billing_Year;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Cumplimiento_Facturacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCumplimiento_Facturacion(long id, Cumplimiento_Facturacion Cumplimiento_Facturacion)
        {
            if (id != Cumplimiento_Facturacion.Cufa_Id)
            {
                return BadRequest();
            }

            _context.Entry(Cumplimiento_Facturacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cumplimiento_FacturacionExists(id))
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

        // POST: api/Cumplimiento_Facturacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cumplimiento_Facturacion>> PostCumplimiento_Facturacion(Cumplimiento_Facturacion Cumplimiento_Facturacion)
        {
            _context.Cumplimiento_Facturacion.Add(Cumplimiento_Facturacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCumplimiento_Facturacion", new { id = Cumplimiento_Facturacion.Cufa_Id }, Cumplimiento_Facturacion);
        }

        // DELETE: api/Cumplimiento_Facturacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCumplimiento_Facturacion(long id)
        {
            var Cumplimiento_Facturacion = await _context.Cumplimiento_Facturacion.FindAsync(id);
            if (Cumplimiento_Facturacion == null)
            {
                return NotFound();
            }

            _context.Cumplimiento_Facturacion.Remove(Cumplimiento_Facturacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cumplimiento_FacturacionExists(long id)
        {
            return _context.Cumplimiento_Facturacion.Any(e => e.Cufa_Id == id);
        }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}

public class UpdateGoalDto
{
    public GoalType Type { get; set; }
    public decimal Goal { get; set; }
}

public enum GoalType
{
    Dia,
    Mes,
    Anual
}

public class UpdateBillingDto
{
    [Range(0.00, double.MaxValue)]
    public decimal Billing_Day { get; set; }

    [Range(0.00, double.MaxValue)]
    public decimal Billing_Month { get; set; }

    [Range(0.00, double.MaxValue)]
    public decimal Billing_Year { get; set; }
}
