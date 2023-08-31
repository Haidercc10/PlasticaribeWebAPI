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
    public class Materia_PrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Materia_PrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Materia_Prima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia_Prima>>> GetMaterias_Primas()
        {
          if (_context.Materias_Primas == null)
          {
              return NotFound();
          }
            return await _context.Materias_Primas.ToListAsync();
        }

        // GET: api/Materia_Prima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia_Prima>> GetMateria_Prima(long id)
        {
          if (_context.Materias_Primas == null)
          {
              return NotFound();
          }
            var materia_Prima = await _context.Materias_Primas.FindAsync(id);

            if (materia_Prima == null)
            {
                return NotFound();
            }

            return materia_Prima;
        }

        //Consulta que traerá la materia prima, tintas, bopp generico
        [HttpGet("getMpTintaBopp")]
        public ActionResult getMpTintaBopp()
        {
            var materiaPrima = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id != 84
                               select new
                               {
                                   Id = mp.MatPri_Id,
                                   Nombre = mp.MatPri_Nombre,
                                   Categoria = mp.CatMP_Id,
                                   Stock = mp.MatPri_Stock,
                               };

            var tinta = from tt in _context.Set<Tinta>()
                        where tt.Tinta_Id != 2001
                        select new
                        {
                            Id = tt.Tinta_Id,
                            Nombre = tt.Tinta_Nombre,
                            Categoria = tt.CatMP_Id,
                            Stock = tt.Tinta_Stock,
                        };

            var bopp = from bp in _context.Set<Bopp_Generico>()
                       where bp.BoppGen_Id != 1
                       select new
                       {
                           Id = bp.BoppGen_Id,
                           Nombre = bp.BoppGen_Nombre,
                           Categoria = 6,
                           Stock = Convert.ToDecimal(0.00)
                       };

            return Ok(materiaPrima.Concat(tinta).Concat(bopp));
        }

        //Consulta que traerá las categorias de materia prima de la tabla Materia_Prima
        [HttpGet("getCategoriasMateriaPrima")]
        public ActionResult GetCategoriasMateriaPrima()
        {
            var con = from mp in _context.Set<Materia_Prima>()
                      group mp by new
                      {
                          mp.CatMP_Id
                      } into mp
                      select mp.Key.CatMP_Id;
            return Ok(con);
        }

        //Consulta que traerá la materia prima, tintas, bopp
        [HttpGet("getInfo_MPTintasBOPP")]
        public ActionResult GetInfo_MPTintasBOPP()
        {
            var materiaPrima = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id != 84
                               select new
                               {
                                   Id = mp.MatPri_Id,
                                   Nombre = mp.MatPri_Nombre,
                                   Categoria = mp.CatMP_Id,
                               };

            var tinta = from tt in _context.Set<Tinta>()
                        where tt.Tinta_Id != 2001
                        select new
                        {
                            Id = tt.Tinta_Id,
                            Nombre = tt.Tinta_Nombre,
                            Categoria = tt.CatMP_Id,
                        };

            var bopp = from bp in _context.Set<BOPP>()
                       where bp.BOPP_Id != 449
                             && bp.BOPP_Stock > 0
                       select new
                       {
                           Id = bp.BOPP_Serial,
                           Nombre = bp.BOPP_Nombre,
                           Categoria = bp.CatMP_Id,
                       };

            return Ok(materiaPrima.Concat(tinta).Concat(bopp));
        }

        //Consulta que traerá la materia prima, tintas, bopp por un ID
        [HttpGet("getInfo_MpTintasBopp_Id/{id}")]
        public ActionResult getInfo_MpTintaBopp_Id(long id)
        {
            var materiaPrima = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id == id
                               select new
                               {
                                   Id = mp.MatPri_Id,
                                   Serial = mp.MatPri_Id,
                                   Nombre = mp.MatPri_Nombre,
                                   UndMedida = Convert.ToString(mp.UndMed_Id),
                                   Precio = mp.MatPri_Precio,
                                   Categoria = mp.CatMP_Id,
                                   Stock = Convert.ToDecimal(mp.MatPri_Stock),
                               };

            var tinta = from tt in _context.Set<Tinta>()
                        where tt.Tinta_Id == id
                        select new
                        {
                            Id = tt.Tinta_Id,
                            Serial = tt.Tinta_Id,
                            Nombre = tt.Tinta_Nombre,
                            UndMedida = Convert.ToString(tt.UndMed_Id),
                            Precio = tt.Tinta_Precio,
                            Categoria = tt.CatMP_Id,
                            Stock = Convert.ToDecimal(tt.Tinta_Stock),
                        };

            var bopp = from bp in _context.Set<BOPP>()
                       where bp.BOPP_Serial == id
                       select new
                       {
                           Id = bp.BOPP_Id,
                           Serial = bp.BOPP_Serial,
                           Nombre = bp.BOPP_Nombre,
                           UndMedida = Convert.ToString("Kg"),
                           Precio = bp.BOPP_Precio,
                           Categoria = bp.CatMP_Id,
                           Stock = bp.BOPP_Stock,
                       };

            return Ok(materiaPrima.Concat(tinta).Concat(bopp));
        }

        //Consulta que traerá la materia prima, tintas, bopp generico por un ID
        [HttpGet("getInfoMpTintaBopp/{id}")]
        public ActionResult getInfoMpTintaBopp(long id)
        {
            var materiaPrima = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id == id
                               select new
                               {
                                   Id = mp.MatPri_Id,
                                   Nombre = mp.MatPri_Nombre,
                                   UndMedida = Convert.ToString(mp.UndMed_Id),
                                   Precio = mp.MatPri_Precio,
                                   Categoria = mp.CatMP_Id,
                                   Stock = Convert.ToDecimal(mp.MatPri_Stock),
                               };

            var tinta = from tt in _context.Set<Tinta>()
                        where tt.Tinta_Id == id
                        select new
                        {
                            Id = tt.Tinta_Id,
                            Nombre = tt.Tinta_Nombre,
                            UndMedida = Convert.ToString(tt.UndMed_Id),
                            Precio = tt.Tinta_Precio,
                            Categoria = tt.CatMP_Id,
                            Stock = Convert.ToDecimal(tt.Tinta_Stock),
                        };

            var bopp = from bp in _context.Set<Bopp_Generico>()
                       where bp.BoppGen_Id == id
                       select new
                       {
                           Id = bp.BoppGen_Id,
                           Nombre = bp.BoppGen_Nombre,
                           UndMedida = Convert.ToString("Kg"),
                           Precio = Convert.ToDecimal(0),
                           Categoria = 6,
                           Stock = Convert.ToDecimal(0),
                       };

            if (materiaPrima.Count() == 0 && tinta.Count() == 0 && bopp.Count() == 0) return BadRequest("No se encontró una materia prima con el Id " + id);
            return Ok(materiaPrima.Concat(tinta).Concat(bopp));
        }

        [HttpGet("getMateriasPrimasUtilizadasHoy/{fecha1}")]
        public ActionResult getMateriasPrimasUtilizadasHoy(DateTime fecha1)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var AsignacionMp = from dtAsg in _context.Set<DetalleAsignacion_MateriaPrima>()
                      where dtAsg.AsigMp.AsigMp_FechaEntrega == fecha1
                      group dtAsg by new
                      {
                          Id = dtAsg.MatPri_Id,
                          Nombre = dtAsg.MatPri.MatPri_Nombre,
                      } into mp
                      select new
                      {
                          mp.Key.Id,
                          mp.Key.Nombre,
                          Cantidad = mp.Sum(x => x.DtAsigMp_Cantidad) - (from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                                                                         where dev.MatPri_Id == mp.Key.Id
                                                                               && dev.DevMatPri.DevMatPri_Fecha == fecha1
                                                                         group dev by new { dev.MatPri_Id } into dev
                                                                         select dev.Sum(x => x.DtDevMatPri_CantidadDevuelta)).FirstOrDefault(),
                          Asignaciones = mp.Count(),
                      };

            var CreacionTinta = from dtAsigCT in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                                  where dtAsigCT.AsigMPxTinta.AsigMPxTinta_FechaEntrega == fecha1
                                  group dtAsigCT by new
                                  {
                                      Id = dtAsigCT.AsigMPxTinta.Tinta_Id,
                                      Nombre = dtAsigCT.AsigMPxTinta.Tinta.Tinta_Nombre,
                                  } into mp
                                  select new
                                  {
                                      mp.Key.Id,
                                      mp.Key.Nombre,
                                      Cantidad = mp.Sum(x => x.DetAsigMPxTinta_Cantidad),
                                      Asignaciones = mp.Count(),
                                  };

            var AsignacionTinta = from dtAsgT in _context.Set<DetalleAsignacion_Tinta>()
                      where dtAsgT.AsigMp.AsigMp_FechaEntrega == fecha1
                      group dtAsgT by new
                      {
                          Nombre = dtAsgT.Tinta.Tinta_Nombre,
                          Id = dtAsgT.Tinta_Id,
                      } into mp
                      select new
                      {
                          mp.Key.Id,
                          mp.Key.Nombre,
                          Cantidad = mp.Sum(x => x.DtAsigTinta_Cantidad),
                          Asignaciones = mp.Count(),
                      };

            var AsignacionBopp = from bp in _context.Set<DetalleAsignacion_BOPP>()
                                 where bp.AsigBOPP.AsigBOPP_FechaEntrega == fecha1
                                 group bp by new
                                 {
                                     Nombre = bp.BOPP.BOPP_Nombre,
                                     Id = bp.BOPP_Id,
                                 } into bp
                                 select new
                                 {
                                     bp.Key.Id,
                                     bp.Key.Nombre,
                                     Cantidad = bp.Sum(x => x.DtAsigBOPP_Cantidad),
                                     Asignaciones = bp.Count(),
                                 };


