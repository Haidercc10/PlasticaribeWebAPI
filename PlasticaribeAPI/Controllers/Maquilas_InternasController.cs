using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Maquilas_InternasController : ControllerBase
    {
        private readonly dataContext _context;

        public Maquilas_InternasController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquilas_Internas>>> GetMaquilas_Internas()
        {
            return await _context.Maquilas_Internas.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquilas_Internas>> GetMaquilas_Internas(long id)
        {
            var Maquilas_Internas = await _context.Maquilas_Internas.FindAsync(id);

            if (Maquilas_Internas == null)
            {
                return NotFound();
            }

            return Maquilas_Internas;
        }

        //Consulta que devolverá el ultimo codigo de entrada de peletizado.
        [HttpGet("getLastCodeMaquila")]
        public ActionResult getLastCodeMaquila()
        {
            var lastCode = (from m in _context.Set<Models.Maquilas_Internas>() orderby m.MaqInt_Codigo descending select m.MaqInt_Codigo == 0 ? 0 : m.MaqInt_Codigo).FirstOrDefault();
            return Ok(lastCode);
        }

        //Consulta que devolverá el ultimo codigo de entrada de peletizado.
        [HttpGet("getInternalsMaquilasForCode/{code}")]
        public ActionResult getInternalsMaquilasForCode(int code)
        {
            var maquila = from m in _context.Set<Models.Maquilas_Internas>()
                          where m.MaqInt_Codigo == code
                          select new
                          {
                              Code = m.MaqInt_Codigo,
                              Id = m.MaqInt_Id,
                              OT = m.MaqInt_OT,
                              Item = m.Prod_Id,
                              Reference = m.Producto.Prod_Nombre,
                              Weight = m.Peso_Bruto,
                              NetWeight = m.Peso_Neto,
                              Unit = m.Presentacion,
                              ServiceId = m.SvcProd_Id,
                              Service = m.Servicio_Produccion.SvcProd_Nombre,
                              Value = m.MaqInt_ValorPago,
                              RequestedBy = m.Servicio_Produccion.Proceso_Solicita,
                              OperatorId = m.Operario_Id,
                              Operator = m.Operario.Usua_Nombre,
                              DateService = m.MaqInt_Fecha,
                              DateSave = m.MaqInt_FechaRegistro,
                              HourSave = m.MaqInt_HoraRegistro,
                              Observation = m.MaqInt_Observacion,
                              CreatorId = m.Creador_Id,
                              Creator = m.Creador.Usua_Nombre,
                              Turno = m.Turno_Id,
                          };
            return Ok(maquila);
        }

        //Consulta que devolverá el ultimo codigo de entrada de peletizado.
        [HttpGet("getMovMaquilas/{date1}/{date2}")]
        public ActionResult getMovMaquilas(DateTime date1, DateTime date2, string? service = "", string? operative = "", string? ot = "", string? code = "")
        {
            var maquila = from m in _context.Set<Models.Maquilas_Internas>()
                          
                          where m.MaqInt_Fecha >= date1 &&
                          m.MaqInt_Fecha <= date2 &&
                          (service != "" ? m.SvcProd_Id == Convert.ToInt64(service) : m.SvcProd_Id.ToString().Contains(service)) &&
                          (ot != "" ? m.MaqInt_OT == Convert.ToInt64(ot) : m.MaqInt_OT.ToString().Contains(ot)) &&
                          (operative != "" ? m.Operario_Id == Convert.ToInt64(operative) : m.Operario_Id.ToString().Contains(operative)) &&
                          (code != "" ? m.MaqInt_Codigo == Convert.ToInt64(code) : m.MaqInt_Codigo.ToString().Contains(code))
                          select new
                          {
                              Roll = m.MaqInt_Codigo,
                              Item = m.SvcProd_Id,
                              Reference = m.Servicio_Produccion.SvcProd_Nombre,
                              OT = m.MaqInt_OT,
                              Weight = m.Peso_Neto,
                              Operator = m.Operario.Usua_Nombre,
                              Date = m.MaqInt_Fecha,
                              Hour = m.MaqInt_HoraRegistro,
                              Turn = m.Turno_Id,
                              Value_Production = m.MaqInt_ValorPago,
                              Value_Pay = m.MaqInt_ValorPago * m.Peso_Neto,
                              Value_Day = m.Servicio_Produccion.SvcProd_ValorDia,
                              Value_Night = m.Servicio_Produccion.SvcProd_ValorNoche,
                              Value_Sunday = m.Servicio_Produccion.SvcProd_ValorDomFest,
                              Value_Rewinding = m.MaqInt_ValorPago,
                              Position_Job = "Operario Corte",
                              Machine = m.Maquina,
                              Send_Zeus = "0",
                              Material = m.Materiales.Material_Nombre,
                              Printed = m.Impreso == true ? "SI" : "NO",
                              Laminate = "NO APLICA",
                              Rewinding = m.Servicio_Produccion.SvcProd_Id == 15 ? "SI" : "NO",
                              Concept = "MAQUILA"
                          };
            return Ok(maquila);
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquilas_Internas(long id, Maquilas_Internas Maquilas_Internas)
        {
            if (id != Maquilas_Internas.MaqInt_Id)
            {
                return BadRequest();
            }

            _context.Entry(Maquilas_Internas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Maquilas_InternasExists(id))
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
        public async Task<ActionResult<Maquilas_Internas>> PostMaquilas_Internas(Maquilas_Internas Maquilas_Internas)
        {
            _context.Maquilas_Internas.Add(Maquilas_Internas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaquilas_Internas", new { id = Maquilas_Internas.MaqInt_Id }, Maquilas_Internas);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquilas_Internas(long id)
        {
            var Maquilas_Internas = await _context.Maquilas_Internas.FindAsync(id);
            if (Maquilas_Internas == null)
            {
                return NotFound();
            }

            _context.Maquilas_Internas.Remove(Maquilas_Internas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Maquilas_InternasExists(long id)
        {
            return _context.Maquilas_Internas.Any(e => e.MaqInt_Id == id);
        }
    }

}
