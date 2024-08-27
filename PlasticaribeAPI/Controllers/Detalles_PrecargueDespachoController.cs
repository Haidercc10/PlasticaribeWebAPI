using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detalles_PrecargueDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public Detalles_PrecargueDespachoController(dataContext context)
        {
            _context = context;
        }
        //
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalles_PrecargueDespacho>>> GetDetalles_PrecargueDespacho()
        {
            return await _context.Detalles_PrecargueDespacho.ToListAsync();
        }

        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalles_PrecargueDespacho>> GetDetalles_PrecargueDespacho(long id)
        {
            var Detalles_PrecargueDespacho = await _context.Detalles_PrecargueDespacho.FindAsync(id);

            if (Detalles_PrecargueDespacho == null)
            {
                return NotFound();
            }

            return Detalles_PrecargueDespacho;
        }

        //
        [HttpGet("getPreloadId/{id}")]
        public ActionResult getPreloadId(long id)
        {
            var preload = from p in _context.Set<Precargue_Despacho>()
                          from d in _context.Set<Detalles_PrecargueDespacho>()
                          where p.Pcd_Id == d.Pcd_Id &&
                          p.Pcd_Id == id
                          select new
                          {
                              //Header
                              Movement = p.Pcd_Id,
                              OF = p.OF_Id,
                              Date1 = p.Pcd_FechaCrea,
                              Hour1 = p.Pcd_HoraCrea,
                              Date2 = p.Pcd_FechaModifica,
                              Hour2 = p.Pcd_HoraModifica,
                              UserId1 = p.Usua_Crea,
                              UserId2 = p.Usua_Modifica,
                              User1 = p.Usuario1.Usua_Nombre,
                              User2 = p.Usuario2.Usua_Nombre,
                              Observation1 = p.Pcd_Observacion,
                              Observation2 = p.Pcd_ObservacionModifica,
                              StatusId = p.Estado_Id,
                              Status = p.Estados.Estado_Nombre,
                              IdClient = p.Cli_Id,
                              Client = p.Cliente.Cli_Nombre,

                              //Details

                              Roll = d.DtlPcd_Rollo,
                              Item = d.Prod_Id,
                              Reference = d.Producto.Prod_Nombre,
                              Quantity = d.DtlPcd_Cantidad,
                              Presentation = d.UndMed_Id,
                              OT = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlPcd_Rollo select pp.OT).FirstOrDefault(),
                              Weight = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlPcd_Rollo select pp.Peso_Bruto).FirstOrDefault(),
                              NetWeight = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlPcd_Rollo select pp.Peso_Neto).FirstOrDefault(),
                              Ubication = (from pp in _context.Set<Produccion_Procesos>()
                                           from dt in _context.Set<DetalleEntradaRollo_Producto>()
                                           join e in _context.Set<EntradaRollo_Producto>() on dt.EntRolloProd_Id equals e.EntRolloProd_Id
                                           where pp.NumeroRollo_BagPro == d.DtlPcd_Rollo &&
                                                  (dt.Rollo_Id == pp.Numero_Rollo) &&
                                                  e.EntRolloProd_Id >= 28512
                                           orderby e.EntRolloProd_Id descending
                                           select e.EntRolloProd_Observacion).FirstOrDefault(),
                          };

            if (preload == null) return NotFound();
            else if (preload.Any()) return Ok(preload);
            else return BadRequest();
        }

        //
        [HttpGet("getMovementsPreload/{date1}/{date2}")]
        public ActionResult getMovementsPreload(DateTime date1, DateTime date2, string? client = "", string? status = "", string? id = "")
        {
            var preload = from p in _context.Set<Precargue_Despacho>()
                          where
                          p.Pcd_FechaCrea >= date1 &&
                          p.Pcd_FechaCrea <= date2 &&
                          (client != "" ? p.Cli_Id == Convert.ToInt64(client) : p.Cli_Id.ToString().Contains(client)) &&
                          (status != "" ? p.Estado_Id == Convert.ToInt64(status) : p.Estado_Id.ToString().Contains(status)) &&
                          (id != "" ? p.Pcd_Id == Convert.ToInt64(id) : p.Pcd_Id.ToString().Contains(id))
                          select new
                          {
                              //Header
                              Movement = p.Pcd_Id,
                              OF = p.OF_Id, 
                              Date1 = p.Pcd_FechaCrea,
                              Hour1 = p.Pcd_HoraCrea,
                              Date2 = p.Pcd_FechaModifica,
                              Hour2 = p.Pcd_HoraModifica,
                              UserId1 = p.Usua_Crea,
                              UserId2 = p.Usua_Modifica,
                              User1 = p.Usuario1.Usua_Nombre,
                              User2 = p.Usuario2.Usua_Nombre,
                              Observation1 = p.Pcd_Observacion,
                              Observation2 = p.Pcd_ObservacionModifica,
                              StatusId = p.Estado_Id, 
                              Status = p.Estados.Estado_Nombre,
                              IdClient = p.Cli_Id, 
                              Client = p.Cliente.Cli_Nombre, 
                          };

            if (preload == null) return NotFound();
            else if (preload.Any()) return Ok(preload);
            else return BadRequest();
        }

        //
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalles_PrecargueDespacho(long id, Detalles_PrecargueDespacho Detalles_PrecargueDespacho)
        {
            if (id != Detalles_PrecargueDespacho.DtlPcd_Codigo)
            {
                return BadRequest();
            }

            _context.Entry(Detalles_PrecargueDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalles_PrecargueDespachoExists(id))
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
        public async Task<ActionResult<Detalles_PrecargueDespacho>> PostDetalles_PrecargueDespacho(Detalles_PrecargueDespacho Detalles_PrecargueDespacho)
        {
            _context.Detalles_PrecargueDespacho.Add(Detalles_PrecargueDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalles_PrecargueDespacho", new { id = Detalles_PrecargueDespacho.DtlPcd_Codigo }, Detalles_PrecargueDespacho);
        }

        //
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalles_PrecargueDespacho(long id)
        {
            var Detalles_PrecargueDespacho = await _context.Detalles_PrecargueDespacho.FindAsync(id);
            if (Detalles_PrecargueDespacho == null)
            {
                return NotFound();
            }

            _context.Detalles_PrecargueDespacho.Remove(Detalles_PrecargueDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //
        private bool Detalles_PrecargueDespachoExists(long id)
        {
            return _context.Detalles_PrecargueDespacho.Any(e => e.DtlPcd_Codigo == id);
        }
    }
}
