using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Salidas_PeletizadoController : ControllerBase
    {
        private readonly dataContext _context;

        public Salidas_PeletizadoController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salidas_Peletizado>>> GetSalidas_Peletizado()
        {
            return await _context.Salidas_Peletizado.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Salidas_Peletizado>> GetSalidas_Peletizado(long id)
        {
            var Salidas_Peletizado = await _context.Salidas_Peletizado.FindAsync(id);

            if (Salidas_Peletizado == null)
            {
                return NotFound();
            }

            return Salidas_Peletizado;
        }

        [HttpGet("getOutputsPeletizado/{id}")]
        public ActionResult getOutputsPeletizado(long id)
        {
            var output = from s in _context.Set<Salidas_Peletizado>()
                         join d in _context.Set<Detalles_SalidasPeletizado>() on s.SalPel_Id equals d.SalPel_Id
                         where s.SalPel_Id == d.SalPel_Id
                         && d.SalPel_Id == id
                         select new { 
                            Outputs = s, 
                            UserName = s.Usuario.Usua_Nombre,
                            UserModify = s.Usuario2.Usua_Nombre,
                            Status = s.Estados.Estado_Nombre,
                            MatPrima = s.MatPrima.MatPri_Nombre,
                            Details_Ouptuts = d,
                            Type_Recovery = d.Tipo_Recuperado.TpRecu_Nombre,
                            OT = d.Ing_Pele.OT, 
                            Roll = d.Ing_Pele.Rollo_Id,
                         };

            if (output == null) return NotFound();
            return Ok(output);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalidas_Peletizado(long id, Salidas_Peletizado Salidas_Peletizado)
        {
            if (id != Salidas_Peletizado.SalPel_Id)
            {
                return BadRequest();
            }

            _context.Entry(Salidas_Peletizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Salidas_PeletizadoExists(id))
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

        [HttpPut("putStatusOutput/{user}")]
        public async Task<IActionResult> putStatusOutput(long user, [FromBody] List<int> outputs)
        {
            foreach (var item in outputs)
            {
                var output = (from s in _context.Set<Salidas_Peletizado>()
                              join d in _context.Set<Detalles_SalidasPeletizado>() on s.SalPel_Id equals d.SalPel_Id
                              where s.SalPel_Id == d.SalPel_Id
                              && d.SalPel_Id == item
                              && s.Estado_Id == 11
                              select s).FirstOrDefault();

                output.Usua_Aprueba = user;
                output.Estado_Id = 26;
                output.SalPel_FechaAprobado = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                output.SalPel_HoraAprobado = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
                _context.Entry(output).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Salidas_PeletizadoExists(item)) return NotFound();
                    else throw;
                }
            }
            
            return NoContent();
        }

        //
        [HttpPost]
        public async Task<ActionResult<Salidas_Peletizado>> PostSalidas_Peletizado(Salidas_Peletizado Salidas_Peletizado)
        {
            _context.Salidas_Peletizado.Add(Salidas_Peletizado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalidas_Peletizado", new { id = Salidas_Peletizado.SalPel_Id }, Salidas_Peletizado);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalidas_Peletizado(long id)
        {
            var Salidas_Peletizado = await _context.Salidas_Peletizado.FindAsync(id);
            if (Salidas_Peletizado == null)
            {
                return NotFound();
            }

            _context.Salidas_Peletizado.Remove(Salidas_Peletizado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Salidas_PeletizadoExists(long id)
        {
            return _context.Salidas_Peletizado.Any(e => e.SalPel_Id == id);
        }
    }
}
