using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Migrations;
using PlasticaribeAPI.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

        // Consulta para obtener información de las órdenes de trabajo con filtros opcionales y agregación de desperdicios
        [HttpGet("getInfo_OrdenesTrabajo2/{fechaInicial}/{fechaFinal}")]
        public async Task<ActionResult> GetInfo_OrdenesTrabajo2(
        DateTime fechaInicial,
        DateTime fechaFinal,
        string? ot = "",
        string? cli = "",
        string? prod = "",
        string? estado = "",
        string? vendedor = "",
        string? falla = "")
        {
            // -------------------------------------------------------------
            // 1) Agregación de desperdicios: UNA sola pasada por Desperdicio,
            //    agrupada por OT, con sumas condicionales por proceso.
            //    Esto reemplaza las N subconsultas correlacionadas.
            // -------------------------------------------------------------
            var fechaActual = DateTime.Now;
            DateTime fechaUnMesAtras = fechaActual.AddMonths(-1);

            var desperdiciosPorOt = _context.Set<Models.Desperdicio>()
                .GroupBy(d => d.Desp_OT)
                .Select(g => new
                {
                    Ot = g.Key,
                    DespExt = g.Where(x => x.Proceso_Id == "EXT").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespImp = g.Where(x => x.Proceso_Id == "IMP").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespPerf = g.Where(x => x.Proceso_Id == "PERF").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespDob = g.Where(x => x.Proceso_Id == "DBLD").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespLam = g.Where(x => x.Proceso_Id == "LAM").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespEmp = g.Where(x => x.Proceso_Id == "CORTE").Sum(x => (decimal?)x.Desp_PesoKg) ,
                    DespSella = g.Where(x => x.Proceso_Id == "SELLA").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespTotal = g.Sum(x => (decimal?)x.Desp_PesoKg),
                    
                });

            // -------------------------------------------------------------
            // 2) Query principal con filtros condicionales (solo se aplican
            //    si el parámetro viene informado) + LEFT JOIN a la agregación.
            // -------------------------------------------------------------
            var query =
                from orden in _context.Set<Estados_ProcesosOT>().AsNoTracking()
                where (fechaInicial == fechaUnMesAtras ? orden.EstProcOT_FechaCreacion : orden.EstProcOT_FechaInicio) >= fechaInicial
                      && orden.EstProcOT_FechaFinal <= fechaFinal
                      && (string.IsNullOrEmpty(ot) || Convert.ToString(orden.EstProcOT_OrdenTrabajo).Contains(ot))
                      && (string.IsNullOrEmpty(falla) || Convert.ToString(orden.Falla_Id) == falla)
                      && (string.IsNullOrEmpty(estado) || Convert.ToString(orden.Estado_Id) == estado)
                      && (string.IsNullOrEmpty(vendedor) || Convert.ToString(orden.Usuario.Usua_Id) == vendedor)
                      && (string.IsNullOrEmpty(cli) || Convert.ToString(orden.EstProcOT_Cliente).Contains(cli))
                      && (string.IsNullOrEmpty(prod) || Convert.ToString(orden.Prod_Id) == prod)
                join desp in desperdiciosPorOt
                    on orden.EstProcOT_OrdenTrabajo equals desp.Ot into despJoin
                from desp in despJoin.DefaultIfEmpty() // LEFT JOIN: OTs sin desperdicio no se pierden
                select new
                {
                    orden.EstProcOT_Id,
                    Ot = orden.EstProcOT_OrdenTrabajo,
                    Ext = orden.EstProcOT_ExtrusionKg,
                    Desp_ext = desp.DespExt ?? 0m,
                    Sum_ext = (orden.EstProcOT_ExtrusionKg + desp.DespExt ?? 0m),
                    Imp = orden.EstProcOT_ImpresionKg,
                    Desp_imp = desp.DespImp ?? 0m,
                    Sum_imp = (orden.EstProcOT_ImpresionKg + desp.DespImp ?? 0m),
                    Perf = orden.EstProcOT_PerforadoKg,
                    Desp_perf = desp.DespPerf ?? 0m,
                    Sum_perf = (orden.EstProcOT_PerforadoKg + desp.DespPerf ?? 0m),
                    Rot = orden.EstProcOT_RotograbadoKg,
                    Lam = orden.EstProcOT_LaminadoKg,
                    Desp_lam = desp.DespLam ?? 0m,
                    Sum_lam = (orden.EstProcOT_LaminadoKg + desp.DespLam ?? 0m),
                    Dbl = orden.EstProcOT_DobladoKg,
                    Desp_dbl = desp.DespDob ?? 0m,
                    Sum_dbl = (orden.EstProcOT_DobladoKg + desp.DespDob ?? 0m),
                    Cor = orden.EstProcOT_CorteKg,
                    Emp = orden.EstProcOT_EmpaqueKg,
                    Desp_emp = desp.DespEmp ?? 0m,
                    Sum_emp = (orden.EstProcOT_EmpaqueKg + desp.DespEmp ?? 0m),
                    Sel = orden.EstProcOT_SelladoKg,
                    Desp_sel = desp.DespSella ?? 0m,
                    Sum_sel = (orden.EstProcOT_SelladoKg + desp.DespSella ?? 0m),
                    SelUnd = orden.EstProcOT_SelladoUnd,
                    Wik = orden.EstProcOT_WiketiadoKg,
                    WikUnd = orden.EstProcOT_WiketiadoUnd,
                    Desp = desp.DespTotal ?? 0m,
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

            var con = await query.ToListAsync();

            return con.Any() ? Ok(con) : NotFound("¡No se encontró información!");
        }


        //Función que se encarga de mostrar el balance de las ordenes de trabajo, calculando el balance por proceso y el balance general
        [HttpGet("getInfo_OrdenesTrabajoConBalance/{fechaInicial}/{fechaFinal}/{usarFechaCreacion}")]
        public async Task<ActionResult> GetInfo_OrdenesTrabajoConBalance(
            DateTime fechaInicial,
            DateTime fechaFinal,
            bool usarFechaCreacion,
            string? ot = "",
            string? cli = "",
            string? prod = "",
            string? estado = "",
            string? vendedor = "",
            string? falla = "")
        {
#pragma warning disable CS8629 // Dereference of a possibly null value.
            // -------------------------------------------------------------
            // 1) Agregación de desperdicios (una sola pasada, GROUP BY + LEFT JOIN,
            //    igual que en getInfo_OrdenesTrabajo2).
            // -------------------------------------------------------------

            var desperdiciosPorOt = _context.Set<Models.Desperdicio>()
                .GroupBy(d => d.Desp_OT)
                .Select(g => new
                {
                    Ot = g.Key,
                    DespExt = g.Where(x => x.Proceso_Id == "EXT").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespImp = g.Where(x => x.Proceso_Id == "IMP").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespPerf = g.Where(x => x.Proceso_Id == "PERF").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespLam = g.Where(x => x.Proceso_Id == "LAM").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespDbl = g.Where(x => x.Proceso_Id == "DBLD").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespEmp = g.Where(x => x.Proceso_Id == "EMP").Sum(x => (decimal?)x.Desp_PesoKg),
                    DespSel = g.Where(x => x.Proceso_Id == "SELLA").Sum(x => (decimal?)x.Desp_PesoKg),
                    Desp = g.Sum(x => (decimal?)x.Desp_PesoKg),
                });

            // -------------------------------------------------------------
            // 2) Query principal + LEFT JOIN. Todavía SIN los balances -- esos se
            //    calculan después porque dependen de lo que pasó en el proceso
            //    anterior (lógica secuencial, no expresable de forma legible en SQL).
            // -------------------------------------------------------------
            var query =
                from orden in _context.Set<Estados_ProcesosOT>().AsNoTracking()
                where  ( usarFechaCreacion
                        ? orden.EstProcOT_FechaCreacion >= fechaInicial && orden.EstProcOT_FechaCreacion <= fechaFinal
                        : orden.EstProcOT_FechaInicio >= fechaInicial && orden.EstProcOT_FechaFinal <= fechaFinal)
                      && (string.IsNullOrEmpty(ot) || Convert.ToString(orden.EstProcOT_OrdenTrabajo).Contains(ot))
                      && (string.IsNullOrEmpty(falla) || Convert.ToString(orden.Falla_Id) == falla)
                      && (string.IsNullOrEmpty(estado) || Convert.ToString(orden.Estado_Id) == estado)
                      && (string.IsNullOrEmpty(vendedor) || Convert.ToString(orden.Usuario.Usua_Id) == vendedor)
                      && (string.IsNullOrEmpty(cli) || Convert.ToString(orden.EstProcOT_Cliente).Contains(cli))
                      && (string.IsNullOrEmpty(prod) || Convert.ToString(orden.Prod_Id) == prod)
                join desp in desperdiciosPorOt
                    on orden.EstProcOT_OrdenTrabajo equals desp.Ot into despJoin
                from desp in despJoin.DefaultIfEmpty()
                select new OrdenTrabajoConBalanceDto
                {
                    EstProcOT_Id = Convert.ToInt32(orden.EstProcOT_Id),
                    Ot = Convert.ToString(orden.EstProcOT_OrdenTrabajo),
                    Mp = orden.EstProcOT_CantMatPrimaAsignada,
                    Cant = orden.EstProcOT_CantidadPedida,
                    CantUnd = Convert.ToDecimal(orden.EstProcOT_CantidadPedidaUnd),
                    Und = orden.UndMed_Id,

                    Ext = orden.EstProcOT_ExtrusionKg,
                    Desp_ext = desp.DespExt ?? 0m,
                    Sum_ext = (orden.EstProcOT_ExtrusionKg + desp.DespExt ?? 0m),

                    Imp = orden.EstProcOT_ImpresionKg,
                    Desp_imp = desp.DespImp ?? 0m,
                    Sum_imp = (orden.EstProcOT_ImpresionKg + desp.DespImp ?? 0m),

                    Perf = orden.EstProcOT_PerforadoKg ?? 0m,
                    Desp_perf = desp.DespPerf ?? 0m,
                    Sum_perf = (orden.EstProcOT_PerforadoKg + desp.DespPerf ?? 0m),

                    Lam = orden.EstProcOT_LaminadoKg,
                    Desp_lam = desp.DespLam ?? 0m,
                    Sum_lam = (orden.EstProcOT_LaminadoKg + desp.DespLam ?? 0m),

                    Dbl = orden.EstProcOT_DobladoKg,
                    Desp_dbl = desp.DespDbl ?? 0m,
                    Sum_dbl = (orden.EstProcOT_DobladoKg + desp.DespDbl ?? 0m),

                    Emp = orden.EstProcOT_EmpaqueKg,
                    Desp_emp = desp.DespEmp ?? 0m,
                    Sum_emp = (orden.EstProcOT_EmpaqueKg + desp.DespEmp ?? 0m),

                    Sel = orden.EstProcOT_SelladoKg,
                    SelUnd = orden.EstProcOT_SelladoUnd,
                    Desp_sel = desp.DespSel ?? 0m,
                    Sum_sel = (orden.EstProcOT_SelladoKg + desp.DespSel ?? 0m),

                    Desp = Convert.ToDecimal(desp.Desp),

                    Estado_Id = orden.Estado_Id,
                    Est = orden.Estado_OT.Estado_Nombre,
                    Obs = orden.EstProcOT_Observacion,
                    Fecha = orden.EstProcOT_FechaCreacion,
                    FechaInicio = orden.EstProcOT_FechaInicio.Value,
                    FechaFinal = orden.EstProcOT_FechaFinal.Value,
                    Diff_Dias = orden.EstProcOT_FechaFinal != null ? (orden.EstProcOT_FechaFinal - orden.EstProcOT_FechaInicio).Value.Days : 0,
                    Cli = orden.Clientes.Cli_Nombre,

                    Ref = orden.Producto.Prod_Nombre,
                    Item = orden.Prod_Id,

                };

            var con = await query.ToListAsync();

            if (!con.Any())
                return NotFound("¡No se encontró información!");

            // -------------------------------------------------------------
            // 3) Balance por proceso, calculado secuencialmente y asignado
            //    directamente a las columnas planas del DTO.
            // -------------------------------------------------------------
            foreach (var o in con)
            {
                decimal baseSiguiente = o.Mp; //3450
                

                o.Balance_Ext = (o.Ext + o.Desp_ext) - baseSiguiente; //9656-3450
                if (o.Ext > 0)
                {
                    o.Base_Ext = baseSiguiente; //3450
                    baseSiguiente = o.Ext; //9616
                }
                else {
                    o.Balance_Ext = 0;
                    o.Base_Ext = 0;
                } 


                o.Balance_Imp = (o.Imp + o.Desp_imp) - baseSiguiente;
                if (o.Imp > 0)
                {
                    o.Base_Imp = baseSiguiente;
                    baseSiguiente = o.Imp;
                    
                }
                else {
                    o.Balance_Imp = 0;
                    o.Base_Imp = 0;
                } 
               

                o.Balance_Lam = (o.Lam + o.Desp_lam) - baseSiguiente;
                if (o.Lam > 0)
                {
                    o.Base_Lam = baseSiguiente;
                    baseSiguiente = o.Lam;
                }
                else {
                    o.Balance_Lam = 0;
                    o.Base_Lam = 0;
                } 
                

                o.Balance_Perf = (o.Perf + o.Desp_perf) - baseSiguiente;
                if (o.Perf > 0)
                {
                    o.Base_Perf = baseSiguiente;
                    baseSiguiente = o.Perf;
                }
                else { 
                    o.Balance_Perf = 0;
                    o.Base_Perf = 0;
                }
               

                o.Balance_Dbl = (o.Dbl + o.Desp_dbl) - baseSiguiente;
                if (o.Dbl > 0) {
                    o.Base_Dbl = baseSiguiente;
                    baseSiguiente = o.Dbl;
                }   
                else
                {
                    o.Balance_Dbl = 0;
                    o.Base_Dbl = 0; 
                }

                o.Balance_Emp = (o.Emp + o.Desp_emp) - baseSiguiente;
                if (o.Emp > 0) {
                    o.Base_Emp = baseSiguiente;
                    baseSiguiente = o.Emp;
                } 
                else { 
                    o.Balance_Emp = 0;
                    o.Base_Emp = 0;
                }

                o.Balance_Sel = (o.Sel + o.Desp_sel) - baseSiguiente;
                if (o.Sel > 0)
                {
                    o.Base_Sel = baseSiguiente;
                    baseSiguiente = o.Sel;
                }
                else { 
                    o.Balance_Sel = 0;
                    o.Base_Sel = 0;
                }

                /*decimal baseAnterior = o.Mp;

                o.Base_Ext = baseAnterior;
                o.Balance_Ext = o.Ext == 0 ? 0m : (o.Ext + o.Desp_ext) - baseAnterior;
                baseAnterior = o.Ext;

                o.Base_Imp = baseAnterior;
                o.Balance_Imp = o.Imp == 0 ? 0m : (o.Imp + o.Desp_imp) - baseAnterior;
                baseAnterior = o.Imp;

                o.Base_Perf = baseAnterior;
                o.Balance_Perf = o.Perf == 0 ? 0m : (o.Perf + o.Desp_perf) - baseAnterior;
                baseAnterior = o.Perf;

                o.Base_Lam = baseAnterior;
                o.Balance_Lam = o.Lam == 0 ? 0m : (o.Lam + o.Desp_lam) - baseAnterior;
                baseAnterior = o.Lam;

                o.Base_Dbl = baseAnterior;
                o.Balance_Dbl = o.Dbl == 0 ? 0m : (o.Dbl + o.Desp_dbl) - baseAnterior;
                baseAnterior = o.Dbl;

                o.Base_Emp = baseAnterior;
                o.Balance_Emp = o.Emp == 0 ? 0m : (o.Emp + o.Desp_emp) - baseAnterior;
                baseAnterior = o.Emp;

                o.Base_Sel = baseAnterior;
                o.Balance_Sel = o.Sel == 0 ? 0m : (o.Sel + o.Desp_sel) - baseAnterior;
                baseAnterior = o.Sel;*/

                // =========================================================
                // BALANCE GENERAL
                // =========================================================

                // Primer proceso que tenga cantidad

                if (o.Ext > 0)
                {
                    o.Proceso_Inicial = "EXT";
                    o.Cantidad_Inicial = o.Ext;
                }
                else if (o.Mp > 0)
                {
                    o.Proceso_Inicial = "MP";
                    o.Cantidad_Inicial = o.Mp;
                }
                else if (o.Imp > 0)
                {
                    o.Proceso_Inicial = "IMP";
                    o.Cantidad_Inicial = o.Imp;
                }
                else if (o.Perf > 0)
                {
                    o.Proceso_Inicial = "PERF";
                    o.Cantidad_Inicial = o.Perf;
                }
                else if (o.Lam > 0)
                {
                    o.Proceso_Inicial = "LAM";
                    o.Cantidad_Inicial = o.Lam;
                }
                else if (o.Dbl > 0)
                {
                    o.Proceso_Inicial = "DOBL";
                    o.Cantidad_Inicial = o.Dbl;
                }
                else if (o.Emp > 0)
                {
                    o.Proceso_Inicial = "EMP";
                    o.Cantidad_Inicial = o.Emp;
                }
                else if (o.Sel > 0)
                {
                    o.Proceso_Inicial = "SELLA";
                    o.Cantidad_Inicial = o.Sel;
                }
                else { 
                    o.Proceso_Inicial = "N/A";
                    o.Cantidad_Inicial = 0;
                }


                // =========================================================
                // Último proceso que tenga cantidad
                // =========================================================

                if (o.Sel > 0)
                {
                    o.Proceso_Final = "SELLA";
                    o.Cantidad_Final = o.Sel;
                    o.Desperdicio_Final = o.Desp_sel;
                    o.Reportado_Final = o.Sel + o.Desp_sel;
                }
                else if (o.Emp > 0)
                {
                    o.Proceso_Final = "EMP";
                    o.Cantidad_Final = o.Emp;
                    o.Desperdicio_Final = o.Desp_emp;
                    o.Reportado_Final = o.Emp + o.Desp_emp;
                }
                else if (o.Dbl > 0)
                {
                    o.Proceso_Final = "DOBL";
                    o.Cantidad_Final = o.Dbl;
                    o.Desperdicio_Final = o.Desp_dbl;
                    o.Reportado_Final = o.Dbl + o.Desp_dbl;
                }
                else if (o.Lam > 0)
                {
                    o.Proceso_Final = "LAM";
                    o.Cantidad_Final = o.Lam;
                    o.Desperdicio_Final = o.Desp_lam;
                    o.Reportado_Final = o.Lam + o.Desp_lam;
                }
                else if (o.Perf > 0)
                {
                    o.Proceso_Final = "PERF";
                    o.Cantidad_Final = o.Perf;
                    o.Desperdicio_Final = o.Desp_perf;
                    o.Reportado_Final = o.Perf + o.Desp_perf;
                }
                else if (o.Imp > 0)
                {
                    o.Proceso_Final = "IMP";
                    o.Cantidad_Final = o.Imp;
                    o.Desperdicio_Final = o.Desp_imp;
                    o.Reportado_Final = o.Imp + o.Desp_imp;
                }
                else if (o.Ext > 0)
                {
                    o.Proceso_Final = "EXT";
                    o.Cantidad_Final = o.Ext;
                    o.Desperdicio_Final = o.Desp_ext;
                    o.Reportado_Final = o.Ext + o.Desp_ext;
                }
                else if (o.Mp > 0)
                {
                    o.Proceso_Final = "MP";
                    o.Cantidad_Final = o.Mp;
                    o.Desperdicio_Final = Convert.ToDecimal(0);
                    o.Reportado_Final = o.Mp;
                }
                else { 
                    o.Proceso_Final = "N/A";
                    o.Cantidad_Final = 0;
                    o.Desperdicio_Final = 0;
                    o.Reportado_Final = 0;
                }



                    // =========================================================
                    // BALANCE GENERAL
                    // =========================================================

                    o.Balance_General =
                        (o.Cantidad_Final + o.Desperdicio_Final) - o.Cantidad_Inicial;

            }

            return Ok(con);
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
                    estOt.EstProcOT_PerforadoKg,
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
        public ActionResult getProductosOrdenesUltimoMes(DateTime fecha1, DateTime fecha2, string? sales)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion >= fecha1
                            && ot.EstProcOT_FechaCreacion <= fecha2
                            && (string.IsNullOrEmpty(sales) || ot.Usua_Id == Convert.ToInt32(sales))
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
                          Perforado = ot.Sum(x => x.EstProcOT_SelladoUnd),
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
                    estOt.EstProcOT_PerforadoKg,
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(ot);
        }

        //Consula que devolverá la cantidad de ordenes de trabajo que se han creado en el mes, agrupadas por el estado que tienen
        [HttpGet("getOrdenesMes_Estados")]
        public ActionResult GetOrdenesMes_Estado(string? sales)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            int mesActual = (DateTime.Now).Month;
            int anioActual = (DateTime.Now).Year;

            var con = from ot in _context.Set<Estados_ProcesosOT>()
                      where ot.EstProcOT_FechaCreacion.Month == mesActual &&
                            ot.EstProcOT_FechaCreacion.Year == anioActual &&
                            (string.IsNullOrEmpty(sales) || ot.Usua_Id == Convert.ToInt32(sales))
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

        [HttpPost("getOtsForSalesOrder")]
        public ActionResult getOtsForSalesOrder([FromBody] List<CustomerOrders> CustomerOrders)
        {
            List<object> orders = new List<object>();
            int counter = 0;
            foreach (var item in CustomerOrders)
            {
                var ordersProduction = (from e in _context.Set<Estados_ProcesosOT>()
                                        where e.EstProcOT_FechaCreacion >= item.date1 &&
                                              e.EstProcOT_FechaCreacion <= item.date2 &&
                                              e.Prod_Id == item.item //&&
                                                                     //cl.PtPresentacionNom == item.presentation //&&
                                                                     //cl.Cliente == Convert.ToInt32(codBagpro)
                                        select new
                                        {
                                            OT = e.EstProcOT_OrdenTrabajo,
                                            Consecutivo = item.consecutivo,
                                            Item = e.Prod_Id,
                                            Status = e.Estado_Id,
                                            Extrusion = e.EstProcOT_ExtrusionKg,
                                            Impresión = e.EstProcOT_ImpresionKg,
                                            Rotograbado = e.EstProcOT_RotograbadoKg,
                                            Laminado = e.EstProcOT_LaminadoKg,
                                            Corte = e.EstProcOT_CorteKg,
                                            Doblado = e.EstProcOT_DobladoKg,
                                            Empaque = e.EstProcOT_EmpaqueKg,
                                            SelladoUnd = e.EstProcOT_SelladoUnd,
                                            SelladoKg = e.EstProcOT_SelladoKg,
                                            WiketiadoUnd = e.EstProcOT_WiketiadoUnd,
                                            WiketiadoKg = e.EstProcOT_WiketiadoKg,
                                            Perforado = e.EstProcOT_PerforadoKg,
                                        }).FirstOrDefault();

                counter++;
                orders.Add(ordersProduction);
                if (CustomerOrders.Count() == counter) return Ok(orders);
            }
            return Ok(orders);
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

        //Función que actualiza los pesos y cantidades en estados procesos OT al momento de pesar producción
        [HttpPut("putStatusProcessOT/{ot}/{process}/{qty}/{weight}")]
        public async Task<IActionResult> putStatusProcessOT(long ot, string process, decimal qty, decimal weight)
        {

            decimal quantity = 0m;
            decimal quantityProcessFinal = 0m;
            decimal quantityProcessFinal2 = 0m;
            int? status = null;
            var order = (from e in _context.Set<Estados_ProcesosOT>() where e.EstProcOT_OrdenTrabajo == ot select e).FirstOrDefault();

            if (order.EstProcOT_FechaInicio == null) order.EstProcOT_FechaInicio = DateTime.Now;
            if (order.EstProcOT_HoraInicio == null) order.EstProcOT_HoraInicio = DateTime.Now.ToString("HH:mm:ss");
            order.EstProcOT_FechaFinal = DateTime.Now;
            order.EstProcOT_HoraFinal = DateTime.Now.ToString("HH:mm:ss");
            if (process == "EXT") order.EstProcOT_ExtrusionKg = qty;
            if (process == "IMP") order.EstProcOT_ImpresionKg = qty;
            if (process == "ROT") order.EstProcOT_RotograbadoKg = qty;
            if (process == "LAM") order.EstProcOT_LaminadoKg = qty;

            if (process == "EMP") order.EstProcOT_EmpaqueKg = qty;
            if (process == "PERF") order.EstProcOT_PerforadoKg = qty;
            if (process == "DBLD") order.EstProcOT_DobladoKg = qty;
            if (process == "SELLA")
            {
                order.EstProcOT_SelladoKg = weight;
                order.EstProcOT_SelladoUnd = qty;
            }
            if (process == "WIKE")
            {
                order.EstProcOT_WiketiadoKg = weight;
                order.EstProcOT_WiketiadoUnd = qty;
            }
            quantity += (order.EstProcOT_ExtrusionKg + order.EstProcOT_ImpresionKg + Convert.ToDecimal(order.EstProcOT_PerforadoKg) + order.EstProcOT_RotograbadoKg + order.EstProcOT_LaminadoKg);
            quantityProcessFinal = order.EstProcOT_EmpaqueKg;
            quantityProcessFinal2 = order.EstProcOT_SelladoUnd;

            if (quantity >= 0m)
            {
                if (
                       (quantityProcessFinal == 0m && quantityProcessFinal2 > 0m && quantityProcessFinal2 < order.EstProcOT_CantidadPedidaUnd)
                    || (quantityProcessFinal2 == 0m && quantityProcessFinal > 0m && quantityProcessFinal < order.EstProcOT_CantidadPedida)
                    || (quantityProcessFinal > 0m && quantityProcessFinal < order.EstProcOT_CantidadPedida)
                    || (quantityProcessFinal2 > 0m && quantityProcessFinal2 < order.EstProcOT_CantidadPedida)
                    || (quantityProcessFinal > 0m && quantityProcessFinal < order.EstProcOT_CantidadPedida && quantity > 0m)
                    || (quantityProcessFinal2 > 0m && quantityProcessFinal2 < order.EstProcOT_CantidadPedidaUnd && quantity > 0m)
                    || (quantityProcessFinal == 0m && quantityProcessFinal2 == 0m && quantity > 0m)
                    || (quantityProcessFinal == 0m && quantityProcessFinal2 == 0m && quantity > 0m)
                   )
                {
                    status = 16;
                }
                else if (quantityProcessFinal >= order.EstProcOT_CantidadPedida) status = 17;
                else if (quantityProcessFinal2 >= order.EstProcOT_CantidadPedidaUnd) status = 17;
                else status = null;
            }

            if (status != null) order.Estado_Id = (int)status;


            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
        }

        //Función que actualiza la falla y la observación de una orden de trabajo
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

public class CustomerOrders
{
    public DateTime date1 { get; set; }

    public DateTime date2 { get; set; }

    public int item { get; set; }

    public string consecutivo { get; set; }
}

public class OrdenTrabajoConBalanceDto
{
    public int EstProcOT_Id { get; set; }
    public string Ot { get; set; } = "";
    public decimal Mp { get; set; }

    public decimal Base_Ext { get; set; }
    public decimal Ext { get; set; }
    public decimal Desp_ext { get; set; }
    public decimal Sum_ext { get; set; }
    public decimal Balance_Ext { get; set; }

    public decimal Base_Imp { get; set; }
    public decimal Imp { get; set; }
    public decimal Desp_imp { get; set; }
    public decimal Sum_imp { get; set; }
    public decimal Balance_Imp { get; set; }

    public decimal Base_Perf { get; set; }
    public decimal Perf { get; set; }
    public decimal Desp_perf { get; set; }

    public decimal Sum_perf { get; set; }
    public decimal Balance_Perf { get; set; }

    public decimal Base_Lam { get; set; }
    public decimal Lam { get; set; }
    public decimal Desp_lam { get; set; }
    public decimal Balance_Lam { get; set; }
    public decimal Sum_lam { get; set; }

    public decimal Base_Dbl { get; set; }
    public decimal Dbl { get; set; }
    public decimal Desp_dbl { get; set; }
    public decimal Sum_dbl { get; set; }
    public decimal Balance_Dbl { get; set; }

    public decimal Base_Emp { get; set; }
    public decimal Emp { get; set; }
    public decimal Desp_emp { get; set; }
    public decimal Sum_emp { get; set; }
    public decimal Balance_Emp { get; set; }

    public decimal Base_Sel { get; set; }
    public decimal Sel { get; set; }
    public decimal SelUnd { get; set; }
    public decimal Desp_sel { get; set; }
    public decimal Sum_sel { get; set; }
    public decimal Balance_Sel { get; set; }

    public decimal Balance_General { get; set; }

    public decimal Cantidad_Inicial { get; set; }

    public decimal Cantidad_Final { get; set; }

    public decimal Desperdicio_Final { get; set; }

    public string? Proceso_Inicial  { get; set; }

    public string? Proceso_Final { get; set; }

    public decimal Reportado_Final { get; set; }

    public decimal Desp { get; set; }

    public decimal Cant { get; set; }
    public decimal CantUnd { get; set; }

    public string? Und { get; set; }


    public decimal Entrada { get; set; }
    public decimal Salida { get; set; }

    public int? Estado_Id { get; set; }
    public string? Est { get; set; }
    public string? Obs { get; set; }
    public DateTime? Fecha { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFinal { get; set; }

    public int Diff_Dias { get; set; }

    public string? Cli { get; set; }

    public int? Item { get; set; }
    public string? Ref { get; set; }
}

