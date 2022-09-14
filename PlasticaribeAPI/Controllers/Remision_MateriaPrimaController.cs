using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Remision_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Remision_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Remision_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remision_MateriaPrima>>> GetRemisiones_MateriasPrimas()
        {
          if (_context.Remisiones_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.Remisiones_MateriasPrimas.ToListAsync();
        }

        // GET: api/Remision_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remision_MateriaPrima>> GetRemision_MateriaPrima(int id)
        {
          if (_context.Remisiones_MateriasPrimas == null)
          {
              return NotFound();
          }
            var remision_MateriaPrima = await _context.Remisiones_MateriasPrimas.FindAsync(id);

            if (remision_MateriaPrima == null)
            {
                return NotFound();
            }

            return remision_MateriaPrima;
        }

        //Consulta por el id de la remision
        [HttpGet("remision/{Rem_Id}")]
        public ActionResult RemisionId(long Rem_Id)
        {
            var remCompra = _context.Remisiones_MateriasPrimas.Where(f => f.Rem_Id == Rem_Id).ToList();

            return Ok(remCompra);
        }

        //consulta por el Id de la materia prima
        [HttpGet("MP/{MatPri_Id}")]
        public ActionResult MateriaPrimaId(long MatPri_Id)
        {
            var remCompra = _context.Remisiones_MateriasPrimas.Where(f => f.MatPri_Id == MatPri_Id).ToList();

            return Ok(remCompra);
        }

        //consulta por el Id de la materia prima
        [HttpGet("MPFechaActual/{MatPri_Id}")]
        public ActionResult MateriaPrimaIdFechaActual(long MatPri_Id, DateTime Rem_Fecha)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var remCompra = _context.Remisiones_MateriasPrimas
                .Where(f => f.MatPri_Id == MatPri_Id && f.Rem.Rem_Fecha == Rem_Fecha).ToList();

            return Ok(remCompra);
        }

        [HttpGet("consultaMovimiento0/{fecha}")]
        public ActionResult Get (DateTime fecha)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Fecha == fecha)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem_Id,
                    rem.Rem.Rem_Codigo,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.RemiMatPri_Cantidad
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimiento1/{MatPri}/{fecha}")]
        public ActionResult Get(int MatPri, DateTime Fecha)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Fecha == Fecha
                       && rem.MatPri_Id == MatPri)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem_Id,
                    rem.Rem.Rem_Codigo,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.RemiMatPri_Cantidad
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos2/{ot}")]
        public ActionResult GetOT(string ot)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Codigo == ot)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem_Id,
                    rem.Rem.Rem_Codigo,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.RemiMatPri_Cantidad
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos3/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Fecha >= FechaInicial
                       && rem.Rem.Rem_Fecha <= FechaFinal)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem_Id,
                    rem.Rem.Rem_Codigo,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.RemiMatPri_Cantidad
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos4/{FechaInicial}/{MatPri}")]
        public ActionResult Get(DateTime FechaInicial, int MatPri)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Fecha == FechaInicial
                       && rem.MatPri_Id == MatPri)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem_Id,
                    rem.Rem.Rem_Codigo,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.RemiMatPri_Cantidad
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos5/{Ot}/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(string Ot, DateTime FechaInicial, DateTime FechaFinal)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Codigo == Ot
                       && rem.Rem.Rem_Fecha >= FechaInicial
                       && rem.Rem.Rem_Fecha <= FechaFinal)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem_Id,
                    rem.Rem.Rem_Codigo,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.RemiMatPri_Cantidad
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos6/{FechaInicial}/{FechaFinal}/{MatPri}")]
        public ActionResult Get8(DateTime FechaInicial, DateTime FechaFinal, int MatPri)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Fecha >= FechaInicial
                       && rem.Rem.Rem_Fecha <= FechaFinal
                       && rem.MatPri_Id == MatPri)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem_Id,
                    rem.Rem.Rem_Codigo,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.RemiMatPri_Cantidad
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("pdfMovimientos/{codigo}")]
        public ActionResult Get(string codigo)
        {
            var con = _context.Remisiones_MateriasPrimas
                .Where(rem => rem.Rem.Rem_Codigo == codigo)
                .Include(rem => rem.Rem)
                .Select(rem => new
                {
                    rem.Rem.Rem_Id,
                    rem.Rem.Rem_Fecha,
                    rem.Rem.Rem_Observacion,
                    rem.Rem.Prov_Id,
                    rem.Rem.Prov.TipoIdentificacion_Id,
                    rem.Rem.Prov.Prov_Nombre,
                    rem.Rem.Prov.TpProv.TpProv_Nombre,
                    rem.Rem.Prov.Prov_Telefono,
                    rem.Rem.Prov.Prov_Email,
                    rem.Rem.Prov.Prov_Ciudad,
                    rem.Rem.Usua_Id,
                    rem.Rem.Usua.Usua_Nombre,
                    rem.MatPri_Id,
                    rem.MatPri.MatPri_Nombre,
                    rem.UndMed_Id,
                    rem.RemiMatPri_Cantidad,
                    rem.RemiMatPri_ValorUnitario
                })
                .ToList();
            return Ok(con);
        }

        // PUT: api/Remision_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemision_MateriaPrima(int id, Remision_MateriaPrima remision_MateriaPrima)
        {
            if (id != remision_MateriaPrima.Rem_Id)
            {
                return BadRequest();
            }

            _context.Entry(remision_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Remision_MateriaPrimaExists(id))
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

        // POST: api/Remision_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Remision_MateriaPrima>> PostRemision_MateriaPrima(Remision_MateriaPrima remision_MateriaPrima)
        {
          if (_context.Remisiones_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.Remisiones_MateriasPrimas'  is null.");
          }
            _context.Remisiones_MateriasPrimas.Add(remision_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Remision_MateriaPrimaExists(remision_MateriaPrima.Rem_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRemision_MateriaPrima", new { id = remision_MateriaPrima.Rem_Id }, remision_MateriaPrima);
        }

        // DELETE: api/Remision_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemision_MateriaPrima(int id)
        {
            if (_context.Remisiones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var remision_MateriaPrima = await _context.Remisiones_MateriasPrimas.FindAsync(id);
            if (remision_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.Remisiones_MateriasPrimas.Remove(remision_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Remision_MateriaPrimaExists(int id)
        {
            return (_context.Remisiones_MateriasPrimas?.Any(e => e.Rem_Id == id)).GetValueOrDefault();
        }
    }
}
