using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DetallePreEntrega_RolloDespachoController : ControllerBase
    {
        private readonly dataContext _context;

        public DetallePreEntrega_RolloDespachoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetallePreEntrega_RolloDespacho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePreEntrega_RolloDespacho>>> GetDetallesPreEntrega_RollosDespacho()
        {
            return await _context.DetallesPreEntrega_RollosDespacho.ToListAsync();
        }

        // GET: api/DetallePreEntrega_RolloDespacho/5
        [HttpGet("VerificarRollo/{id}")]
        public ActionResult GetVerificarRollo(long id)
        {
            var con = _context.DetallesPreEntrega_RollosDespacho.Where(x => x.Rollo_Id == id).ToList();
            return Ok(con);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePreEntrega_RolloDespacho>> GetDetallePreEntrega_RolloDespacho(long id)
        {
            var detallePreEntrega_RolloDespacho = await _context.DetallesPreEntrega_RollosDespacho.FindAsync(id);

            if (detallePreEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            return detallePreEntrega_RolloDespacho;
        }

        //Funcion que va a buscar la informacion que aparecerá en el PDF
        [HttpGet("crearPdf/{ot}/{proceso}")]
        public ActionResult crearPdf(long ot, string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from rollo in _context.Set<DetallePreEntrega_RolloDespacho>()
                      from emp in _context.Set<Empresa>()
                      where rollo.PreEntRollo_Id == ot
                            && emp.Empresa_Id == 800188732
                      group rollo by new
                      {
                          rollo.PreEntregaRollo.PreEntRollo_Id,
                          rollo.Prod_Id,
                          rollo.Prod.Prod_Nombre,
                          rollo.Rollo_Id,
                          rollo.UndMed_Rollo,
                          rollo.DtlPreEntRollo_Cantidad,
                          rollo.PreEntregaRollo.PreEntRollo_Fecha,
                          rollo.PreEntregaRollo.Usua_Id,
                          rollo.PreEntregaRollo.Usuario.Usua_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre,
                      }
                      into rollos
                      select new
                      {
                          rollos.Key.PreEntRollo_Id,
                          rollos.Key.Prod_Id,
                          rollos.Key.Prod_Nombre,
                          rollos.Key.Rollo_Id,
                          rollos.Key.UndMed_Rollo,
                          rollos.Key.DtlPreEntRollo_Cantidad,
                          rollos.Key.PreEntRollo_Fecha,
                          Creador = rollos.Key.Usua_Id,
                          NombreCreador = rollos.Key.Usua_Nombre,
                          cantRollos = rollos.Count(),
                          rollos.Key.Empresa_Id,
                          rollos.Key.Empresa_Ciudad,
                          rollos.Key.Empresa_COdigoPostal,
                          rollos.Key.Empresa_Correo,
                          rollos.Key.Empresa_Direccion,
                          rollos.Key.Empresa_Telefono,
                          rollos.Key.Empresa_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Funcion que va a buscar la informacion que aparecerá en el PDF
        [HttpGet("crearPdf2/{ot}/{proceso}")]
        public ActionResult crearPdf2(long ot, string proceso)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesPreEntrega_RollosDespacho.Where(x => x.PreEntRollo_Id == ot)
                .GroupBy(x => new
                {
                    x.Prod_Id,
                    x.Prod.Prod_Nombre,
                    x.UndMed_Producto
                })
                .Select(x => new
                {
                    x.Key.Prod_Id,
                    x.Key.Prod_Nombre,
                    x.Key.UndMed_Producto,
                    suma = x.Sum(x => x.DtlPreEntRollo_Cantidad),
                    cantRollos = x.Count()
                });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("CrearPDFUltimoID/{id}")]
        public ActionResult Get(long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from dt in _context.Set<DetallePreEntrega_RolloDespacho>()
                      from emp in _context.Set<Empresa>()
                      where dt.PreEntregaRollo.PreEntRollo_Id == id
                            && emp.Empresa_Id == 800188732
                      orderby dt.PreEntregaRollo.PreEntRollo_Id
                      select new
                      {
                          dt.PreEntRollo_Id,
                          dt.PreEntregaRollo.PreEntRollo_Fecha,
                          dt.PreEntregaRollo.PreEntRollo_Observacion,
                          dt.PreEntregaRollo.Usua_Id,
                          dt.PreEntregaRollo.Usuario.Usua_Nombre,
                          dt.Rollo_Id,
                          dt.Prod_Id,
                          dt.Prod.Prod_Nombre,
                          dt.UndMed_Rollo,
                          dt.DtlPreEntRollo_Cantidad,
                          dt.DtlPreEntRollo_OT,
                          dt.Cli_Id,
                          dt.Cliente.Cli_Nombre,
                          dt.Proceso_Id,
                          dt.Proceso.Proceso_Nombre,
                          emp.Empresa_Id,
                          emp.Empresa_Ciudad,
                          emp.Empresa_COdigoPostal,
                          emp.Empresa_Correo,
                          emp.Empresa_Direccion,
                          emp.Empresa_Telefono,
                          emp.Empresa_Nombre
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpPost("getRollos")]
        public IActionResult getRollos([FromBody] List<long> rollos)
        {
            return Ok(from pre in _context.Set<DetallePreEntrega_RolloDespacho>() where rollos.Contains(pre.Rollo_Id) select new { pre.Rollo_Id, pre.Proceso_Id });
        }

        [HttpGet("getRollos_Ingresar/{fechaInicial}/{fechaFinal}/{proceso}")]
        public ActionResult getRollosIngresar(DateTime fechaInicial, DateTime fechaFinal, string proceso, string? ot = "", string? rollo = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var con = from pre in _context.Set<DetallePreEntrega_RolloDespacho>()
                      where pre.PreEntregaRollo.PreEntRollo_Fecha >= fechaInicial
                            && pre.PreEntregaRollo.PreEntRollo_Fecha <= fechaFinal
                            && pre.Proceso_Id == proceso
                            && Convert.ToString(pre.DtlPreEntRollo_OT).Contains(ot)
                            && Convert.ToString(pre.Rollo_Id).Contains(rollo)
                      select new
                      {
                          Orden = pre.DtlPreEntRollo_OT,
                          Rollo = pre.Rollo_Id,
                          Id_Producto = pre.Prod_Id,
                          Producto = pre.Prod.Prod_Nombre,
                          Fecha_Ingreso = pre.PreEntregaRollo.PreEntRollo_Fecha,
                          Cantidad = pre.DtlPreEntRollo_Cantidad,
                          Presentacion = pre.UndMed_Rollo,
                          Proceso = pre.Proceso_Id,
                      };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("getInformactionAboutPreIn_ById/{id}")]
        public ActionResult GetInformactionAboutPreIn_ById(int id)
        {
            var preIn = from pre in _context.Set<PreEntrega_RolloDespacho>()
                        join dtPre in _context.Set<DetallePreEntrega_RolloDespacho>() on pre.PreEntRollo_Id equals dtPre.PreEntRollo_Id
                        where pre.PreEntRollo_Id == id
                        select new
                        {
                            Pre = new {
                                Pre_Id = pre.PreEntRollo_Id,
                                Date = pre.PreEntRollo_Fecha + " " + pre.PreEntRollo_Hora,
                                Observation = pre.PreEntRollo_Observacion,
                                Id_User = pre.Usua_Id,
                                User_Name = pre.Usuario.Usua_Nombre
                            },
                            Details = new
                            {
                                Item = dtPre.Prod_Id,
                                Reference = dtPre.Prod.Prod_Nombre,
                                Quantity = dtPre.DtlPreEntRollo_Cantidad,
                                Presentation = dtPre.UndMed_Producto,
                                Production = dtPre.Rollo_Id,
                                OrderProduction = dtPre.DtlPreEntRollo_OT,
                                Process = dtPre.Proceso_Id,
                                ProcessName = dtPre.Proceso.Proceso_Nombre,
                                Price = (from pp in _context.Set<Produccion_Procesos>()
                                         where pp.Prod_Id == dtPre.Prod_Id &&
                                               pp.NumeroRollo_BagPro == dtPre.Rollo_Id &&
                                               pp.OT == dtPre.DtlPreEntRollo_OT
                                         select pp.PrecioVenta_Producto).FirstOrDefault(),
                            },
                        };
            return preIn.Any() ? Ok(preIn) : NotFound();
        }

        [HttpGet("getInformactionAboutPreInToSendDesp_ById/{id}/{item}")]
        public ActionResult GetInformactionAboutPreInToSendDesp_ById(int id, int item)
        {
            var preIn = from pre in _context.Set<PreEntrega_RolloDespacho>()
                        join dtPre in _context.Set<DetallePreEntrega_RolloDespacho>() on pre.PreEntRollo_Id equals dtPre.PreEntRollo_Id
                        join pp in _context.Set<Produccion_Procesos>() on dtPre.Rollo_Id equals pp.NumeroRollo_BagPro
                        where pre.PreEntRollo_Id == id &&
                              dtPre.Prod_Id == item &&
                              pp.NumeroRollo_BagPro == dtPre.Rollo_Id &&
                              pp.OT == dtPre.DtlPreEntRollo_OT && 
                              pp.Estado_Rollo == 31 &&
                              pp.Envio_Zeus == false
                        select new
                        {
                            Pre = new
                            {
                                Pre_Id = pre.PreEntRollo_Id,
                                Date = pre.PreEntRollo_Fecha + " " + pre.PreEntRollo_Hora,
                                Observation = pre.PreEntRollo_Observacion,
                                Id_User = pre.Usua_Id,
                                User_Name = pre.Usuario.Usua_Nombre
                            },
                            Details = new
                            {
                                Item = dtPre.Prod_Id,
                                Reference = dtPre.Prod.Prod_Nombre,
                                Quantity = dtPre.DtlPreEntRollo_Cantidad,
                                Presentation = dtPre.UndMed_Producto,
                                Production = dtPre.Rollo_Id,
                                ProductionPlasticaribe = pp.Numero_Rollo,
                                OrderProduction = dtPre.DtlPreEntRollo_OT,
                                Process = dtPre.Proceso_Id,
                                ProcessName = dtPre.Proceso.Proceso_Nombre,
                                Price = pp.PrecioVenta_Producto,
                            },
                        };
            return preIn.Any() ? Ok(preIn) : NotFound();
        }

        //Consulta para movimientos de preingresos de producción
        [HttpGet("getPreInProduction/{fechaInicial}/{fechaFinal}")]
        public ActionResult getPreInProduction(DateTime fechaInicial, DateTime fechaFinal, string? process = "", string? ot = "", string? item = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var con = from det in _context.Set<DetallePreEntrega_RolloDespacho>()
                      from pre in _context.Set<PreEntrega_RolloDespacho>()
                      where pre.PreEntRollo_Fecha >= fechaInicial
                            && pre.PreEntRollo_Fecha <= fechaFinal
                            && pre.PreEntRollo_Id == det.PreEntRollo_Id 
                            && Convert.ToString(det.Proceso_Id).Contains(process)
                            && Convert.ToString(det.DtlPreEntRollo_OT).Contains(ot)
                            && Convert.ToString(det.Prod_Id).Contains(item)
                      group pre by new {
                          pre.PreEntRollo_Id,
                          pre.Usua_Id,
                          pre.Usuario.Usua_Nombre,
                          pre.PreEntRollo_Fecha,
                          pre.PreEntRollo_Hora,
                      } into p 
                      select new
                      {
                          Id_PreIngreso = p.Key.PreEntRollo_Id,
                          Id_Usuario = p.Key.Usua_Id,
                          Usuario = p.Key.Usua_Nombre,
                          Fecha_Ingreso = p.Key.PreEntRollo_Fecha,
                          Hora_Ingreso = p.Key.PreEntRollo_Hora,
                          Cantidad = p.Count(),
                      };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (con == null) return BadRequest("No se encontraron resultados de búsqueda");
            return Ok(con);
        }

        // PUT: api/DetallePreEntrega_RolloDespacho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePreEntrega_RolloDespacho(long id, DetallePreEntrega_RolloDespacho detallePreEntrega_RolloDespacho)
        {
            if (id != detallePreEntrega_RolloDespacho.DtlPreEntRollo_Id)
            {
                return BadRequest();
            }

            _context.Entry(detallePreEntrega_RolloDespacho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePreEntrega_RolloDespachoExists(id))
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

        // POST: api/DetallePreEntrega_RolloDespacho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallePreEntrega_RolloDespacho>> PostDetallePreEntrega_RolloDespacho(DetallePreEntrega_RolloDespacho detallePreEntrega_RolloDespacho)
        {
            _context.DetallesPreEntrega_RollosDespacho.Add(detallePreEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallePreEntrega_RolloDespacho", new { id = detallePreEntrega_RolloDespacho.DtlPreEntRollo_Id }, detallePreEntrega_RolloDespacho);
        }

        [HttpPost("postDetailsPreIn")]
        public async Task<ActionResult<DetallePreEntrega_RolloDespacho>> PostDetailsPreIn(DetallePreEntrega_RolloDespacho detallePreEntrega_RolloDespacho)
        {
            _context.DetallesPreEntrega_RollosDespacho.Add(detallePreEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallePreEntrega_RolloDespacho", new { id = detallePreEntrega_RolloDespacho.DtlPreEntRollo_Id }, detallePreEntrega_RolloDespacho);
        }

        // DELETE: api/DetallePreEntrega_RolloDespacho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallePreEntrega_RolloDespacho(long id)
        {
            var detallePreEntrega_RolloDespacho = await _context.DetallesPreEntrega_RollosDespacho.FindAsync(id);
            if (detallePreEntrega_RolloDespacho == null)
            {
                return NotFound();
            }

            _context.DetallesPreEntrega_RollosDespacho.Remove(detallePreEntrega_RolloDespacho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Eliminar Rollo de la tabla
        [HttpDelete("EliminarRollo/{rollo}")]
        public ActionResult EliminarRollo(long rollo)
        {
            var x = (from y in _context.Set<DetallePreEntrega_RolloDespacho>()
                     where y.Rollo_Id == rollo
                     select y).FirstOrDefault();

            if (x == null)
            {
                return NoContent();
            }


            _context.DetallesPreEntrega_RollosDespacho.Remove(x);
            _context.SaveChanges();

            return NoContent();

        }

        private bool DetallePreEntrega_RolloDespachoExists(long id)
        {
            return _context.DetallesPreEntrega_RollosDespacho.Any(e => e.DtlPreEntRollo_Id == id);
        }
    }
}
