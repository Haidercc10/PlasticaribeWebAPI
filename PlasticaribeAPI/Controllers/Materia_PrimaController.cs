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
    public class Materia_PrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Materia_PrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Materia_Prima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia_Prima>>> GetMaterias_Primas()
        {
          if (_context.Materias_Primas == null)
          {
              return NotFound();
          }
            return await _context.Materias_Primas.ToListAsync();
        }

        // GET: api/Materia_Prima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia_Prima>> GetMateria_Prima(long id)
        {
          if (_context.Materias_Primas == null)
          {
              return NotFound();
          }
            var materia_Prima = await _context.Materias_Primas.FindAsync(id);

            if (materia_Prima == null)
            {
                return NotFound();
            }

            return materia_Prima;
        }

        [HttpGet("ConsultaInventario/{fecha1}/{fecha2}/{id}/{categoria}")]
        public ActionResult Get(DateTime fecha1, DateTime fecha2, long id, long categoria)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = _context.DetallesAsignaciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.AsigMp.AsigMp_FechaEntrega >= fecha1 
                       && asg.AsigMp.AsigMp_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigMp_Cantidad);

            //Devoluciones de Materia Prima
            var conDevoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.DevMatPri.DevMatPri_Fecha >= fecha1
                       && asg.DevMatPri.DevMatPri_Fecha <= fecha2).Sum(asg => asg.DtDevMatPri_CantidadDevuelta);

            //Facturas de Materia Prima
            var conFacturas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.Facco.Facco_FechaFactura >= fecha1
                       && asg.Facco.Facco_FechaFactura <= fecha2).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Materia Prima
            var conRemisiones = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.Rem.Rem_Fecha >= fecha1
                       && asg.Rem.Rem_Fecha <= fecha2).Sum(asg => asg.RemiMatPri_Cantidad);

            //Recuperado de Materia Prima
            var conRecuperado = _context.DetallesRecuperados_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.RecMp.RecMp_FechaIngreso >= fecha1
                       && asg.RecMp.RecMp_FechaIngreso <= fecha2).Sum(asg => asg.RecMatPri_Cantidad);

            //Suma Entradas
            var entrada = conDevoluciones + conFacturas + conRemisiones + conRecuperado;

            //Materia Prima
            var con = (from mp in _context.Set<Materia_Prima>()
                               from Inv in _context.Set<InventarioInicialXDia_MatPrima>()
                               where mp.MatPri_Id == id
                                     && mp.CatMP_Id == categoria
                                     && Inv.MatPri_Id == mp.MatPri_Id
                               group mp by new
                               {
                                   mp.MatPri_Id,
                                   mp.MatPri_Nombre,
                                   Inv.InvInicial_Stock,
                                   mp.MatPri_Stock,
                                   mp.UndMed_Id,
                                   mp.MatPri_Precio,
                                   mp.CatMP.CatMP_Nombre
                               } into x
                               select new
                               {
                                   Id = x.Key.MatPri_Id,
                                   Nombre = x.Key.MatPri_Nombre,
                                   Inicial = x.Key.InvInicial_Stock,
                                   Entrada = entrada,
                                   Salida = conAsg,
                                   Stock = x.Key.MatPri_Stock,
                                   Diferencia = x.Key.InvInicial_Stock - x.Key.MatPri_Stock,
                                   Presentacion = x.Key.UndMed_Id,
                                   Precio = x.Key.MatPri_Precio,
                                   SubTotal = x.Key.MatPri_Stock * x.Key.MatPri_Precio,
                                   Categoria = x.Key.CatMP_Nombre,
                               });

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP_Id == id
                       && asg.BOPP.CatMP_Id == categoria
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2).Sum(asg => asg.BOPP.BOPP_CantidadInicialKg);

            //var con = 

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            return Ok(con);
        }

        // GET: api/Materia_Prima/5
        [HttpGet("categoria/{CatMP_Id}")]
        public ActionResult<Materia_Prima> Getcaegoria(long CatMP_Id)
        {
            var materia_Prima = _context.Materias_Primas.Where(mp => mp.CatMP_Id == CatMP_Id).ToList();

            if (materia_Prima == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(materia_Prima);
            }
        }

        // PUT: api/Materia_Prima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria_Prima(long id, Materia_Prima materia_Prima)
        {
            if (id != materia_Prima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(materia_Prima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Materia_PrimaExists(id))
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

        // POST: api/Materia_Prima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materia_Prima>> PostMateria_Prima(Materia_Prima materia_Prima)
        {
          if (_context.Materias_Primas == null)
          {
              return Problem("Entity set 'dataContext.Materias_Primas'  is null.");
          }
            _context.Materias_Primas.Add(materia_Prima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateria_Prima", new { id = materia_Prima.MatPri_Id }, materia_Prima);
        }

        // DELETE: api/Materia_Prima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria_Prima(long id)
        {
            if (_context.Materias_Primas == null)
            {
                return NotFound();
            }
            var materia_Prima = await _context.Materias_Primas.FindAsync(id);
            if (materia_Prima == null)
            {
                return NotFound();
            }

            _context.Materias_Primas.Remove(materia_Prima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Materia_PrimaExists(long id)
        {
            return (_context.Materias_Primas?.Any(e => e.MatPri_Id == id)).GetValueOrDefault();
        }
    }
}
