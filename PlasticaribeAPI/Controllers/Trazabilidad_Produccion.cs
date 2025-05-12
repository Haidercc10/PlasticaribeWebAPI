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
        [HttpGet("getTraceability/{date1}/{date2}")]
        public async Task<ActionResult<Models.Trazabilidad_Produccion>> GetTrazabilidad_Produccion(DateTime date1, DateTime date2, string? ot = "", string? item = "", string? roll = "")
        {
            var Trazabilidad_Produccion = from tr in _context.Set<Trazabilidad_Produccion>()
                                          where tr.Trz_Fecha >= date1 &&
                                          tr.Trz_Fecha <= date2 &&
                                          (item != "" ? tr.Prod_Anterior == Convert.ToInt64(item) : tr.Prod_Anterior.ToString().Contains(item)) &&
                                          (ot != "" ? tr.Trz_OtAnterior == Convert.ToInt64(ot) : tr.Trz_OtAnterior.ToString().Contains(ot)) &&
                                          (roll != "" ? tr.Trz_EtiquetaAnterior == Convert.ToInt64(roll) : tr.Trz_EtiquetaAnterior.ToString().Contains(roll))
                                          select new
                                          {
                                              MotherRoll = tr.Trz_EtiquetaAnterior,
                                              MotherOT = tr.Trz_OtAnterior,
                                              MotherItem = tr.Prod_Anterior,
                                              MotherReference = tr.ProductoAnt.Prod_Nombre,
                                              MotherProcess_Id = tr.Proceso_Anterior,
                                              MotherProcess = tr.ProcesoAnt.Proceso_Nombre,

                                              Roll = tr.Trz_Etiqueta,
                                              OT = tr.Trz_Ot,
                                              Client = tr.Clientes.Cli_Nombre,
                                              Item = tr.Prod_Id,
                                              Reference = tr.Producto.Prod_Nombre,
                                              Net = tr.Trz_PesoNeto,
                                              Gross = tr.Trz_PesoBruto,
                                              Qty = tr.Trz_Cantidad,
                                              RealQty = tr.Presentacion == "Kg" ? tr.Trz_PesoNeto : tr.Trz_Cantidad,
                                              Presentacion = tr.Presentacion,
                                              Process_Id = tr.Proceso_Id,
                                              Process = tr.Procesos.Proceso_Nombre,
                                              Mq = tr.Trz_Maquina,
                                              Date = tr.Trz_Fecha,
                                              Hour = tr.Trz_Hora, 
                                              Operator1 = tr.Operario_1,
                                              Operator2 = tr.Operario_2,
                                              Operator3 = tr.Operario_3,
                                              Operator4 = tr.Operario_4,
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
