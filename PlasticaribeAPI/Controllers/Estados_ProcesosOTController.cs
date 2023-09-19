using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Estados_ProcesosOTController : ControllerBase
    {
        private readonly dataContext _context;

        public Estados_ProcesosOTController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Estados_ProcesosOT
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estados_ProcesosOT>>> GetEstados_ProcesosOT()
        {
            if (_context.Estados_ProcesosOT == null)
            {
                return NotFound();
            }
            return await _context.Estados_ProcesosOT.ToListAsync();
        }

        // GET: api/Estados_ProcesosOT/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estados_ProcesosOT>> GetEstados_ProcesosOT(long id)
        {
            if (_context.Estados_ProcesosOT == null)
            {
                return NotFound();
            }
            var estados_ProcesosOT = await _context.Estados_ProcesosOT.FindAsync(id);

            if (estados_ProcesosOT == null)
            {
                return NotFound();
            }

            return estados_ProcesosOT;
        }

        //Consulta que tomará diferentes parametros para consultar los datos de las ordenes de trabajo
        [HttpGet("getInfo_OrdenesTrabajo/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetInfo_OrdenesTrabajo(DateTime fechaInicial, DateTime fechaFinal, string? ot = "", string? cli = "", string? prod = "", string? estado = "", string? vendedor = "", string? falla = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var con = from orden in _context.Set<Estados_ProcesosOT>()
                      where orden.EstProcOT_FechaCreacion >= fechaInicial
                            && orden.EstProcOT_FechaCreacion <= fechaFinal
                            && Convert.ToString(orden.EstProcOT_OrdenTrabajo).Contains(ot)
                            && Convert.ToString(orden.Falla_Id).Contains(falla)
                            && Convert.ToString(orden.Estado_Id).Contains(estado)
                            && Convert.ToString(orden.Usuario.Usua_Id).Contains(vendedor)
                            && orden.EstProcOT_Cliente.Contains(cli)
                            && Convert.ToString(orden.Prod_Id).Contains(prod)
                      select new
                      {
                          orden.EstProcOT_Id,
                          Ot = orden.EstProcOT_OrdenTrabajo,
                          Ext = orden.EstProcOT_ExtrusionKg,
                          Imp = orden.EstProcOT_ImpresionKg,
                          Rot = orden.EstProcOT_RotograbadoKg,
                          Lam = orden.EstProcOT_LaminadoKg,
                          Dbl = orden.EstProcOT_DobladoKg,
                          Cor = orden.EstProcOT_CorteKg,
                          Emp = orden.EstProcOT_EmpaqueKg,
                          Sel = orden.EstProcOT_SelladoKg,
                          SelUnd = orden.EstProcOT_SelladoUnd,
                          Wik = orden.EstProcOT_WiketiadoKg,
                          WikUnd = orden.EstProcOT_WiketiadoUnd,
                          DespUnd = (from bg in _context.Set<Detalles_BodegasRollos>()
                                     where bg.BgRollo_BodegaActual == "DESP" &&
                                           bg.BgRollo_OrdenTrabajo == orden.EstProcOT_OrdenTrabajo &&
                                           bg.UndMed_Id != "Kg"
                                     select bg.DtBgRollo_Cantidad).Sum(),
                          Desp = (from bg in _context.Set<Detalles_BodegasRollos>()
                                  where bg.BgRollo_BodegaActual == "DESP" &&
                                        bg.BgRollo_OrdenTrabajo == orden.EstProcOT_OrdenTrabajo &&
                                        bg.UndMed_Id == "Kg"
                                  select bg.DtBgRollo_Cantidad).Sum(),
                          orden.Falla_Id,
                          Falla = orden.FallaTecnica.Falla_Nombre,
                          orden.Estado_Id,
                          Est = orden.Estado_OT.Estado_Nombre,
                          Obs = orden.EstProcOT_Observacion,
                          Fecha = orden.EstProcOT_FechaCreacion,
                          Cant = orden.EstProcOT_CantidadPedida,
                          Und = orden.UndMed_Id,
                          orden.UnidadMedida.UndMed_Nombre,
                          FechaInicio = orden.EstProcOT_FechaInicio,
                          FechaFinal = orden.EstProcOT_FechaFinal,
                          CantUnd = orden.EstProcOT_CantidadPedidaUnd,
                          Usu = orden.Usua_Id,
                          NombreUsu = orden.Usuario.Usua_Nombre,
                          orden.Cli_Id,
                          CliNombre = orden.Clientes.Cli_Nombre,
                          orden.Prod_Id,
                          Prod = orden.Producto.Prod_Nombre,
                          Salida = orden.EstProcOT_CantProdFacturada,
                          Entrada = orden.EstProcOT_CantProdIngresada,
                          Mp = orden.EstProcOT_CantMatPrimaAsignada,
                          Cli = orden.EstProcOT_Cliente,
                          Ped = orden.EstProcOT_Pedido,
                      };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return con.Any() ? Ok(con) : NotFound("¡No se encontró información!");
        }

        //Consulta por OT
        [HttpGet("consultaPorOT/{EstProcOT_OrdenTrabajo}")]
        public ActionResult GetPorOT(long EstProcOT_OrdenTrabajo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new
                {
                    estOt.EstProcOT_Id,
                    estOt.EstProcOT_OrdenTrabajo,
                    estOt.EstProcOT_ExtrusionKg,
                    estOt.EstProcOT_ImpresionKg,
                    estOt.EstProcOT_RotograbadoKg,
                    estOt.EstProcOT_LaminadoKg,
                    estOt.EstProcOT_DobladoKg,
                    estOt.EstProcOT_CorteKg,
                    estOt.EstProcOT_EmpaqueKg,
                    estOt.EstProcOT_SelladoKg,
                    estOt.EstProcOT_SelladoUnd,
                    estOt.EstProcOT_WiketiadoKg,
                    estOt.EstProcOT_WiketiadoUnd,
                    estOt.Falla_Id,
                    estOt.FallaTecnica.Falla_Nombre,
                    estOt.Estado_Id,
                    estOt.Estado_OT.Estado_Nombre,
                    estOt.EstProcOT_Observacion,
                    estOt.EstProcOT_FechaCreacion,
                    estOt.EstProcOT_CantidadPedida,
                    estOt.UndMed_Id,
                    estOt.UnidadMedida.UndMed_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                    estOt.EstProcOT_Pedido,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        /******************************************************** Consultas para mostrar informacion general ****************************************************************/
        [HttpGet("getCantOrdenesUltimoMes/{fecha1}/{fecha2}")]
        public ActionResult getCantOrdenesUltimoMes(DateTime fecha1, DateTime fecha2)
        {
            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion >= fecha1
                            && ot.EstProcOT_FechaCreacion <= fecha2
                      group ot by new { ot.EstProcOT_Cliente }
                      into ot
                      select new
                      {
                          ot.Key.EstProcOT_Cliente,
                          cantidad = ot.Count(),
                      };
            return Ok(con);
        }

        [HttpGet("getProductosOrdenesUltimoMes/{fecha1}/{fecha2}")]
        public ActionResult getProductosOrdenesUltimoMes(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion >= fecha1
                            && ot.EstProcOT_FechaCreacion <= fecha2
                      group ot by new
                      {
                          ot.Prod_Id,
                          ot.Producto.Prod_Nombre
                      }
                      into ot
                      select new
                      {
                          ot.Key.Prod_Id,
                          ot.Key.Prod_Nombre,
                          cantidad = ot.Count(),
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("getVendedoresOrdenesUltimoMes/{fecha1}/{fecha2}")]
        public ActionResult getVendedoresOrdenesUltimoMes(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion >= fecha1
                            && ot.EstProcOT_FechaCreacion <= fecha2
                      group ot by new
                      {
                          ot.Usua_Id,
                          ot.Usuario.Usua_Nombre
                      }
                      into ot
                      select new
                      {
                          ot.Key.Usua_Id,
                          ot.Key.Usua_Nombre,
                          cantidad = ot.Count(),
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("getProcesosOrdenesUltimoMes/{fecha1}/{fecha2}")]
        public ActionResult getProcesosOrdenesUltimoMes(DateTime fecha1, DateTime fecha2)
        {
            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion >= fecha1
                            && ot.EstProcOT_FechaCreacion <= fecha2
                      group ot by new
                      {
                          ot.Cli_Id
                      }
                      into ot
                      select new
                      {
                          Extrusion = ot.Sum(x => x.EstProcOT_ExtrusionKg),
                          Impresion = ot.Sum(x => x.EstProcOT_ImpresionKg),
                          Rotograbado = ot.Sum(x => x.EstProcOT_RotograbadoKg),
                          Laminado = ot.Sum(x => x.EstProcOT_LaminadoKg),
                          Corte = ot.Sum(x => x.EstProcOT_CorteKg),
                          Doblado = ot.Sum(x => x.EstProcOT_DobladoKg),
                          SelladoKg = ot.Sum(x => x.EstProcOT_SelladoKg),
                          SelladoUnd = ot.Sum(x => x.EstProcOT_SelladoUnd),
                          cantidad = ot.Count(),
                      };
            return Ok(con);
        }

        [HttpGet("getTotalMateriaPrimaAsignadaMes/{fecha1}/{fecha2}")]
        public ActionResult getTotalMateriaPrimaAsignadaMes(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion >= fecha1
                            && ot.EstProcOT_FechaCreacion <= fecha2
                      group ot by new
                      {
                          ot.Usua_Id,
                      }
                      into ot
                      select new
                      {
                          cantidad = ot.Sum(x => x.EstProcOT_CantMatPrimaAsignada),
                          extruido = ot.Sum(x => x.EstProcOT_ExtrusionKg),
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        [HttpGet("getOrdenesTrabajo_Pedido/{pedido}")]
        public ActionResult getOrdenesTrabajo_Pedido(long pedido)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT
                .Where(epOT => epOT.EstProcOT_Pedido == pedido)
                .Select(estOt => new
                {
                    estOt.EstProcOT_Id,
                    estOt.EstProcOT_OrdenTrabajo,
                    estOt.EstProcOT_ExtrusionKg,
                    estOt.EstProcOT_ImpresionKg,
                    estOt.EstProcOT_RotograbadoKg,
                    estOt.EstProcOT_LaminadoKg,
                    estOt.EstProcOT_DobladoKg,
                    estOt.EstProcOT_CorteKg,
                    estOt.EstProcOT_EmpaqueKg,
                    estOt.EstProcOT_SelladoKg,
                    estOt.EstProcOT_SelladoUnd,
                    estOt.EstProcOT_WiketiadoKg,
                    estOt.EstProcOT_WiketiadoUnd,
                    estOt.Falla_Id,
                    estOt.FallaTecnica.Falla_Nombre,
                    estOt.Estado_Id,
                    estOt.Estado_OT.Estado_Nombre,
                    estOt.EstProcOT_Observacion,
                    estOt.EstProcOT_FechaCreacion,
                    estOt.EstProcOT_CantidadPedida,
                    estOt.UndMed_Id,
                    estOt.UnidadMedida.UndMed_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                    estOt.EstProcOT_Pedido,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consula que devolverá la cantidad de ordenes de trabajo que se han creado en el mes, agrupadas por el estado que tienen
        [HttpGet("getOrdenesMes_Estados")]
        public ActionResult GetOrdenesMes_Estado()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            int mesActual = (DateTime.Now).Month;
            int anioActual = (DateTime.Now).Year;

            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion.Month == mesActual &&
                            ot.EstProcOT_FechaCreacion.Year == anioActual
                      group ot by new
                      {
                          ot.Estado_Id,
                          ot.Estado_OT.Estado_Nombre
                      } into ot
                      select new
                      {
                          ot.Key.Estado_Id,
                          ot.Key.Estado_Nombre,
                          Cantidad = ot.Count(),
                      };
            return con.Any() ? Ok(con) : NoContent();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        [HttpPut("putEstadoOrden/{ot}")]
        public IActionResult PutEstadoOrden(long ot, int estadoOt)
        {
            try
            {
                var Actualizado = _context.Estados_ProcesosOT.Where(x => x.EstProcOT_OrdenTrabajo == ot).First<Estados_ProcesosOT>();
                Actualizado.Estado_Id = estadoOt;

                _context.SaveChanges();

                return Ok(Actualizado);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        [HttpPut("ActualizacionFallaObservacion/{EstProcOT_OrdenTrabajo}")]
        public IActionResult Put(long EstProcOT_OrdenTrabajo, Estados_ProcesosOT Estados_ProcesosOT)
        {
            try
            {
                var Actualizado = _context.Estados_ProcesosOT.Where(x => x.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo).First<Estados_ProcesosOT>();
                Actualizado.Falla_Id = Estados_ProcesosOT.Falla_Id;
                Actualizado.EstProcOT_Observacion = Estados_ProcesosOT.EstProcOT_Observacion;
                Actualizado.Estado_Id = Estados_ProcesosOT.Estado_Id;
                Actualizado.EstProcOT_Pedido = Estados_ProcesosOT.EstProcOT_Pedido;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Estados_ProcesosOTExists(EstProcOT_OrdenTrabajo))
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

        // PUT: api/Estados_ProcesosOT/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstados_ProcesosOT(long id, Estados_ProcesosOT estados_ProcesosOT)
        {
            if (id != estados_ProcesosOT.EstProcOT_Id)
            {
                return BadRequest();
            }

            _context.Entry(estados_ProcesosOT).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Estados_ProcesosOTExists(id))
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

        // POST: api/Estados_ProcesosOT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estados_ProcesosOT>> PostEstados_ProcesosOT(Estados_ProcesosOT estados_ProcesosOT)
        {
            if (_context.Estados_ProcesosOT == null)
            {
                return Problem("Entity set 'dataContext.Estados_ProcesosOT'  is null.");
            }
            _context.Estados_ProcesosOT.Add(estados_ProcesosOT);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstados_ProcesosOT", new { id = estados_ProcesosOT.EstProcOT_Id }, estados_ProcesosOT);
        }

        // DELETE: api/Estados_ProcesosOT/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstados_ProcesosOT(long id)
        {
            if (_context.Estados_ProcesosOT == null)
            {
                return NotFound();
            }
            var estados_ProcesosOT = await _context.Estados_ProcesosOT.FindAsync(id);
            if (estados_ProcesosOT == null)
            {
                return NotFound();
            }

            _context.Estados_ProcesosOT.Remove(estados_ProcesosOT);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Estados_ProcesosOTExists(long id)
        {
            return (_context.Estados_ProcesosOT?.Any(e => e.EstProcOT_Id == id)).GetValueOrDefault();
        }
    }
}
