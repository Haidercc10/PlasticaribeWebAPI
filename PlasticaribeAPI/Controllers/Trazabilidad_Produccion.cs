using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Trazabilidad_ProduccionController : ControllerBase
    {
        private readonly dataContext _context;

        public Trazabilidad_ProduccionController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Trazabilidad_Produccion>>> GetTrazabilidad_Produccion()
        {
            return await _context.Trazabilidad_Produccion.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Trazabilidad_Produccion>> GetTrazabilidad_Produccion(int id)
        {
            var Trazabilidad_Produccion = await _context.Trazabilidad_Produccion.FindAsync(id);

            if (Trazabilidad_Produccion == null)
            {
                return NotFound();
            }

            return Trazabilidad_Produccion;
        }

        //
        [HttpGet("getTraceability/{date1}/{date2}/{process}")]
        public async Task<ActionResult<Models.Trazabilidad_Produccion>> GetTrazabilidad_Produccion(DateTime date1, DateTime date2, string process, string? ot = "", string? item = "", string? roll = "")
        {
            var Trazabilidad_Produccion = from tr in _context.Set<Trazabilidad_Produccion>()
                                          where tr.Trz_Fecha >= date1 &&
                                          tr.Trz_Fecha <= date2 &&
                                          (tr.Proceso_Anterior == process || tr.Proceso_Id == process) &&
                                          (item != "" ? (tr.Prod_Anterior == Convert.ToInt64(item) || tr.Prod_Id == Convert.ToInt64(item)) : (tr.Prod_Anterior.ToString().Contains(item) || tr.Prod_Id.ToString().Contains(item))) &&
                                          (ot != "" ? (tr.Trz_OtAnterior == Convert.ToInt64(ot) || tr.Trz_Ot == Convert.ToInt64(ot)) : (tr.Trz_OtAnterior.ToString().Contains(ot) || tr.Trz_Ot.ToString().Contains(ot))) &&
                                          (roll != "" ? (tr.Trz_EtiquetaAnterior == Convert.ToInt64(roll) || tr.Trz_Etiqueta == Convert.ToInt64(roll)) : (tr.Trz_EtiquetaAnterior.ToString().Contains(roll) || tr.Trz_Etiqueta.ToString().Contains(roll)))
                                          orderby tr.Trz_OtAnterior, tr.Trz_EtiquetaAnterior 
                                          group tr by new {
                                              MotherRoll = tr.Trz_EtiquetaAnterior,
                                              MotherOT = tr.Trz_OtAnterior,
                                              MotherItem = tr.Prod_Anterior,
                                              MotherReference = tr.ProductoAnt.Prod_Nombre,
                                              MotherProcess_Id = tr.Proceso_Anterior,
                                              MotherProcess = tr.ProcesoAnt.Proceso_Nombre,
                                          } into grp
                                          select new
                                          {
                                              MotherRoll = grp.Key.MotherRoll,
                                              MotherOT = grp.Key.MotherOT,
                                              MotherItem = grp.Key.MotherItem,
                                              MotherReference = grp.Key.MotherReference,
                                              MotherProcess_Id = grp.Key.MotherProcess_Id,
                                              MotherProcess = grp.Key.MotherProcess,
                                          };

            if (Trazabilidad_Produccion == null)
            {
                return NotFound();
            }
            return Ok(Trazabilidad_Produccion);
        }

        [HttpGet("getTraceabilityForProduction/{production}/{process}")]
        public IActionResult getTraceabilityForProduction(long production, string process)
        {
            var Trazabilidad_Produccion = from tr in _context.Set<Trazabilidad_Produccion>()
                                          where tr.Trz_EtiquetaAnterior == production
                                          && tr.ProcesoAnt.Proceso_Nombre == process
                                          group tr by new
                                          {
                                              ChildRoll = tr.Trz_Etiqueta,
                                              ChildOT = tr.Trz_Ot,
                                              ChildItem = tr.Prod_Id,
                                              ChildReference = tr.Producto.Prod_Nombre,
                                              ChildProcess_Id = tr.Proceso_Id,
                                              ChildProcess = tr.Procesos.Proceso_Nombre,
                                              ChildQuantity = tr.Presentacion == "Kg" ? tr.Trz_PesoNeto : tr.Trz_Cantidad == Convert.ToDecimal(0) ? tr.Trz_PesoNeto : tr.Trz_Cantidad,
                                              ChildPresentation = tr.Presentacion,
                                              ChildOperator = tr.Usuario1.Usua_Nombre,
                                              ChildDate = tr.Trz_Fecha,
                                              ChildHour = tr.Trz_Hora,
                                              ChildTurn = ""
                                          } into grp
                                          select new
                                          {
                                              ChildRoll = grp.Key.ChildRoll,
                                              ChildOT = grp.Key.ChildOT,
                                              ChildItem = grp.Key.ChildItem,
                                              ChildReference = grp.Key.ChildReference,
                                              ChildProcess_Id = grp.Key.ChildProcess_Id,
                                              ChildProcess = grp.Key.ChildProcess,
                                              ChildQuantity = grp.Key.ChildQuantity,
                                              ChildPresentation = grp.Key.ChildPresentation,
                                              ChildOperator = grp.Key.ChildOperator,
                                              ChildDate = grp.Key.ChildDate,
                                              ChildHour = grp.Key.ChildHour,
                                              ChildTurn = grp.Key.ChildTurn,
                                          };

            if (Trazabilidad_Produccion == null)
            {
                return NotFound();
            }
            return Ok(Trazabilidad_Produccion);
        }

        //
        [HttpGet("getFormatTraceability/{date1}/{date2}/{process}")]
        public async Task<ActionResult<Models.Trazabilidad_Produccion>> getFormatTraceability(DateTime date1, DateTime date2, string process, string? ot = "", string? item = "", string? roll = "")
        {
            var Trazabilidad_Produccion = from tr in _context.Set<Trazabilidad_Produccion>()
                                          where tr.Trz_Fecha >= date1 &&
                                          tr.Trz_Fecha <= date2 &&
                                          (tr.Proceso_Anterior == process || tr.Proceso_Id == process) &&
                                          (item != "" ? (tr.Prod_Anterior == Convert.ToInt64(item) || tr.Prod_Id == Convert.ToInt64(item)) : (tr.Prod_Anterior.ToString().Contains(item) || tr.Prod_Id.ToString().Contains(item))) &&
                                          (ot != "" ? (tr.Trz_OtAnterior == Convert.ToInt64(ot) || tr.Trz_Ot == Convert.ToInt64(ot)) : (tr.Trz_OtAnterior.ToString().Contains(ot) || tr.Trz_Ot.ToString().Contains(ot))) &&
                                          (roll != "" ? (tr.Trz_EtiquetaAnterior == Convert.ToInt64(roll) || tr.Trz_Etiqueta == Convert.ToInt64(roll)) : (tr.Trz_EtiquetaAnterior.ToString().Contains(roll) || tr.Trz_Etiqueta.ToString().Contains(roll)))
                                          orderby tr.Trz_OtAnterior, tr.Trz_EtiquetaAnterior, tr.Proceso_Anterior
                                          select new
                                          {
                                              tr,
                                              Client = tr.Clientes.Cli_Nombre,
                                              OldReference = tr.ProductoAnt.Prod_Nombre,
                                              NewReference = tr.Producto.Prod_Nombre,
                                              NewOperator = tr.Usuario1.Usua_Nombre,
                                              OldProcess = tr.ProcesoAnt.Proceso_Nombre,
                                              NewProcess = tr.Procesos.Proceso_Nombre,
                                          };
            
            if (Trazabilidad_Produccion == null)
            {
                return NotFound();
            }
            return Ok(Trazabilidad_Produccion);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrazabilidad_Produccion(int id, Models.Trazabilidad_Produccion Trazabilidad_Produccion)
        {
            if (id != Trazabilidad_Produccion.Trz_Id)
            {
                return BadRequest();
            }

            _context.Entry(Trazabilidad_Produccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Trazabilidad_ProduccionExists(id))
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

        //
        [HttpPost]
        public async Task<ActionResult<Trazabilidad_Produccion>> PostTrazabilidad_Produccion(Models.Trazabilidad_Produccion Trazabilidad_Produccion)
        {
            _context.Trazabilidad_Produccion.Add(Trazabilidad_Produccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrazabilidad_Produccion", new { id = Trazabilidad_Produccion.Trz_Id }, Trazabilidad_Produccion);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrazabilidad_Produccion(int id)
        {
            var Trazabilidad_Produccion = await _context.Trazabilidad_Produccion.FindAsync(id);
            if (Trazabilidad_Produccion == null)
            {
                return NotFound();
            }

            _context.Trazabilidad_Produccion.Remove(Trazabilidad_Produccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Trazabilidad_ProduccionExists(int id)
        {
            return _context.Trazabilidad_Produccion.Any(e => e.Trz_Id == id);
        }
    }
}
