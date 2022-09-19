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

        /** Consultar */
        [HttpGet("DatosMatPrimaxId/{Id}")]
        public ActionResult GetDatosMatPrixId(long Id)
        {

            //var queryInvInicial = _context.InventarioInicialXDias_MatPrima.Where(mp => mp.MatPri_Id == Id).Select(inv => new { inv.InvInicial_Stock });

            var matPrima = (from mp in _context.Set<Materia_Prima>()
                         from invIni in _context.Set<InventarioInicialXDia_MatPrima>()
                         from asgmp in _context.Set<Asignacion_MatPrima>() 
                         from dasgmp in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where mp.MatPri_Id == Id
                         && mp.MatPri_Id == invIni.MatPri_Id
                         && dasgmp.AsigMp_Id == asgmp.AsigMp_Id
                         && dasgmp.MatPri_Id == mp.MatPri_Id
                         
                         select new
                         {
                             ID = mp.MatPri_Id,
                             Nombre = mp.MatPri_Nombre,
                             Stock = mp.MatPri_Stock,
                             Medida = mp.UndMed_Id,
                             Precio = mp.MatPri_Precio,
                             Subtotal = mp.MatPri_Stock * mp.MatPri_Precio,
                             Categoria = mp.CatMP.CatMP_Nombre, 
                             Stock_Inicial = invIni.InvInicial_Stock
                         });

            var tinta = (from tnt in _context.Set<Tinta>()
                         where tnt.Tinta_Id == Id
                         select new
                         {
                             ID = tnt.Tinta_Id,
                             Nombre = tnt.Tinta_Nombre,
                             Stock = tnt.Tinta_Stock,
                             Medida = tnt.UndMed_Id,
                             Precio = tnt.Tinta_Precio,
                             Subtotal = tnt.Tinta_Stock * tnt.Tinta_Precio,
                             Categoria = tnt.CatMP.CatMP_Nombre,
                             Stock_Inicial = tnt.Tinta_InvInicial
                         });

            var BOPP = (from bopp in _context.Set<BOPP>()
                         where bopp.BOPP_Id == Id
                         select new
                         {
                             ID = bopp.BOPP_Id,
                             Nombre = bopp.BOPP_Nombre,
                             Stock = bopp.BOPP_Stock,
                             Medida = bopp.UndMed_Id,
                             Precio = bopp.BOPP_Precio,
                             Subtotal = bopp.BOPP_Stock * bopp.BOPP_Precio,
                             Categoria = bopp.CatMP.CatMP_Nombre,
                             Stock_Inicial = bopp.BOPP_CantidadInicialKg
                         });
            
            var Query = matPrima.Concat(tinta).Concat(BOPP);

            return Ok(Query);

            /*into y
            select new
            {
                //Materia Prima
                MateriaPrima = y.Key.MatPri_Id,
                NombreMP = y.Key.MatPri_Nombre,
                CantMP = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad),
                UndMedida = y.Key.UndMed_Id,
                Precio = y.Key.MatPri_Precio,
                SubTotal = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad) * y.Key.MatPri_Precio,
                Proceso = y.Key.Proceso_Id,
                NombreProceso = y.Key.Proceso_Nombre
            });

var conTinta = (from AsgTinta in _context.Set<DetalleAsignacion_Tinta>()
               where AsgTinta.AsigMp.AsigMP_OrdenTrabajo == ot
               group AsgTinta by new
               {
                   AsgTinta.Tinta_Id,
                   AsgTinta.Tinta.Tinta_Nombre,
                   AsgTinta.UndMed_Id,
                   AsgTinta.Tinta.Tinta_Precio,
                   AsgTinta.Proceso_Id,
                   AsgTinta.Proceso.Proceso_Nombre
               } into y
               select new
               {
                   //Tintas
                   MateriaPrima = y.Key.Tinta_Id,
                   NombreMP = y.Key.Tinta_Nombre,
                   CantMP = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad),
                   UndMedida = y.Key.UndMed_Id,
                   Precio = y.Key.Tinta_Precio,
                   SubTotal = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad) * y.Key.Tinta_Precio,
                   Proceso = y.Key.Proceso_Id,
                   NombreProceso = y.Key.Proceso_Nombre
               });

var conBopp = (from AsgBopp in _context.Set<DetalleAsignacion_BOPP>()
              where AsgBopp.DtAsigBOPP_OrdenTrabajo == ot
              group AsgBopp by new
              {
                  AsgBopp.BOPP_Id,
                  AsgBopp.BOPP.BOPP_Nombre,
                  AsgBopp.UndMed_Id,
                  AsgBopp.BOPP.BOPP_Precio,
                  AsgBopp.Proceso_Id,
                  AsgBopp.Proceso.Proceso_Nombre
              } into y
              select new
              {
                  //BOPP
                  MateriaPrima = y.Key.BOPP_Id,
                  NombreMP = y.Key.BOPP_Nombre,
                  CantMP = y.Sum(AsgBopp => AsgBopp.BOPP.BOPP_CantidadInicialKg),
                  UndMedida = y.Key.UndMed_Id,
                  Precio = y.Key.BOPP_Precio,
                  SubTotal = y.Sum(AsgBopp => AsgBopp.BOPP.BOPP_CantidadInicialKg) * y.Key.BOPP_Precio,
                  Proceso = y.Key.Proceso_Id,
                  NombreProceso = y.Key.Proceso_Nombre
              });

var con = conMp.Concat(conTinta).Concat(conBopp);*/


            //var inv_materia_Prima = _context.InventarioInicialXDias_MatPrima.Where(mp => mp.MatPri_Id == MatPri_Id).Select(inv => new {  })

            /** var materia_Prima = _context.Materias_Primas.Where(mp => mp.MatPri_Id == MatPri_Id)
                 .Include(rel => rel.CatMP)
                 .Include(rel => rel.TpBod)
                 .Select(agr => new
                 {
                     agr.MatPri_Id,
                     agr.MatPri_Nombre,
                     agr.MatPri_Stock,
                     agr.UndMed_Id,
                     agr.MatPri_Precio,
                     Subtotal = agr.MatPri_Stock * agr.MatPri_Precio,
                     agr.CatMP.CatMP_Nombre
                 }).
                 First(); */

            /*if (matPrima == null)
            {
                return NotFound();
            }*/

            //return Ok(matPrima);


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
