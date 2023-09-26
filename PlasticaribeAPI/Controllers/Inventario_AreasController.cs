using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inventario_AreasController : ControllerBase
    {
        private readonly dataContext _context;

        public Inventario_AreasController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Inventario_Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario_Areas>>> GetInventarios_Areas()
        {
            if (_context.Inventarios_Areas == null)
            {
                return NotFound();
            }
            return await _context.Inventarios_Areas.ToListAsync();
        }

        // GET: api/Inventario_Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario_Areas>> GetInventario_Areas(long id)
        {
            if (_context.Inventarios_Areas == null)
            {
                return NotFound();
            }
            var inventario_Areas = await _context.Inventarios_Areas.FindAsync(id);

            if (inventario_Areas == null)
            {
                return NotFound();
            }

            return inventario_Areas;
        }

        // GET: Busqueda de inventarios de areas por fechas
        [HttpGet("getInvAreas_Fechas/{fecha1}/{fecha2}")]
        public ActionResult GetInvAreas_Fechas(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var inventario_Areas = from ia in _context.Set<Inventario_Areas>()
                                   where ia.InvFecha_Inventario >= fecha1 &&
                                   ia.InvFecha_Inventario <= fecha2
                                   select new
                                   {
                                       Fecha_Inventario = ia.InvFecha_Inventario,
                                       OT = Convert.ToString(ia.OT) != "0" ? Convert.ToString(ia.OT) : "",
                                       Item = ia.Prod_Id == 1 && ia.MatPri_Id != 84 ? ia.MatPri_Id : ia.Prod_Id,
                                       Referencia = ia.Prod_Id == 1 && ia.MatPri_Id != 84 ? ia.MatPrima.MatPri_Nombre : ia.Item.Prod_Nombre,
                                       Stock = ia.InvStock,
                                       Precio = ia.InvPrecio,
                                       Subtotal = ia.InvStock * ia.InvPrecio,
                                       Id_Area = ia.Proceso_Id,
                                       Nombre_Area = ia.Proceso.Proceso_Nombre,
                                       EsMaterial = ia.Prod_Id == 1 && ia.MatPri_Id != 84 ? true : false,
                                   };

            if (inventario_Areas == null) return BadRequest("No se encontraron registros de inventarios en las fechas consultadas");
            else return Ok(inventario_Areas);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // PUT: api/Inventario_Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario_Areas(long id, Inventario_Areas inventario_Areas)
        {
            if (id != inventario_Areas.InvCodigo)
            {
                return BadRequest();
            }

            _context.Entry(inventario_Areas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Inventario_AreasExists(id))
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

        // POST: api/Inventario_Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventario_Areas>> PostInventario_Areas(Inventario_Areas inventario_Areas)
        {
            if (_context.Inventarios_Areas == null)
            {
                return Problem("Entity set 'dataContext.Inventarios_Areas'  is null.");
            }
            _context.Inventarios_Areas.Add(inventario_Areas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario_Areas", new { id = inventario_Areas.InvCodigo }, inventario_Areas);
        }

        // DELETE: api/Inventario_Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario_Areas(long id)
        {
            if (_context.Inventarios_Areas == null)
            {
                return NotFound();
            }
            var inventario_Areas = await _context.Inventarios_Areas.FindAsync(id);
            if (inventario_Areas == null)
            {
                return NotFound();
            }

            _context.Inventarios_Areas.Remove(inventario_Areas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Inventario_AreasExists(long id)
        {
            return (_context.Inventarios_Areas?.Any(e => e.InvCodigo == id)).GetValueOrDefault();
        }
    }
}
