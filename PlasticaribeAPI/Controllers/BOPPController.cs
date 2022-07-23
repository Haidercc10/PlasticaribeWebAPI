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
    public class BOPPController : ControllerBase
    {
        private readonly dataContext _context;

        public BOPPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/BOPP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BOPP>>> GetBOPP()
        {
          if (_context.BOPP == null)
          {
              return NotFound();
          }
            return await _context.BOPP.ToListAsync();
        }

        // GET: api/BOPP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BOPP>> GetBOPP(long id)
        {
          if (_context.BOPP == null)
          {
              return NotFound();
          }
            var bOPP = await _context.BOPP.FindAsync(id);

            if (bOPP == null)
            {
                return NotFound();
            }

            return bOPP;
        }

        /** Obtener BOPP consultado por serial */
        [HttpGet("serial/{BOPP_Serial}")]
        public ActionResult<BOPP> GetSerial(string BOPP_Serial)
        {
            var bOPP = _context.BOPP.Where(bopp => bopp.BOPP_Serial == BOPP_Serial).ToList();

            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }

        [HttpGet("fecha/{BOPP_FechaIngreso}")]
        public ActionResult<BOPP> GetFecha(DateTime BOPP_FechaIngreso)
        {
            var bOPP = _context.BOPP.Where(bopp => bopp.BOPP_FechaIngreso == BOPP_FechaIngreso).ToList();

            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }

        /** Get para contar la cantidad de unidades que hay de cada BOPP según su descripción, 
        precio total según existencias y cantidad en kilos agrupados */
        [HttpGet("BoppAgrupado")]
        public ActionResult<BOPP> GetBoppAgrupado()
        {
            /** Consulta la tabla de BOPP Agrupa por descripción */
            var bOPP = _context.BOPP.GroupBy(x => new {x.BOPP_Descripcion, x.BOPP_CantidadMicras })
            /** Selecciona los campos Descripción, Cantidad Micras, Suma el Precio total, Suma los Kilos, Cuenta cantidad de cada BOPP */
                                    .Select(bopp => new
                                    {
                                        bopp.Key.BOPP_Descripcion,
                                        bopp.Key.BOPP_CantidadMicras,
                                        sumaPrecio = bopp.Sum(x => x.BOPP_Precio),
                                        sumaKilosIngresados = bopp.Sum(x => x.BOPP_CantidadInicialKg),
                                        sumaKilosActual = bopp.Sum(x => x.BOPP_Stock),
                                        conteoDescripcion = bopp.Count() 
                                    })
                                    /** Lista el resultado agrupado */
                                    .ToList();

            if (bOPP == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bOPP);
            }
        }


        // PUT: api/BOPP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBOPP(long id, BOPP bOPP)
        {
            if (id != bOPP.BOPP_Id)
            {
                return BadRequest();
            }

            _context.Entry(bOPP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOPPExists(id))
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

        // POST: api/BOPP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BOPP>> PostBOPP(BOPP bOPP)
        {
          if (_context.BOPP == null)
          {
              return Problem("Entity set 'dataContext.BOPP'  is null.");
          }
            _context.BOPP.Add(bOPP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBOPP", new { id = bOPP.BOPP_Id }, bOPP);
        }

        // DELETE: api/BOPP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBOPP(long id)
        {
            if (_context.BOPP == null)
            {
                return NotFound();
            }
            var bOPP = await _context.BOPP.FindAsync(id);
            if (bOPP == null)
            {
                return NotFound();
            }

            _context.BOPP.Remove(bOPP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BOPPExists(long id)
        {
            return (_context.BOPP?.Any(e => e.BOPP_Id == id)).GetValueOrDefault();
        }
    }
}
