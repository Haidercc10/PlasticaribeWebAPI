using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using static Grpc.Core.Metadata;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ingreso_PeletizadoController : ControllerBase
    {
        private readonly dataContext _context;

        public Ingreso_PeletizadoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Ingreso_Peletizado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Ingreso_Peletizado>>> GetIngreso_Peletizado()
        {
            return await _context.Ingreso_Peletizado.ToListAsync();
        }

        // GET: api/Ingreso_Peletizado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Ingreso_Peletizado>> GetIngreso_Peletizado(long id)
        {
            var area = await _context.Ingreso_Peletizado.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        //Consulta que devolverá el ultimo codigo de entrada de peletizado.
        [HttpGet("getLastCodeEntry")]
        public ActionResult getLastCodeEntry()
        {
            var lastCode = (from ing in _context.Set<Models.Ingreso_Peletizado>() orderby ing.IngPel_Codigo descending select ing.IngPel_Codigo == 0 ? 0 : ing.IngPel_Codigo ).FirstOrDefault();
            return Ok(lastCode);
        }

            //Consulta que devolverá la entrada de peletizado que se realizó en el sistema en la fecha y hora que se le pasen por parametros.
        [HttpGet("getEntryPeletizado/{date1}/{date2}/{hour}")]
        public ActionResult getEntryBOPP(DateTime date1, DateTime date2, string hour)
        {
            var entry = from ing in _context.Set<Ingreso_Peletizado>()
                        where ing.IngPel_FechaIngreso >= date1 &&
                        ing.IngPel_FechaIngreso <= date2 &&
                        ing.IngPel_HoraIngreso == hour
                        orderby ing.IngPel_Id ascending
                        select new
                        {
                            Entries = ing,
                            Users = new
                            {
                                User = ing.Usua_Id,
                                NameUser = ing.Usuario.Usua_Nombre,
                                User2 = ing.Usua_Modifica,
                                NameUser2 = ing.Usuario2.Usua_Nombre,
                            },
                            Fails = new
                            {
                                Id = ing.Falla_Id,
                                Fail = ing.Fallas.Falla_Nombre
                            },
                            Type_Recovery = new
                            {
                                Id = ing.TpRecu_Id,
                                Recovery = ing.Tipo_Recuperado.TpRecu_Nombre
                            },
                            Material = new
                            {
                                Id = ing.Material_Id,
                                Material = ing.Materiales.Material_Nombre
                            },
                            MatPrimas = new
                            {
                                Id = ing.MatPri_Id,
                                MatPrima = ing.MatPrima.MatPri_Nombre,
                                Presentation = ing.MatPrima.UndMed_Id,
                            },
                            Statuses = new
                            {
                                Id = ing.Estado_Id,
                                Status = ing.Estados.Estado_Nombre,
                            },
                            Process = new
                            {
                                Id = ing.Proceso_Id,
                                ProcessName = ing.Proceso.Proceso_Nombre,
                            },
                            Product = new
                            {
                                Item = ing.Prod_Id,
                                Reference = ing.Producto.Prod_Nombre,
                            }
                        };

            if (entry.Count() > 0) return Ok(entry);
            else return BadRequest();
        }

        //Consulta que devolverá la entrada de peletizado que se realizó en el sistema en la fecha y hora que se le pasen por parametros.
        [HttpGet("getMovementsPeletizado/{date1}/{date2}")]
        public ActionResult getMovementsPeletizado(DateTime date1, DateTime date2, string? mp = "", string? ot = "", string? status = "", string? typeMov = "")
        {
            var entries = from ing in _context.Set<Ingreso_Peletizado>()
                          where ing.IngPel_FechaIngreso >= date1 &&
                          ing.IngPel_FechaIngreso <= date2 &&
                          (mp != "" ? ing.MatPri_Id == Convert.ToInt64(mp) : ing.MatPri_Id.ToString().Contains(mp)) &&
                          (ot != "" ? ing.OT == Convert.ToInt64(ot) : ing.OT.ToString().Contains(ot)) &&
                          (status != "" ? ing.Estado_Id == Convert.ToInt64(status) : ing.Estado_Id.ToString().Contains(status)) &&
                          ("ENTRADA".Contains(typeMov) ? true : false)
                          select new
                          {
                              Mov = new
                              {
                                  Code = ing.IngPel_Codigo,
                                  Id = ing.IngPel_Id,
                                  Date = ing.IngPel_FechaIngreso.Value,
                                  Hour = ing.IngPel_HoraIngreso,
                                  DateOk = ing.IngPel_FechaModifica.Value,
                                  HourOk = ing.IngPel_HoraModifica,
                                  Observation = ing.IngPel_Observacion,
                                  Status_Id = ing.Estado_Id,
                                  Qty = ing.IngPel_Cantidad, 
                                  InitialQty = ing.IngPel_CantInicial,
                                  Recovery_Id = ing.TpRecu_Id, 
                                  Presentation = ing.Tipo_Recuperado.TpRecu_Nombre,
                              },
                              Status = ing.Estados.Estado_Nombre,
                              NameUser = ing.Usuario.Usua_Nombre,
                              NameUser2 = ing.Usuario2.Usua_Nombre,
                              Recovery = ing.Tipo_Recuperado.TpRecu_Nombre,
                              MatPrimas = new
                              {
                                  Id = ing.MatPri_Id,
                                  MatPrima = ing.MatPrima.MatPri_Nombre,
                                  Presentation = ing.MatPrima.UndMed_Id,
                              },
                              TypeMov = Convert.ToString("ENTRADA"),
                          };

            var outputs = from s in _context.Set<Salidas_Peletizado>()
                          where s.SalPel_Fecha >= date1 &&
                          s.SalPel_Fecha <= date2 &&
                          (mp != "" ? s.MatPri_Id == Convert.ToInt64(mp) : s.MatPri_Id.ToString().Contains(mp)) &&
                          (status != "" ? s.Estado_Id == Convert.ToInt64(status) : s.Estado_Id.ToString().Contains(status)) &&
                          ("SALIDA".Contains(typeMov) ? true : false)
                          select new 
                          {
                            Mov = new {
                                Code = s.SalPel_Id,
                                Id = s.SalPel_Id,
                                Date = s.SalPel_Fecha,
                                Hour = s.SalPel_Hora,
                                DateOk = s.SalPel_FechaAprobado,
                                HourOk = s.SalPel_HoraAprobado,
                                Observation = s.SalPel_Observacion,
                                Status_Id = s.Estado_Id,
                                Qty = s.SalPel_Peso,
                                InitialQty = s.SalPel_Peso,
                                Recovery_Id = Convert.ToString(""),
                                Presentation = Convert.ToString(""),
                            },
                            Status = s.Estados.Estado_Nombre,
                            NameUser = s.Usuario.Usua_Nombre,
                            NameUser2 = s.Usuario2.Usua_Nombre,
                            Recovery = "",
                            MatPrimas = new
                            {
                                Id = s.MatPri_Id,
                                MatPrima = s.MatPrima.MatPri_Nombre,
                                Presentation = s.MatPrima.UndMed_Id,
                            },
                            TypeMov = Convert.ToString("SALIDA"),
                          };
        
           return Ok(entries.Concat(outputs));
        }

        //Consulta que devolverá el inventario de peletizado agrupado por recuperado.
        [HttpGet("getStockPele_Grouped")]
        public ActionResult getStockPele_Group()
        {
            int[] statusAvailables = { 19, 42 };

            var inventory = from ing in _context.Set<Ingreso_Peletizado>()
                        from mp in _context.Set<Materia_Prima>()
                        where
                        ing.MatPri_Id == mp.MatPri_Id &&
                        statusAvailables.Contains(ing.Estado_Id)
                        group ing by new {
                            Id = ing.MatPri_Id,
                            MatPrima = mp.MatPri_Nombre,
                            Presentation = mp.UndMed_Id,
                            Price = mp.MatPri_Precio,
                            Category_Id = mp.CatMP_Id,
                            Category = mp.CatMP.CatMP_Nombre
                        } into ing
                        select new
                        {
                            Id_MatPrima = ing.Key.Id,
                            MatPrima = ing.Key.MatPrima,
                            Stock = ing.Sum(x => x.IngPel_Cantidad),
                            Price = ing.Key.Price,
                            Subtotal = ing.Sum(x => x.IngPel_Cantidad) * ing.Key.Price,
                            Presentation = ing.Key.Presentation,
                            Category_Id = ing.Key.Category_Id,
                            Category = ing.Key.Category,
                        };

            if (inventory.Count() > 0) return Ok(inventory);
            else return BadRequest();
        }

        //Consulta que devolverá el inventario de peletizado detallado por recuperado.
        [HttpGet("getStockPele_Details/{matPrima}")]
        public ActionResult getStockPele_Details(int matPrima)
        {
            int[] statusAvailables = { 19, 42 };

            var inventory = from ing in _context.Set<Ingreso_Peletizado>()
                            from mp in _context.Set<Materia_Prima>()
                            where
                            ing.MatPri_Id == mp.MatPri_Id &&
                            statusAvailables.Contains(ing.Estado_Id) &&
                            ing.MatPri_Id == matPrima
                            select new
                            {
                                Entries = ing,
                                Users = new
                                {
                                    User = ing.Usua_Id,
                                    NameUser = ing.Usuario.Usua_Nombre,
                                    User2 = ing.Usua_Modifica,
                                    NameUser2 = ing.Usuario2.Usua_Nombre,
                                },
                                Fails = new
                                {
                                    Id = ing.Falla_Id,
                                    Fail = ing.Fallas.Falla_Nombre
                                },
                                Type_Recovery = new
                                {
                                    Id = ing.TpRecu_Id,
                                    Recovery = ing.Tipo_Recuperado.TpRecu_Nombre
                                },
                                Material = new
                                {
                                    Id = ing.Material_Id,
                                    Material = ing.Materiales.Material_Nombre
                                },
                                MatPrimas = new
                                {
                                    Id = ing.MatPri_Id,
                                    MatPrima = ing.MatPrima.MatPri_Nombre,
                                    Presentation = ing.MatPrima.UndMed_Id,
                                },
                                Statuses = new
                                {
                                    Id = ing.Estado_Id,
                                    Status = ing.Estados.Estado_Nombre,
                                },
                                Process = new
                                {
                                    Id = ing.Proceso_Id,
                                    Status = ing.Proceso.Proceso_Nombre,
                                },
                                Product = new
                                {
                                    Item = ing.Prod_Id,
                                    Reference = ing.Producto.Prod_Nombre,
                                },
                                Weight = ing.IngPel_Cantidad,
                                Weight2 = ing.IngPel_Cantidad,
                            };

            if (inventory.Count() > 0) return Ok(inventory);
            else return BadRequest();
        }

        //Consulta que devolverá el inventario de peletizado agrupado por recuperado.
        [HttpGet("getStockForRecovery/{mat}")]
        public ActionResult getStockGrouped(int mat)
        {
            int[] statusAvailables = { 19, 42 };

            var inventory = from ing in _context.Set<Ingreso_Peletizado>()
                            from mp in _context.Set<Materia_Prima>()
                            where
                            ing.MatPri_Id == mp.MatPri_Id &&
                            statusAvailables.Contains(ing.Estado_Id) &&
                            ing.MatPri_Id == mat
                            group ing by new
                            {
                                Id = ing.MatPri_Id,
                                MatPrima = mp.MatPri_Nombre,
                                Presentation = mp.UndMed_Id,
                                Price = mp.MatPri_Precio,
                                Category_Id = mp.CatMP_Id,
                                Category = mp.CatMP.CatMP_Nombre
                            } into ing
                            select new
                            {
                                Id_MatPrima = ing.Key.Id,
                                MatPrima = ing.Key.MatPrima,
                                Stock = ing.Sum(x => x.IngPel_Cantidad),
                                Stock2 = ing.Sum(x => x.IngPel_Cantidad),
                                Price = ing.Key.Price,
                                Subtotal = ing.Sum(x => x.IngPel_Cantidad) * ing.Key.Price,
                                Presentation = ing.Key.Presentation,
                                Category_Id = ing.Key.Category_Id,
                                Category = ing.Key.Category,
                            };

            if (inventory.Count() > 0) return Ok(inventory);
            else return BadRequest();
        }

        [HttpPut("putEntryPeletizado")]
        async public Task<IActionResult> putEntryPeletizado([FromBody] List<Peletizado> pelets)
        {
            int count = 0;

            foreach (var item in pelets)
            {
                var entry = (from ing in _context.Set<Ingreso_Peletizado>() where ing.IngPel_Id == item.code select ing).FirstOrDefault();
                entry.IngPel_Cantidad -= item.quantity;
                entry.Estado_Id = entry.IngPel_Cantidad == Convert.ToDecimal(0) ? 23 : 42;
                _context.Entry(entry).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Ingreso_PeletizadoExists(item.code)) return NotFound();
                    else throw;
                }
                count++;
                if (pelets.Count() == count) return NoContent();
            }
            return NoContent();
        }

        // PUT: api/Ingreso_Peletizado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngreso_Peletizado(long id, Models.Ingreso_Peletizado ingreso_Peletizado)
        {
            if (id != ingreso_Peletizado.IngPel_Id)
            {
                return BadRequest();
            }

            _context.Entry(ingreso_Peletizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ingreso_PeletizadoExists(id))
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

        // POST: api/Ingreso_Peletizado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingreso_Peletizado>> PostIngreso_Peletizado(Models.Ingreso_Peletizado ingreso_Peletizado)
        {
            _context.Ingreso_Peletizado.Add(ingreso_Peletizado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngreso_Peletizado", new { id = ingreso_Peletizado.IngPel_Id }, ingreso_Peletizado);
        }

        // DELETE: api/Ingreso_Peletizado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso_Peletizado(long id)
        {
            var ingreso_Peletizado = await _context.Ingreso_Peletizado.FindAsync(id);
            if (ingreso_Peletizado == null)
            {
                return NotFound();
            }

            _context.Ingreso_Peletizado.Remove(ingreso_Peletizado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Ingreso_PeletizadoExists(long id)
        {
            return _context.Ingreso_Peletizado.Any(e => e.IngPel_Id == id);
        }
    }

    public class Peletizado
    { 
        public long code { get; set; }

        [Precision(18, 2)]
        public decimal quantity { get; set; }

        public string typeRecovery { get; set; }

        public string unit { get; set; }
    }

}
