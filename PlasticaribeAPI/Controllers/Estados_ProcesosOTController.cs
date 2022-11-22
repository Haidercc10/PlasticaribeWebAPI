using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        //Consulta Todo
        [HttpGet("consultaGeneral")]
        public ActionResult Get()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                });
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
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
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por fallas
        [HttpGet("consultaPorFallas/{Falla_Id}")]
        public ActionResult GetPorFallas(int Falla_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.Falla_Id == Falla_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por la fecha de crecion de la orden de trabajo
        [HttpGet("consultaPorFecha/{EstProcOT_FechaCreacion}")]
        public ActionResult GetPorFecha(DateTime EstProcOT_FechaCreacion)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion == EstProcOT_FechaCreacion)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta todos los registros de la orden de trabajo entre 2 fechas
        [HttpGet("consultaPorFechas/")]
        public ActionResult GetPorFechas(DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por OT y por falals
        [HttpGet("consultaPorOtFalla/{EstProcOT_OrdenTrabajo}/{Falla_Id}")]
        public ActionResult GetPorOtFallas(long EstProcOT_OrdenTrabajo, int Falla_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo && epOT.Falla_Id == Falla_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por OT y por fecha de creacion
        [HttpGet("consultaPorOtFecha/{EstProcOT_OrdenTrabajo}/{EstProcOT_FechaCreacion}")]
        public ActionResult GetPorOtFecha(long EstProcOT_OrdenTrabajo, DateTime EstProcOT_FechaCreacion)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo && epOT.EstProcOT_FechaCreacion == EstProcOT_FechaCreacion)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por OT y 2 fechas
        [HttpGet("consultaPorOtFechas/{EstProcOT_OrdenTrabajo}")]
        public ActionResult GetPorOtFechas(long EstProcOT_OrdenTrabajo, DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT
                .Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo && epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por 2 fechas y la falla
        [HttpGet("consultaPorFechasFallas/{Falla_Id}")]
        public ActionResult GetPorFechasFallas(DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2, int Falla_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT
                .Where(epOT => epOT.Falla_Id == Falla_Id && epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por OT, 2 fechas y falla
        [HttpGet("consultaPorOtFechasFallas/{EstProcOT_OrdenTrabajo}/{Falla_Id}")]
        public ActionResult Get(long EstProcOT_OrdenTrabajo, DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2, int Falla_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT
                .Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo && epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2 && epOT.Falla_Id == Falla_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por falla y fecha de creacion
        [HttpGet("consultaPorOtFechsFalla/{EstProcOT_FechaCreacion}/{Falla_Id}")]
        public ActionResult Get(int Falla_Id, DateTime EstProcOT_FechaCreacion)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion == EstProcOT_FechaCreacion && epOT.Falla_Id == Falla_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(ot);
        }

        //Consulta por el estado de la orden de trabajo
        [HttpGet("consultarPorEstados/{Estado_Id}")]
        public ActionResult Get(int Estado_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.Estado_Id == Estado_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(ot);
        }

        //Consulta por el estado de la orden de trabajo y por la falla
        [HttpGet("consultaPorEstadosFallas/{Estado_Id}/{Falla_Id}")]
        public ActionResult Get(int Estado_Id, int Falla_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.Estado_Id == Estado_Id && epOT.Falla_Id == Falla_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por la fecha de creacion de la orden de trabajo, estado y falla
        [HttpGet("consultaPorFechaEstadoFalla/{EstProcOT_FechaCreacion}/{Estado_Id}/{Falla_Id}")]
        public ActionResult Get(DateTime EstProcOT_FechaCreacion, int Estado_Id, int Falla_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion == EstProcOT_FechaCreacion && epOT.Estado_Id == Estado_Id && epOT.Falla_Id == Falla_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por 2 fechas de creacionpor estados
        [HttpGet("consultaPorFechasEstado/{EstProcOT_FechaCreacion1}/{EstProcOT_FechaCreacion2}/{Estado_Id}")]
        public ActionResult Get(DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2, int Estado_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2 && epOT.Estado_Id == Estado_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por fechas de creacion, estados y fallas
        [HttpGet("consultaPorFechasEstadoFallas/{EstProcOT_FechaCreacion1}/{EstProcOT_FechaCreacion2}/{Estado_Id}/{Falla_Id}")]
        public ActionResult Get(DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2, int Estado_Id, int Falla_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2 && epOT.Estado_Id == Estado_Id && epOT.Falla_Id == Falla_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        /** Metodos para Estados OT por Vendedores **/

        /*1 POR OT Y VENDEDOR */
        [HttpGet("consultaPorOTVendedor/{EstProcOT_OrdenTrabajo}/{Vendedor}")]
        public ActionResult GetXOT(long EstProcOT_OrdenTrabajo, int Vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo && epOT.Usua_Id == Vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,

                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        /*2 POR Fecha Creacion Y VENDEDOR */

        [HttpGet("consultaPorFechaVendedor/{EstProcOT_FechaCreacion}/{Vendedor}")]
        public ActionResult GetXFecha(DateTime EstProcOT_FechaCreacion, int Vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion == EstProcOT_FechaCreacion && epOT.Usua_Id == Vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }


        /*3 Por Fechas y Vendedor */
        [HttpGet("consultaPorFechasVendedor/{EstProcOT_FechaCreacion1}/{EstProcOT_FechaCreacion2}/{Vendedor}")]
        public ActionResult GetXFechas(DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2, int Vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2 && epOT.Usua_Id == Vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        /*4 POR OT, Fecha Ini y Vendedor */

        [HttpGet("consultaPorOtFechaVendedor/{EstProcOT_OrdenTrabajo}/{EstProcOT_FechaCreacion}/{Vendedor}")]
        public ActionResult GetXOtFecha(long EstProcOT_OrdenTrabajo, DateTime EstProcOT_FechaCreacion, int Vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo && epOT.EstProcOT_FechaCreacion == EstProcOT_FechaCreacion && epOT.Usua_Id == Vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        /*5 Por Fechas, OT, Vendedor */

        [HttpGet("consultaPorOtFechasVendedor/{EstProcOT_FechaCreacion1}/{EstProcOT_FechaCreacion2}/{EstProcOT_OrdenTrabajo}/{Vendedor}")]
        public ActionResult GetXOtFechas(long EstProcOT_OrdenTrabajo, DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2, int Vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT
                .Where(epOT => epOT.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo && epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2 && epOT.Usua_Id == Vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        /*6 Por Estados y vendedor*/
        [HttpGet("consultarPorEstadosVendedor/{Estado_Id}/{Vendedor}")]
        public ActionResult GetXEstado(int Estado_Id, int Vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.Estado_Id == Estado_Id && epOT.Usua_Id == Vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                    estOt.Usua_Id,
                    estOt.Usuario.Usua_Nombre,
                    estOt.EstProcOT_FechaInicio,
                    estOt.EstProcOT_FechaFinal,
                    estOt.EstProcOT_CantidadPedidaUnd,
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(ot);
        }

        /*7 Por Fechas, estados y vendedor*/
        [HttpGet("consultaPorFechasEstadoVendedor/{EstProcOT_FechaCreacion1}/{EstProcOT_FechaCreacion2}/{Estado_Id}/{Vendedor}")]
        public ActionResult GetXFechasXEstadoXVendedor(DateTime EstProcOT_FechaCreacion1, DateTime EstProcOT_FechaCreacion2, int Estado_Id, int Vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion >= EstProcOT_FechaCreacion1 && epOT.EstProcOT_FechaCreacion <= EstProcOT_FechaCreacion2 && epOT.Estado_Id == Estado_Id && epOT.Usua_Id == Vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                    estOt.Cli_Id,
                    estOt.Clientes.Cli_Nombre,
                    estOt.Prod_Id,
                    estOt.Producto.Prod_Nombre,
                    estOt.EstProcOT_CantProdFacturada,
                    estOt.EstProcOT_CantProdIngresada,
                    estOt.EstProcOT_CantMatPrimaAsignada,
                    estOt.EstProcOT_Cliente,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por productos 
        [HttpGet("consultaPorProductos/{Prod_Id}")]
        public ActionResult GetProducto(int Prod_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.Prod_Id == Prod_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        // Consulta por Clientes
        [HttpGet("consultaPorClientes/{Cli_Id}")]
        public ActionResult GetClientes(long Cli_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ot = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == Cli_Id)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Include(estOt => estOt.Usuario)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consulta por Cliente, Producto, OT, Fechas, fallas, estado, vendedor
        [HttpGet("consultaSinOT/{cli}/{prod}/{fecha1}/{fecha2}/{falla}/{estado}/{vendedor}")]
        public ActionResult get(long cli, int prod, DateTime fecha1, DateTime fecha2, int falla, int estado, long vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli && epOT.Prod_Id == prod && epOT.EstProcOT_FechaCreacion >= fecha1 && epOT.EstProcOT_FechaCreacion <= fecha2 && epOT.Falla_Id == falla && epOT.Estado_Id == estado && epOT.Usua_Id == vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta por Estado y Cliente
        [HttpGet("consultaEstadoCliente/{cli}/{estado}")]
        public ActionResult get(long cli, int estado)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli && epOT.Estado_Id == estado)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta por Estado y Producto
        [HttpGet("consultaProductoEstado/{prod}/{estado}")]
        public ActionResult get(int prod, int estado)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Prod_Id == prod && epOT.Estado_Id == estado)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta por Cliente, vendedor
        [HttpGet("consultaVendedorCliente/{cli}/{vendedor}")]
        public ActionResult get(long cli, long vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli && epOT.Usua_Id == vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta por Cliente, vendedor
        [HttpGet("consultaVendedorProducto/{prod}/{vendedor}")]
        public ActionResult get(int prod, long vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Prod_Id == prod && epOT.Usua_Id == vendedor)
                .Include(estOT => estOT.FallaTecnica)
                .Include(estOT => estOT.Estado_OT)
                .Include(estOt => estOt.UnidadMedida)
                .Select(estOt => new {
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
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        //Consulta por Cliente, Producto, OT, Fechas, fallas, estado, vendedor
        [HttpGet("consulta/")]
        public ActionResult get(long? ot, long? cli, int? prod, DateTime? fecha1, DateTime? fecha2, int? falla, int? estado, long? vendedor)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (cli != null && prod != null && fecha1 != null && fecha2 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && prod != null && fecha1 != null && fecha2 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && fecha1 != null && fecha2 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion == fecha1
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null && falla != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null && falla != null && estado != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null && falla != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null && estado != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Estado_Id == estado)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && falla != null && estado != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion == fecha1
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && falla != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion == fecha1
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion == fecha1
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && fecha1 != null && fecha2 != null && falla != null && estado != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && fecha1 != null && fecha2 != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && prod != null && fecha1 != null && fecha2 != null && falla != null && estado != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && prod != null && fecha1 != null && fecha2 != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (cli != null && prod != null && fecha1 != null && fecha2 != null && falla != null && estado != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (cli != null && prod != null && fecha1 != null && fecha2 != null && falla != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (cli != null && prod != null && fecha1 != null && fecha2 != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (cli != null && prod != null && fecha1 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion == fecha1
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (cli != null && fecha1 != null && fecha2 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Cli_Id == cli
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (prod != null && fecha1 != null && fecha2 != null && falla != null && estado != null && vendedor != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && fecha2 != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else if (ot != null && cli != null && prod != null && fecha1 != null && falla != null)
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_OrdenTrabajo == ot
                                                      && epOT.Cli_Id == cli
                                                      && epOT.Prod_Id == prod
                                                      && epOT.EstProcOT_FechaCreacion >= fecha1
                                                      && epOT.EstProcOT_FechaCreacion <= fecha2
                                                      && epOT.Falla_Id == falla
                                                      && epOT.Estado_Id == estado
                                                      && epOT.Usua_Id == vendedor)
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
                    })
                    .ToList();
                return Ok(con);
            }
            else
            {
                var con = _context.Estados_ProcesosOT.Where(epOT => epOT.EstProcOT_FechaCreacion == fecha1)
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
                    })
                    .ToList();
                return Ok(con);
            }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        /** Fin Consultas por vendedor */

        [HttpPut("ActualizacionFallaObservacion/{EstProcOT_OrdenTrabajo}")]
        public IActionResult Put(long EstProcOT_OrdenTrabajo, Estados_ProcesosOT Estados_ProcesosOT)
        {
            try
            {
                var Actualizado = _context.Estados_ProcesosOT.Where(x => x.EstProcOT_OrdenTrabajo == EstProcOT_OrdenTrabajo).First<Estados_ProcesosOT>();
                Actualizado.Falla_Id = Estados_ProcesosOT.Falla_Id;
                Actualizado.EstProcOT_Observacion = Estados_ProcesosOT.EstProcOT_Observacion;
                Actualizado.Estado_Id = Estados_ProcesosOT.Estado_Id;

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
