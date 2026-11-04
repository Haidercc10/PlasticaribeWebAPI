using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Security.Claims;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
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

        // NOTA: versión heredada que ejecuta varias subconsultas por fila. Usar GetPreloadIdAsync (getPreloadIdAsync/{id}).
        // Aún la consume Precargue_RollosDespacho en el front; no marcar [Obsolete] ni eliminar hasta migrar ese consumidor.
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
                             ProcessId = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlPcd_Rollo select pp.Proceso_Id).FirstOrDefault(),
                             Process = (from pp in _context.Set<Produccion_Procesos>() where pp.Prod_Id == d.Prod_Id && pp.NumeroRollo_BagPro == d.DtlPcd_Rollo select pp.Proceso.Proceso_Nombre).FirstOrDefault(),
                          };

            if (preload == null) return NotFound();
            else if (preload.Any()) return Ok(preload);
            else return BadRequest();
        }

        [HttpGet("getPreloadIdAsync/{id}")]
        public async Task<ActionResult> GetPreloadIdAsync(long id)
        {
            // ============================================================
            // 1. HEADER DEL PRECARGUE
            // ============================================================

            var preload = await _context.Set<Precargue_Despacho>()
                .AsNoTracking()
                .Where(p => p.Pcd_Id == id)
                .Select(p => new
                {
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
                    Client = p.Cliente.Cli_Nombre
                })
                .FirstOrDefaultAsync();

            if (preload == null)
            {
                return NotFound();
            }


            // ============================================================
            // 2. DETALLES DEL PRECARGUE
            // ============================================================

            var details = await _context.Set<Detalles_PrecargueDespacho>()
                .AsNoTracking()
                .Where(d => d.Pcd_Id == id)
                .Select(d => new
                {
                    Roll = d.DtlPcd_Rollo,
                    Item = d.Prod_Id,
                    Reference = d.Producto.Prod_Nombre,
                    Quantity = d.DtlPcd_Cantidad,
                    Presentation = d.UndMed_Id
                })
                .ToListAsync();

            if (details.Count == 0)
            {
                return BadRequest();
            }


            // ============================================================
            // 3. OBTENER LAS COMBINACIONES ROLLO + PRODUCTO
            // ============================================================

            var combinations = details
                .Select(x => new
                {
                    x.Roll,
                    x.Item
                })
                .Distinct()
                .ToList();


            // ============================================================
            // 4. PRODUCCIÓN
            // ============================================================

            var rolls = combinations
                .Select(x => x.Roll)
                .Distinct()
                .ToList();

            var products = combinations
                .Select(x => x.Item)
                .Distinct()
                .ToList();


            var production = await _context.Set<Produccion_Procesos>()
                .AsNoTracking()
                .Where(pp =>
                    rolls.Contains(pp.NumeroRollo_BagPro) &&
                    products.Contains(pp.Prod_Id))
                // Orden explícito para que g.First() (paso 5) tome siempre el mismo registro cuando
                // un rollo+producto tenga varios registros de producción: el de menor Id.
                .OrderBy(pp => pp.Id)
                .Select(pp => new
                {
                    Roll = pp.NumeroRollo_BagPro,
                    Item = pp.Prod_Id,

                    OT = pp.OT,
                    Weight = pp.Peso_Bruto,
                    NetWeight = pp.Peso_Neto,

                    ProcessId = pp.Proceso_Id,
                    Process = pp.Proceso.Proceso_Nombre,

                    Numero_Rollo = pp.Numero_Rollo
                })
                .ToListAsync();


            // ============================================================
            // 5. CONVERTIR PRODUCCIÓN EN DICCIONARIO
            // ============================================================

            var productionDict = production
                .GroupBy(x => new
                {
                    x.Roll,
                    x.Item
                })
                .ToDictionary(
                    g => (g.Key.Roll, g.Key.Item),
                    g => g.First()
                );


            // ============================================================
            // 6. OBTENER UBICACIONES
            // ============================================================

            var locations = await (
                from pp in _context.Set<Produccion_Procesos>().AsNoTracking()

                join dt in _context.Set<DetalleEntradaRollo_Producto>().AsNoTracking()
                    on pp.Numero_Rollo equals dt.Rollo_Id

                join e in _context.Set<EntradaRollo_Producto>().AsNoTracking()
                    on dt.EntRolloProd_Id equals e.EntRolloProd_Id

                where rolls.Contains(pp.NumeroRollo_BagPro)
                      && e.EntRolloProd_Id >= 28512

                orderby e.EntRolloProd_Id descending

                select new
                {
                    Roll = pp.NumeroRollo_BagPro,
                    Location = e.EntRolloProd_Observacion,
                    EntryId = e.EntRolloProd_Id
                }
            ).ToListAsync();


            // ============================================================
            // 7. OBTENER LA ÚLTIMA UBICACIÓN POR ROLLO
            // ============================================================

            var locationDict = locations
                .GroupBy(x => x.Roll)
                .ToDictionary(
                    g => g.Key,
                    g => g.First().Location
                );


            // ============================================================
            // 8. ARMAR RESULTADO
            // ============================================================

            var result = details.Select(detail =>
            {
                productionDict.TryGetValue(
                    (detail.Roll, detail.Item),
                    out var prod
                );

                locationDict.TryGetValue(
                    detail.Roll,
                    out var location
                );

                return new
                {
                    // ============================
                    // Header
                    // ============================

                    preload.Movement,
                    preload.OF,

                    preload.Date1,
                    preload.Hour1,

                    preload.Date2,
                    preload.Hour2,

                    preload.UserId1,
                    preload.UserId2,

                    preload.User1,
                    preload.User2,

                    preload.Observation1,
                    preload.Observation2,

                    preload.StatusId,
                    preload.Status,

                    preload.IdClient,
                    preload.Client,

                    // ============================
                    // Details
                    // ============================

                    detail.Roll,
                    detail.Item,
                    detail.Reference,
                    detail.Quantity,
                    detail.Presentation,

                    // ============================
                    // Production
                    // ============================

                    OT = prod?.OT,

                    Weight = prod?.Weight,

                    NetWeight = prod?.NetWeight,

                    ProcessId = prod?.ProcessId,

                    Process = prod?.Process,

                    // ============================
                    // Location
                    // ============================

                    Ubication = location
                };
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un precargue en proceso (estado 2) para retomarlo desde el módulo de precargue de despacho.
        /// Devuelve el encabezado y los rollos con la misma forma que getInformationDispatch, más el estado actual de cada rollo.
        /// </summary>
        /// <param name="id">Número del precargue.</param>
        /// <returns>200 con { header, rolls } (rolls puede estar vacío); 400 si no está en proceso; 404 si no existe.</returns>
        [HttpGet("getPreloadToResume/{id}")]
        public async Task<ActionResult> GetPreloadToResume(long id)
        {
            var header = await _context.Set<Precargue_Despacho>()
                .AsNoTracking()
                .Where(p => p.Pcd_Id == id)
                .Select(p => new
                {
                    Movement = p.Pcd_Id,
                    StatusId = p.Estado_Id,
                    IdClient = p.Cli_Id,
                    Client = p.Cliente.Cli_Nombre,
                    SalesId = p.Usua_Vendedor,
                    Sales = p.Usuario3.Usua_Nombre,
                    Observation = p.Pcd_Observacion
                })
                .FirstOrDefaultAsync();

            if (header == null) return NotFound("El precargue no fue encontrado.");
            if (header.StatusId != 2) return BadRequest("Solo se pueden retomar precargues en proceso.");

            var details = await _context.Set<Detalles_PrecargueDespacho>()
                .AsNoTracking()
                .Where(d => d.Pcd_Id == id)
                .Select(d => new
                {
                    Roll = d.DtlPcd_Rollo,
                    Item = d.Prod_Id,
                    Reference = d.Producto.Prod_Nombre,
                    Qty = d.DtlPcd_Cantidad,
                    Unit = d.UndMed_Id,
                    d.DtlPcd_OT,
                    d.DtlPcd_PesoBruto
                })
                .ToListAsync();

            var rolls = details.Select(d => d.Roll).Distinct().ToList();
            var items = details.Select(d => d.Item).Distinct().ToList();
            string[] process = { "SELLA", "EMP", "EXT" };

            // Una sola consulta de producción; por rollo + producto se toma el registro más reciente, como en getInformationDispatch.
            var production = (await _context.Set<Produccion_Procesos>()
                .AsNoTracking()
                .Where(pp => rolls.Contains(pp.NumeroRollo_BagPro) && items.Contains(pp.Prod_Id) && process.Contains(pp.Proceso_Id))
                .OrderByDescending(pp => pp.Id)
                .Select(pp => new
                {
                    Roll = pp.NumeroRollo_BagPro,
                    Item = pp.Prod_Id,
                    RollPl = pp.Numero_Rollo,
                    Weight = pp.Peso_Bruto,
                    pp.OT,
                    ProcessId = pp.Proceso_Id,
                    Process = pp.Proceso.Proceso_Nombre,
                    Price = pp.PrecioVenta_Producto,
                    CurrentStatus = pp.Estado_Rollo
                })
                .ToListAsync())
                .GroupBy(pp => (pp.Roll, pp.Item))
                .ToDictionary(g => g.Key, g => g.First());

            var result = details.Select(d =>
            {
                production.TryGetValue((d.Roll, d.Item), out var prod);
                return new
                {
                    d.Roll,
                    RollPl = prod?.RollPl,
                    d.Item,
                    d.Reference,
                    header.IdClient,
                    header.Client,
                    d.Qty,
                    Weight = d.DtlPcd_PesoBruto ?? prod?.Weight,
                    d.Unit,
                    OT = d.DtlPcd_OT ?? prod?.OT,
                    ProcessId = prod?.ProcessId,
                    Process = prod?.Process,
                    Price = prod?.Price,
                    CurrentStatus = prod?.CurrentStatus
                };
            }).ToList();

            return Ok(new { header, rolls = result });
        }

        //Función para filtrar los precargues en mov. precargue rollos. 
        [HttpGet("getMovementsPreload/{date1}/{date2}")]
        public async Task <ActionResult> getMovementsPreload(DateTime date1, DateTime date2, long? client = null, int? status = null, long? id = null, long? sales = null)
        {
            if (date1 > date2) return BadRequest("La fecha inicial no puede ser mayor que la fecha final.");

            var preload = await (from p in _context.Set<Precargue_Despacho>()
                          where
                          p.Pcd_FechaCrea >= date1 &&
                          p.Pcd_FechaCrea <= date2 &&
                          (client == null || p.Cli_Id == client) &&
                          (status == null || p.Estado_Id == status) &&
                          (id == null || p.Pcd_Id == id) &&
                          (sales == null || p.Usua_Vendedor == sales)
                          select new
                          {
                              //Header
                              Movement = p.Pcd_Id,
                              OF = p.OF_Id, 
                              Date1 = p.Pcd_FechaCrea,
                              Hour1 = p.Pcd_HoraCrea,
                              Date2 = p.Pcd_FechaModifica, // validar si fechacrea = fechamodifica, poner vacio, sino, poner fecha modificacion en el front-end. *nueva: si ambas fechas y ambas horas son iguales, no ponga nada
                              Hour2 = p.Pcd_HoraModifica, // como la hora modifica solo sale cuando salga la fecha modifica pues solo envia la hora modifica sin validar.
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
                              SalesId = p.Usua_Vendedor,
                              Sales = p.Usuario3.Usua_Nombre,
                          }).ToListAsync();

            return Ok(preload);
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

        /// <summary>Roles autorizados para cambiar el cliente de un precargue de despacho.</summary>
        private static readonly HashSet<string> RolesEditanClientePrecargue = new() { "1", "97", "10", "6", "96" };

        /// <summary>
        /// Cambia el cliente de un precargue de despacho y actualiza el vendedor al asesor del cliente nuevo.
        /// Permitido en cualquier estado excepto anulado (3) y solo para los roles 1, 97, 10, 6 y 96.
        /// </summary>
        /// <param name="id">Número del precargue.</param>
        /// <param name="idClient">Identificador del cliente nuevo.</param>
        /// <param name="sales">Identificador del vendedor (usuario) asociado al cliente nuevo.</param>
        /// <returns>200 si se actualiza; 400 si los datos no son válidos o el precargue está anulado;
        /// 403 si el rol no está autorizado; 404 si el precargue no existe.</returns>
        [HttpPut("editPreloadClientName/{id}/{idClient}")]
        public async Task<IActionResult> EditPreloadClientName(string id, string idClient, [FromQuery] long? sales = null)
        {
            if (!RolesEditanClientePrecargue.Contains(User.FindFirstValue(ClaimTypes.Role) ?? string.Empty))
                return Forbid();

            if (string.IsNullOrWhiteSpace(id) || !long.TryParse(id, out long preloadId))
                return BadRequest("El ID del precargue no es válido.");
            if (string.IsNullOrWhiteSpace(idClient) || !long.TryParse(idClient, out long clientId))
                return BadRequest("El ID del cliente no es válido.");
            if (sales == null)
                return BadRequest("El vendedor del cliente es requerido.");
            if (!await _context.Usuarios.AnyAsync(u => u.Usua_Id == sales))
                return BadRequest($"El vendedor {sales} asociado al cliente no existe en Plasticaribe.");

            var preload = await _context.Precargue_Despacho.FirstOrDefaultAsync(pcd => pcd.Pcd_Id == preloadId);
            if (preload == null) return NotFound("El precargue no fue encontrado con los datos ingresados.");
            if (preload.Estado_Id == 3) return BadRequest("No se puede editar un precargue anulado.");

            preload.Cli_Id = clientId;
            preload.Usua_Vendedor = sales;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
}
