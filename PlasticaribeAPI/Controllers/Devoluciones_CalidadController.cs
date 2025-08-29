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
    public class Devoluciones_CalidadController : ControllerBase
    {
        private readonly dataContext _context;

        public Devoluciones_CalidadController(dataContext context)
        {
            _context = context;
        }

        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Devoluciones_Calidad>>> GetDevoluciones_Calidad()
        {
            return await _context.Devoluciones_Calidad.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Devoluciones_Calidad>> GetDevoluciones_Calidad(long id)
        {
            var Devoluciones_Calidad = await _context.Devoluciones_Calidad.FindAsync(id);

            if (Devoluciones_Calidad == null)
            {
                return NotFound();
            }

            return Devoluciones_Calidad;
        }

        //Devoluciones por areas.
        // Función que cargará el total de devoluciones por area en dinero.
        [HttpGet("getTotalMoneyForArea/{year}")]
        public ActionResult getTotalMoneyForArea(int year, string? month="")
        {
            var dev = from d in _context.Set<Devoluciones_Calidad>()
                        where d.Dvc_Ano == year
                        && d.Dvc_Mes.Contains(month)
                        group d by new {
                             AreaId = d.Proceso_Id, 
                             Area = d.Proceso.Proceso_Nombre,
                             Year = d.Dvc_Ano
                        } 
                        into g
                        select new { 
                            Ano = g.Key.Year,
                            Area = g.Key.Area,
                            AreaId = g.Key.AreaId,
                            Total = g.Sum(x => x.Dvc_Subtotal),
                            Weight = g.Sum(x => x.Dvc_PesoNeto),
                            Qty = g.Count(),
                        };
            return Ok(dev);
        }

        // Función que cargará el total por tipos de devoluciones en dinero.
        [HttpGet("getTotalMoneyForRejectedType/{year}")]
        public ActionResult getTotalMoneyForRejectedType(int year, string? month = "")
        {
            var dev = from d in _context.Set<Devoluciones_Calidad>()
                      where d.Dvc_Ano == year
                      && d.Dvc_Mes.Contains(month) 
                      group d by new
                      {
                          RejectedType = d.Dvc_TipoRechazo,
                          Year = d.Dvc_Ano
                      }
                        into g
                      select new
                      {
                          Year = g.Key.Year,
                          RejectedType = g.Key.RejectedType,
                          Total = g.Sum(x => x.Dvc_Subtotal),
                          Weight = g.Sum(x => x.Dvc_PesoNeto),
                          Qty = g.Count(),
                      };
            return Ok(dev);
        }

        // Función que cargará el total por tipos de devoluciones en dinero.
        [HttpGet("getDevolutionsForRejectedType/{year}")]
        public ActionResult getDevolutionsForRejectedType(int year)
        {
            var dev = from d in _context.Set<Devoluciones_Calidad>()
                      where d.Dvc_Ano == year
                      orderby d.Dvc_Fecha.Value.Month
                      group d by new
                      {
                          RejectedType = d.Dvc_TipoRechazo,
                          Year = d.Dvc_Ano,
                          Month = d.Dvc_Mes,
                          MonthNro = d.Dvc_Fecha.Value.Month
                      }
                        into g
                      select new
                      {
                          Year = g.Key.Year,
                          RejectedType = g.Key.RejectedType,
                          Month = g.Key.Month,
                          MonthNro = g.Key.MonthNro,
                          Total = g.Sum(x => x.Dvc_Subtotal),
                          Weight = g.Sum(x => x.Dvc_PesoNeto),
                          Qty = g.Count(),
                      };
            return Ok(dev);
        }

        // Función que mostrará el total de devoluciones por mes.
        [HttpGet("getTotalMoneyForMonth/{year}")]
        public ActionResult getTotalMoneyForMonth(int year)
        {
            var datos = new List<object>();
            for (int i = 0; i < 12; i++)
            {
                int mes = (i + 1);

                var dev = from d in _context.Set<Devoluciones_Calidad>()
                          where d.Dvc_Ano == year
                          && d.Dvc_Fecha.Value.Month == mes
                          group d by new
                          {
                              Month = d.Dvc_Fecha.Value.Month,
                              NameMonth = d.Dvc_Mes,
                              Year = d.Dvc_Ano
                          }
                        into g
                          select new
                          {
                              Year = g.Key.Year,
                              Month = g.Key.Month,
                              NameMonth = g.Key.NameMonth,
                              Total = g.Sum(x => x.Dvc_Subtotal),
                          };

                datos.Add(dev);
                if (i == 11) return Ok(datos);
            }
            return Ok(datos);
        }

        //Devoluciones por clientes.
        // Función que cargará el total de devoluciones por area en dinero.
        [HttpGet("getTotalMoneyForClient/{year}")]
        public ActionResult getTotalMoneyForClient(int year, string? month = "")
        {
            var dev = from d in _context.Set<Devoluciones_Calidad>()
                      where d.Dvc_Ano == year && d.Dvc_Mes.Contains(month)
                      group d by new
                      {
                          ClientId = d.Cli_Id,
                          Client = d.Cliente.Cli_Nombre,
                          Year = d.Dvc_Ano
                      }
                        into g
                      select new
                      {
                          Year = g.Key.Year,
                          ClientId = g.Key.ClientId,
                          Client = g.Key.Client,
                          Total = g.Sum(x => x.Dvc_Subtotal),
                          Weight = g.Sum(x => x.Dvc_PesoNeto),
                          Qty = g.Count(),
                      };
            return Ok(dev);
        }
        // Func
        [HttpGet("getMovementsDvQuality/{date1}/{date2}")]
        public ActionResult getMovementsDvQuality(DateTime date1, DateTime date2, string? client = "", string? ot = "", string? typeRejected = "", string? item = "")
        {
            var dev = from d in _context.Set<Devoluciones_Calidad>()
                      where 
                      d.Dvc_Fecha >= date1 && 
                      d.Dvc_Fecha <= date2 &&
                      (client != "" ? d.Cli_Id == Convert.ToInt64(client) : d.Cli_Id.ToString().Contains(client)) &&
                      (ot != "" ? d.Dvc_OT == Convert.ToInt64(ot) : d.Dvc_OT.ToString().Contains(ot)) &&
                      (typeRejected != "" ? d.Dvc_TipoRechazo == typeRejected : d.Dvc_TipoRechazo.ToString().Contains(typeRejected)) &&
                      (item != "" ? d.Prod_Id == Convert.ToInt64(item) : d.Prod_Id.ToString().Contains(item))
                      select new
                      {
                          Devs = d, 
                          client = d.Cliente,
                          Item = d.Producto, 
                          Fails = d.Fallas,
                          Process = d.Proceso,
                          Req = d.Requerimiento,
                      };
            return Ok(dev);
        }


        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevoluciones_Calidad(long id, Devoluciones_Calidad Devoluciones_Calidad)
        {
            if (id != Devoluciones_Calidad.Dvc_Id)
            {
                return BadRequest();
            }

            _context.Entry(Devoluciones_Calidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Devoluciones_CalidadExists(id))
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
        public async Task<ActionResult<Devoluciones_Calidad>> PostDevoluciones_Calidad(Devoluciones_Calidad Devoluciones_Calidad)
        {
            _context.Devoluciones_Calidad.Add(Devoluciones_Calidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevoluciones_Calidad", new { id = Devoluciones_Calidad.Dvc_Id }, Devoluciones_Calidad);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevoluciones_Calidad(long id)
        {
            var Devoluciones_Calidad = await _context.Devoluciones_Calidad.FindAsync(id);
            if (Devoluciones_Calidad == null)
            {
                return NotFound();
            }

            _context.Devoluciones_Calidad.Remove(Devoluciones_Calidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Devoluciones_CalidadExists(long id)
        {
            return _context.Devoluciones_Calidad.Any(e => e.Dvc_Id == id);
        }
    }
}
