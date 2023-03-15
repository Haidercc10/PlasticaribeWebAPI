using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DetalleAsignacion_TintaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_TintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_Tinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_Tinta>>> GetDetalleAsignaciones_Tintas()
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return NotFound();
          }
            return await _context.DetalleAsignaciones_Tintas.ToListAsync();
        }

        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_Tinta>> GetDetalleAsignacion_Tinta(long id)
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return NotFound();
          }
            var detalleAsignacion_Tinta = await _context.DetalleAsignaciones_Tintas.FindAsync(id);

            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            }

            return detalleAsignacion_Tinta;
        }


        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("asignacion/{AsigMp_Id}")]
        public ActionResult<DetalleAsignacion_Tinta> GetDetalleAsignacion(long AsigMp_Id)
        {
            var detalleAsignacion_Tinta = _context.DetalleAsignaciones_Tintas.Where(dtAsg => dtAsg.AsigMp_Id == AsigMp_Id).ToList();

            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(detalleAsignacion_Tinta);
            }

        }


        /** Consulta para traer la información de tintas agrupadas */
        [HttpGet("AsignacionesTintasxOT/{AsigMP_OrdenTrabajo}")]
        public IActionResult GetAsignacion_TintaAgrupada(long AsigMP_OrdenTrabajo)
        {
            if (_context.DetalleAsignaciones_Tintas == null)
            {
                return NotFound();
            }

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                      where asg.AsigMp.AsigMP_OrdenTrabajo == AsigMP_OrdenTrabajo
                      group asg by new
                      {
                          asg.Tinta_Id,
                          asg.Tinta.Tinta_Nombre,
                          asg.UndMed_Id,
                          asg.Tinta.Tinta_Precio,
                          asg.Proceso.Proceso_Nombre,
                      } into asg
                      select new
                      {
                          Tinta = asg.Key.Tinta_Id,
                          Tinta2 = asg.Key.Tinta_Id,
                          NombreTinta = asg.Key.Tinta_Nombre,
                          NombreTinta2 = asg.Key.Tinta_Nombre,
                          Suma = asg.Sum(x => x.DtAsigTinta_Cantidad),
                          Presentacion = asg.Key.UndMed_Id,
                          PrecioTinta = asg.Key.Tinta_Precio,
                          Proceso = asg.Key.Proceso_Nombre,
                          Materia_Prima = asg.Key.Tinta_Id,
                          Nombre_MateriaPrima = asg.Key.Tinta_Nombre
                      };

            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr.AsigMPxTinta.Tinta_Id == AsigMP_OrdenTrabajo
                           group cr by new
                           {
                               cr.AsigMPxTinta.Tinta_Id,
                               cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               cr.UndMed_Id,
                               cr.AsigMPxTinta.Tinta.Tinta_Precio,
                               cr.Proceso.Proceso_Nombre,
                               Tinta2 = cr.Tinta_Id,
                               Tinta2_Nombre = cr.TintasDAMPxT.Tinta_Nombre,
                               cr.MatPri_Id,
                               cr.MatPri.MatPri_Nombre,
                           } into cr
                           select new
                           {
                               Tinta = cr.Key.Tinta_Id,
                               Tinta2 = cr.Key.Tinta2,
                               NombreTinta = cr.Key.Tinta_Nombre,
                               NombreTinta2 = cr.Key.Tinta2_Nombre,
                               Suma = cr.Sum(x => x.DetAsigMPxTinta_Cantidad),
                               Presentacion = cr.Key.UndMed_Id,
                               PrecioTinta = cr.Key.Tinta_Precio,
                               Proceso = cr.Key.Proceso_Nombre,
                               Materia_Prima = cr.Key.MatPri_Id,
                               Nombre_MateriaPrima = cr.Key.MatPri_Nombre,
                           };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            if (asig == null || creacion == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(asig.Concat(creacion));
            }
        }


        // GET: api/DetalleAsignacion_Tinta/5
        [HttpGet("SumaTintas_MatPrimasAsignadas/{OT}")]
        public ActionResult<DetalleAsignacion_Tinta> GetDetalleAsignacion2(long OT)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var union_Asignaciones = _context.DetalleAsignaciones_Tintas
                .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == OT).Sum(da => da.DtAsigTinta_Cantidad);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var union_Asignaciones2 = _context.DetallesAsignaciones_MateriasPrimas
               .Where(dtAsg => dtAsg.AsigMp.AsigMP_OrdenTrabajo == OT).Sum(da => da.DtAsigMp_Cantidad);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var devoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(dev => dev.DevMatPri.DevMatPri_OrdenTrabajo == OT).Sum(dev => dev.DtDevMatPri_CantidadDevuelta);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


            var union_Asignaciones3 = (union_Asignaciones + union_Asignaciones2) - devoluciones;

            return Ok(union_Asignaciones3);
            

        }


        /** Consulta por fecha de entrega */
        [HttpGet("MovTintasxFechaEntrega/{FechaInicial}")]
        public ActionResult Get(DateTime FechaInicial)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMp_FechaEntrega == FechaInicial
                       select new
                       {
                           ID = Convert.ToInt32(asg.AsigMp_Id),
                           OT = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = Convert.ToString(asg.AsigMp.EstadoOT.Estado_Nombre),
                       };

            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega == FechaInicial
                           select new
                           {
                               ID = Convert.ToInt32(cr.AsigMPxTinta_Id),
                               OT = Convert.ToString(cr.AsigMPxTinta.Tinta_Id),
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Usuario = cr.AsigMPxTinta.Usua_Id,
                               Usuario_Nombre = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Tinta = cr.AsigMPxTinta.Tinta_Id,
                               NombreTinta = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad = cr.DetAsigMPxTinta_Cantidad,
                               Presentacion = cr.UndMed_Id,
                               PrecioTinta = cr.TintasDAMPxT.Tinta_Precio,
                               Tinta2 = cr.Tinta_Id,
                               Nombre_Tinta2 = cr.TintasDAMPxT.Tinta_Nombre,
                               MateriaPrima = cr.MatPri_Id,
                               Nombre_MateriaPrima = cr.MatPri.MatPri_Nombre,
                               CantidadAsignada = cr.DetAsigMPxTinta_Cantidad,
                               Estado = Convert.ToString(cr.Proceso.Proceso_Nombre),
                           };

            var facturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                           where fac.Facco.Facco_FechaFactura == FechaInicial
                                 && fac.Tinta_Id != 2001
                           select new
                           {
                               ID = Convert.ToInt32(fac.Facco_Id),
                               OT = Convert.ToString(fac.Facco.Facco_Codigo),
                               Fecha = fac.Facco.Facco_FechaFactura,
                               Usuario = fac.Facco.Usua_Id,
                               Usuario_Nombre = fac.Facco.Usua.Usua_Nombre,
                               Tinta = fac.Tinta_Id,
                               NombreTinta = fac.Tinta.Tinta_Nombre,
                               Cantidad = fac.FaccoMatPri_Cantidad,
                               Presentacion = fac.UndMed_Id,
                               PrecioTinta = fac.Tinta.Tinta_Precio,
                               Tinta2 = fac.Tinta_Id,
                               Nombre_Tinta2 = fac.Tinta.Tinta_Nombre,
                               MateriaPrima = fac.Tinta.Tinta_Id,
                               Nombre_MateriaPrima = fac.Tinta.Tinta_Nombre,
                               CantidadAsignada = fac.FaccoMatPri_Cantidad,
                               Estado = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                           };

            var remisiones = from rem in _context.Set<Remision_MateriaPrima>()
                             where rem.Rem.Rem_Fecha == FechaInicial
                                   && rem.Tinta_Id != 2001
                             select new
                             {
                                 ID = Convert.ToInt32(rem.Rem_Id),
                                 OT = Convert.ToString(rem.Rem.Rem_Codigo),
                                 Fecha = rem.Rem.Rem_Fecha,
                                 Usuario = rem.Rem.Usua_Id,
                                 Usuario_Nombre = rem.Rem.Usua.Usua_Nombre,
                                 Tinta = rem.Tinta_Id,
                                 NombreTinta = rem.Tinta.Tinta_Nombre,
                                 Cantidad = rem.RemiMatPri_Cantidad,
                                 Presentacion = rem.UndMed_Id,
                                 PrecioTinta = rem.Tinta.Tinta_Precio,
                                 Tinta2 = rem.Tinta_Id,
                                 Nombre_Tinta2 = rem.Tinta.Tinta_Nombre,
                                 MateriaPrima = rem.Tinta.Tinta_Id,
                                 Nombre_MateriaPrima = rem.Tinta.Tinta_Nombre,
                                 CantidadAsignada = rem.RemiMatPri_Cantidad,
                                 Estado = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                             };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(asig.Concat(creacion).Concat(facturas).Concat(remisiones));
        }


        /** Consulta por Estado */
        [HttpGet("MovTintasxEstado/{estado}")]
        public ActionResult Get(int estado)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.Estado_OrdenTrabajo == estado
                       select new
                       {
                           OT = asg.AsigMp.AsigMP_OrdenTrabajo,
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = asg.AsigMp.EstadoOT.Estado_Nombre,
                       };

            /*var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr. == estado
                           select new
                           {
                               OT = cr.AsigMPxTinta.Tinta_Id,
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Usuario = cr.AsigMPxTinta.Usua_Id,
                               Usuario_Nombre = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Tinta = cr.AsigMPxTinta.Tinta_Id,
                               NombreTinta = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad = cr.DetAsigMPxTinta_Cantidad,
                               Presentacion = cr.UndMed_Id,
                               PrecioTinta = cr.TintasDAMPxT.Tinta_Precio,
                               Tinta2 = cr.Tinta_Id,
                               Nombre_Tinta2 = cr.TintasDAMPxT.Tinta_Nombre,
                               MateriaPrima = cr.MatPri_Id,
                               Nombre_MateriaPrima = cr.MatPri.MatPri_Nombre,
                               CantidadAsignada = cr.DetAsigMPxTinta_Cantidad,
                               Estado = cr.Proceso.Proceso_Nombre,
                           };*/
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(asig);
        }


        /** Consulta por Tinta y Fecha Inicial */
        [HttpGet("MovTintasxIdxFechaEntrega/{FechaInicial}/{nTinta}")]
        public ActionResult GetMatPri(DateTime FechaInicial, int nTinta)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMp_FechaEntrega == FechaInicial && asg.Tinta_Id == nTinta
                       select new
                       {
                           ID = Convert.ToInt32(asg.AsigMp_Id),
                           OT = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = Convert.ToString(asg.AsigMp.EstadoOT.Estado_Nombre),
                       };

            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega == FechaInicial && cr.AsigMPxTinta.Tinta_Id == nTinta
                           select new
                           {
                               ID = Convert.ToInt32(cr.AsigMPxTinta_Id),
                               OT = Convert.ToString(cr.AsigMPxTinta.Tinta_Id),
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Usuario = cr.AsigMPxTinta.Usua_Id,
                               Usuario_Nombre = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Tinta = cr.AsigMPxTinta.Tinta_Id,
                               NombreTinta = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad = cr.DetAsigMPxTinta_Cantidad,
                               Presentacion = cr.UndMed_Id,
                               PrecioTinta = cr.TintasDAMPxT.Tinta_Precio,
                               Tinta2 = cr.Tinta_Id,
                               Nombre_Tinta2 = cr.TintasDAMPxT.Tinta_Nombre,
                               MateriaPrima = cr.MatPri_Id,
                               Nombre_MateriaPrima = cr.MatPri.MatPri_Nombre,
                               CantidadAsignada = cr.DetAsigMPxTinta_Cantidad,
                               Estado = Convert.ToString(cr.Proceso.Proceso_Nombre),
                           };

            var facturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                           where fac.Facco.Facco_FechaFactura == FechaInicial 
                                 && fac.Tinta_Id == nTinta
                                 && fac.Tinta_Id != 2001
                           select new
                           {
                               ID = Convert.ToInt32(fac.Facco_Id),
                               OT = Convert.ToString(fac.Facco.Facco_Codigo),
                               Fecha = fac.Facco.Facco_FechaFactura,
                               Usuario = fac.Facco.Usua_Id,
                               Usuario_Nombre = fac.Facco.Usua.Usua_Nombre,
                               Tinta = fac.Tinta_Id,
                               NombreTinta = fac.Tinta.Tinta_Nombre,
                               Cantidad = fac.FaccoMatPri_Cantidad,
                               Presentacion = fac.UndMed_Id,
                               PrecioTinta = fac.Tinta.Tinta_Precio,
                               Tinta2 = fac.Tinta_Id,
                               Nombre_Tinta2 = fac.Tinta.Tinta_Nombre,
                               MateriaPrima = fac.Tinta.Tinta_Id,
                               Nombre_MateriaPrima = fac.Tinta.Tinta_Nombre,
                               CantidadAsignada = fac.FaccoMatPri_Cantidad,
                               Estado = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                           };

            var remisiones = from rem in _context.Set<Remision_MateriaPrima>()
                             where rem.Rem.Rem_Fecha == FechaInicial 
                                   && rem.Tinta_Id == nTinta
                                   && rem.Tinta_Id != 2001
                             select new
                             {
                                 ID = Convert.ToInt32(rem.Rem_Id),
                                 OT = Convert.ToString(rem.Rem.Rem_Codigo),
                                 Fecha = rem.Rem.Rem_Fecha,
                                 Usuario = rem.Rem.Usua_Id,
                                 Usuario_Nombre = rem.Rem.Usua.Usua_Nombre,
                                 Tinta = rem.Tinta_Id,
                                 NombreTinta = rem.Tinta.Tinta_Nombre,
                                 Cantidad = rem.RemiMatPri_Cantidad,
                                 Presentacion = rem.UndMed_Id,
                                 PrecioTinta = rem.Tinta.Tinta_Precio,
                                 Tinta2 = rem.Tinta_Id,
                                 Nombre_Tinta2 = rem.Tinta.Tinta_Nombre,
                                 MateriaPrima = rem.Tinta.Tinta_Id,
                                 Nombre_MateriaPrima = rem.Tinta.Tinta_Nombre,
                                 CantidadAsignada = rem.RemiMatPri_Cantidad,
                                 Estado = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                             };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(asig.Concat(creacion).Concat(facturas).Concat(remisiones));
        }

        /** Consulta por OT */
        [HttpGet("MovTintasxOT/{ot}")]
        public ActionResult GetOT(string ot)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMP_OrdenTrabajo == Convert.ToInt64(ot)
                       select new
                       {
                           ID = Convert.ToInt32(asg.AsigMp_Id),
                           OT = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = Convert.ToString(asg.AsigMp.EstadoOT.Estado_Nombre),
                       };

            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr.AsigMPxTinta.Tinta_Id == Convert.ToInt64(ot)
                           select new
                           {
                               ID = Convert.ToInt32(cr.AsigMPxTinta_Id),
                               OT = Convert.ToString(cr.AsigMPxTinta.Tinta_Id),
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Usuario = cr.AsigMPxTinta.Usua_Id,
                               Usuario_Nombre = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Tinta = cr.AsigMPxTinta.Tinta_Id,
                               NombreTinta = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad = cr.DetAsigMPxTinta_Cantidad,
                               Presentacion = cr.UndMed_Id,
                               PrecioTinta = cr.TintasDAMPxT.Tinta_Precio,
                               Tinta2 = cr.Tinta_Id,
                               Nombre_Tinta2 = cr.TintasDAMPxT.Tinta_Nombre,
                               MateriaPrima = cr.MatPri_Id,
                               Nombre_MateriaPrima = cr.MatPri.MatPri_Nombre,
                               CantidadAsignada = cr.DetAsigMPxTinta_Cantidad,
                               Estado = Convert.ToString(cr.Proceso.Proceso_Nombre),
                           };

            var facturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                           where fac.Facco.Facco_Codigo == Convert.ToString(ot)
                                 && fac.Tinta_Id != 2001
                           select new
                           {
                               ID = Convert.ToInt32(fac.Facco_Id),
                               OT = Convert.ToString(fac.Facco.Facco_Codigo),
                               Fecha = fac.Facco.Facco_FechaFactura,
                               Usuario = fac.Facco.Usua_Id,
                               Usuario_Nombre = fac.Facco.Usua.Usua_Nombre,
                               Tinta = fac.Tinta_Id,
                               NombreTinta = fac.Tinta.Tinta_Nombre,
                               Cantidad = fac.FaccoMatPri_Cantidad,
                               Presentacion = fac.UndMed_Id,
                               PrecioTinta = fac.Tinta.Tinta_Precio,
                               Tinta2 = fac.Tinta_Id,
                               Nombre_Tinta2 = fac.Tinta.Tinta_Nombre,
                               MateriaPrima = fac.Tinta.Tinta_Id,
                               Nombre_MateriaPrima = fac.Tinta.Tinta_Nombre,
                               CantidadAsignada = fac.FaccoMatPri_Cantidad,
                               Estado = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                           };

            var remisiones = from rem in _context.Set<Remision_MateriaPrima>()
                             where rem.Rem.Rem_Codigo == Convert.ToString(ot)
                                   && rem.Tinta_Id != 2001
                             select new
                             {
                                 ID = Convert.ToInt32(rem.Rem_Id),
                                 OT = Convert.ToString(rem.Rem.Rem_Codigo),
                                 Fecha = rem.Rem.Rem_Fecha,
                                 Usuario = rem.Rem.Usua_Id,
                                 Usuario_Nombre = rem.Rem.Usua.Usua_Nombre,
                                 Tinta = rem.Tinta_Id,
                                 NombreTinta = rem.Tinta.Tinta_Nombre,
                                 Cantidad = rem.RemiMatPri_Cantidad,
                                 Presentacion = rem.UndMed_Id,
                                 PrecioTinta = rem.Tinta.Tinta_Precio,
                                 Tinta2 = rem.Tinta_Id,
                                 Nombre_Tinta2 = rem.Tinta.Tinta_Nombre,
                                 MateriaPrima = rem.Tinta.Tinta_Id,
                                 Nombre_MateriaPrima = rem.Tinta.Tinta_Nombre,
                                 CantidadAsignada = rem.RemiMatPri_Cantidad,
                                 Estado = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                             };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(asig.Concat(creacion).Concat(facturas).Concat(remisiones));
        }

        /** Consulta por Fechas */
        [HttpGet("MovTintasxFechas/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMp_FechaEntrega >= FechaInicial 
                             && asg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       select new
                       {
                           ID = Convert.ToInt32(asg.AsigMp_Id),
                           OT = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = Convert.ToString(asg.AsigMp.EstadoOT.Estado_Nombre),
                       };

            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= FechaInicial
                                 && cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= FechaInicial
                           select new
                           {
                               ID = Convert.ToInt32(cr.AsigMPxTinta_Id),
                               OT = Convert.ToString(cr.AsigMPxTinta.Tinta_Id),
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Usuario = cr.AsigMPxTinta.Usua_Id,
                               Usuario_Nombre = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Tinta = cr.AsigMPxTinta.Tinta_Id,
                               NombreTinta = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad = cr.DetAsigMPxTinta_Cantidad,
                               Presentacion = cr.UndMed_Id,
                               PrecioTinta = cr.TintasDAMPxT.Tinta_Precio,
                               Tinta2 = cr.Tinta_Id,
                               Nombre_Tinta2 = cr.TintasDAMPxT.Tinta_Nombre,
                               MateriaPrima = cr.MatPri_Id,
                               Nombre_MateriaPrima = cr.MatPri.MatPri_Nombre,
                               CantidadAsignada = cr.DetAsigMPxTinta_Cantidad,
                               Estado = Convert.ToString(cr.Proceso.Proceso_Nombre),
                           };

            var facturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                           where fac.Facco.Facco_FechaFactura >= FechaInicial
                                 && fac.Facco.Facco_FechaFactura <= FechaFinal
                                 && fac.Tinta_Id != 2001
                           select new
                           {
                               ID = Convert.ToInt32(fac.Facco_Id),
                               OT = Convert.ToString(fac.Facco.Facco_Codigo),
                               Fecha = fac.Facco.Facco_FechaFactura,
                               Usuario = fac.Facco.Usua_Id,
                               Usuario_Nombre = fac.Facco.Usua.Usua_Nombre,
                               Tinta = fac.Tinta_Id,
                               NombreTinta = fac.Tinta.Tinta_Nombre,
                               Cantidad = fac.FaccoMatPri_Cantidad,
                               Presentacion = fac.UndMed_Id,
                               PrecioTinta = fac.Tinta.Tinta_Precio,
                               Tinta2 = fac.Tinta_Id,
                               Nombre_Tinta2 = fac.Tinta.Tinta_Nombre,
                               MateriaPrima = fac.Tinta.Tinta_Id,
                               Nombre_MateriaPrima = fac.Tinta.Tinta_Nombre,
                               CantidadAsignada = fac.FaccoMatPri_Cantidad,
                               Estado = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                           };

            var remisiones = from rem in _context.Set<Remision_MateriaPrima>()
                             where rem.Rem.Rem_Fecha >= FechaInicial
                                   && rem.Rem.Rem_Fecha <= FechaFinal
                                   && rem.Tinta_Id != 2001
                             select new
                             {
                                 ID = Convert.ToInt32(rem.Rem_Id),
                                 OT = Convert.ToString(rem.Rem.Rem_Codigo),
                                 Fecha = rem.Rem.Rem_Fecha,
                                 Usuario = rem.Rem.Usua_Id,
                                 Usuario_Nombre = rem.Rem.Usua.Usua_Nombre,
                                 Tinta = rem.Tinta_Id,
                                 NombreTinta = rem.Tinta.Tinta_Nombre,
                                 Cantidad = rem.RemiMatPri_Cantidad,
                                 Presentacion = rem.UndMed_Id,
                                 PrecioTinta = rem.Tinta.Tinta_Precio,
                                 Tinta2 = rem.Tinta_Id,
                                 Nombre_Tinta2 = rem.Tinta.Tinta_Nombre,
                                 MateriaPrima = rem.Tinta.Tinta_Id,
                                 Nombre_MateriaPrima = rem.Tinta.Tinta_Nombre,
                                 CantidadAsignada = rem.RemiMatPri_Cantidad,
                                 Estado = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                             };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(asig.Concat(creacion).Concat(facturas).Concat(remisiones));
        }

        /** Consulta por OT y Fechas */
        [HttpGet("MovTintasxOTxFechas/{Ot}/{FechaInicial}/{FechaFinal}")]
        public ActionResult Get(long Ot, DateTime FechaInicial, DateTime FechaFinal)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMP_OrdenTrabajo == Ot
                             && asg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                             && asg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       select new
                       {
                           ID = Convert.ToInt32(asg.AsigMp_Id),
                           OT = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = Convert.ToString(asg.AsigMp.EstadoOT.Estado_Nombre),
                       };

            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr.AsigMPxTinta.Tinta_Id == Ot
                                 && cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= FechaInicial
                                 && cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= FechaFinal
                           select new
                           {
                               ID = Convert.ToInt32(cr.AsigMPxTinta_Id),
                               OT = Convert.ToString(cr.AsigMPxTinta.Tinta_Id),
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Usuario = cr.AsigMPxTinta.Usua_Id,
                               Usuario_Nombre = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Tinta = cr.AsigMPxTinta.Tinta_Id,
                               NombreTinta = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad = cr.DetAsigMPxTinta_Cantidad,
                               Presentacion = cr.UndMed_Id,
                               PrecioTinta = cr.TintasDAMPxT.Tinta_Precio,
                               Tinta2 = cr.Tinta_Id,
                               Nombre_Tinta2 = cr.TintasDAMPxT.Tinta_Nombre,
                               MateriaPrima = cr.MatPri_Id,
                               Nombre_MateriaPrima = cr.MatPri.MatPri_Nombre,
                               CantidadAsignada = cr.DetAsigMPxTinta_Cantidad,
                               Estado = Convert.ToString(cr.Proceso.Proceso_Nombre),
                           };

            var facturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                           where fac.Facco.Facco_Codigo == Convert.ToString(Ot)
                                 && fac.Facco.Facco_FechaFactura >= FechaInicial
                                 && fac.Facco.Facco_FechaFactura <= FechaFinal
                                 && fac.Tinta_Id != 2001
                           select new
                           {
                               ID = Convert.ToInt32(fac.Facco_Id),
                               OT = Convert.ToString(fac.Facco.Facco_Codigo),
                               Fecha = fac.Facco.Facco_FechaFactura,
                               Usuario = fac.Facco.Usua_Id,
                               Usuario_Nombre = fac.Facco.Usua.Usua_Nombre,
                               Tinta = fac.Tinta_Id,
                               NombreTinta = fac.Tinta.Tinta_Nombre,
                               Cantidad = fac.FaccoMatPri_Cantidad,
                               Presentacion = fac.UndMed_Id,
                               PrecioTinta = fac.Tinta.Tinta_Precio,
                               Tinta2 = fac.Tinta_Id,
                               Nombre_Tinta2 = fac.Tinta.Tinta_Nombre,
                               MateriaPrima = fac.Tinta.Tinta_Id,
                               Nombre_MateriaPrima = fac.Tinta.Tinta_Nombre,
                               CantidadAsignada = fac.FaccoMatPri_Cantidad,
                               Estado = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                           };

            var remisiones = from rem in _context.Set<Remision_MateriaPrima>()
                             where rem.Rem.Rem_Codigo == Convert.ToString(Ot)
                                   && rem.Rem.Rem_Fecha >= FechaInicial
                                   && rem.Rem.Rem_Fecha <= FechaFinal
                                   && rem.Tinta_Id != 2001
                             select new
                             {
                                 ID = Convert.ToInt32(rem.Rem_Id),
                                 OT = Convert.ToString(rem.Rem.Rem_Codigo),
                                 Fecha = rem.Rem.Rem_Fecha,
                                 Usuario = rem.Rem.Usua_Id,
                                 Usuario_Nombre = rem.Rem.Usua.Usua_Nombre,
                                 Tinta = rem.Tinta_Id,
                                 NombreTinta = rem.Tinta.Tinta_Nombre,
                                 Cantidad = rem.RemiMatPri_Cantidad,
                                 Presentacion = rem.UndMed_Id,
                                 PrecioTinta = rem.Tinta.Tinta_Precio,
                                 Tinta2 = rem.Tinta_Id,
                                 Nombre_Tinta2 = rem.Tinta.Tinta_Nombre,
                                 MateriaPrima = rem.Tinta.Tinta_Id,
                                 Nombre_MateriaPrima = rem.Tinta.Tinta_Nombre,
                                 CantidadAsignada = rem.RemiMatPri_Cantidad,
                                 Estado = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                             };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(asig.Concat(creacion).Concat(facturas).Concat(remisiones));
        }

        /** Consulta por Estado y Fechas */
        [HttpGet("MovTintasxFechasxEstado/{FechaInicial}/{FechaFinal}/{estado}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal, int estado)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.Estado_OrdenTrabajo == estado
                             && asg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                             && asg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                       select new
                       {
                           OT = asg.AsigMp.AsigMP_OrdenTrabajo,
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = asg.AsigMp.EstadoOT.Estado_Nombre,
                       };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(asig);
        }


        /** Consulta por Tinta y Fechas */
        [HttpGet("MovTintasxFechasxTinta/{FechaInicial}/{FechaFinal}/{nTinta}")]
        public ActionResult Get8(DateTime FechaInicial, DateTime FechaFinal, int nTinta)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                             && asg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                             && asg.Tinta_Id == nTinta
                       select new
                       {
                           ID = Convert.ToInt32(asg.AsigMp_Id),
                           OT = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = Convert.ToString(asg.AsigMp.EstadoOT.Estado_Nombre),
                       };

            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           where cr.AsigMPxTinta.Tinta_Id == nTinta
                                 && cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= FechaInicial
                                 && cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= FechaFinal
                           select new
                           {
                               ID = Convert.ToInt32(cr.AsigMPxTinta_Id),
                               OT = Convert.ToString(cr.AsigMPxTinta.Tinta_Id),
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Usuario = cr.AsigMPxTinta.Usua_Id,
                               Usuario_Nombre = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Tinta = cr.AsigMPxTinta.Tinta_Id,
                               NombreTinta = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad = cr.DetAsigMPxTinta_Cantidad,
                               Presentacion = cr.UndMed_Id,
                               PrecioTinta = cr.TintasDAMPxT.Tinta_Precio,
                               Tinta2 = cr.Tinta_Id,
                               Nombre_Tinta2 = cr.TintasDAMPxT.Tinta_Nombre,
                               MateriaPrima = cr.MatPri_Id,
                               Nombre_MateriaPrima = cr.MatPri.MatPri_Nombre,
                               CantidadAsignada = cr.DetAsigMPxTinta_Cantidad,
                               Estado = Convert.ToString(cr.Proceso.Proceso_Nombre),
                           };

            var facturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                           where fac.Facco.Facco_FechaFactura >= FechaInicial
                                 && fac.Facco.Facco_FechaFactura <= FechaFinal
                                 && fac.Tinta_Id == nTinta
                                 && fac.Tinta_Id != 2001
                           select new
                           {
                               ID = Convert.ToInt32(fac.Facco_Id),
                               OT = Convert.ToString(fac.Facco.Facco_Codigo),
                               Fecha = fac.Facco.Facco_FechaFactura,
                               Usuario = fac.Facco.Usua_Id,
                               Usuario_Nombre = fac.Facco.Usua.Usua_Nombre,
                               Tinta = fac.Tinta_Id,
                               NombreTinta = fac.Tinta.Tinta_Nombre,
                               Cantidad = fac.FaccoMatPri_Cantidad,
                               Presentacion = fac.UndMed_Id,
                               PrecioTinta = fac.Tinta.Tinta_Precio,
                               Tinta2 = fac.Tinta_Id,
                               Nombre_Tinta2 = fac.Tinta.Tinta_Nombre,
                               MateriaPrima = fac.Tinta.Tinta_Id,
                               Nombre_MateriaPrima = fac.Tinta.Tinta_Nombre,
                               CantidadAsignada = fac.FaccoMatPri_Cantidad,
                               Estado = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                           };

            var remisiones = from rem in _context.Set<Remision_MateriaPrima>()
                             where rem.Rem.Rem_Fecha >= FechaInicial
                                   && rem.Rem.Rem_Fecha <= FechaFinal
                                   && rem.Tinta_Id == nTinta
                                   && rem.Tinta_Id != 2001
                             select new
                             {
                                 ID = Convert.ToInt32(rem.Rem_Id),
                                 OT = Convert.ToString(rem.Rem.Rem_Codigo),
                                 Fecha = rem.Rem.Rem_Fecha,
                                 Usuario = rem.Rem.Usua_Id,
                                 Usuario_Nombre = rem.Rem.Usua.Usua_Nombre,
                                 Tinta = rem.Tinta_Id,
                                 NombreTinta = rem.Tinta.Tinta_Nombre,
                                 Cantidad = rem.RemiMatPri_Cantidad,
                                 Presentacion = rem.UndMed_Id,
                                 PrecioTinta = rem.Tinta.Tinta_Precio,
                                 Tinta2 = rem.Tinta_Id,
                                 Nombre_Tinta2 = rem.Tinta.Tinta_Nombre,
                                 MateriaPrima = rem.Tinta.Tinta_Id,
                                 Nombre_MateriaPrima = rem.Tinta.Tinta_Nombre,
                                 CantidadAsignada = rem.RemiMatPri_Cantidad,
                                 Estado = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                             };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(asig.Concat(creacion).Concat(facturas).Concat(remisiones));
        }


        /** Consulta por Tinta, estado y Fechas */
        [HttpGet("MovTintasxFechasxTintaxEstado/{FechaInicial}/{FechaFinal}/{nTintas}/{estado}")]
        public ActionResult Get(DateTime FechaInicial, DateTime FechaFinal, int nTintas, int estado)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                             && asg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                             && asg.Tinta_Id == nTintas
                             && asg.AsigMp.Estado_OrdenTrabajo == estado
                       select new
                       {
                           OT = asg.AsigMp.AsigMP_OrdenTrabajo,
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = asg.AsigMp.EstadoOT.Estado_Nombre,
                       };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(asig);
        }


        /** Consulta por OT, Tinta, estado y Fechas */
        [HttpGet("MovTintasxOTxFechasxTintaxEstado/{Ot}/{FechaInicial}/{FechaFinal}/{nTintas}/{estado}")]
        public ActionResult Get(long Ot, DateTime FechaInicial, DateTime FechaFinal, int nTintas, int estado)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig = from asg in _context.Set<DetalleAsignacion_Tinta>()
                       where asg.AsigMp.AsigMP_OrdenTrabajo == Ot
                             && asg.AsigMp.AsigMp_FechaEntrega >= FechaInicial
                             && asg.AsigMp.AsigMp_FechaEntrega <= FechaFinal
                             && asg.Tinta_Id == nTintas
                             && asg.AsigMp.Estado_OrdenTrabajo == estado
                       select new
                       {
                           OT = asg.AsigMp.AsigMP_OrdenTrabajo,
                           Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                           Usuario = asg.AsigMp.Usua_Id,
                           Usuario_Nombre = asg.AsigMp.Usua.Usua_Nombre,
                           Tinta = asg.Tinta_Id,
                           NombreTinta = asg.Tinta.Tinta_Nombre,
                           Cantidad = asg.DtAsigTinta_Cantidad,
                           Presentacion = asg.UndMed_Id,
                           PrecioTinta = asg.Tinta.Tinta_Precio,
                           Tinta2 = asg.Tinta_Id,
                           Nombre_Tinta2 = asg.Tinta.Tinta_Nombre,
                           MateriaPrima = asg.Tinta_Id,
                           Nombre_MateriaPrima = asg.Tinta.Tinta_Nombre,
                           CantidadAsignada = asg.DtAsigTinta_Cantidad,
                           Estado = asg.AsigMp.EstadoOT.Estado_Nombre,
                       };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(asig);
        }


        /** Consulta por OT para PDF's */
        [HttpGet("pdfMovTintasxOT/{Ot}")]
        public ActionResult Get(long Ot)
        {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetalleAsignaciones_Tintas
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
                    dtAsg.Tinta_Id,
                    dtAsg.Tinta.Tinta_Nombre,
                    dtAsg.UndMed_Id,
                    dtAsg.DtAsigTinta_Cantidad,
                    dtAsg.Proceso_Id,
                    dtAsg.Proceso.Proceso_Nombre
                })
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return Ok(con);
        }

        // PUT: api/DetalleAsignacion_Tinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_Tinta(long id, DetalleAsignacion_Tinta detalleAsignacion_Tinta)
        {
            if (id != detalleAsignacion_Tinta.AsigMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_Tinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_TintaExists(id))
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

        // POST: api/DetalleAsignacion_Tinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_Tinta>> PostDetalleAsignacion_Tinta(DetalleAsignacion_Tinta detalleAsignacion_Tinta)
        {
          if (_context.DetalleAsignaciones_Tintas == null)
          {
              return Problem("Entity set 'dataContext.DetalleAsignaciones_Tintas'  is null.");
          }
            _context.DetalleAsignaciones_Tintas.Add(detalleAsignacion_Tinta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_TintaExists(detalleAsignacion_Tinta.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_Tinta", new { id = detalleAsignacion_Tinta.Codigo }, detalleAsignacion_Tinta);
        }

        // DELETE: api/DetalleAsignacion_Tinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_Tinta(long id)
        {
            if (_context.DetalleAsignaciones_Tintas == null)
            {
                return NotFound();
            }
            var detalleAsignacion_Tinta = await _context.DetalleAsignaciones_Tintas.FindAsync(id);
            if (detalleAsignacion_Tinta == null)
            {
                return NotFound();
            }

            _context.DetalleAsignaciones_Tintas.Remove(detalleAsignacion_Tinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_TintaExists(long id)
        {
            return (_context.DetalleAsignaciones_Tintas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
