using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
                           Precio = (from b in _context.Set<BOPP>() where b.BoppGen_Id == bp.BoppGen_Id orderby b.BOPP_Id descending select b.BOPP_Precio).FirstOrDefault(),  //Convert.ToDecimal(0),
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
                                     Descripcion = mp.MatPri_Descripcion,
                                     Inicial = inv.InvInicial_Stock,
                                     Actual = mp.MatPri_Stock,
                                     Item = mp.MatPri_Id,
                                     Referencia = mp.MatPri_Nombre,
                                     Stock = mp.MatPri_Stock,
                                     Precio = mp.MatPri_Precio,
                                     Subtotal = (mp.MatPri_Stock * mp.MatPri_Precio),
                                     IdCategoria = mp.CatMP_Id,
                                     IdSubcategoria = mp.SubCatMP_Id,
                                 };

            var bopp = from bp in _context.Set<BOPP>()
                       where bp.BOPP_Stock != 0
                       select new
                       {
                           Id_Materia_Prima = bp.BOPP_Serial,
                           Nombre_Materia_Prima = bp.BOPP_Nombre,
                           Descripcion = bp.BOPP_Descripcion,
                           Inicial = bp.BOPP_CantidadInicialKg,
                           Actual = bp.BOPP_Stock,
                           Item = bp.BOPP_Serial,
                           Referencia = bp.BOPP_Nombre,
                           Stock = bp.BOPP_Stock,
                           Precio = bp.BOPP_Precio,
                           Subtotal = (bp.BOPP_Stock * bp.BOPP_Precio),
                           IdCategoria = bp.CatMP_Id,
                           IdSubcategoria = bp.SubCatMP_Id,
                       };

            var tintas = from t in _context.Set<Tinta>()
                         select new
                         {
                             Id_Materia_Prima = t.Tinta_Id,
                             Nombre_Materia_Prima = t.Tinta_Nombre,
                             Descripcion = t.Tinta_Descripcion,
                             Inicial = t.Tinta_InvInicial,
                             Actual = t.Tinta_Stock,
                             Item = t.Tinta_Id,
                             Referencia = t.Tinta_Nombre,
                             Stock = t.Tinta_Stock,
                             Precio = t.Tinta_Precio,
                             Subtotal = (t.Tinta_Stock * t.Tinta_Precio),
                             IdCategoria = t.CatMP_Id,
                             IdSubcategoria = t.SubCatMP_Id,
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
                           mp.MatPri_Descripcion,
                           Inv.InvInicial_Stock,
                           mp.MatPri_Stock,
                           mp.UndMed_Id,
                           mp.MatPri_Precio,
                           mp.CatMP.CatMP_Nombre,
                           mp.CatMP_Id,
                           mp.MatPri_PrecioEstandar, 
                           mp.SubCatMP_Id, 
                           mp.SubcategoriasMP.SubCatMP_Nombre
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Id2 = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Descripcion = x.Key.MatPri_Descripcion,
                           Ancho = Convert.ToDouble(0.00),
                           Micras = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = salidaMateriaPrima,
                           Stock = x.Key.MatPri_Stock,
                           Diferencia = x.Key.MatPri_Stock - x.Key.InvInicial_Stock,
                           Presentacion = x.Key.UndMed_Id,
                           Precio = x.Key.MatPri_Precio,
                           PrecioEstandar = x.Key.MatPri_PrecioEstandar,
                           SubTotal = x.Key.MatPri_Stock * x.Key.MatPri_Precio,
                           Categoria = x.Key.CatMP_Nombre,
                           Categoria_Id = x.Key.CatMP_Id,
                           Subcategoria = x.Key.SubCatMP_Nombre,
                           Subcategoria_Id = x.Key.SubCatMP_Id
                       });

            //Entrada de BOPP
            var conBopp = (from bopp in _context.Set<BOPP>()
                           join bpGen in _context.Set<Bopp_Generico>() on bopp.BoppGen_Id equals bpGen.BoppGen_Id
                           where bopp.BOPP_Serial == id
                                 && (conAsgBopp > 0 || bopp.BOPP_Stock > 0)
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Id2 = bopp.BOPP_Id,
                               Nombre = bopp.BOPP_Nombre,
                               Descripcion = bopp.BOPP_Descripcion,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Micras = Convert.ToDouble(bopp.BOPP_CantidadMicras),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(conEntradaBOPP),
                               Salida = conAsgBopp,
                               Stock = bopp.BOPP_Stock,
                               Diferencia = bopp.BOPP_Stock - bopp.BOPP_CantidadInicialKg,
                               Presentacion = bopp.UndMed_Kg,
                               Precio = bopp.BOPP_Precio,
                               PrecioEstandar = bpGen.BoppGen_PrecioEstandar,
                               SubTotal = bopp.BOPP_Stock * bopp.BOPP_Precio,
                               Categoria = bopp.CatMP.CatMP_Nombre,
                               Categoria_Id = bopp.CatMP_Id,
                               Subcategoria = bopp.SubcategoriasMP.SubCatMP_Nombre,
                               Subcategoria_Id = bopp.SubCatMP_Id
                           });

            //Tinta
            var conTinta = (from tinta in _context.Set<Tinta>()
                            where tinta.Tinta_Id == id
                            select new
                            {
                                Id = tinta.Tinta_Id,
                                Id2 = tinta.Tinta_Id,
                                Nombre = tinta.Tinta_Nombre,
                                Descripcion = tinta.Tinta_Descripcion,
                                Ancho = Convert.ToDouble(0.00),
                                Micras = Convert.ToDouble(0.00),
                                Inicial = tinta.Tinta_InvInicial,
                                Entrada = Convert.ToDouble(entradaTintas),
                                Salida = salidaTintas,
                                Stock = tinta.Tinta_Stock,
                                Diferencia = tinta.Tinta_Stock - tinta.Tinta_InvInicial,
                                Presentacion = tinta.UndMed_Id,
                                Precio = tinta.Tinta_Precio,
                                PrecioEstandar = tinta.Tinta_PrecioEstandar,
                                SubTotal = tinta.Tinta_Stock * tinta.Tinta_Precio,
                                Categoria = tinta.CatMP.CatMP_Nombre,
                                Categoria_Id = tinta.CatMP_Id,
                                Subcategoria = tinta.SubcategoriasMP.SubCatMP_Nombre,
                                Subcategoria_Id = tinta.SubCatMP_Id
                            });

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con.Concat(conBopp).Concat(conTinta));
        }

        /* ===================================================================================================
         * VERSIÓN CONSOLIDADA DEL INVENTARIO (reporte de materia prima).
         * Reemplaza el flujo getInventarioMateriasPrimas + getInventario/{id} (una petición por material)
         * por una sola petición que calcula todos los movimientos agrupados por material.
         * Los endpoints anteriores se conservan sin cambios para no afectar otras versiones del proyecto web.
         * =================================================================================================== */

        //Ids de materiales comodín que no deben mostrarse en el reporte (antes se excluían en el front).
        private static readonly HashSet<long> IdsExcluidosInventario = new() { 2001, 2072, 84, 88, 89 };

        //Roles que pueden ver precios y subtotales del inventario.
        private static readonly HashSet<string> RolesVenPreciosInventario = new() { "1", "104" };

        //Clase auxiliar para agrupar movimientos por material en SQL.
        private class MovimientoCantidad
        {
            public long Id { get; set; }
            public decimal Cantidad { get; set; }
        }

        //Agrupa y suma en SQL las cantidades por material. Devuelve un diccionario Id -> cantidad.
        private static async Task<Dictionary<long, decimal>> SumarPorMaterial(IQueryable<MovimientoCantidad> query) =>
            await query.GroupBy(x => x.Id)
                       .Select(g => new { g.Key, Total = g.Sum(x => x.Cantidad) })
                       .ToDictionaryAsync(x => x.Key, x => x.Total);

        private static decimal Valor(Dictionary<long, decimal> dic, long id) => dic.TryGetValue(id, out var v) ? v : 0;

        [HttpGet("getInventarioConsolidado/{fecha1}/{fecha2}")]
        public async Task<ActionResult> GetInventarioConsolidado(DateTime fecha1, DateTime fecha2)
        {
            fecha1 = fecha1.Date;
            fecha2 = fecha2.Date;
            var hoy = DateTime.Today;
            if (fecha1 > fecha2) return BadRequest("La fecha inicial no puede ser mayor que la fecha final.");
            if (fecha2 > hoy) return BadRequest("La fecha final no puede ser mayor que el día de hoy.");
            bool verPrecios = RolesVenPreciosInventario.Contains(User.FindFirstValue(ClaimTypes.Role) ?? string.Empty);

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            // ---------- Salidas de Materia Prima ----------
            var asgMp = await SumarPorMaterial(_context.DetallesAsignaciones_MateriasPrimas.AsNoTracking()
                .Where(x => x.AsigMp.AsigMp_FechaEntrega >= fecha1 && x.AsigMp.AsigMp_FechaEntrega <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.MatPri_Id, Cantidad = x.DtAsigMp_Cantidad }));

            var asgMpCreacionTintas = await SumarPorMaterial(_context.DetallesAsignaciones_MatPrimasXTintas.AsNoTracking()
                .Where(x => x.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1 && x.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.MatPri_Id, Cantidad = x.DetAsigMPxTinta_Cantidad }));

            // ---------- Entradas de Materia Prima ----------
            var devolucionesMp = await SumarPorMaterial(_context.DetallesDevoluciones_MateriasPrimas.AsNoTracking()
                .Where(x => x.DevMatPri.DevMatPri_Fecha >= fecha1 && x.DevMatPri.DevMatPri_Fecha <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.MatPri_Id, Cantidad = x.DtDevMatPri_CantidadDevuelta }));

            var facturasMp = await SumarPorMaterial(_context.FacturasCompras_MateriaPrimas.AsNoTracking()
                .Where(x => x.Facco.Facco_FechaFactura >= fecha1 && x.Facco.Facco_FechaFactura <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.MatPri_Id, Cantidad = x.FaccoMatPri_Cantidad }));

            var remisionesMp = await SumarPorMaterial(_context.Remisiones_MateriasPrimas.AsNoTracking()
                .Where(x => x.Rem.Rem_Fecha >= fecha1 && x.Rem.Rem_Fecha <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.MatPri_Id, Cantidad = x.RemiMatPri_Cantidad }));

            var recuperadoMp = await SumarPorMaterial(_context.DetallesRecuperados_MateriasPrimas.AsNoTracking()
                .Where(x => x.RecMp.RecMp_FechaIngreso >= fecha1 && x.RecMp.RecMp_FechaIngreso <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.MatPri_Id, Cantidad = x.RecMatPri_Cantidad }));

            // ---------- Salidas de Tintas ----------
            var asgTintas = await SumarPorMaterial(_context.DetalleAsignaciones_Tintas.AsNoTracking()
                .Where(x => x.AsigMp.AsigMp_FechaEntrega >= fecha1 && x.AsigMp.AsigMp_FechaEntrega <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.Tinta_Id, Cantidad = x.DtAsigTinta_Cantidad }));

            var asgTintasCreacionTintas = await SumarPorMaterial(_context.DetallesAsignaciones_MatPrimasXTintas.AsNoTracking()
                .Where(x => x.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1 && x.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.Tinta_Id, Cantidad = x.DetAsigMPxTinta_Cantidad }));

            // ---------- Entradas de Tintas ----------
            var facturasTintas = await SumarPorMaterial(_context.FacturasCompras_MateriaPrimas.AsNoTracking()
                .Where(x => x.Facco.Facco_FechaFactura >= fecha1 && x.Facco.Facco_FechaFactura <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.Tinta_Id, Cantidad = x.FaccoMatPri_Cantidad }));

            var remisionesTintas = await SumarPorMaterial(_context.Remisiones_MateriasPrimas.AsNoTracking()
                .Where(x => x.Rem.Rem_Fecha >= fecha1 && x.Rem.Rem_Fecha <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.Tinta_Id, Cantidad = x.RemiMatPri_Cantidad }));

            var creacionTintas = await SumarPorMaterial(_context.Asignaciones_MatPrimasXTintas.AsNoTracking()
                .Where(x => x.AsigMPxTinta_FechaEntrega >= fecha1 && x.AsigMPxTinta_FechaEntrega <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.Tinta_Id, Cantidad = x.AsigMPxTinta_Cantidad }));

            // ---------- Movimientos de BOPP (por rollo: BOPP_Id) ----------
            var asgBopp = await SumarPorMaterial(_context.DetallesAsignaciones_BOPP.AsNoTracking()
                .Where(x => x.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1 && x.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.BOPP_Id, Cantidad = x.DtAsigBOPP_Cantidad }));

            var entradaBopp = await SumarPorMaterial(_context.BOPP.AsNoTracking()
                .Where(x => x.BOPP_FechaIngreso >= fecha1 && x.BOPP_FechaIngreso <= fecha2)
                .Select(x => new MovimientoCantidad { Id = x.BOPP_Id, Cantidad = x.BOPP_CantidadInicialKg }));

            // ---------- Inventario inicial del día de Materia Prima ----------
            // Si un material tiene más de un registro se toma el más reciente (mayor Codigo).
            var inicialMp = (await _context.Set<InventarioInicialXDia_MatPrima>().AsNoTracking()
                .Select(x => new { x.Codigo, x.MatPri_Id, x.InvInicial_Stock })
                .ToListAsync())
                .GroupBy(x => x.MatPri_Id)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(x => x.Codigo).First().InvInicial_Stock);

            // ---------- Maestros ----------
            var materiasPrimas = await _context.Set<Materia_Prima>().AsNoTracking()
                .Select(mp => new
                {
                    mp.MatPri_Id,
                    mp.MatPri_Nombre,
                    mp.MatPri_Descripcion,
                    mp.MatPri_Stock,
                    mp.UndMed_Id,
                    mp.MatPri_Precio,
                    mp.MatPri_PrecioEstandar,
                    mp.CatMP_Id,
                    CatMP_Nombre = mp.CatMP.CatMP_Nombre,
                    mp.SubCatMP_Id,
                    SubCatMP_Nombre = mp.SubcategoriasMP.SubCatMP_Nombre,
                    mp.MatPri_Fecha,
                }).ToListAsync();

            var tintas = await _context.Set<Tinta>().AsNoTracking()
                .Select(t => new
                {
                    t.Tinta_Id,
                    t.Tinta_Nombre,
                    t.Tinta_Descripcion,
                    t.Tinta_InvInicial,
                    t.Tinta_Stock,
                    t.UndMed_Id,
                    t.Tinta_Precio,
                    t.Tinta_PrecioEstandar,
                    t.CatMP_Id,
                    CatMP_Nombre = t.CatMP.CatMP_Nombre,
                    t.SubCatMP_Id,
                    SubCatMP_Nombre = t.SubcategoriasMP.SubCatMP_Nombre,
                }).ToListAsync();

            // Rollos con stock o con movimientos dentro del rango (así aparecen los rollos consumidos en el periodo).
            var idsBoppConMovimiento = asgBopp.Keys.Concat(entradaBopp.Keys).Distinct().ToList();
            var bopps = await (from b in _context.BOPP.AsNoTracking()
                               join bg in _context.Set<Bopp_Generico>() on b.BoppGen_Id equals bg.BoppGen_Id
                               where b.BOPP_Stock > 0 || idsBoppConMovimiento.Contains(b.BOPP_Id)
                               select new
                               {
                                   b.BOPP_Id,
                                   b.BOPP_Serial,
                                   b.BOPP_Nombre,
                                   b.BOPP_Descripcion,
                                   b.BOPP_Ancho,
                                   b.BOPP_CantidadMicras,
                                   b.BOPP_CantidadInicialKg,
                                   b.BOPP_Stock,
                                   b.UndMed_Kg,
                                   b.BOPP_Precio,
                                   bg.BoppGen_PrecioEstandar,
                                   b.CatMP_Id,
                                   CatMP_Nombre = b.CatMP.CatMP_Nombre,
                                   b.SubCatMP_Id,
                                   SubCatMP_Nombre = b.SubcategoriasMP.SubCatMP_Nombre,
                               }).ToListAsync();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            var resultado = new List<object>(materiasPrimas.Count + tintas.Count + bopps.Count);

            // Materias primas con inventario inicial del día (igual que la versión anterior). Las creadas dentro del rango
            // todavía no tienen ese registro (se genera aparte), así que se incluyen con inventario inicial en cero.
            foreach (var mp in materiasPrimas)
            {
                if (IdsExcluidosInventario.Contains(mp.MatPri_Id)) continue;
                if (!inicialMp.TryGetValue(mp.MatPri_Id, out var inicial))
                {
                    if (mp.MatPri_Fecha == null || mp.MatPri_Fecha.Value.Date < fecha1) continue;
                    inicial = 0;
                }
                resultado.Add(new
                {
                    Tipo = "MP",
                    Clave = $"MP-{mp.MatPri_Id}",
                    Id = mp.MatPri_Id,
                    Id2 = mp.MatPri_Id,
                    Nombre = mp.MatPri_Nombre,
                    Descripcion = mp.MatPri_Descripcion,
                    Ancho = 0d,
                    Micras = 0d,
                    Inicial = inicial,
                    Entrada = Valor(devolucionesMp, mp.MatPri_Id) + Valor(facturasMp, mp.MatPri_Id) + Valor(remisionesMp, mp.MatPri_Id) + Valor(recuperadoMp, mp.MatPri_Id),
                    Salida = Valor(asgMp, mp.MatPri_Id) + Valor(asgMpCreacionTintas, mp.MatPri_Id),
                    Stock = mp.MatPri_Stock,
                    Diferencia = mp.MatPri_Stock - inicial,
                    Presentacion = mp.UndMed_Id,
                    Precio = verPrecios ? mp.MatPri_Precio : 0,
                    PrecioEstandar = mp.MatPri_PrecioEstandar,
                    SubTotal = verPrecios ? mp.MatPri_Stock * mp.MatPri_Precio : 0,
                    Categoria = mp.CatMP_Nombre,
                    Categoria_Id = mp.CatMP_Id,
                    Subcategoria = mp.SubCatMP_Nombre,
                    Subcategoria_Id = mp.SubCatMP_Id,
                });
            }

            // Tintas
            foreach (var t in tintas)
            {
                if (IdsExcluidosInventario.Contains(t.Tinta_Id)) continue;
                resultado.Add(new
                {
                    Tipo = "TINTA",
                    Clave = $"TINTA-{t.Tinta_Id}",
                    Id = t.Tinta_Id,
                    Id2 = t.Tinta_Id,
                    Nombre = t.Tinta_Nombre,
                    Descripcion = t.Tinta_Descripcion,
                    Ancho = 0d,
                    Micras = 0d,
                    Inicial = t.Tinta_InvInicial,
                    Entrada = Valor(facturasTintas, t.Tinta_Id) + Valor(remisionesTintas, t.Tinta_Id) + Valor(creacionTintas, t.Tinta_Id),
                    Salida = Valor(asgTintas, t.Tinta_Id) + Valor(asgTintasCreacionTintas, t.Tinta_Id),
                    Stock = t.Tinta_Stock,
                    Diferencia = t.Tinta_Stock - t.Tinta_InvInicial,
                    Presentacion = t.UndMed_Id,
                    Precio = verPrecios ? t.Tinta_Precio : 0,
                    PrecioEstandar = t.Tinta_PrecioEstandar,
                    SubTotal = verPrecios ? t.Tinta_Stock * t.Tinta_Precio : 0,
                    Categoria = t.CatMP_Nombre,
                    Categoria_Id = t.CatMP_Id,
                    Subcategoria = t.SubCatMP_Nombre,
                    Subcategoria_Id = t.SubCatMP_Id,
                });
            }

            // Biorientados (un registro por rollo)
            foreach (var b in bopps)
            {
                if (IdsExcluidosInventario.Contains(b.BOPP_Serial)) continue;
                resultado.Add(new
                {
                    Tipo = "BOPP",
                    Clave = $"BOPP-{b.BOPP_Id}",
                    Id = b.BOPP_Serial,
                    Id2 = b.BOPP_Id,
                    Nombre = b.BOPP_Nombre,
                    Descripcion = b.BOPP_Descripcion,
                    Ancho = (double)b.BOPP_Ancho,
                    Micras = (double)b.BOPP_CantidadMicras,
                    Inicial = b.BOPP_CantidadInicialKg,
                    Entrada = Valor(entradaBopp, b.BOPP_Id),
                    Salida = Valor(asgBopp, b.BOPP_Id),
                    Stock = b.BOPP_Stock,
                    Diferencia = b.BOPP_Stock - b.BOPP_CantidadInicialKg,
                    Presentacion = b.UndMed_Kg,
                    Precio = verPrecios ? b.BOPP_Precio : 0,
                    PrecioEstandar = b.BoppGen_PrecioEstandar,
                    SubTotal = verPrecios ? b.BOPP_Stock * b.BOPP_Precio : 0,
                    Categoria = b.CatMP_Nombre,
                    Categoria_Id = b.CatMP_Id,
                    Subcategoria = b.SubCatMP_Nombre,
                    Subcategoria_Id = b.SubCatMP_Id,
                });
            }

            return Ok(resultado);
        }

        //Movimientos de Materia Prima, Tintas, Biorientados ***
        [HttpGet("getMovimientos/{fecha1}/{fecha2}")]
        public async Task<ActionResult> GetMovimientos(DateTime fecha1, DateTime fecha2, string? codigo = "", string? tipoMov = "", string? materiaPrima = "", string? tipoDoc = "")
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8629 // Dereference of a possibly null value.
            //Asignaciones de Materia Prima
            var conAsg = from asg in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where asg.AsigMp.AsigMp_FechaRealEntrega >= fecha1
                               && asg.AsigMp.AsigMp_FechaRealEntrega <= fecha2
                               && Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo).Contains(codigo)
                               && tipoMov == "ASIGMP"
                               && "SALIDA".Contains(tipoDoc)
                               && (materiaPrima != "" ? Convert.ToString(asg.MatPri_Id) == materiaPrima : Convert.ToString(asg.MatPri_Id).Contains(materiaPrima))
                         select new
                         {
                             Id = asg.AsigMp_Id,
                             Codigo = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                             Movimiento = Convert.ToString("ASIGMP"),
                             Tipo_Movimiento = Convert.ToString("Asignación de Materia Prima"),
                             Fecha = asg.AsigMp.AsigMp_FechaRealEntrega.Value,
                             Hora = asg.AsigMp.AsigMp_Hora,
                             Usuario = asg.AsigMp.Usua.Usua_Nombre,
                             Materia_Prima_Id = Convert.ToInt16(asg.MatPri_Id),
                             Materia_Prima = Convert.ToString(asg.MatPri.MatPri_Nombre),
                             Tinta_Id = Convert.ToInt16(2001),
                             Tinta = Convert.ToString(""),
                             Bopp_Id = Convert.ToInt64(449),
                             Bopp = Convert.ToString(""),
                             Cantidad = Convert.ToDecimal(asg.DtAsigMp_Cantidad),
                             Unidad_Medida = asg.UndMed_Id,
                             Precio = asg.DtAsigMp_Precio ?? 0m,
                             Proveedor = Convert.ToString("PLASTICARIBE SAS"), 
                             Subcategoria = Convert.ToString(asg.MatPri.SubcategoriasMP.SubCatMP_Nombre),
                             TpDoc = Convert.ToString("SALIDA"),
                         };

            //Asignacion de Materia Prima para la creacion de Tintas
            var conAsgMPCreacionTintas = from cr in _context.Set<DetalleAsignacion_MatPrimaXTinta>()
                                         where cr.AsigMPxTinta.AsigMPxTinta_FechaRealEntrega >= fecha1
                                               && cr.AsigMPxTinta.AsigMPxTinta_FechaRealEntrega <= fecha2
                                               && Convert.ToString(cr.AsigMPxTinta.Tinta_Id).Contains(codigo)
                                               && "CRTINTAS".Contains(tipoMov)
                                               && "ENTRADA".Contains(tipoDoc)
                                               && (materiaPrima != "" ? (Convert.ToString(cr.Tinta_Id) == materiaPrima || Convert.ToString(cr.MatPri_Id) == materiaPrima) :
                                                                        (Convert.ToString(cr.Tinta_Id).Contains(materiaPrima) || Convert.ToString(cr.MatPri_Id).Contains(materiaPrima)))
                                         select new
                                         {
                                             Id = cr.AsigMPxTinta_Id,
                                             Codigo = Convert.ToString((Convert.ToString(cr.AsigMPxTinta.Tinta_Id)) + " " + (cr.AsigMPxTinta.Tinta.Tinta_Nombre)),
                                             Movimiento = Convert.ToString("CRTINTAS"),
                                             Tipo_Movimiento = Convert.ToString("Creación de Tintas"),
                                             Fecha = cr.AsigMPxTinta.AsigMPxTinta_FechaEntrega,
                                             Hora = cr.AsigMPxTinta.AsigMPxTinta_Hora,
                                             Usuario = cr.AsigMPxTinta.Usua.Usua_Nombre,
                                             Materia_Prima_Id = Convert.ToInt16(cr.MatPri_Id),
                                             Materia_Prima = Convert.ToString(cr.MatPri.MatPri_Nombre),
                                             Tinta_Id = Convert.ToInt16(cr.Tinta_Id),
                                             Tinta = Convert.ToString(cr.TintasDAMPxT.Tinta_Nombre),
                                             Bopp_Id = Convert.ToInt64(449),
                                             Bopp = Convert.ToString(""),
                                             Cantidad = Convert.ToDecimal(cr.DetAsigMPxTinta_Cantidad),
                                             Unidad_Medida = cr.UndMed_Id,
                                             Precio = Convert.ToDecimal(cr.TintasDAMPxT.Tinta_Precio),
                                             Proveedor = Convert.ToString("PLASTICARIBE SAS"), 
                                             Subcategoria = Convert.ToString(cr.MatPri.SubcategoriasMP.SubCatMP_Nombre),
                                             TpDoc = Convert.ToString("ENTRADA"),
                                         };

            //Devoluciones de Materia Prima
            var conDevoluciones = from dev in _context.Set<DetalleDevolucion_MateriaPrima>()
                                  where dev.DevMatPri.DevMatPri_Fecha >= fecha1
                                        && dev.DevMatPri.DevMatPri_Fecha <= fecha2
                                        && Convert.ToString(dev.DevMatPri.DevMatPri_OrdenTrabajo).Contains(codigo)
                                        && "DEVMP".Contains(tipoMov)
                                        && "DEVOLUCION".Contains(tipoDoc)
                                        && (materiaPrima != "" ? (Convert.ToString(dev.MatPri_Id) == materiaPrima || Convert.ToString(dev.Tinta.Tinta_Id) == materiaPrima || Convert.ToString(dev.Bopp.BOPP_Serial) == materiaPrima) :
                                                                 (Convert.ToString(dev.MatPri_Id).Contains(materiaPrima) || Convert.ToString(dev.Tinta.Tinta_Id).Contains(materiaPrima) || Convert.ToString(dev.Bopp.BOPP_Serial).Contains(materiaPrima)))
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
                                      Precio = dev.Tinta.Tinta_Id > 2001 ? Convert.ToDecimal(dev.Tinta.Tinta_Precio) : Convert.ToDecimal(dev.MatPri.MatPri_Precio),
                                      Proveedor = Convert.ToString("PLASTICARIBE"), 
                                      Subcategoria = Convert.ToString(dev.MatPri.SubcategoriasMP.SubCatMP_Nombre),
                                      TpDoc = Convert.ToString("DEVOLUCION"),
                                  };

            //Facturas de Materia Prima
            var conFacturas = from fac in _context.Set<FacturaCompra_MateriaPrima>()
                              where fac.Facco.Facco_FechaFactura >= fecha1
                                    && fac.Facco.Facco_FechaFactura <= fecha2
                                    && Convert.ToString(fac.Facco.Facco_Codigo).Contains(codigo)
                                    && fac.Facco.TpDoc_Id.Contains(tipoMov)
                                    && "ENTRADA".Contains(tipoDoc)
                                    && (materiaPrima != "" ? (Convert.ToString(fac.MatPri_Id) == materiaPrima || Convert.ToString(fac.Tinta.Tinta_Id) == materiaPrima || Convert.ToString(fac.Bopp_Generico.BoppGen_Id) == materiaPrima) :
                                                             (Convert.ToString(fac.MatPri_Id).Contains(materiaPrima) || Convert.ToString(fac.Tinta.Tinta_Id).Contains(materiaPrima) || Convert.ToString(fac.Bopp_Generico.BoppGen_Id).Contains(materiaPrima)))
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
                                  Precio = Convert.ToDecimal(fac.FaccoMatPri_ValorUnitario),
                                  Proveedor = Convert.ToString(fac.Facco.Prov.Prov_Nombre), 
                                  Subcategoria = Convert.ToString(fac.MatPri.SubcategoriasMP.SubCatMP_Nombre),
                                  TpDoc = Convert.ToString("ENTRADA"),
                              };

            //Remisiones de Materia Prima
            var conRemisiones = from rem in _context.Set<Remision_MateriaPrima>()
                                where rem.Rem.Rem_Fecha >= fecha1
                                      && rem.Rem.Rem_Fecha <= fecha2
                                      && Convert.ToString(rem.Rem.Rem_Codigo).Contains(codigo)
                                      && rem.Rem.TpDoc_Id.Contains(tipoMov)
                                      && "ENTRADA".Contains(tipoDoc)
                                      && (materiaPrima != "" ? (Convert.ToString(rem.MatPri_Id) == materiaPrima || Convert.ToString(rem.Tinta.Tinta_Id) == materiaPrima || Convert.ToString(rem.Bopp.BoppGen_Id) == materiaPrima) :
                                                               (Convert.ToString(rem.MatPri_Id).Contains(materiaPrima) || Convert.ToString(rem.Tinta.Tinta_Id).Contains(materiaPrima) || Convert.ToString(rem.Bopp.BoppGen_Id).Contains(materiaPrima)))
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
                                    Precio = Convert.ToDecimal(rem.RemiMatPri_ValorUnitario.Value),
                                    Proveedor = Convert.ToString(rem.Rem.Prov.Prov_Nombre), 
                                    Subcategoria = Convert.ToString(rem.MatPri.SubcategoriasMP.SubCatMP_Nombre),
                                    TpDoc = Convert.ToString("ENTRADA"),
                                };

            //Asignaciones de BOPP
            var conAsgBopp = from asg in _context.Set<DetalleAsignacion_BOPP>()
                             where asg.AsigBOPP.AsigBOPP_FechaRealEntrega >= fecha1
                                   && asg.AsigBOPP.AsigBOPP_FechaRealEntrega <= fecha2
                                   && Convert.ToString(asg.DtAsigBOPP_OrdenTrabajo).Contains(codigo)
                                   && Convert.ToString(asg.TpDoc_Id).Contains(tipoMov)
                                   && "SALIDA".Contains(tipoDoc)
                                   && (materiaPrima != "" ? Convert.ToString(asg.BOPP_Id) == materiaPrima : Convert.ToString(asg.BOPP_Id).Contains(materiaPrima))
                             select new
                             {
                                 Id = Convert.ToInt64(asg.AsigBOPP_Id),
                                 Codigo = Convert.ToString(asg.DtAsigBOPP_OrdenTrabajo),
                                 Movimiento = Convert.ToString(asg.Tipo_Documento.TpDoc_Id),
                                 Tipo_Movimiento = Convert.ToString(asg.Tipo_Documento.TpDoc_Nombre),
                                 Fecha = asg.AsigBOPP.AsigBOPP_FechaRealEntrega.Value,
                                 Hora = asg.AsigBOPP.AsigBOPP_Hora,
                                 Usuario = asg.AsigBOPP.Usua.Usua_Nombre,
                                 Materia_Prima_Id = Convert.ToInt16(84),
                                 Materia_Prima = Convert.ToString(""),
                                 Tinta_Id = Convert.ToInt16(2001),
                                 Tinta = Convert.ToString(""),
                                 Bopp_Id = Convert.ToInt64(asg.BOPP_Id),
                                 Bopp = Convert.ToString(asg.BOPP.BOPP_Nombre),
                                 Cantidad = Convert.ToDecimal(asg.DtAsigBOPP_Cantidad),
                                 Unidad_Medida = asg.UndMed_Id,
                                 Precio = Convert.ToDecimal(asg.BOPP.BOPP_Precio),
                                 Proveedor = Convert.ToString("PLASTICARIBE SAS"), 
                                 Subcategoria = Convert.ToString(asg.BOPP.SubcategoriasMP.SubCatMP_Nombre),
                                 TpDoc = Convert.ToString("SALIDA"),
                             };

            //Entrada de BOPP
            var conEntradaBOPP = from ent in _context.Set<BOPP>()
                                 where ent.BOPP_FechaIngreso >= fecha1
                                       && ent.BOPP_FechaIngreso <= fecha2
                                       && Convert.ToString(ent.BOPP_Id).Contains(codigo)
                                       && Convert.ToString("ENTBIO").Contains(tipoMov)
                                       && "ENTRADA".Contains(tipoDoc)
                                       && (materiaPrima != "" ? Convert.ToString(ent.BOPP_Serial) == materiaPrima : Convert.ToString(ent.BOPP_Serial).Contains(materiaPrima))
                                 select new
                                 {
                                     Id = Convert.ToInt64(ent.BOPP_Id),
                                     Codigo = Convert.ToString(ent.BOPP_Id),
                                     Movimiento = Convert.ToString("ENTBIO"),
                                     Tipo_Movimiento = Convert.ToString("Entrada de Biorientado"),
                                     Fecha = ent.BOPP_FechaIngreso,
                                     Hora = ent.BOPP_Hora,
                                     Usuario = ent.Usua.Usua_Nombre,
                                     Materia_Prima_Id = Convert.ToInt16(84),
                                     Materia_Prima = Convert.ToString(""),
                                     Tinta_Id = Convert.ToInt16(2001),
                                     Tinta = Convert.ToString(""),
                                     Bopp_Id = Convert.ToInt64(ent.BOPP_Serial),
                                     Bopp = Convert.ToString(ent.BOPP_Nombre),
                                     Cantidad = Convert.ToDecimal(ent.BOPP_Stock),
                                     Unidad_Medida = ent.UndMed_Kg,
                                     Precio = Convert.ToDecimal(ent.BOPP_Precio),
                                     Proveedor = Convert.ToString(ent.Proveedor.Prov_Nombre), 
                                     Subcategoria = Convert.ToString(ent.SubcategoriasMP.SubCatMP_Nombre),
                                     TpDoc = Convert.ToString("ENTRADA"),
                                 };

            //Asignacion de Tinta
            var conAsgTinta = from asg in _context.Set<DetalleAsignacion_Tinta>()
                              where asg.AsigMp.AsigMp_FechaRealEntrega >= fecha1
                                    && asg.AsigMp.AsigMp_FechaRealEntrega <= fecha2
                                    && Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo).Contains(codigo)
                                    && Convert.ToString("ASIGTINTAS").Contains(tipoMov)
                                    && "SALIDA".Contains(tipoDoc)
                                    && (materiaPrima != "" ? Convert.ToString(asg.Tinta_Id) == materiaPrima : Convert.ToString(asg.Tinta_Id).Contains(materiaPrima))
                              select new
                              {
                                  Id = Convert.ToInt64(asg.AsigMp_Id),
                                  Codigo = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                                  Movimiento = Convert.ToString("ASIGTINTAS"),
                                  Tipo_Movimiento = Convert.ToString("Asignación de Tintas"),
                                  Fecha = asg.AsigMp.AsigMp_FechaRealEntrega.Value,
                                  Hora = asg.AsigMp.AsigMp_Hora,
                                  Usuario = asg.AsigMp.Usua.Usua_Nombre,
                                  Materia_Prima_Id = Convert.ToInt16(84),
                                  Materia_Prima = Convert.ToString(""),
                                  Tinta_Id = Convert.ToInt16(asg.Tinta_Id),
                                  Tinta = Convert.ToString(asg.Tinta.Tinta_Nombre),
                                  Bopp_Id = Convert.ToInt64(449),
                                  Bopp = Convert.ToString(""),
                                  Cantidad = Convert.ToDecimal(asg.DtAsigTinta_Cantidad),
                                  Unidad_Medida = asg.UndMed_Id,
                                  Precio = asg.DtAsigTinta_Precio ?? 0m,
                                  Proveedor = Convert.ToString("PLASTICARIBE SAS"),
                                  Subcategoria = Convert.ToString(asg.Tinta.SubcategoriasMP.SubCatMP_Nombre),
                                  TpDoc = Convert.ToString("SALIDA"),
                              };

            // Ejecutar consultas de lectura secuencialmente y usar AsNoTracking para mejorar rendimiento
            var listAsg = await conAsg.AsNoTracking().ToListAsync();
            var listAsgMPCreacionTintas = await conAsgMPCreacionTintas.AsNoTracking().ToListAsync();
            var listDevoluciones = await conDevoluciones.AsNoTracking().ToListAsync();
            var listFacturas = await conFacturas.AsNoTracking().ToListAsync();
            var listRemisiones = await conRemisiones.AsNoTracking().ToListAsync();
            var listAsgBopp = await conAsgBopp.AsNoTracking().ToListAsync();
            var listEntradaBOPP = await conEntradaBOPP.AsNoTracking().ToListAsync();
            var listAsgTinta = await conAsgTinta.AsNoTracking().ToListAsync();

            var result = listAsg
                .Concat(listAsgMPCreacionTintas)
                .Concat(listDevoluciones)
                .Concat(listFacturas)
                .Concat(listRemisiones)
                .Concat(listAsgBopp)
                .Concat(listEntradaBOPP)
                .Concat(listAsgTinta);

            return Ok(result);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

        [HttpGet("getInfoMovimientoAsignaciones/{codigo}/{tipoMov}")]
        public ActionResult GetInfoMovimientoAsignaciones(string codigo, string tipoMov)
        {
            // Coincidencia exacta: con Contains el PDF de la asignación 7 incluía también la 17, 70, 107, etc.
            if (!long.TryParse(codigo, out var id)) return BadRequest("El código de la asignación no es válido.");

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = from asg in _context.Set<DetalleAsignacion_MateriaPrima>()
                         from emp in _context.Set<Empresa>()
                         where asg.AsigMp_Id == id
                               && tipoMov == "ASIGMP"
                               && emp.Empresa_Id == 800188732
                         select new
                         {
                             Id = asg.AsigMp_Id,
                             Codigo = Convert.ToString(asg.AsigMp.AsigMP_OrdenTrabajo),
                             Movimiento = Convert.ToString("ASIGMP"),
                             Tipo_Movimiento = Convert.ToString("Asignación de Materia Prima"),
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
                             Solicitud_Id = asg.AsigMp.SolMpExt_Id ?? 0,
                             emp.Empresa_Id,
                             emp.Empresa_Ciudad,
                             emp.Empresa_COdigoPostal,
                             emp.Empresa_Correo,
                             emp.Empresa_Direccion,
                             emp.Empresa_Telefono,
                             emp.Empresa_Nombre,
                             FechaReal = asg.AsigMp.AsigMp_FechaRealEntrega
                         };

            //Asignaciones de BOPP
            var conAsgBopp = from asg in _context.Set<DetalleAsignacion_BOPP>()
                             from emp in _context.Set<Empresa>()
                             where asg.AsigBOPP_Id == id
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
                                 Solicitud_Id = Convert.ToInt64(0),
                                 emp.Empresa_Id,
                                 emp.Empresa_Ciudad,
                                 emp.Empresa_COdigoPostal,
                                 emp.Empresa_Correo,
                                 emp.Empresa_Direccion,
                                 emp.Empresa_Telefono,
                                 emp.Empresa_Nombre,
                                 FechaReal = asg.AsigBOPP.AsigBOPP_FechaRealEntrega
                             };

            //Asignacion de Tinta
            var conAsgTinta = from asg in _context.Set<DetalleAsignacion_Tinta>()
                              from emp in _context.Set<Empresa>()
                              where tipoMov == "ASIGTINTAS"
                                    && asg.AsigMp_Id == id
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
                                  Solicitud_Id = asg.AsigMp.SolMpExt_Id ?? 0,
                                  emp.Empresa_Id,
                                  emp.Empresa_Ciudad,
                                  emp.Empresa_COdigoPostal,
                                  emp.Empresa_Correo,
                                  emp.Empresa_Direccion,
                                  emp.Empresa_Telefono,
                                  emp.Empresa_Nombre,
                                  FechaReal = asg.AsigMp.AsigMp_FechaRealEntrega
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
                              from doc in _context.Set<Detalle_OrdenCompra>()
                              from emp in _context.Set<Empresa>()
                              join of in _context.Set<OrdenesCompras_FacturasCompras>() on fac.Facco_Id equals of.Facco_Id
                              join oc in _context.Set<Orden_Compra>() on of.Oc_Id equals oc.Oc_Id
                              where Convert.ToString(fac.Facco.Facco_Id).Contains(codigo)
                                    && fac.Facco.TpDoc_Id.Contains(tipoMov)
                                    && emp.Empresa_Id == 800188732 
                                    && fac.MatPri_Id == doc.MatPri_Id
                                    && fac.Tinta_Id == doc.Tinta_Id
                                    && fac.Bopp_Id == doc.BOPP_Id
                                    && doc.Oc_Id == oc.Oc_Id
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
                                  Categoria = fac.MatPri_Id != 84 && fac.Tinta_Id == 2001 && fac.Bopp_Id == 1 ? fac.MatPri.CatMP_Id:
                                              fac.MatPri_Id == 84 && fac.Tinta_Id != 2001 && fac.Bopp_Id == 1 ? fac.Tinta.CatMP_Id : 6,
                                  Precio = fac.FaccoMatPri_ValorUnitario,
                                  SubTotal = 0, //fac.MatPri_Id != 84 && fac.Tinta_Id == 2001 && fac.Bopp_Id == 1 && fac.MatPri.CatMP_Id != 18 ? Convert.ToDecimal(fac.FaccoMatPri_Cantidad) * Convert.ToDecimal(fac.FaccoMatPri_ValorUnitario) :
                                             //fac.MatPri_Id != 84 && fac.Tinta_Id == 2001 && fac.Bopp_Id == 1 && fac.MatPri.CatMP_Id == 18 ? Convert.ToString(fac.MatPri.MatPri_Nombre).Contains("CONO") ? Convert.ToDecimal(fac.FaccoMatPri_Cantidad) * (Convert.ToDecimal(fac.MatPri.MatPri_Nombre.Replace("CONO ", "").Replace(" CMS", "").Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0]) + Convert.ToDecimal(fac.MatPri.MatPri_Nombre.Replace("CONO ", "").Replace(" CMS", "").Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[1])) / 2 * Convert.ToDecimal(fac.FaccoMatPri_ValorUnitario) : Convert.ToDecimal(fac.FaccoMatPri_Cantidad) * Convert.ToDecimal(fac.FaccoMatPri_ValorUnitario) :
                                             //fac.MatPri_Id == 84 && fac.Tinta_Id != 2001 && fac.Bopp_Id == 1 ? Convert.ToDecimal(fac.FaccoMatPri_Cantidad) * Convert.ToDecimal(fac.FaccoMatPri_ValorUnitario) : Convert.ToDecimal(0),
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
                                  //Iva y Retenciones
                                  Orden_Compra = oc.Oc_Id,
                                  Valor_Total = Convert.ToDecimal(fac.Facco.Facco_ValorTotal),
                                  Peso_Total = Convert.ToDecimal(fac.FaccoMatPri_Cantidad),
                                  Iva = oc.IVA,
                                  ReteIva = oc.ReteIVA,
                                  ReteFuente = oc.ReteFuente,
                                  ReteIca = oc.ReteICA,
                                  Base = oc.Proveedor.CA_ReteICA.Base,
                              };

            //Remisiones de Materia Prima
            var conRemisiones = from rem in _context.Set<Remision_MateriaPrima>()
                                from doc in _context.Set<Detalle_OrdenCompra>()
                                from emp in _context.Set<Empresa>()
                                join ro in _context.Set<Remision_OrdenCompra>() on rem.Rem_Id equals ro.Rem_Id
                                join oc in _context.Set<Orden_Compra>() on ro.Oc_Id equals oc.Oc_Id
                                where Convert.ToString(rem.Rem.Rem_Id).Contains(codigo)
                                      && rem.Rem.TpDoc_Id.Contains(tipoMov)
                                      && emp.Empresa_Id == 800188732
                                      && rem.MatPri_Id == doc.MatPri_Id
                                      && rem.Tinta_Id == doc.Tinta_Id
                                      && rem.Bopp_Id == doc.BOPP_Id
                                      && doc.Oc_Id == oc.Oc_Id
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
                                    Categoria = rem.MatPri_Id != 84 && rem.Tinta_Id == 2001 && rem.Bopp_Id == 1 ? rem.MatPri.CatMP_Id :
                                                rem.MatPri_Id == 84 && rem.Tinta_Id != 2001 && rem.Bopp_Id == 1 ? rem.Tinta.CatMP_Id : 6,
                                    Precio = rem.RemiMatPri_ValorUnitario.Value,
                                    SubTotal = 0, //rem.MatPri_Id != 84 && rem.Tinta_Id == 2001 && rem.Bopp_Id == 1 && rem.MatPri.CatMP_Id != 18 ? Convert.ToDecimal(rem.RemiMatPri_Cantidad) * Convert.ToDecimal(rem.RemiMatPri_ValorUnitario.Value) :
                                               //rem.MatPri_Id != 84 && rem.Tinta_Id == 2001 && rem.Bopp_Id == 1 && rem.MatPri.CatMP_Id == 18 ? Convert.ToString(rem.MatPri.MatPri_Nombre).Contains("CONO") ? Convert.ToDecimal(rem.RemiMatPri_Cantidad) * (Convert.ToDecimal(rem.MatPri.MatPri_Nombre.Replace("CONO ", "").Replace(" CMS", "").Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0]) + Convert.ToDecimal(rem.MatPri.MatPri_Nombre.Replace("CONO ", "").Replace(" CMS", "").Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[1])) / 2 * Convert.ToDecimal(rem.RemiMatPri_ValorUnitario.Value) : Convert.ToDecimal(rem.RemiMatPri_Cantidad) * Convert.ToDecimal(rem.RemiMatPri_ValorUnitario.Value) :
                                               //rem.MatPri_Id == 84 && rem.Tinta_Id != 2001 && rem.Bopp_Id == 1 ? Convert.ToDecimal(rem.RemiMatPri_Cantidad) * Convert.ToDecimal(rem.RemiMatPri_ValorUnitario.Value) : Convert.ToDecimal(0),
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
                                    //Iva y Retenciones
                                    Orden_Compra = oc.Oc_Id,
                                    Valor_Total = Convert.ToDecimal(rem.Rem.Rem_PrecioEstimado.Value),
                                    Peso_Total = Convert.ToDecimal(rem.RemiMatPri_Cantidad),
                                    Iva = oc.IVA,
                                    ReteIva = oc.ReteIVA,
                                    ReteFuente = oc.ReteFuente,
                                    ReteIca = oc.ReteICA,
                                    Base = oc.Proveedor.CA_ReteICA.Base,
                                };
            return Ok(conFacturas.Concat(conRemisiones));
        }

        //Consulta que traerá la materia prima, tintas, bopp generico
        [HttpGet("getMateriales_Receta")]
        public ActionResult GetMateriales_Receta()
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

            var bopp = from bp in _context.Set<Bopp_Generico>()
                       where bp.BoppGen_Id != 1
                       select new
                       {
                           Id = bp.BoppGen_Id,
                           Nombre = bp.BoppGen_Nombre,
                           Categoria = 6,
                       };

            return Ok(materiaPrima.Concat(tinta).Concat(bopp));
        }

        //Consulta que traerá las materias primas através de una sentencia tipo like
        [HttpGet("getPolietilenos/{nombre}")]
        public ActionResult getPolietilenos(string nombre)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var materiaPrima = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id != 84
                               && mp.MatPri_Nombre.Contains(nombre)
                               select new
                               {
                                   Item = mp.MatPri_Id,
                                   Referencia = mp.MatPri_Nombre,
                                   PrecioKg = mp.MatPri_Precio,
                                   Categoria = mp.CatMP_Id,
                               };

            var tinta = from tt in _context.Set<Tinta>()
                        where tt.Tinta_Id != 2001
                        select new
                        {
                            Item = tt.Tinta_Id,
                            Referencia = tt.Tinta_Nombre,
                            PrecioKg = tt.Tinta_Precio,
                            Categoria = tt.CatMP_Id,
                        };

            var bopp = from bp in _context.Set<Bopp_Generico>()
                       where bp.BoppGen_Id != 1
                       select new
                       {
                           Item = bp.BoppGen_Id,
                           Referencia = bp.BoppGen_Nombre,
                           PrecioKg = (from b in _context.Set<BOPP>() where b.BoppGen_Id == bp.BoppGen_Id orderby b.BOPP_Id descending select b.BOPP_Precio).FirstOrDefault(),
                           Categoria = (from b in _context.Set<BOPP>() where b.BoppGen_Id == bp.BoppGen_Id orderby b.BOPP_Id descending select b.CatMP_Id).FirstOrDefault(),
                       };

            if (materiaPrima == null && tinta == null && bopp == null) return BadRequest("No se encontró la materia prima consultada!");
            return Ok(materiaPrima.Concat(tinta).Concat(bopp));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        //Consulta que traerá las materias primas através de una sentencia tipo like
        [HttpGet("getPeletizados")]
        public ActionResult getPeletizados()
        {
            int[] categories = { 10, 4 };
#pragma warning disable CS8604 // Possible null reference argument.
            var peletizado = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id != 84
                               && categories.Contains(mp.CatMP_Id)
                               select new
                               {
                                   Id = mp.MatPri_Id,
                                   MatPrima = mp.MatPri_Nombre,
                                   CategoryId = mp.CatMP_Id, 
                                   Category = mp.CatMP.CatMP_Nombre, 
                                   Stock = mp.MatPri_Stock, 
                                   Presentation = mp.UndMed_Id,
                                   Price = mp.MatPri_Precio,
                                   Subtotal = (mp.MatPri_Stock * mp.MatPri_Precio)
                               };
            return Ok(peletizado);
        }

        //Función que traerá todas las subcategorias de materias primas y tintas con su respectivo stock para el módulo de solicitud de materia prima
        [HttpGet("getAllSubcategoriesForName/{material}/{proceso}")]
        public async Task<ActionResult> getAllSubcategoriesForId(string material, string proceso)
        {
            if (proceso == "EXT")
            {
                var materiaPrima = await (from mp in _context.Set<Materia_Prima>()
                                          join s in _context.Set<Subcategorias_MatPrima>() on mp.SubCatMP_Id equals s.SubCatMP_Id
                                          where mp.MatPri_Id != 84
                                          && s.SubCatMP_Nombre.Contains(material)
                                          && mp.SubCatMP_Id != null
                                          group mp by new
                                          {
                                              mp.SubCatMP_Id,
                                              mp.SubcategoriasMP.SubCatMP_Nombre
                                          } into g
                                          select new
                                          {
                                              Id_Subcategoria = g.Key.SubCatMP_Id,
                                              Subcategoria = g.Key.SubCatMP_Nombre,
                                              Stock = g.Sum(x => x.MatPri_Stock),
                                              Und = g.Select(x => x.UndMed_Id).FirstOrDefault(),
                                          }).ToListAsync();

                return Ok(materiaPrima);
            }
            else if (proceso != "EXT"){
                var tinta = await (from tt in _context.Set<Tinta>()
                                   join s in _context.Set<Subcategorias_MatPrima>() on tt.SubCatMP_Id equals s.SubCatMP_Id
                                   where tt.Tinta_Id != 2001
                                   && s.SubCatMP_Nombre.Contains(material)
                                   group tt by new
                                    {
                                        tt.SubCatMP_Id,
                                        tt.SubcategoriasMP.SubCatMP_Nombre
                                    } into g
                                    select new
                                    {
                                        Stock = g.Sum(x => x.Tinta_Stock),
                                        Subcategoria = g.Key.SubCatMP_Nombre,
                                        Id_Subcategoria = g.Key.SubCatMP_Id,
                                    }).ToListAsync();

                return Ok(tinta);
            }
            return NotFound();
        }

        /// <summary>
        /// Busca subcategorías de materias primas (proceso EXT) o de tintas (demás procesos) por Id o por nombre,
        /// con su stock total y unidad de medida, para el módulo de solicitud de materia prima.
        /// Versión optimizada de <c>getAllSubcategoriesForName</c>:
        /// - El texto viaja por query string, así nombres con '/', '%' o '#' no rompen la ruta.
        /// - Si el texto es numérico también busca por Id exacto de subcategoría.
        /// - Agrupa por las columnas de la subcategoría ya unida (sin navegación adicional) y obtiene la unidad
        ///   con MAX en lugar de FirstOrDefault, que EF traduce como subconsulta correlacionada por grupo.
        /// - Ordena primero la coincidencia exacta por Id, luego los nombres que empiezan por el texto, y limita los resultados.
        /// </summary>
        /// <param name="proceso">EXT para materias primas; cualquier otro proceso para tintas.</param>
        /// <param name="texto">Id de la subcategoría o parte de su nombre.</param>
        /// <param name="cancellationToken">Cancela la consulta si el cliente aborta la petición.</param>
        [HttpGet("buscarSubcategorias/{proceso}")]
        public async Task<ActionResult> buscarSubcategorias(string proceso, [FromQuery] string texto, CancellationToken cancellationToken)
        {
            const int maxResultados = 50;
            texto = texto?.Trim() ?? string.Empty;
            if (texto.Length == 0) return BadRequest("Debe ingresar el Id o parte del nombre de la subcategoría.");

            bool esId = int.TryParse(texto, out int id);

            if (proceso == "EXT")
            {
                var materiasPrimas = await (from mp in _context.Set<Materia_Prima>().AsNoTracking()
                                            join s in _context.Set<Subcategorias_MatPrima>().AsNoTracking() on mp.SubCatMP_Id equals s.SubCatMP_Id
                                            where mp.MatPri_Id != 84
                                            && ((esId && s.SubCatMP_Id == id) || s.SubCatMP_Nombre.Contains(texto))
                                            group mp by new { s.SubCatMP_Id, s.SubCatMP_Nombre } into g
                                            orderby (esId && g.Key.SubCatMP_Id == id) descending,
                                                    g.Key.SubCatMP_Nombre.StartsWith(texto) descending,
                                                    g.Key.SubCatMP_Nombre
                                            select new
                                            {
                                                Id_Subcategoria = g.Key.SubCatMP_Id,
                                                Subcategoria = g.Key.SubCatMP_Nombre,
                                                Stock = g.Sum(x => x.MatPri_Stock),
                                                Und = g.Max(x => x.UndMed_Id),
                                            }).Take(maxResultados).ToListAsync(cancellationToken);

                return Ok(materiasPrimas);
            }

            var tintas = await (from tt in _context.Set<Tinta>().AsNoTracking()
                                join s in _context.Set<Subcategorias_MatPrima>().AsNoTracking() on tt.SubCatMP_Id equals s.SubCatMP_Id
                                where tt.Tinta_Id != 2001
                                && ((esId && s.SubCatMP_Id == id) || s.SubCatMP_Nombre.Contains(texto))
                                group tt by new { s.SubCatMP_Id, s.SubCatMP_Nombre } into g
                                orderby (esId && g.Key.SubCatMP_Id == id) descending,
                                        g.Key.SubCatMP_Nombre.StartsWith(texto) descending,
                                        g.Key.SubCatMP_Nombre
                                select new
                                {
                                    Id_Subcategoria = g.Key.SubCatMP_Id,
                                    Subcategoria = g.Key.SubCatMP_Nombre,
                                    Stock = g.Sum(x => x.Tinta_Stock),
                                    Und = g.Max(x => x.UndMed_Id),
                                }).Take(maxResultados).ToListAsync(cancellationToken);

            return Ok(tintas);
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

        //Categorías que corresponden a tintas, chips y solventes (se crean desde el endpoint de tintas).
        internal static readonly HashSet<int> CategoriasTintas = new() { 7, 8, 13 };

        /// <summary>
        /// Crea una materia prima validando los datos (a diferencia del POST genérico, que guarda lo que reciba).
        /// Responde 400 si falta un dato o no es válido y 409 si ya existe una materia prima con el mismo nombre.
        /// </summary>
        [HttpPost("crear")]
        public async Task<ActionResult> CrearMateriaPrima(CrearMateriaPrimaRequest request)
        {
            var nombre = (request.Nombre ?? string.Empty).Trim().ToUpper();
            var descripcion = string.IsNullOrWhiteSpace(request.Descripcion) ? nombre : request.Descripcion.Trim().ToUpper();
            if (nombre.Length == 0) return BadRequest("El nombre de la materia prima es obligatorio.");
            if (request.Precio < 0) return BadRequest("El precio no puede ser negativo.");
            if (CategoriasTintas.Contains(request.CatMP_Id)) return BadRequest("Las tintas, chips y solventes se crean desde la opción de tintas.");
            if (!await _context.Categorias_MatPrima.AnyAsync(c => c.CatMP_Id == request.CatMP_Id)) return BadRequest("La categoría no existe.");
            if (request.SubCatMP_Id == null) return BadRequest("La subcategoría es obligatoria.");
            if (!await _context.Subcategorias_MatPrima.AnyAsync(s => s.SubCatMP_Id == request.SubCatMP_Id && s.CatMP_Id == request.CatMP_Id))
                return BadRequest("La subcategoría no pertenece a la categoría elegida.");

            var existente = await _context.Materias_Primas.AsNoTracking()
                .Where(m => m.MatPri_Nombre.Trim().ToUpper() == nombre)
                .Select(m => new { m.MatPri_Id })
                .FirstOrDefaultAsync();
            if (existente != null) return Conflict($"Ya existe una materia prima con el nombre {nombre} (Id {existente.MatPri_Id}).");

            var ahora = DateTime.Now;
            var materiaPrima = new Materia_Prima
            {
                MatPri_Nombre = nombre,
                MatPri_Descripcion = descripcion,
                MatPri_Stock = 0,
                UndMed_Id = "Kg",
                CatMP_Id = request.CatMP_Id,
                MatPri_Precio = request.Precio,
                TpBod_Id = 4,
                MatPri_Fecha = ahora.Date,
                MatPri_Hora = ahora.ToString("H:mm:ss"),
                SubCatMP_Id = request.SubCatMP_Id,
            };
            _context.Materias_Primas.Add(materiaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateria_Prima", new { id = materiaPrima.MatPri_Id }, materiaPrima);
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
        // Corrección de IDE1006: Cambiar el nombre del método a PascalCase.
        // Corrección de CS1998: Usar await al llamar a ToListAsync() para que el método sea realmente asíncrono.
        [HttpGet("getSubcategories/{proceso}")]
        public async Task<ActionResult> GetSubcategories(string proceso)
        {
            if (proceso == "EXT")
            {
                var result = await (from mp in _context.Set<Materia_Prima>()
                                    where mp.MatPri_Id != 84
                                    select new
                                    {
                                        Item = mp.MatPri_Id,
                                        Referencia = mp.MatPri_Nombre,
                                        Stock = mp.MatPri_Stock,
                                        Subcategoria = mp.SubcategoriasMP.SubCatMP_Nombre,
                                        Id_Subcategoria = mp.SubCatMP_Id,
                                        Precio = mp.MatPri_Precio,
                                    }).ToListAsync();
                return Ok(result);
            }
            else if (proceso != "EXT")
            {
                var result = await (from tt in _context.Set<Tinta>()
                                    where tt.Tinta_Id != 2001
                                    select new
                                    {
                                        Item = tt.Tinta_Id,
                                        Referencia = tt.Tinta_Nombre,
                                        Stock = tt.Tinta_Stock,
                                        Subcategoria = tt.SubcategoriasMP.SubCatMP_Nombre,
                                        Id_Subcategoria = tt.SubCatMP_Id,
                                        Precio = tt.Tinta_Precio,
                                    }).ToListAsync();
                return Ok(result);
            }
            return NotFound();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
