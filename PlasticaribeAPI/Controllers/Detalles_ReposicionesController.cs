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
    public class Detalles_ReposicionesController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_ReposicionesController(dataContext context)
        {
            _context = context;
        }
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_Reposiciones>>> GetDetalles_Reposiciones()
        {
            return await _context.Detalles_Reposiciones.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_Reposiciones>> GetDetalles_Reposiciones(long id)
        {
            var Detalles_Reposiciones = await _context.Detalles_Reposiciones.FindAsync(id);

            if (Detalles_Reposiciones == null)
            {
                return NotFound();
            }

            return Detalles_Reposiciones;
        }

        //
        [HttpGet("getRepositionId/{id}")]
        public ActionResult getRepositionId(long id)
        {
            var Reposition = from p in _context.Set<Reposiciones>()
                          from d in _context.Set<Detalles_Reposiciones>()
                          where p.Rep_Id == d.Rep_Id &&
                          p.Rep_Id == id &&
                          p.Estado_Id == 11
                          select new
                          {
                              //Header
                              Movement = p.Rep_Id,
                              Date1 = p.Rep_FechaCrea,
                              Hour1 = p.Rep_HoraCrea,
                              Date2 = p.Rep_FechaSalida,
                              Hour2 = p.Rep_HoraSalida,
                              UserId1 = p.Usua_Crea,
                              UserId2 = p.Usua_Salida,
                              User1 = p.Usuario1.Usua_Nombre,
                              User2 = p.Usuario2.Usua_Nombre,
                              Observation1 = p.Rep_Observacion,
                              Observation2 = p.Rep_ObservacionSalida,
                              StatusId = p.Estado_Id,
                              Status = p.Estados.Estado_Nombre,
                              IdClient = p.Cli_Id,
                              Client = p.Cliente.Cli_Nombre,

                              //Details

                              Roll = d.DtlRep_Rollo,
                              Item = d.Prod_Id,
                              Reference = d.Producto.Prod_Nombre,
                              Quantity = d.DtlRep_Cantidad,
                              Presentation = d.UndMed_Id,
                              OT = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlRep_Rollo select pp.OT).FirstOrDefault(),
                              Weight = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlRep_Rollo select pp.Peso_Bruto).FirstOrDefault(),
                              NetWeight = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlRep_Rollo select pp.Peso_Neto).FirstOrDefault(),
                              Ubication = (from pp in _context.Set<Produccion_Procesos>()
                                           from dt in _context.Set<DetalleEntradaRollo_Producto>()
                                           join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                                           where pp.NumeroRollo_BagPro == d.DtlRep_Rollo &&
                                                  (dt.Rollo_Id == pp.Numero_Rollo) &&
                                                  e.EntRolloProd_Id >= 28512
                                           orderby e.EntRolloProd_Id descending
                                           select e.EntRolloProd_Observacion).FirstOrDefault(),
                          };

            if (Reposition == null) return NotFound();
            else if (Reposition.Any()) return Ok(Reposition);
            else return BadRequest();
        }

        //
        [HttpGet("getMovementsReposition/{date1}/{date2}")]
        public ActionResult getMovementsReposition(DateTime date1, DateTime date2, string? client = "", string? status = "", string? id = "")
        {
            var Reposition = from p in _context.Set<Reposiciones>()
                          where
                          p.Rep_FechaCrea >= date1 &&
                          p.Rep_FechaCrea <= date2 &&
                          (client != "" ? p.Cli_Id == Convert.ToInt64(client) : p.Cli_Id.ToString().Contains(client)) &&
                          (status != "" ? p.Estado_Id == Convert.ToInt64(status) : p.Estado_Id.ToString().Contains(status)) &&
                          (id != "" ? p.Rep_Id == Convert.ToInt64(id) : p.Rep_Id.ToString().Contains(id))
                          select new
                          {
                              //Header
                              Movement = p.Rep_Id,
                              Date1 = p.Rep_FechaCrea,
                              Hour1 = p.Rep_HoraCrea,
                              Date2 = p.Rep_FechaSalida,
                              Hour2 = p.Rep_HoraSalida,
                              UserId1 = p.Usua_Crea,
                              UserId2 = p.Usua_Salida,
                              User1 = p.Usuario1.Usua_Nombre,
                              User2 = p.Usuario2.Usua_Nombre,
                              Observation1 = p.Rep_Observacion,
                              Observation2 = p.Rep_ObservacionSalida,
                              StatusId = p.Estado_Id,
                              Status = p.Estados.Estado_Nombre,
                              IdClient = p.Cli_Id,
                              Client = p.Cliente.Cli_Nombre,
                          };

            if (Reposition == null) return NotFound();
            else if (Reposition.Any()) return Ok(Reposition);
            else return BadRequest();
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_Reposiciones(long id, Detalles_Reposiciones Detalles_Reposiciones)
        {
            if (id != Detalles_Reposiciones.DtlRep_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(Detalles_Reposiciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_ReposicionesExists(id))
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
        public async Task<ActionResult<Detalles_Reposiciones>> PostDetalles_Reposiciones(Detalles_Reposiciones Detalles_Reposiciones)
        {
            _context.Detalles_Reposiciones.Add(Detalles_Reposiciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_Reposiciones", new { id = Detalles_Reposiciones.DtlRep_Codigo }, Detalles_Reposiciones);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_Reposiciones(long id)
        {
            var Detalles_Reposiciones = await _context.Detalles_Reposiciones.FindAsync(id);
            if (Detalles_Reposiciones == null)
            {
                return NotFound();
            }

            _context.Detalles_Reposiciones.Remove(Detalles_Reposiciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Detalles_ReposicionesExists(long id)
        {
            return _context.Detalles_Reposiciones.Any(e => e.DtlRep_Codigo == id);
        }
    }
}