#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(AsignacionMp.Concat(CreacionTinta).Concat(AsignacionTinta));
        }

        [HttpGet("getMateriasPrimasUltilizadasMes/{fecha1}/{fecha2}")]
        public ActionResult getMateriasPrimasUltilizadasMes(DateTime fecha1, DateTime fecha2)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var AsignacionMp = from dtAsg in _context.Set<DetalleAsignacion_MateriaPrima>()
                               where dtAsg.AsigMp.AsigMp_FechaEntrega >= fecha1
                                     && dtAsg.AsigMp.AsigMp_FechaEntrega <= fecha2
                               group dtAsg by new
                               {
                                   Id = dtAsg.MatPri_Id,
                                   Nombre = dtAsg.MatPri.MatPri_Nombre,
                               } into mp
                               select new
                               {
                                   mp.Key.Id,
                                   mp.Key.Nombre,
                                   Cantidad = mp.Sum(x => x.DtAsigMp_Cantidad) - (from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                                                                                  where dev.MatPri_Id == mp.Key.Id
                                                                                        && dev.DevMatPri.DevMatPri_Fecha >= fecha1
                                                                                        && dev.DevMatPri.DevMatPri_Fecha <= fecha2
                                                                                  group dev by new { dev.MatPri_Id } into dev
                                                                                  select dev.Sum(x => x.DtDevMatPri_CantidadDevuelta)).FirstOrDefault(),
                                   Asignaciones = mp.Count(),
                               };

            var CreacionTinta = from dtAsigCT in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                                where dtAsigCT.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                                      && dtAsigCT.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2
                                group dtAsigCT by new
                                {
                                    Id = dtAsigCT.AsigMPxTinta.Tinta_Id,
                                    Nombre = dtAsigCT.AsigMPxTinta.Tinta.Tinta_Nombre,
                                } into mp
                                select new
                                {
                                    mp.Key.Id,
                                    mp.Key.Nombre,
                                    Cantidad = mp.Sum(x => x.DetAsigMPxTinta_Cantidad),
                                    Asignaciones = mp.Count(),
                                };

            var AsignacionTinta = from dtAsgT in _context.Set<DetalleAsignacion_Tinta>()
                                  where dtAsgT.AsigMp.AsigMp_FechaEntrega >= fecha1
                                        && dtAsgT.AsigMp.AsigMp_FechaEntrega <= fecha2
                                  group dtAsgT by new
                                  {
                                      Nombre = dtAsgT.Tinta.Tinta_Nombre,
                                      Id = dtAsgT.Tinta_Id,
                                  } into mp
                                  select new
                                  {
                                      mp.Key.Id,
                                      mp.Key.Nombre,
                                      Cantidad = mp.Sum(x => x.DtAsigTinta_Cantidad),
                                      Asignaciones = mp.Count(),
                                  };

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(AsignacionMp.Concat(CreacionTinta).Concat(AsignacionTinta));
        }

        // Materias primas, Tintas, Biorientados
        [HttpGet("getInventarioMateriasPrimas")]
        public ActionResult getInventarioMateriasPrimas()
        {
            var materiasPrimas = from mp in _context.Set<Materia_Prima>()
                                 from inv in _context.Set<InventarioInicialXDia_MatPrima>()
                                 where mp.MatPri_Id == inv.MatPri_Id
                                 select new
                                 {
                                     Id_Materia_Prima = mp.MatPri_Id,
                                     Nombre_Materia_Prima = mp.MatPri_Nombre,
                                     Inicial = inv.InvInicial_Stock,
                                     Actual = mp.MatPri_Stock,
                                 };

            var bopp = from bp in _context.Set<BOPP>()
                       where bp.BOPP_Stock != 0
                       select new
                       {
                           Id_Materia_Prima = bp.BOPP_Serial,
                           Nombre_Materia_Prima = bp.BOPP_Nombre,
                           Inicial = bp.BOPP_CantidadInicialKg,
                           Actual = bp.BOPP_Stock,
                       };

            var tintas = from t in _context.Set<Tinta>()
                         select new
                         {
                             Id_Materia_Prima = t.Tinta_Id,
                             Nombre_Materia_Prima = t.Tinta_Nombre,
                             Inicial = t.Tinta_InvInicial,
                             Actual = t.Tinta_Stock,
                         };

            return Ok(materiasPrimas.Concat(bopp).Concat(tintas));
        }

        [HttpGet("getInventario/{fecha1}/{fecha2}/{id}")]
        public ActionResult GetInventario(DateTime fecha1, DateTime fecha2, long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = _context.DetallesAsignaciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.AsigMp.AsigMp_FechaEntrega >= fecha1
                       && asg.AsigMp.AsigMp_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigMp_Cantidad);

            //Asignacion de Materia Prima para la creacion de Tintas
            var conAsgMPCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.MatPri_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

            //Devoluciones de Materia Prima
            var conDevoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.DevMatPri.DevMatPri_Fecha >= fecha1
                       && asg.DevMatPri.DevMatPri_Fecha <= fecha2).Sum(asg => asg.DtDevMatPri_CantidadDevuelta);

            //Facturas de Materia Prima
            var conFacturas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Facco.Facco_FechaFactura >= fecha1
                       && asg.Facco.Facco_FechaFactura <= fecha2).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Materia Prima
            var conRemisiones = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Rem.Rem_Fecha >= fecha1
                       && asg.Rem.Rem_Fecha <= fecha2).Sum(asg => asg.RemiMatPri_Cantidad);

            //Recuperado de Materia Prima
            var conRecuperado = _context.DetallesRecuperados_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.RecMp.RecMp_FechaIngreso >= fecha1
                       && asg.RecMp.RecMp_FechaIngreso <= fecha2).Sum(asg => asg.RecMatPri_Cantidad);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigBOPP_Cantidad);

            //Entrada de BOPP
            var conEntradaBOPP = _context.BOPP
                .Where(x => x.BOPP_FechaIngreso >= fecha1
                       && x.BOPP_FechaIngreso <= fecha2
                       && x.BOPP_Serial == id).Sum(x => x.BOPP_CantidadInicialKg);

            //Asignacion de Tinta
            var conAsgTinta = _context.DetalleAsignaciones_Tintas
                .Where(asg => asg.Tinta_Id == id
                        && asg.AsigMp.AsigMp_FechaEntrega >= fecha1
                        && asg.AsigMp.AsigMp_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigTinta_Cantidad);

            //Asignacion de Tintas para la creacion de Tintas
            var conAsgTintaCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

            //Facturas de Tintas
            var conFacturasTintas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Facco.Facco_FechaFactura >= fecha1
                       && asg.Facco.Facco_FechaFactura <= fecha2).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Tintas
            var conRemisionesTintas = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Rem.Rem_Fecha >= fecha1
                       && asg.Rem.Rem_Fecha <= fecha2).Sum(asg => asg.RemiMatPri_Cantidad);

            //Creacion de Tintas
            var conCreacionTintas = _context.Asignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta_FechaEntrega >= fecha1
                       && asg.AsigMPxTinta_FechaEntrega <= fecha2).Sum(asg => asg.AsigMPxTinta_Cantidad);

            //Suma Salidas
            var salidaMateriaPrima = conAsg + conAsgMPCreacionTintas;
            var salidaTintas = conAsgTinta + conAsgTintaCreacionTintas;

            //Suma Entradas
            var entrada = conDevoluciones + conFacturas + conRemisiones + conRecuperado;
            var entradaTintas = conFacturasTintas + conRemisionesTintas + conCreacionTintas;

            //Materia Prima
            var con = (from mp in _context.Set<Materia_Prima>()
                       from Inv in _context.Set<InventarioInicialXDia_MatPrima>()
                       where mp.MatPri_Id == id
                             && Inv.MatPri_Id == mp.MatPri_Id
                       group mp by new
                       {
                           mp.MatPri_Id,
                           mp.MatPri_Nombre,
                           Inv.InvInicial_Stock,
                           mp.MatPri_Stock,
                           mp.UndMed_Id,
                           mp.MatPri_Precio,
                           mp.CatMP.CatMP_Nombre,
                           mp.CatMP_Id
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Ancho = Convert.ToDouble(0.00),
                           Micras = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = salidaMateriaPrima,
                           Stock = x.Key.MatPri_Stock,
                           Diferencia = x.Key.MatPri_Stock - x.Key.InvInicial_Stock,
                           Presentacion = x.Key.UndMed_Id,
                           Precio = x.Key.MatPri_Precio,
                           SubTotal = x.Key.MatPri_Stock * x.Key.MatPri_Precio,
                           Categoria = x.Key.CatMP_Nombre,
                           Categoria_Id = x.Key.CatMP_Id
                       });

            //Entrada de BOPP
            var conBopp = (from bopp in _context.Set<BOPP>()
                           where bopp.BOPP_Serial == id
                                 && (conAsgBopp > 0 || bopp.BOPP_Stock > 0)
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Micras = Convert.ToDouble(bopp.BOPP_CantidadMicras),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(conEntradaBOPP),
                               Salida = conAsgBopp,
                               Stock = bopp.BOPP_Stock,
                               Diferencia = bopp.BOPP_Stock - bopp.BOPP_CantidadInicialKg,
                               Presentacion = bopp.UndMed_Id,
                               Precio = bopp.BOPP_Precio,
                               SubTotal = bopp.BOPP_Stock * bopp.BOPP_Precio,
                               Categoria = bopp.CatMP.CatMP_Nombre,
                               Categoria_Id = bopp.CatMP_Id
                           });

            //Tinta
            var conTinta = (from tinta in _context.Set<Tinta>()
                            where tinta.Tinta_Id == id
                            select new
                            {
                                Id = tinta.Tinta_Id,
                                Nombre = tinta.Tinta_Nombre,
                                Ancho = Convert.ToDouble(0.00),
                                Micras = Convert.ToDouble(0.00),
                                Inicial = tinta.Tinta_InvInicial,
                                Entrada = Convert.ToDouble(entradaTintas),
                                Salida = salidaTintas,
                                Stock = tinta.Tinta_Stock,
                                Diferencia = tinta.Tinta_Stock - tinta.Tinta_InvInicial,
                                Presentacion = tinta.UndMed_Id,
                                Precio = tinta.Tinta_Precio,
                                SubTotal = tinta.Tinta_Stock * tinta.Tinta_Precio,
                                Categoria = tinta.CatMP.CatMP_Nombre,
                                Categoria_Id = tinta.CatMP_Id
                            });

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con.Concat(conBopp).Concat(conTinta));
        }

        //Movimientos de Materia Prima, Tintas, Biorientados
        [HttpGet("getMovimientos/{fecha1}/{fecha2}")]
        public ActionResult GetMoviemientos(DateTime fecha1, DateTime fecha2, string? codigo = "", string? tipoMov = "", string? materiaPrima = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            //Asignaciones de Materia Prima
            var conAsg = from asg in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where asg.AsigMp.AsigMp_FechaEntrega >= fecha1
                               && asg.AsigMp.AsigMp_FechaEntrega <= fecha2
                               && Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo).Contains(codigo)
                               && "ASIGMP".Contains(tipoMov)
                               && (materiaPrima != "" ? Convert.ToString(asg.MatPri_Id) == materiaPrima : Convert.ToString(asg.MatPri_Id).Contains(materiaPrima))
                         select new
                         {
                             Id = asg.AsigMp_Id,
                             Codigo = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                             Movimiento = Convert.ToString("ASIGMP"),
                             Tipo_Movimiento = Convert.ToString("Asignación de Materia Prima"),
                             Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                             Usuario = asg.AsigMp.Usua.Usua_Nombre,
                             Materia_Prima_Id = Convert.ToInt16(asg.MatPri_Id),
                             Materia_Prima = Convert.ToString(asg.MatPri.MatPri_Nombre),
                             Tinta_Id = Convert.ToInt16(2001),
                             Tinta = Convert.ToString(""),
                             Bopp_Id = Convert.ToInt64(449),
                             Bopp = Convert.ToString(""),
                             Cantidad = Convert.ToDecimal(asg.DtAsigMp_Cantidad),
                             Unidad_Medida = asg.UndMed_Id,
                         };

            //Asignacion de Materia Prima para la creacion de Tintas
            var conAsgMPCreacionTintas = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                                         where cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                                               && cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2
                                               && Convert.ToString(cr.AsigMPxTinta.Tinta_Id).Contains(codigo)
                                               && "CRTINTAS".Contains(tipoMov)
                                               && (materiaPrima != "" ? (Convert.ToString(cr.Tinta_Id) == materiaPrima || Convert.ToString(cr.MatPri_Id) == materiaPrima) : 
                                                                        (Convert.ToString(cr.Tinta_Id).Contains(materiaPrima) || Convert.ToString(cr.MatPri_Id).Contains(materiaPrima)))
                                         select new 
                                         {
                                             Id = cr.AsigMPxTinta_Id,
                                             Codigo = Convert.ToString((Convert.ToString(cr.AsigMPxTinta.Tinta_Id)) + " " + (cr.AsigMPxTinta.Tinta.Tinta_Nombre)),
                                             Movimiento = Convert.ToString("CRTINTAS"),
                                             Tipo_Movimiento = Convert.ToString("Creación de Tintas"),
                                             Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                                             Usuario = cr.AsigMPxTinta.Usua.Usua_Nombre,
                                             Materia_Prima_Id = Convert.ToInt16(cr.MatPri_Id),
                                             Materia_Prima = Convert.ToString(cr.MatPri.MatPri_Nombre),
                                             Tinta_Id = Convert.ToInt16(cr.Tinta_Id),
                                             Tinta = Convert.ToString(cr.TintasDAMPxT.Tinta_Nombre),
                                             Bopp_Id = Convert.ToInt64(449),
                                             Bopp = Convert.ToString(""),
                                             Cantidad = Convert.ToDecimal(cr.DetAsigMPxTinta_Cantidad),
                                             Unidad_Medida = cr.UndMed_Id,
                                         };

            //Devoluciones de Materia Prima
            var conDevoluciones = from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                                  where dev.DevMatPri.DevMatPri_Fecha >= fecha1
                                        && dev.DevMatPri.DevMatPri_Fecha <= fecha2
                                        && Convert.ToString(dev.DevMatPri.DevMatPri_OrdenTrabajo).Contains(codigo)
                                        && "DEVMP".Contains(tipoMov)
                                        && (materiaPrima != "" ? (Convert.ToString(dev.MatPri_Id) == materiaPrima || Convert.ToString(dev.Tinta.Tinta_Id) == materiaPrima || Convert.ToString(dev.Bopp.BOPP_Serial) == materiaPrima) :
                                                                 (Convert.ToString(dev.MatPri_Id).Contains(materiaPrima) || Convert.ToString(dev.Tinta.Tinta_Id).Contains(materiaPrima) || Convert.ToString(dev.Bopp.BOPP_Serial).Contains(materiaPrima)))
                                  select new
                                  {
                                      Id = dev.DevMatPri_Id,
                                      Codigo = Convert.ToString(dev.DevMatPri.DevMatPri_OrdenTrabajo),
                                      Movimiento = Convert.ToString("DEVMP"),
                                      Tipo_Movimiento = Convert.ToString("Devolución de Materia Prima"),
                                      Fecha = dev.DevMatPri.DevMatPri_Fecha,
                                      Usuario = dev.DevMatPri.Usua.Usua_Nombre,
                                      Materia_Prima_Id = Convert.ToInt16(dev.MatPri_Id),
                                      Materia_Prima = Convert.ToString(dev.MatPri.MatPri_Nombre),
                                      Tinta_Id = Convert.ToInt16(dev.Tinta.Tinta_Id),
                                      Tinta = Convert.ToString(dev.Tinta.Tinta_Nombre),
                                      Bopp_Id = Convert.ToInt64(dev.Bopp.BOPP_Serial),
                                      Bopp = Convert.ToString(dev.Bopp.BOPP_Nombre),
                                      Cantidad = Convert.ToDecimal(dev.DtDevMatPri_CantidadDevuelta),
                                      Unidad_Medida = dev.UndMed_Id,
                                  };

            //Facturas de Materia Prima
            var conFacturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                              where fac.Facco.Facco_FechaFactura >= fecha1
                                    && fac.Facco.Facco_FechaFactura <= fecha2
                                    && Convert.ToString(fac.Facco.Facco_Codigo).Contains(codigo)
                                    && fac.Facco.TpDoc_Id.Contains(tipoMov)
                                    && (materiaPrima != "" ? (Convert.ToString(fac.MatPri_Id) == materiaPrima || Convert.ToString(fac.Tinta.Tinta_Id) == materiaPrima || Convert.ToString(fac.Bopp_Generico.BoppGen_Id) == materiaPrima) : 
                                                             (Convert.ToString(fac.MatPri_Id).Contains(materiaPrima) || Convert.ToString(fac.Tinta.Tinta_Id).Contains(materiaPrima) || Convert.ToString(fac.Bopp_Generico.BoppGen_Id).Contains(materiaPrima)))
                              select new
                              {
                                  Id = fac.Facco_Id,
                                  Codigo = Convert.ToString(fac.Facco.Facco_Codigo),
                                  Movimiento = Convert.ToString(fac.Facco.TpDoc.TpDoc_Id),
                                  Tipo_Movimiento = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                                  Fecha = fac.Facco.Facco_FechaFactura,
                                  Usuario = fac.Facco.Usua.Usua_Nombre,
                                  Materia_Prima_Id = Convert.ToInt16(fac.MatPri_Id),
                                  Materia_Prima = Convert.ToString(fac.MatPri.MatPri_Nombre),
                                  Tinta_Id = Convert.ToInt16(fac.Tinta.Tinta_Id),
                                  Tinta = Convert.ToString(fac.Tinta.Tinta_Nombre),
                                  Bopp_Id = Convert.ToInt64(fac.Bopp_Generico.BoppGen_Id),
                                  Bopp = Convert.ToString(fac.Bopp_Generico.BoppGen_Nombre),
                                  Cantidad = Convert.ToDecimal(fac.FaccoMatPri_Cantidad),
                                  Unidad_Medida = fac.UndMed_Id,
                              };

            //Remisiones de Materia Prima
            var conRemisiones = from rem in _context.Set<Remision_MateriaPrima>()
                                where rem.Rem.Rem_Fecha >= fecha1
                                      && rem.Rem.Rem_Fecha <= fecha2
                                      && Convert.ToString(rem.Rem.Rem_Codigo).Contains(codigo)
                                      && rem.Rem.TpDoc_Id.Contains(tipoMov)
                                      && (materiaPrima != "" ? (Convert.ToString(rem.MatPri_Id) == materiaPrima || Convert.ToString(rem.Tinta.Tinta_Id) == materiaPrima || Convert.ToString(rem.Bopp.BoppGen_Id) == materiaPrima) :
                                                               (Convert.ToString(rem.MatPri_Id).Contains(materiaPrima) || Convert.ToString(rem.Tinta.Tinta_Id).Contains(materiaPrima) || Convert.ToString(rem.Bopp.BoppGen_Id).Contains(materiaPrima)))
                                select new
                                {
                                    Id = Convert.ToInt64(rem.Rem_Id),
                                    Codigo = Convert.ToString(rem.Rem.Rem_Codigo),
                                    Movimiento = Convert.ToString(rem.Rem.TpDoc.TpDoc_Id),
                                    Tipo_Movimiento = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                                    Fecha = rem.Rem.Rem_Fecha,
                                    Usuario = rem.Rem.Usua.Usua_Nombre,
                                    Materia_Prima_Id = Convert.ToInt16(rem.MatPri_Id),
                                    Materia_Prima = Convert.ToString(rem.MatPri.MatPri_Nombre),
                                    Tinta_Id = Convert.ToInt16(rem.Tinta.Tinta_Id),
                                    Tinta = Convert.ToString(rem.Tinta.Tinta_Nombre),
                                    Bopp_Id = Convert.ToInt64(rem.Bopp.BoppGen_Id),
                                    Bopp = Convert.ToString(rem.Bopp.BoppGen_Nombre),
                                    Cantidad = Convert.ToDecimal(rem.RemiMatPri_Cantidad),
                                    Unidad_Medida = rem.UndMed_Id,
                                };

            //Asignaciones de BOPP
            var conAsgBopp = from asg in _context.Set<DetalleAsignacion_BOPP>()
                             where asg.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1
                                   && asg.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2
                                   && Convert.ToString(asg.DtAsigBOPP_OrdenTrabajo).Contains(codigo)
                                   && Convert.ToString(asg.TpDoc_Id).Contains(tipoMov)
                                   && (materiaPrima != "" ? Convert.ToString(asg.BOPP_Id) == materiaPrima : Convert.ToString(asg.BOPP_Id).Contains(materiaPrima))
                             select new
                             {
                                 Id = Convert.ToInt64(asg.AsigBOPP_Id),
                                 Codigo = Convert.ToString(asg.DtAsigBOPP_OrdenTrabajo),
                                 Movimiento = Convert.ToString(asg.Tipo_Documento.TpDoc_Id),
                                 Tipo_Movimiento = Convert.ToString(asg.Tipo_Documento.TpDoc_Nombre),
                                 Fecha = asg.AsigBOPP.AsigBOPP_FechaEntrega,
                                 Usuario = asg.AsigBOPP.Usua.Usua_Nombre,
                                 Materia_Prima_Id = Convert.ToInt16(84),
                                 Materia_Prima = Convert.ToString(""),
                                 Tinta_Id = Convert.ToInt16(2001),
                                 Tinta = Convert.ToString(""),
                                 Bopp_Id = Convert.ToInt64(asg.BOPP_Id),
                                 Bopp = Convert.ToString(asg.BOPP.BOPP_Nombre),
                                 Cantidad = Convert.ToDecimal(asg.DtAsigBOPP_Cantidad),
                                 Unidad_Medida = asg.UndMed_Id,
                             };

            //Entrada de BOPP
            var conEntradaBOPP = from ent in _context.Set<BOPP>()
                                 where ent.BOPP_FechaIngreso >= fecha1
                                       && ent.BOPP_FechaIngreso <= fecha2
                                       && Convert.ToString(ent.BOPP_Id).Contains(codigo)
                                       && Convert.ToString("ENTBIO").Contains(tipoMov)
                                       && (materiaPrima != "" ? Convert.ToString(ent.BOPP_Serial) == materiaPrima : Convert.ToString(ent.BOPP_Serial).Contains(materiaPrima))
                                 select new
                                 {
                                     Id = Convert.ToInt64(ent.BOPP_Id),
                                     Codigo = Convert.ToString(ent.BOPP_Id),
                                     Movimiento = Convert.ToString("ENTBIO"),
                                     Tipo_Movimiento = Convert.ToString("Entrada de Biorientado"),
                                     Fecha = ent.BOPP_FechaIngreso,
                                     Usuario = ent.Usua.Usua_Nombre,
                                     Materia_Prima_Id = Convert.ToInt16(84),
                                     Materia_Prima = Convert.ToString(""),
                                     Tinta_Id = Convert.ToInt16(2001),
                                     Tinta = Convert.ToString(""),
                                     Bopp_Id = Convert.ToInt64(ent.BOPP_Serial),
                                     Bopp = Convert.ToString(ent.BOPP_Nombre),
                                     Cantidad = Convert.ToDecimal(ent.BOPP_Stock),
                                     Unidad_Medida = ent.UndMed_Kg,
                                 };

            //Asignacion de Tinta
           var conAsgTinta = from asg in _context.Set<DetalleAsignacion_Tinta>()
                              where asg.AsigMp.AsigMp_FechaEntrega >= fecha1
                                    && asg.AsigMp.AsigMp_FechaEntrega <= fecha2
                                    && Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo).Contains(codigo)
                                    && Convert.ToString("ASIGTINTAS").Contains(tipoMov)
                                    && (materiaPrima != "" ? Convert.ToString(asg.Tinta_Id) == materiaPrima : Convert.ToString(asg.Tinta_Id).Contains(materiaPrima))
                              select new
                              {
                                  Id = Convert.ToInt64(asg.AsigMp_Id),
                                  Codigo = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                                  Movimiento = Convert.ToString("ASIGTINTAS"),
                                  Tipo_Movimiento = Convert.ToString("Asignación de Tintas"),
                                  Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                                  Usuario = asg.AsigMp.Usua.Usua_Nombre,
                                  Materia_Prima_Id = Convert.ToInt16(84),
                                  Materia_Prima = Convert.ToString(""),
                                  Tinta_Id = Convert.ToInt16(asg.Tinta_Id),
                                  Tinta = Convert.ToString(asg.Tinta.Tinta_Nombre),
                                  Bopp_Id = Convert.ToInt64(449),
                                  Bopp = Convert.ToString(""),
                                  Cantidad = Convert.ToDecimal(asg.DtAsigTinta_Cantidad),
                                  Unidad_Medida = asg.UndMed_Id,
                              };

            return Ok(conAsg.Concat(conAsgMPCreacionTintas).Concat(conDevoluciones).Concat(conFacturas).Concat(conRemisiones).Concat(conAsgBopp).Concat(conEntradaBOPP).Concat(conAsgTinta));
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

        [HttpGet("getInfoMovimientoAsignaciones/{codigo}/{tipoMov}")]
        public ActionResult GetInfoMovimientoAsignaciones(string codigo, string tipoMov)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = from asg in _context.Set<DetalleAsignacion_MateriaPrima>()
                         from emp in _context.Set<Empresa>()
                         where Convert.ToString(asg.AsigMp.AsigMp_Id).Contains(codigo)
                               && "ASIGMP".Contains(tipoMov)
                               && emp.Empresa_Id == 800188732
                         select new
                         {
                             Id = asg.AsigMp_Id,
                             Codigo = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                             Movimiento = Convert.ToString("ASIGMP"),
                             Tipo_Movimiento = Convert.ToString("Asigmación de Materia Prima"),
                             Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                             Hora = asg.AsigMp.AsigMp_Hora,
                             Usuario = asg.AsigMp.Usua.Usua_Nombre,
                             Maquina = Convert.ToString(asg.AsigMp.AsigMp_Maquina),
                             Materia_Prima_Id = Convert.ToInt16(asg.MatPri_Id),
                             Materia_Prima = Convert.ToString(asg.MatPri.MatPri_Nombre),
                             Tinta_Id = Convert.ToInt16(2001),
                             Tinta = Convert.ToString(""),
                             Bopp_Id = Convert.ToInt64(449),
                             Bopp = Convert.ToString(""),
                             Cantidad = Convert.ToDecimal(asg.DtAsigMp_Cantidad),
                             Unidad_Medida = asg.UndMed_Id,
                             Precio = Convert.ToDecimal(asg.MatPri.MatPri_Precio),
                             SubTotal = Convert.ToDecimal(asg.MatPri.MatPri_Precio * asg.DtAsigMp_Cantidad),
                             Observacion = asg.AsigMp.AsigMp_Observacion,
                             emp.Empresa_Id,
                             emp.Empresa_Ciudad,
                             emp.Empresa_COdigoPostal,
                             emp.Empresa_Correo,
                             emp.Empresa_Direccion,
                             emp.Empresa_Telefono,
                             emp.Empresa_Nombre
                         };

            //Asignaciones de BOPP
            var conAsgBopp = from asg in _context.Set<DetalleAsignacion_BOPP>()
                             from emp in _context.Set<Empresa>()
                             where Convert.ToString(asg.AsigBOPP_Id).Contains(codigo)
                                   && asg.TpDoc_Id.Contains(tipoMov)
                                   && emp.Empresa_Id == 800188732
                             select new
                             {
                                 Id = Convert.ToInt64(asg.AsigBOPP_Id),
                                 Codigo = Convert.ToString(asg.DtAsigBOPP_OrdenTrabajo),
                                 Movimiento = Convert.ToString(asg.Tipo_Documento.TpDoc_Id),
                                 Tipo_Movimiento = Convert.ToString(asg.Tipo_Documento.TpDoc_Nombre),
                                 Fecha = asg.AsigBOPP.AsigBOPP_FechaEntrega,
                                 Hora = asg.AsigBOPP.AsigBOPP_Hora,
                                 Usuario = asg.AsigBOPP.Usua.Usua_Nombre,
                                 Maquina = Convert.ToString(""),
                                 Materia_Prima_Id = Convert.ToInt16(84),
                                 Materia_Prima = Convert.ToString(""),
                                 Tinta_Id = Convert.ToInt16(2001),
                                 Tinta = Convert.ToString(""),
                                 Bopp_Id = Convert.ToInt64(asg.BOPP_Id),
                                 Bopp = Convert.ToString(asg.BOPP.BOPP_Nombre),
                                 Cantidad = Convert.ToDecimal(asg.DtAsigBOPP_Cantidad),
                                 Unidad_Medida = asg.UndMed_Id,
                                 Precio = Convert.ToDecimal(asg.BOPP.BOPP_Precio),
                                 SubTotal = Convert.ToDecimal(asg.BOPP.BOPP_Precio * asg.DtAsigBOPP_Cantidad),
                                 Observacion = asg.AsigBOPP.AsigBOPP_Observacion,
                                 emp.Empresa_Id,
                                 emp.Empresa_Ciudad,
                                 emp.Empresa_COdigoPostal,
                                 emp.Empresa_Correo,
                                 emp.Empresa_Direccion,
                                 emp.Empresa_Telefono,
                                 emp.Empresa_Nombre
                             };

            //Asignacion de Tinta
            var conAsgTinta = from asg in _context.Set<DetalleAsignacion_Tinta>()
                              from emp in _context.Set<Empresa>()
                              where "ASIGTINTAS".Contains(tipoMov)
                                    && Convert.ToString(asg.AsigMp_Id).Contains(codigo)
                                    && emp.Empresa_Id == 800188732
                              select new
                              {
                                  Id = Convert.ToInt64(asg.AsigMp_Id),
                                  Codigo = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                                  Movimiento = Convert.ToString("ASIGTINTAS"),
                                  Tipo_Movimiento = Convert.ToString("Asignación de Tintas"),
                                  Fecha = asg.AsigMp.AsigMp_FechaEntrega,
                                  Hora = asg.AsigMp.AsigMp_Hora,
                                  Usuario = asg.AsigMp.Usua.Usua_Nombre,
                                  Maquina = Convert.ToString(asg.AsigMp.AsigMp_Maquina),
                                  Materia_Prima_Id = Convert.ToInt16(84),
                                  Materia_Prima = Convert.ToString(""),
                                  Tinta_Id = Convert.ToInt16(asg.Tinta_Id),
                                  Tinta = Convert.ToString(asg.Tinta.Tinta_Nombre),
                                  Bopp_Id = Convert.ToInt64(449),
                                  Bopp = Convert.ToString(""),
                                  Cantidad = Convert.ToDecimal(asg.DtAsigTinta_Cantidad),
                                  Unidad_Medida = asg.UndMed_Id,
                                  Precio = Convert.ToDecimal(asg.Tinta.Tinta_Precio),
                                  SubTotal = Convert.ToDecimal(asg.Tinta.Tinta_Precio * asg.DtAsigTinta_Cantidad),
                                  Observacion = asg.AsigMp.AsigMp_Observacion,
                                  emp.Empresa_Id,
                                  emp.Empresa_Ciudad,
                                  emp.Empresa_COdigoPostal,
                                  emp.Empresa_Correo,
                                  emp.Empresa_Direccion,
                                  emp.Empresa_Telefono,
                                  emp.Empresa_Nombre
                              };

            return Ok(conAsg.Concat(conAsgBopp).Concat(conAsgTinta));
        }

        [HttpGet("getInfoMovimientoCreacionTinta/{codigo}")]
        public ActionResult GetInfoMovimientoCreacionTinta(long codigo)
        {
            var creacion = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                           from emp in _context.Set<Empresa>()
                           where cr.AsigMPxTinta_Id == codigo
                                 && emp.Empresa_Id == 800188732
                           select new
                           {
                               Id = cr.AsigMPxTinta_Id,
                               Tinta_Creada_Id = cr.AsigMPxTinta.Tinta_Id,
                               Tinta_Creada = cr.AsigMPxTinta.Tinta.Tinta_Nombre,
                               Cantidad_Creada = cr.AsigMPxTinta.AsigMPxTinta_Cantidad,
                               Movimiento = Convert.ToString("CRTINTAS"),
                               Tipo_Movimiento = Convert.ToString("Creación de Tintas"),
                               Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                               Hora = cr.AsigMPxTinta.AsigMPxTinta_Hora,
                               Usuario = cr.AsigMPxTinta.Usua.Usua_Nombre,
                               Materia_Prima_Id = Convert.ToInt16(cr.MatPri_Id),
                               Materia_Prima = Convert.ToString(cr.MatPri.MatPri_Nombre),
                               Tinta_Id = Convert.ToInt16(cr.Tinta_Id),
                               Tinta = Convert.ToString(cr.TintasDAMPxT.Tinta_Nombre),
                               Cantidad = Convert.ToDecimal(cr.DetAsigMPxTinta_Cantidad),
                               Unidad_Medida = cr.UndMed_Id,
                               Precio = 0,
                               SubTotal = 0,
                               Observacion = cr.AsigMPxTinta.AsigMPxTinta_Observacion,
                               emp.Empresa_Id,
                               emp.Empresa_Ciudad,
                               emp.Empresa_COdigoPostal,
                               emp.Empresa_Correo,
                               emp.Empresa_Direccion,
                               emp.Empresa_Telefono,
                               emp.Empresa_Nombre
                           };
            return Ok(creacion);
        }

        [HttpGet("getInfoMovimientosDevoluciones/{codigo}")]
        public ActionResult GetInfoMovimientosDevoluciones(long codigo)
        {
            var conDevoluciones = from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                                  from emp in _context.Set<Empresa>()
                                  where dev.DevMatPri_Id == codigo
                                        && emp.Empresa_Id == 800188732
                                  select new
                                  {
                                      Id = dev.DevMatPri_Id,
                                      Codigo = Convert.ToString(dev.DevMatPri.DevMatPri_OrdenTrabajo),
                                      Movimiento = Convert.ToString("DEVMP"),
                                      Tipo_Movimiento = Convert.ToString("Devolución de Materia Prima"),
                                      Fecha = dev.DevMatPri.DevMatPri_Fecha,
                                      Hora = dev.DevMatPri.DevMatPri_Hora,
                                      Usuario = dev.DevMatPri.Usua.Usua_Nombre,
                                      Materia_Prima_Id = Convert.ToInt16(dev.MatPri_Id),
                                      Materia_Prima = Convert.ToString(dev.MatPri.MatPri_Nombre),
                                      Tinta_Id = Convert.ToInt16(dev.Tinta.Tinta_Id),
                                      Tinta = Convert.ToString(dev.Tinta.Tinta_Nombre),
                                      Bopp_Id = Convert.ToInt64(dev.Bopp.BOPP_Serial),
                                      Bopp = Convert.ToString(dev.Bopp.BOPP_Nombre),
                                      Cantidad = Convert.ToDecimal(dev.DtDevMatPri_CantidadDevuelta),
                                      Unidad_Medida = dev.UndMed_Id,
                                      Precio = 0,
                                      SubTotal = 0,
                                      Observacion = dev.DevMatPri.DevMatPri_Motivo,
                                      emp.Empresa_Id,
                                      emp.Empresa_Ciudad,
                                      emp.Empresa_COdigoPostal,
                                      emp.Empresa_Correo,
                                      emp.Empresa_Direccion,
                                      emp.Empresa_Telefono,
                                      emp.Empresa_Nombre
                                  };
            return Ok(conDevoluciones);
        }

        [HttpGet("getInfoMovimientosEntradas/{codigo}/{tipoMov}")]
        public ActionResult GetInfoMovimientosEntradas(string codigo, string tipoMov)
        {
            //Facturas de Materia Prima
            var conFacturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                              from emp in _context.Set<Empresa>()
                              where Convert.ToString(fac.Facco.Facco_Id).Contains(codigo)
                                    && fac.Facco.TpDoc_Id.Contains(tipoMov)
                                    && emp.Empresa_Id == 800188732
                              select new
                              {
                                  Id = fac.Facco_Id,
                                  Codigo = Convert.ToString(fac.Facco.Facco_Codigo),
                                  Movimiento = Convert.ToString(fac.Facco.TpDoc.TpDoc_Id),
                                  Tipo_Movimiento = Convert.ToString(fac.Facco.TpDoc.TpDoc_Nombre),
                                  Fecha = fac.Facco.Facco_FechaFactura,
                                  Hora = fac.Facco.Facco_Hora,
                                  Usuario = fac.Facco.Usua.Usua_Nombre,
                                  Materia_Prima_Id = Convert.ToInt16(fac.MatPri_Id),
                                  Materia_Prima = Convert.ToString(fac.MatPri.MatPri_Nombre),
                                  Tinta_Id = Convert.ToInt16(fac.Tinta.Tinta_Id),
                                  Tinta = Convert.ToString(fac.Tinta.Tinta_Nombre),
                                  Bopp_Id = Convert.ToInt64(fac.Bopp_Generico.BoppGen_Id),
                                  Bopp = Convert.ToString(fac.Bopp_Generico.BoppGen_Nombre),
                                  Cantidad = Convert.ToDecimal(fac.FaccoMatPri_Cantidad),
                                  Unidad_Medida = fac.UndMed_Id,
                                  Precio = 0,
                                  SubTotal = 0,
                                  Observacion = fac.Facco.Facco_Observacion,
                                  emp.Empresa_Id,
                                  emp.Empresa_Ciudad,
                                  emp.Empresa_COdigoPostal,
                                  emp.Empresa_Correo,
                                  emp.Empresa_Direccion,
                                  emp.Empresa_Telefono,
                                  emp.Empresa_Nombre,
                                  Tipo_Id_Proveedor = fac.Facco.Prov.TipoIdentificacion_Id,
                                  Proveedor_Id = fac.Facco.Prov_Id,
                                  Proveedor = fac.Facco.Prov.Prov_Nombre,
                                  Tipo_Proveedor = fac.Facco.Prov.TpProv.TpProv_Nombre,
                                  Telefono_Proveedor = fac.Facco.Prov.Prov_Telefono,
                                  Ciudad_Proveedor = fac.Facco.Prov.Prov_Ciudad,
                                  Correo_Proveedor = fac.Facco.Prov.Prov_Email,
                              };

            //Remisiones de Materia Prima
            var conRemisiones = from rem in _context.Set<Remision_MateriaPrima>()
                                from emp in _context.Set<Empresa>()
                                where Convert.ToString(rem.Rem.Rem_Id).Contains(codigo)
                                      && rem.Rem.TpDoc_Id.Contains(tipoMov)
                                      && emp.Empresa_Id == 800188732
                                select new
                                {
                                    Id = Convert.ToInt64(rem.Rem_Id),
                                    Codigo = Convert.ToString(rem.Rem.Rem_Codigo),
                                    Movimiento = Convert.ToString(rem.Rem.TpDoc.TpDoc_Id),
                                    Tipo_Movimiento = Convert.ToString(rem.Rem.TpDoc.TpDoc_Nombre),
                                    Fecha = rem.Rem.Rem_Fecha,
                                    Hora = rem.Rem.Rem_Hora,
                                    Usuario = rem.Rem.Usua.Usua_Nombre,
                                    Materia_Prima_Id = Convert.ToInt16(rem.MatPri_Id),
                                    Materia_Prima = Convert.ToString(rem.MatPri.MatPri_Nombre),
                                    Tinta_Id = Convert.ToInt16(rem.Tinta.Tinta_Id),
                                    Tinta = Convert.ToString(rem.Tinta.Tinta_Nombre),
                                    Bopp_Id = Convert.ToInt64(rem.Bopp.BoppGen_Id),
                                    Bopp = Convert.ToString(rem.Bopp.BoppGen_Nombre),
                                    Cantidad = Convert.ToDecimal(rem.RemiMatPri_Cantidad),
                                    Unidad_Medida = rem.UndMed_Id,
                                    Precio = 0,
                                    SubTotal = 0,
                                    Observacion = rem.Rem.Rem_Observacion,
                                    emp.Empresa_Id,
                                    emp.Empresa_Ciudad,
                                    emp.Empresa_COdigoPostal,
                                    emp.Empresa_Correo,
                                    emp.Empresa_Direccion,
                                    emp.Empresa_Telefono,
                                    emp.Empresa_Nombre,
                                    Tipo_Id_Proveedor = rem.Rem.Prov.TipoIdentificacion_Id,
                                    Proveedor_Id = rem.Rem.Prov_Id,
                                    Proveedor = rem.Rem.Prov.Prov_Nombre,
                                    Tipo_Proveedor = rem.Rem.Prov.TpProv.TpProv_Nombre,
                                    Telefono_Proveedor = rem.Rem.Prov.Prov_Telefono,
                                    Ciudad_Proveedor = rem.Rem.Prov.Prov_Ciudad,
                                    Correo_Proveedor = rem.Rem.Prov.Prov_Email,
                                };
            return Ok(conFacturas.Concat(conRemisiones));
        }

        // PUT: api/Materia_Prima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria_Prima(long id, Materia_Prima materia_Prima)
        {
            if (id != materia_Prima.MatPri_Id)
            {
                return BadRequest();
            }

            _context.Entry(materia_Prima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Materia_PrimaExists(id))
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

        // POST: api/Materia_Prima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materia_Prima>> PostMateria_Prima(Materia_Prima materia_Prima)
        {
          if (_context.Materias_Primas == null)
          {
              return Problem("Entity set 'dataContext.Materias_Primas'  is null.");
          }
            _context.Materias_Primas.Add(materia_Prima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateria_Prima", new { id = materia_Prima.MatPri_Id }, materia_Prima);
        }

        // DELETE: api/Materia_Prima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria_Prima(long id)
        {
            if (_context.Materias_Primas == null)
            {
                return NotFound();
            }
            var materia_Prima = await _context.Materias_Primas.FindAsync(id);
            if (materia_Prima == null)
            {
                return NotFound();
            }

            _context.Materias_Primas.Remove(materia_Prima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Materia_PrimaExists(long id)
        {
            return (_context.Materias_Primas?.Any(e => e.MatPri_Id == id)).GetValueOrDefault();
        }
    }
}
