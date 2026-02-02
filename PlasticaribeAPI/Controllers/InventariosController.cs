using Aspose.Imaging.FileFormats.Cmx.ObjectModel.Enums;
using Aspose.Imaging.FileFormats.Tga;
using Intercom.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Collections.Generic;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {

        private readonly dataContext _context;

        public InventariosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventarios>>> GetInventarios()
        {
            return await _context.Inventarios.ToListAsync();
        }

        // GET: api/Inventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventarios>> GetInventarios(string id)
        {
            var Inventarios = await _context.Inventarios.FindAsync(id);

            if (Inventarios == null)
            {
                return NotFound();
            }

            return Inventarios;
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Funcion para obtener el inventario fisico
        [HttpGet("getInventorySnapshot/{inventory}")]
        public ActionResult<IEnumerable<InventorySnapshotDto>> getInventorySnapshot(int inventory)
        {

            var tomaFisicaAgrupada = (from tf in _context.Set<Toma_Fisica_Inventario>()
                                     join t in _context.Set<Toma_Fisica>()
                                         on tf.Toma_Id equals t.Toma_Id
                                     where t.InvSnap_Id == inventory
                                     group tf by tf.Prod_Id into g
                                     select new
                                     {
                                        Prod_Id = g.Key,
                                        PhysicalCount = g.Sum(x => x.Tfi_CantidadReal),
                                        PhysicalRollos = g.Count()
                                     }).ToList();


            var snapshotBase =  (from i in _context.Set<Inventarios>()
                                join p in _context.Set<Producto>()
                                    on i.Prod_Id equals p.Prod_Id
                                where i.InvSnap_Id == inventory
                                group i by new
                                {
                                    i.Prod_Id,
                                    p.Prod_Nombre,
                                    i.Presentacion,
                                    i.Inv_PrecioVenta,
                                    i.Inv_Existencias,
                                } into g
                                select new
                                {
                                    g.Key.Prod_Id,
                                    g.Key.Prod_Nombre,
                                    g.Key.Presentacion,
                                    g.Key.Inv_PrecioVenta,

                                    Stock = g.Key.Inv_Existencias,
                                    Quantity = g.Sum(x => x.Inv_Cantidad),
                                    GrossWeight = g.Sum(x => x.Inv_PesoBruto),
                                    Count = g.Count()
                                }).ToList();

            var snapshot =
                            (from s in snapshotBase
                            join tf in tomaFisicaAgrupada
                                on s.Prod_Id equals tf.Prod_Id into tfJoin
                            from tf in tfJoin.DefaultIfEmpty()
                            select new InventorySnapshotDto
                            {
                                Item = s.Prod_Id,
                                Reference = s.Prod_Nombre,

                                Stock = s.Stock,
                                Quantity = s.Quantity,
                                GrossWeight = s.GrossWeight,
                                Price = s.Inv_PrecioVenta,
                                Unit = s.Presentacion,
                                Count = s.Count,

                                PhysicalQty = tf != null ? tf.PhysicalCount : 0,
                                PhysicalRollos = tf != null ? tf.PhysicalRollos : 0,

                                Diference = s.Stock - (tf != null ? tf.PhysicalCount : 0),
                                Diference2 = s.Quantity - (tf != null ? tf.PhysicalCount : 0),
                                Diference3 = s.Stock - s.Quantity,

                                Subtotal = s.Stock * s.Inv_PrecioVenta,
                                DiferenceUnits = s.Count - (tf != null ? tf.PhysicalRollos : 0),
                                SubtotalDetailed = s.Quantity * s.Inv_PrecioVenta,
                                SubTotalPhysical = (tf != null ? tf.PhysicalCount : 0) * s.Inv_PrecioVenta
                            }).ToList();

            var soloTomaFisica =
                                (from t in _context.Set<Toma_Fisica>()
                                join tfi in _context.Set<Toma_Fisica_Inventario>()
                                    on t.Toma_Id equals tfi.Toma_Id
                                join e in _context.Set<Existencia_Productos>()
                                    on tfi.Prod_Id equals e.Prod_Id
                                join p in _context.Set<Producto>()
                                    on tfi.Prod_Id equals p.Prod_Id

                                join i in _context.Set<Inventarios>()
                                        .Where(x => x.InvSnap_Id == inventory)
                                    on tfi.Prod_Id equals i.Prod_Id into invJoin
                                from inv in invJoin.DefaultIfEmpty()

                                where t.InvSnap_Id == inventory
                                   && inv == null
                                group tfi by new
                                {
                                    tfi.Prod_Id,
                                    p.Prod_Nombre,
                                    tfi.Tfi_PrecioVenta,
                                    tfi.Presentacion
                                } into g
                                select new InventorySnapshotDto
                                {
                                    Item = g.Key.Prod_Id,
                                    Reference = g.Key.Prod_Nombre,

                                    Stock = 0,
                                    Quantity = 0,
                                    GrossWeight = 0,
                                    Price = g.Key.Tfi_PrecioVenta,
                                    Unit = g.Key.Presentacion,
                                    Count = 0,

                                    PhysicalQty = g.Sum(x => x.Tfi_CantidadReal),
                                    PhysicalRollos = g.Count(),

                                    Diference = -g.Sum(x => x.Tfi_CantidadReal),
                                    Diference2 = -g.Sum(x => x.Tfi_CantidadReal),
                                    Diference3 = 0,

                                    Subtotal = g.Key.Tfi_PrecioVenta * g.Sum(x => x.Tfi_CantidadReal),
                                    DiferenceUnits = -g.Count(),
                                    SubtotalDetailed = g.Key.Tfi_PrecioVenta * g.Sum(x => x.Tfi_CantidadReal),
                                    SubTotalPhysical = g.Key.Tfi_PrecioVenta * g.Sum(x => x.Tfi_CantidadReal)
                                }).ToList();

            var result = snapshot.Concat(soloTomaFisica);

            return Ok(result);
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Funcion para obtener el inventario fisico detallado por item
        [HttpGet("getInventorySnapshotForItem/{item}/{unit}")]
        public ActionResult getInventorySnapshotDetailed(long item, string unit)
        {
            var snapshot =
                from i in _context.Set<Inventarios>()
                join p in _context.Set<Producto>() on i.Prod_Id equals p.Prod_Id
                join c in _context.Set<Clientes>() on i.Cli_Id equals c.Cli_Id
                where p.Prod_Id == item
                && i.Presentacion == unit
                select new
                {
                    Item = p.Prod_Id,
                    Reference = p.Prod_Nombre,
                    Label = i.Inv_Etiqueta,
                    LabelPL = i.Inv_NumeroRollo,
                    Ot = i.Inv_OT,
                    Client = c.Cli_Nombre,
                    Quantity = i.Inv_Cantidad,
                    GrossWeight = i.Inv_PesoBruto,
                    Price = i.Inv_PrecioVenta,
                    Unit = i.Presentacion,
                    Process = i.Proceso_Id,
                    Location = i.Inv_Ubicacion,
                    Date = i.Inv_Fecha,
                    SubTotal = i.Inv_Cantidad * i.Inv_PrecioVenta
                };

            return Ok(snapshot);
        }

        // PUT: api/Inventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarios(long id, Inventarios Inventarios)
        {
            if (id != Inventarios.Inv_Id)
            {
                return BadRequest();
            }

            _context.Entry(Inventarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventariosExists(id))
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

        // POST: api/Inventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventarios>> PostInventarios(Inventarios Inventarios)
        {
            _context.Inventarios.Add(Inventarios);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InventariosExists(Inventarios.Inv_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInventarios", new { id = Inventarios.Inv_Id }, Inventarios);
        }

        // DELETE: api/Inventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventarios(string id)
        {
            var Inventarios = await _context.Inventarios.FindAsync(id);
            if (Inventarios == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(Inventarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventariosExists(long id)
        {
            return _context.Inventarios.Any(e => e.Inv_Id == id);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

public class InventorySnapshotDto
{
    public long Item { get; set; }
    public string Reference { get; set; }
    public decimal Stock { get; set; }
    public decimal Quantity { get; set; }
    public decimal GrossWeight { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; }
    public int Count { get; set; }
    public decimal PhysicalQty { get; set; }
    public int PhysicalRollos { get; set; }
    public decimal Diference { get; set; }
    public decimal Diference2 { get; set; }
    public decimal Diference3 { get; set; }
    public decimal Subtotal { get; set; }
    public int DiferenceUnits { get; set; }
    public decimal SubtotalDetailed { get; set; }
    public decimal SubTotalPhysical { get; set; }
}
