using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatPrima_Material_PigmentoController : ControllerBase
    {
        private readonly dataContext _context;

        public MatPrima_Material_PigmentoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/MatPrima_Material_Pigmento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatPrima_Material_Pigmento>>> GetMatPrima_Material_Pigmento()
        {
            return await _context.MatPrimas_Materiales_Pigmentos.ToListAsync();
        }

        // GET: api/MatPrima_Material_Pigmento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatPrima_Material_Pigmento>> GetMatPrima_Material_Pigmento(long id)
        {
            var mmp = await _context.MatPrimas_Materiales_Pigmentos.FindAsync(id);

            if (mmp == null)
            {
                return NotFound();
            }

            return mmp;
        }

        //Consulta para obtener los diferentes tipos de peletizado dependiendo el material y el pigmento.
        [HttpGet("getPeletizadoForMaterialPigment/{mat}/{pig}")]
        public ActionResult getPeletizadoForMaterialPigment(int mat, int pig)
        {
            var peletizado = from mmp in _context.Set<MatPrima_Material_Pigmento>()
                       where mmp.Material_Id == mat &&
                       mmp.Pigmt_Id == pig 
                       select new { 
                           Id = mmp.MatPri_Id,
                           MatPrima = mmp.MatPrima.MatPri_Nombre
                       };

            return Ok(peletizado);
        }

        // PUT: api/MatPrima_Material_Pigmento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatPrima_Material_Pigmento(long id, MatPrima_Material_Pigmento matPrima_Material_Pigmento)
        {
            if (id != matPrima_Material_Pigmento.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(matPrima_Material_Pigmento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatPrima_Material_PigmentoExists(id))
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

        // POST: api/MatPrima_Material_Pigmento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MatPrima_Material_Pigmento>> PostMatPrima_Material_Pigmento(MatPrima_Material_Pigmento matPrima_Material_Pigmento)
        {
            _context.MatPrimas_Materiales_Pigmentos.Add(matPrima_Material_Pigmento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatPrima_Material_Pigmento", new { id = matPrima_Material_Pigmento.Codigo }, matPrima_Material_Pigmento);
        }

        // DELETE: api/MatPrima_Material_Pigmento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatPrima_Material_Pigmento(long id)
        {
            var matPrima_Material_Pigmento = await _context.MatPrimas_Materiales_Pigmentos.FindAsync(id);
            if (matPrima_Material_Pigmento == null)
            {
                return NotFound();
            }

            _context.MatPrimas_Materiales_Pigmentos.Remove(matPrima_Material_Pigmento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatPrima_Material_PigmentoExists(long id)
        {
            return _context.MatPrimas_Materiales_Pigmentos.Any(e => e.Codigo == id);
        }
    }
}
