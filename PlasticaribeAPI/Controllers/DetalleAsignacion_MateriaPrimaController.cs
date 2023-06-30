using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DetalleAsignacion_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_MateriaPrima>>> GetDetallesAsignaciones_MateriasPrimas()
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            return await _context.DetallesAsignaciones_MateriasPrimas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> GetDetalleAsignacion_MateriaPrima(long id)
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return NotFound();
          }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(id);

            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            return detalleAsignacion_MateriaPrima;
        }

        [HttpGet("estadoOT/{Estado_OrdenTrabajo}")]
        public ActionResult<DetalleAsignacion_MateriaPrima> GetDetalleAsignacion_MateriaPrima(int Estado_OrdenTrabajo)
        {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var DetAsignacion_MatPrima = _context.DetallesAsignaciones_MateriasPrimas
                /** Consulta OT */
                .Where(da => da.AsigMp.Estado_OrdenTrabajo == Estado_OrdenTrabajo)
                /** Relación con Materia Prima a través de la Prop. navegación */
                .Include(da => da.MatPri)
                /** Relación con Asignaciones_MatPrima a través de la Prop. navegación */
                .Include(da => da.AsigMp)
                /** Relación con Proceso a través de la Prop. navegación */
                .Include(da => da.Proceso)
                /** Agrupar por ciertos campos */
                /** Agrupar por ciertos campos */
                .GroupBy(da => new {
                    da.MatPri_Id,
                    da.MatPri.MatPri_Nombre,
                    da.UndMed_Id,
                    da.MatPri.MatPri_Precio,
                    da.Proceso.Proceso_Nombre,
                    da.AsigMp.AsigMp_FechaEntrega,
                    da.AsigMp.Usua.Usua_Nombre,
                    //da.AsigMp_Id,
                    da.AsigMp.Estado_OrdenTrabajo,
                    da.AsigMp.AsigMP_OrdenTrabajo
                })
                /** Campos a mostrar */
                .Select(agr => new
                {
                    /** 'Key' hace referencia a los campos que están en el GroupBy */
                    agr.Key.MatPri_Id,
                    agr.Key.MatPri_Nombre,
                    agr.Key.AsigMp_FechaEntrega,
                    agr.Key.Usua_Nombre,
                    Sum = agr.Sum(da => da.DtAsigMp_Cantidad),
                    agr.Key.UndMed_Id,
                    agr.Key.MatPri_Precio,
                    agr.Key.Proceso_Nombre,
                    agr.Key.Estado_OrdenTrabajo,
                    agr.Key.AsigMP_OrdenTrabajo
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (DetAsignacion_MatPrima == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(DetAsignacion_MatPrima);
            }
        }

        /*Materia Prima agrupada */
        [HttpGet("AsignacionesAgrupadas/{AsigMP_OrdenTrabajo}")] 
        public IActionResult GetAsignacion_MatPrimaAgrupada(long AsigMP_OrdenTrabajo)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var DetAsignacion_MatPrima = _context.DetallesAsignaciones_MateriasPrimas
                /** Consulta OT */
                .Where(da => da.AsigMp.AsigMP_OrdenTrabajo == AsigMP_OrdenTrabajo)
                /** Relación con Materia Prima a través de la Prop. navegación */
                .Include(da => da.MatPri)
                /** Relación con Asignaciones_MatPrima a través de la Prop. navegación */
                .Include(da => da.AsigMp)
                /** Relación con Proceso a través de la Prop. navegación */
                .Include(da => da.Proceso)
                /** Agrupar por ciertos campos */
                .GroupBy(da => new { da.MatPri_Id, da.MatPri.MatPri_Nombre, da.UndMed_Id, da.MatPri.MatPri_Precio, da.Proceso.Proceso_Nombre})
                /** Campos a mostrar */
                .Select(agr => new 
                {
                /** 'Key' hace referencia a los campos que están en el GroupBy */    
                      agr.Key.MatPri_Id,
                      agr.Key.MatPri_Nombre,
                      Sum = agr.Sum(da => da.DtAsigMp_Cantidad),
                      agr.Key.UndMed_Id,
                      agr.Key.MatPri_Precio,
                      agr.Key.Proceso_Nombre
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (DetAsignacion_MatPrima == null)
            {
                return NotFound();
            } else
            {
                return Ok(DetAsignacion_MatPrima);
            }

            
        }

        /* Materia Prima agrupada con OT Consultada para movimientos. */
        [HttpGet("AsignacionesAgrupadasXvalores/{AsigMP_OrdenTrabajo}")]
        public IActionResult GetAsignacion_MatPrimaValores(long AsigMP_OrdenTrabajo)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var DetAsignacion_MatPrima = _context.DetallesAsignaciones_MateriasPrimas
                /** Consulta OT */
                .Where(da => da.AsigMp.AsigMP_OrdenTrabajo == AsigMP_OrdenTrabajo)
                /** Relación con Materia Prima a través de la Prop. navegación */
                .Include(da => da.MatPri)
                /** Relación con Asignaciones_MatPrima a través de la Prop. navegación */
                .Include(da => da.AsigMp)
                /** Relación con Proceso a través de la Prop. navegación */
                .Include(da => da.Proceso)
                
                /** Agrupar por ciertos campos */
                .GroupBy(da => new { da.MatPri_Id, da.MatPri.MatPri_Nombre, da.UndMed_Id, 
                                     da.MatPri.MatPri_Precio, da.Proceso.Proceso_Nombre, 
                                     da.AsigMp.AsigMp_FechaEntrega, da.AsigMp.Usua.Usua_Nombre, da.AsigMp_Id })
                /** Campos a mostrar */
                .Select(agr => new
                {
                    /** 'Key' hace referencia a los campos que están en el GroupBy */
                    agr.Key.MatPri_Id,
                    agr.Key.MatPri_Nombre,
                    agr.Key.AsigMp_FechaEntrega,
                    agr.Key.Usua_Nombre,
                    Sum = agr.Sum(da => da.DtAsigMp_Cantidad),
                    agr.Key.UndMed_Id,
                    agr.Key.MatPri_Precio,
                    agr.Key.Proceso_Nombre
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (DetAsignacion_MatPrima == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(DetAsignacion_MatPrima);
            }
        }

        [HttpGet("asignacion/{AsigMp_Id}")]
        public ActionResult<DetalleAsignacion_MateriaPrima> GetIdAsignacion(long AsigMp_Id)
        {
            var detallesAsignacion = _context.DetallesAsignaciones_MateriasPrimas.Where(dtA => dtA.AsigMp_Id == AsigMp_Id).ToList();

            if (detallesAsignacion == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(detallesAsignacion);
            }
        }

        [HttpGet("matPri/{MatPri_Id}")]
        public ActionResult<DetalleAsignacion_MateriaPrima> GetIdMateriaPrima(long MatPri_Id)
        {
            var detallesAsignacion = _context.DetallesAsignaciones_MateriasPrimas.Where (dtA => dtA.MatPri_Id == MatPri_Id).ToList();

            if (detallesAsignacion == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(detallesAsignacion);
            }
        }
        
        [HttpGet("matPriFechaActual/{MatPri_Id}")]
        public ActionResult<DetalleAsignacion_MateriaPrima> GetIdMateriaPrimaFechaActual(long MatPri_Id, DateTime AsigMp_FechaEntrega)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var detallesAsignacion = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtA => dtA.MatPri_Id == MatPri_Id && dtA.AsigMp.AsigMp_FechaEntrega == AsigMp_FechaEntrega).ToList();

            if (detallesAsignacion == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(detallesAsignacion);
            }
        }

        //Aplica RRMP
        [HttpGet("consultaMovimientos0/{FechaInicial}")]
        public ActionResult Get(DateTime FechaInicial)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega == FechaInicial)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos1/{estado}")]
        public ActionResult Get(int estado)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }
        
        //Aplica RRMP
        [HttpGet("consultaMovimientos2/{MatPri}/{FechaInicial}")]
        public ActionResult GetMatPri(int MatPri, DateTime FechaInicial)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.MatPri_Id == MatPri
                       && dtAsg.AsigMp.AsigMp_FechaEntrega == FechaInicial)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos3/{ot}")]
        public ActionResult GetOT(long ot)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == ot)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        //Aplica RRMP
        [HttpGet("consultaMovimientos4/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos6/{Ot}/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(long Ot, DateTime FechaInicial, DateTime FechaFinal)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == Ot
                       && dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos7/{FechaInicial}/{FechaFinal}/{estado}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal, int estado)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }
        
        //Aplica RRMP 
        [HttpGet("consultaMovimientos8/{FechaInicial}/{FechaFinal}/{MatPri}")]
        public ActionResult Get8(DateTime FechaInicial, DateTime FechaFinal, int MatPri)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.MatPri_Id == MatPri)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos9/{FechaInicial}/{FechaFinal}/{MatPri}/{estado}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal, int MatPri, int estado)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.MatPri_Id == MatPri
                       && dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        [HttpGet("consultaMovimientos10/{Ot}/{FechaInicial}/{FechaFinal}/{MatPri}/{estado}")]
        public ActionResult Get (long Ot, DateTime FechaInicial, DateTime FechaFinal, int MatPri, int estado)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == Ot 
                       && dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial 
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal 
                       && dtAsg.MatPri_Id == MatPri
                       && dtAsg.AsigMp.Estado_OrdenTrabajo == estado)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        [HttpGet("pdfMovimientos/{Ot}")]
        public ActionResult Get(long Ot)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == Ot)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp.AsigMp_Id,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.AsigMp_Observacion,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_Maquina,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.UndMed_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.Proceso_Id,
                    dtAsg.Proceso.Proceso_Nombre
                })
                .ToList();
            return Ok(con);
        }

        [HttpGet("AsignacionesTotales/{ot}")]
        public ActionResult GetAsignacionesTotales(long ot)
        {
            var conMp = (from Asgmp in _context.Set<DetalleAsignacion_MateriaPrima>()
                        where Asgmp.AsigMp.AsigMP_OrdenTrabajo == ot
                        group Asgmp by new { 
                            Asgmp.MatPri_Id,
                            Asgmp.MatPri.MatPri_Nombre,
                            Asgmp.UndMed_Id,
                            Asgmp.MatPri.MatPri_Precio,
                            Asgmp.Proceso_Id,
                            Asgmp.Proceso.Proceso_Nombre
                        } into y
                        select new
                        {
                            //Materia Prima
                            MatPri_Id = Convert.ToInt16(y.Key.MatPri_Id),
                            Tinta_Id = Convert.ToInt16(2001),
                            Bopp_Id = Convert.ToInt16(449),
                            MateriaPrima = y.Key.MatPri_Id,
                            NombreMP = y.Key.MatPri_Nombre,
                            CantMP = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad),
                            UndMedida = y.Key.UndMed_Id,
                            Precio = y.Key.MatPri_Precio,
                            SubTotal = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad) * y.Key.MatPri_Precio,
                            Proceso = y.Key.Proceso_Id,
                            NombreProceso = y.Key.Proceso_Nombre
                        });

            var conTinta = (from AsgTinta in _context.Set<DetalleAsignacion_Tinta>()
                            where AsgTinta.AsigMp.AsigMP_OrdenTrabajo == ot
                            group AsgTinta by new
                            {
                                AsgTinta.Tinta_Id,
                                AsgTinta.Tinta.Tinta_Nombre,
                                AsgTinta.UndMed_Id,
                                AsgTinta.Tinta.Tinta_Precio,
                                AsgTinta.Proceso_Id,
                                AsgTinta.Proceso.Proceso_Nombre
                            } into y
                            select new
                            {
                                //Tintas
                                MatPri_Id = Convert.ToInt16(84),
                                Tinta_Id = Convert.ToInt16(y.Key.Tinta_Id),
                                Bopp_Id = Convert.ToInt16(449),
                                MateriaPrima = y.Key.Tinta_Id,
                                NombreMP = y.Key.Tinta_Nombre,
                                CantMP = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad),
                                UndMedida = y.Key.UndMed_Id,
                                Precio = y.Key.Tinta_Precio,
                                SubTotal = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad) * y.Key.Tinta_Precio,
                                Proceso = y.Key.Proceso_Id,
                                NombreProceso = y.Key.Proceso_Nombre
                            });

            var conBopp = (from AsgBopp in _context.Set<DetalleAsignacion_BOPP>()
                           where AsgBopp.DtAsigBOPP_OrdenTrabajo == ot
                           group AsgBopp by new
                           {
                               AsgBopp.BOPP_Id,
                               AsgBopp.BOPP.BOPP_Nombre,
                               AsgBopp.UndMed_Id,
                               AsgBopp.BOPP.BOPP_Precio,
                               AsgBopp.Proceso_Id,
                               AsgBopp.Proceso.Proceso_Nombre
                           } into y
                           select new
                           {
                               //BOPP
                               MatPri_Id = Convert.ToInt16(84),
                               Tinta_Id = Convert.ToInt16(2001),
                               Bopp_Id = Convert.ToInt16(y.Key.BOPP_Id),
                               MateriaPrima = y.Key.BOPP_Id,
                               NombreMP = y.Key.BOPP_Nombre,
                               CantMP = y.Sum(AsgBopp => AsgBopp.DtAsigBOPP_Cantidad),
                               UndMedida = y.Key.UndMed_Id,
                               Precio = y.Key.BOPP_Precio,
                               SubTotal = y.Sum(AsgBopp => AsgBopp.DtAsigBOPP_Cantidad) * y.Key.BOPP_Precio,
                               Proceso = y.Key.Proceso_Id,
                               NombreProceso = y.Key.Proceso_Nombre
                           });
            var con = conMp.Concat(conTinta).Concat(conBopp);

            return Ok(con);
        }

        /** CONSULTA PARA REPORTE DE RECUPERADO */
        [HttpGet("ReporteRecuperadoMP/{FechaInicial}/{FechaFinal}/{MatPri}/{estado}")]
        public ActionResult Get2(DateTime FechaInicial, DateTime FechaFinal, int MatPri)
        {
            var con = _context.DetallesAsignaciones_MateriasPrimas
                .Where(dtAsg => dtAsg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                       && dtAsg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       && dtAsg.MatPri_Id == MatPri)
                .Include(dtAsg => dtAsg.AsigMp)
                .Select(dtAsg => new
                {
                    dtAsg.AsigMp_Id,
                    dtAsg.AsigMp.AsigMP_OrdenTrabajo,
                    dtAsg.AsigMp.AsigMp_FechaEntrega,
                    dtAsg.AsigMp.Usua.Usua_Nombre,
                    dtAsg.AsigMp.Usua_Id,
                    dtAsg.MatPri.MatPri_Nombre,
                    dtAsg.MatPri_Id,
                    dtAsg.DtAsigMp_Cantidad,
                    dtAsg.AsigMp.Estado_OrdenTrabajo,
                    dtAsg.AsigMp.EstadoOT.Estado_Nombre
                }).ToList();
            return Ok(con);
        }

        //Consulta que traerá la cantidad de materia prima asignada, teniendo en cuenta la materia prima devuelta
        [HttpGet("getMateriaPrimaAsignada/{ot}")]
        public ActionResult GetMateriaPrimaAsignada(int ot)
        {
            var asig = (from asg in _context.Set<DetalleAsignacion_MateriaPrima>()
                       where asg.AsigMp.AsigMP_OrdenTrabajo == ot
                       select asg.DtAsigMp_Cantidad).Sum();

            var asgBopp = (from asgbopp in _context.Set<DetalleAsignacion_BOPP>()
                          where asgbopp.DtAsigBOPP_OrdenTrabajo == ot
                          select asgbopp.DtAsigBOPP_Cantidad).Sum();

            var devol = (from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                        where dev.DevMatPri.DevMatPri_OrdenTrabajo == ot
                        select dev.DtDevMatPri_CantidadDevuelta).Sum();

            var asigs = (asig + asgBopp) - devol;
            return Ok(asigs);
        }

        /** Obtener las materias primas asignadas en base a solicitudes de material */
        [HttpGet("getAsignacionesConSolicitudes/{idSolicitud}")]
        public ActionResult GetAsignacionesConSolicitudes(long idSolicitud)
        {
            var conMp = (from Asgmp in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where Asgmp.AsigMp.SolMpExt_Id == idSolicitud
                         group Asgmp by new
                         {
                             Asgmp.MatPri_Id,
                             Asgmp.MatPri.MatPri_Nombre,
                             Asgmp.UndMed_Id,
                             Asgmp.MatPri.MatPri_Precio
                         } into y
                         select new
                         {
                             //Materia Prima
                             MatPri_Id = Convert.ToInt16(y.Key.MatPri_Id),
                             Tinta_Id = Convert.ToInt16(2001),
                             MateriaPrima = y.Key.MatPri_Id,
                             NombreMP = y.Key.MatPri_Nombre,
                             CantMP = y.Sum(Asgmp => Asgmp.DtAsigMp_Cantidad),
                             UndMedida = y.Key.UndMed_Id,
                         });

            var conTinta = (from AsgTinta in _context.Set<DetalleAsignacion_Tinta>()
                            where AsgTinta.AsigMp.SolMpExt_Id == idSolicitud
                            group AsgTinta by new
                            {
                                AsgTinta.Tinta_Id,
                                AsgTinta.Tinta.Tinta_Nombre,
                                AsgTinta.UndMed_Id,
                            } into y
                            select new
                            {
                                //Tintas
                                MatPri_Id = Convert.ToInt16(84),
                                Tinta_Id = Convert.ToInt16(y.Key.Tinta_Id),
                                MateriaPrima = y.Key.Tinta_Id,
                                NombreMP = y.Key.Tinta_Nombre,
                                CantMP = y.Sum(AsgTinta => AsgTinta.DtAsigTinta_Cantidad),
                                UndMedida = y.Key.UndMed_Id,
                            });

            var con = conMp.Concat(conTinta);
            if (con == null) return BadRequest("No se encontraron datos de la solicitud consultada");
            return Ok(con);
        }

        // PUT: api/DetalleAsignacion_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_MateriaPrima(long AsigMp_Id, long MatPri_Id, DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
            if (AsigMp_Id != detalleAsignacion_MateriaPrima.AsigMp_Id && MatPri_Id != detalleAsignacion_MateriaPrima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_MateriaPrimaExists(AsigMp_Id) && !DetalleAsignacion_MateriaPrimaExists(MatPri_Id))
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

        // POST: api/DetalleAsignacion_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_MateriaPrima>> PostDetalleAsignacion_MateriaPrima(DetalleAsignacion_MateriaPrima detalleAsignacion_MateriaPrima)
        {
          if (_context.DetallesAsignaciones_MateriasPrimas == null)
          {
              return Problem("Entity set 'dataContext.DetallesAsignaciones_MateriasPrimas'  is null.");
          }
            _context.DetallesAsignaciones_MateriasPrimas.Add(detalleAsignacion_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_MateriaPrimaExists(detalleAsignacion_MateriaPrima.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_MateriaPrima", new { id = detalleAsignacion_MateriaPrima.Codigo }, detalleAsignacion_MateriaPrima);
        }

        // DELETE: api/DetalleAsignacion_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_MateriaPrima(long id)
        {
            if (_context.DetallesAsignaciones_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_MateriaPrima = await _context.DetallesAsignaciones_MateriasPrimas.FindAsync(id);
            if (detalleAsignacion_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_MateriasPrimas.Remove(detalleAsignacion_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_MateriaPrimaExists(long id)
        {
            return (_context.DetallesAsignaciones_MateriasPrimas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
