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

        //Consulta que devolverá la entrada de peletizado que se realizó en el sistema en la fecha y hora que se le pasen por parametros.
        [HttpGet("getEntryPeletizado/{date1}/{date2}/{hour}")]
        public ActionResult getEntryBOPP(DateTime date1, DateTime date2, string hour)
        {
            var entry = from ing in _context.Set<Ingreso_Peletizado>()
                        where ing.IngPel_FechaIngreso >= date1 &&
                        ing.IngPel_FechaIngreso <= date2 &&
                        ing.IngPel_HoraIngreso == hour
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
        public ActionResult getMovementsPeletizado(DateTime date1, DateTime date2, string? mp = "", string? ot = "", string? roll = "")
        {
            var entry = from ing in _context.Set<Ingreso_Peletizado>()
                        where ing.IngPel_FechaIngreso >= date1 &&
                        ing.IngPel_FechaIngreso <= date2 && 
                        (mp != "" ? ing.MatPri_Id == Convert.ToInt64(mp) : ing.MatPri_Id.ToString().Contains(mp)) &&
                        (ot != "" ? ing.OT == Convert.ToInt64(ot) : ing.OT.ToString().Contains(ot)) &&
                        (roll != "" ? ing.Rollo_Id == Convert.ToInt64(roll) : ing.Rollo_Id.ToString().Contains(roll) || ing.Rollo_Id == null)
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

            if (entry != null) return Ok(entry);
            else if (entry == null) return NotFound();
            else return BadRequest();
        }

        //Consulta que devolverá el inventario de peletizado agrupado por recuperado.
        [HttpGet("getStockPele_Grouped")]
        public ActionResult getStockPele_Group()
        {
            var inventory = from ing in _context.Set<Ingreso_Peletizado>()
                        from mp in _context.Set<Materia_Prima>()
                        where
                        ing.MatPri_Id == mp.MatPri_Id &&
                        ing.Estado_Id == 19
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
            var inventory = from ing in _context.Set<Ingreso_Peletizado>()
                            from mp in _context.Set<Materia_Prima>()
                            where
                            ing.MatPri_Id == mp.MatPri_Id &&
                            ing.Estado_Id == 19 &&
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
                                }
                            };

            if (inventory.Count() > 0) return Ok(inventory);
            else return BadRequest();
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

}
