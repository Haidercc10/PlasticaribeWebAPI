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

        //
        [HttpGet("ConsultaInventario1/{fecha1}/{fecha2}/{id}/{categoria}")]
        public ActionResult Get(DateTime fecha1, DateTime fecha2, long id, long categoria)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = _context.DetallesAsignaciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.AsigMp.AsigMp_FechaEntrega >= fecha1 
                       && asg.AsigMp.AsigMp_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigMp_Cantidad);

            //Devoluciones de Materia Prima
            var conDevoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.DevMatPri.DevMatPri_Fecha >= fecha1
                       && asg.DevMatPri.DevMatPri_Fecha <= fecha2).Sum(asg => asg.DtDevMatPri_CantidadDevuelta);

            //Facturas de Materia Prima
            var conFacturas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.Facco.Facco_FechaFactura >= fecha1
                       && asg.Facco.Facco_FechaFactura <= fecha2).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Materia Prima
            var conRemisiones = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.Rem.Rem_Fecha >= fecha1
                       && asg.Rem.Rem_Fecha <= fecha2).Sum(asg => asg.RemiMatPri_Cantidad);

            //Recuperado de Materia Prima
            var conRecuperado = _context.DetallesRecuperados_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.RecMp.RecMp_FechaIngreso >= fecha1
                       && asg.RecMp.RecMp_FechaIngreso <= fecha2).Sum(asg => asg.RecMatPri_Cantidad);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.BOPP.CatMP_Id == categoria
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2).Sum(asg => asg.BOPP.BOPP_CantidadInicialKg);

            //Asignacion de Tinta
            var conAsgTinta = _context.DetalleAsignaciones_Tintas
                .Where(asg => asg.Tinta_Id == id
                        && asg.Tinta.CatMP_Id == categoria
                        && asg.AsigMp.AsigMp_FechaEntrega >= fecha1
                        && asg.AsigMp.AsigMp_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigTinta_Cantidad);

            //Suma Entradas
            var entrada = conDevoluciones + conFacturas + conRemisiones + conRecuperado;

            //Materia Prima
            var con = (from mp in _context.Set<Materia_Prima>()
                               from Inv in _context.Set<InventarioInicialXDia_MatPrima>()
                               where mp.MatPri_Id == id
                                     && mp.CatMP_Id == categoria
                                     && Inv.MatPri_Id == mp.MatPri_Id
                               group mp by new
                               {
                                   mp.MatPri_Id,
                                   mp.MatPri_Nombre,
                                   Inv.InvInicial_Stock,
                                   mp.MatPri_Stock,
                                   mp.UndMed_Id,
                                   mp.MatPri_Precio,
                                   mp.CatMP.CatMP_Nombre
                               } into x
                               select new
                               {
                                   Id = x.Key.MatPri_Id,
                                   Nombre = x.Key.MatPri_Nombre,
                                   Ancho = Convert.ToDouble(0.00),
                                   Inicial = x.Key.InvInicial_Stock,
                                   Entrada = Convert.ToDouble(entrada),
                                   Salida = conAsg,
                                   Stock = x.Key.MatPri_Stock,
                                   Diferencia = x.Key.MatPri_Stock - x.Key.InvInicial_Stock,
                                   Presentacion = x.Key.UndMed_Id,
                                   Precio = x.Key.MatPri_Precio,
                                   SubTotal = x.Key.MatPri_Stock * x.Key.MatPri_Precio,
                                   Categoria = x.Key.CatMP_Nombre,
                               });

            //Entrada de BOPP
            var conBopp = (from bopp in _context.Set<BOPP>()
                           where bopp.BOPP_Serial == id 
                                 && bopp.CatMP_Id == categoria
                                 && bopp.BOPP_FechaIngreso >= fecha1
                                 && bopp.BOPP_FechaIngreso <= fecha2
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(bopp.BOPP_CantidadInicialKg),
                               Salida = conAsgBopp,
                               Stock = bopp.BOPP_Stock,
                               Diferencia = bopp.BOPP_Stock - bopp.BOPP_CantidadInicialKg,
                               Presentacion = bopp.UndMed_Id,
                               Precio = bopp.BOPP_Precio,
                               SubTotal = bopp.BOPP_Stock * bopp.BOPP_Precio,
                               Categoria = bopp.CatMP.CatMP_Nombre,
                           });

            //Tinta
            var conTinta = (from tinta in _context.Set<Tinta>()
                            where tinta.Tinta_Id == id 
                                  && tinta.CatMP_Id == categoria
                            select new
                            {
                                Id = tinta.Tinta_Id,
                                Nombre = tinta.Tinta_Nombre,
                                Ancho = Convert.ToDouble(0.00),
                                Inicial = tinta.Tinta_InvInicial,
                                Entrada = Convert.ToDouble(tinta.Tinta_InvInicial),
                                Salida = conAsgTinta,
                                Stock = tinta.Tinta_Stock,
                                Diferencia = tinta.Tinta_Stock - tinta.Tinta_InvInicial,
                                Presentacion = tinta.UndMed_Id,
                                Precio = tinta.Tinta_Precio,
                                SubTotal = tinta.Tinta_Stock * tinta.Tinta_Precio,
                                Categoria = tinta.CatMP.CatMP_Nombre,
                            });

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con.Concat(conBopp).Concat(conTinta));
        }

        //
        [HttpGet("ConsultaInventario2/{fecha1}/{id}")]
        public ActionResult Get(DateTime fecha1, long id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = _context.DetallesAsignaciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.AsigMp.AsigMp_FechaEntrega == fecha1).Sum(asg => asg.DtAsigMp_Cantidad);

            //Devoluciones de Materia Prima
            var conDevoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.DevMatPri.DevMatPri_Fecha == fecha1).Sum(asg => asg.DtDevMatPri_CantidadDevuelta);

            //Facturas de Materia Prima
            var conFacturas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Facco.Facco_FechaFactura == fecha1).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Materia Prima
            var conRemisiones = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Rem.Rem_Fecha == fecha1).Sum(asg => asg.RemiMatPri_Cantidad);

            //Recuperado de Materia Prima
            var conRecuperado = _context.DetallesRecuperados_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.RecMp.RecMp_FechaIngreso == fecha1).Sum(asg => asg.RecMatPri_Cantidad);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega == fecha1).Sum(asg => asg.BOPP.BOPP_CantidadInicialKg);

            //Asignacion de Tinta
            var conAsgTinta = _context.DetalleAsignaciones_Tintas
                .Where(asg => asg.Tinta_Id == id
                        && asg.AsigMp.AsigMp_FechaEntrega == fecha1).Sum(asg => asg.DtAsigTinta_Cantidad);

            //Suma Entradas
            var entrada = conDevoluciones + conFacturas + conRemisiones + conRecuperado;

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
                           mp.CatMP.CatMP_Nombre
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Ancho = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = conAsg,
                           Stock = x.Key.MatPri_Stock,
                           Diferencia = x.Key.MatPri_Stock - x.Key.InvInicial_Stock,
                           Presentacion = x.Key.UndMed_Id,
                           Precio = x.Key.MatPri_Precio,
                           SubTotal = x.Key.MatPri_Stock * x.Key.MatPri_Precio,
                           Categoria = x.Key.CatMP_Nombre,
                       });

            //Entrada de BOPP
            var conBopp = (from bopp in _context.Set<BOPP>()
                           where bopp.BOPP_Serial == id
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(bopp.BOPP_CantidadInicialKg),
                               Salida = conAsgBopp,
                               Stock = bopp.BOPP_Stock,
                               Diferencia = bopp.BOPP_Stock - bopp.BOPP_CantidadInicialKg,
                               Presentacion = bopp.UndMed_Id,
                               Precio = bopp.BOPP_Precio,
                               SubTotal = bopp.BOPP_Stock * bopp.BOPP_Precio,
                               Categoria = bopp.CatMP.CatMP_Nombre,
                           });

            //Tinta
            var conTinta = (from tinta in _context.Set<Tinta>()
                            where tinta.Tinta_Id == id
                            select new
                            {
                                Id = tinta.Tinta_Id,
                                Nombre = tinta.Tinta_Nombre,
                                Ancho = Convert.ToDouble(0.00),
                                Inicial = tinta.Tinta_InvInicial,
                                Entrada = Convert.ToDouble(tinta.Tinta_InvInicial),
                                Salida = conAsgTinta,
                                Stock = tinta.Tinta_Stock,
                                Diferencia = tinta.Tinta_Stock - tinta.Tinta_InvInicial,
                                Presentacion = tinta.UndMed_Id,
                                Precio = tinta.Tinta_Precio,
                                SubTotal = tinta.Tinta_Stock * tinta.Tinta_Precio,
                                Categoria = tinta.CatMP.CatMP_Nombre,
                            });

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con.Concat(conBopp).Concat(conTinta));
        }

        //
        [HttpGet("ConsultaInventario3/{fecha1}/{id}/{categoria}")]
        public ActionResult Get(DateTime fecha1, long id, long categoria)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = _context.DetallesAsignaciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.AsigMp.AsigMp_FechaEntrega == fecha1).Sum(asg => asg.DtAsigMp_Cantidad);

            //Devoluciones de Materia Prima
            var conDevoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.DevMatPri.DevMatPri_Fecha == fecha1).Sum(asg => asg.DtDevMatPri_CantidadDevuelta);

            //Facturas de Materia Prima
            var conFacturas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.Facco.Facco_FechaFactura == fecha1).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Materia Prima
            var conRemisiones = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.Rem.Rem_Fecha == fecha1).Sum(asg => asg.RemiMatPri_Cantidad);

            //Recuperado de Materia Prima
            var conRecuperado = _context.DetallesRecuperados_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.MatPri.CatMP_Id == categoria
                       && asg.RecMp.RecMp_FechaIngreso == fecha1).Sum(asg => asg.RecMatPri_Cantidad);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.BOPP.CatMP_Id == categoria
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega == fecha1).Sum(asg => asg.BOPP.BOPP_CantidadInicialKg);

            //Asignacion de Tinta
            var conAsgTinta = _context.DetalleAsignaciones_Tintas
                .Where(asg => asg.Tinta_Id == id
                        && asg.Tinta.CatMP_Id == categoria
                        && asg.AsigMp.AsigMp_FechaEntrega == fecha1).Sum(asg => asg.DtAsigTinta_Cantidad);

            //Suma Entradas
            var entrada = conDevoluciones + conFacturas + conRemisiones + conRecuperado;

            //Materia Prima
            var con = (from mp in _context.Set<Materia_Prima>()
                       from Inv in _context.Set<InventarioInicialXDia_MatPrima>()
                       where mp.MatPri_Id == id
                             && mp.CatMP_Id == categoria
                             && Inv.MatPri_Id == mp.MatPri_Id
                       group mp by new
                       {
                           mp.MatPri_Id,
                           mp.MatPri_Nombre,
                           Inv.InvInicial_Stock,
                           mp.MatPri_Stock,
                           mp.UndMed_Id,
                           mp.MatPri_Precio,
                           mp.CatMP.CatMP_Nombre
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Ancho = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = conAsg,
                           Stock = x.Key.MatPri_Stock,
                           Diferencia = x.Key.MatPri_Stock - x.Key.InvInicial_Stock,
                           Presentacion = x.Key.UndMed_Id,
                           Precio = x.Key.MatPri_Precio,
                           SubTotal = x.Key.MatPri_Stock * x.Key.MatPri_Precio,
                           Categoria = x.Key.CatMP_Nombre,
                       });

            //Entrada de BOPP
            var conBopp = (from bopp in _context.Set<BOPP>()
                           where bopp.BOPP_Serial == id
                                 && bopp.CatMP_Id == categoria
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(bopp.BOPP_CantidadInicialKg),
                               Salida = conAsgBopp,
                               Stock = bopp.BOPP_Stock,
                               Diferencia = bopp.BOPP_Stock - bopp.BOPP_CantidadInicialKg,
                               Presentacion = bopp.UndMed_Id,
                               Precio = bopp.BOPP_Precio,
                               SubTotal = bopp.BOPP_Stock * bopp.BOPP_Precio,
                               Categoria = bopp.CatMP.CatMP_Nombre,
                           });

            //Tinta
            var conTinta = (from tinta in _context.Set<Tinta>()
                            where tinta.Tinta_Id == id
                                  && tinta.CatMP_Id == categoria
                            select new
                            {
                                Id = tinta.Tinta_Id,
                                Nombre = tinta.Tinta_Nombre,
                                Ancho = Convert.ToDouble(0.00),
                                Inicial = tinta.Tinta_InvInicial,
                                Entrada = Convert.ToDouble(tinta.Tinta_InvInicial),
                                Salida = conAsgTinta,
                                Stock = tinta.Tinta_Stock,
                                Diferencia = tinta.Tinta_Stock - tinta.Tinta_InvInicial,
                                Presentacion = tinta.UndMed_Id,
                                Precio = tinta.Tinta_Precio,
                                SubTotal = tinta.Tinta_Stock * tinta.Tinta_Precio,
                                Categoria = tinta.CatMP.CatMP_Nombre,
                            });

#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con.Concat(conBopp).Concat(conTinta));
        }

        [HttpGet("DatosMatPrimaxId/{Id}")]
        public ActionResult GetDatosMatPrixId(long Id, DateTime date)
        {

            //DateTime dt = new DateTime();
            //DateTime date = dt.Date;

            //var queryInvInicial = _context.InventarioInicialXDias_MatPrima.Where(mp => mp.MatPri_Id == Id).Select(inv => new { inv.InvInicial_Stock });
            var asig_MatPrima = (_context.DetallesAsignaciones_MateriasPrimas.
                                Where(mp => mp.MatPri_Id == Id
                                && mp.AsigMp.AsigMp_FechaEntrega == date));

           /** var Recu_MatPrima = (_context..
                                Where(mp => mp.MatPri_Id == Id
                                && mp.AsigMp.AsigMp_FechaEntrega == date)); */


            var matPrima = (from mp in _context.Set<Materia_Prima>()
                         from invIni in _context.Set<InventarioInicialXDia_MatPrima>()
                         from asmp in _context.Set<Asignacion_MatPrima>()
                         from dasmp in _context.Set<DetalleAsignacion_MateriaPrima>()
                         where mp.MatPri_Id == Id
                         && mp.MatPri_Id == invIni.MatPri_Id
                         && dasmp.MatPri_Id == mp.MatPri_Id
                         && dasmp.AsigMp_Id == asmp.AsigMp_Id
                         && asmp.AsigMp_FechaEntrega == date
                         group dasmp by new
                         {
                             mp.MatPri_Id,
                             mp.MatPri_Nombre,
                             invIni.InvInicial_Stock,
                             mp.MatPri_Stock,
                             mp.UndMed_Id,
                             mp.MatPri_Precio,
                             mp.CatMP.CatMP_Nombre,                            
                         } into ASG 
                           select new
                         {
                             ID = ASG.Key.MatPri_Id,
                             Nombre = ASG.Key.MatPri_Nombre,
                             Stock_Inicial = ASG.Key.InvInicial_Stock,
                             Cantidad_Asignada = ASG.Sum(dta => dta.DtAsigMp_Cantidad),
                             Stock = ASG.Key.MatPri_Stock,
                             Medida = ASG.Key.UndMed_Id,
                             Precio = ASG.Key.MatPri_Precio,
                             Subtotal = ASG.Key.MatPri_Stock * ASG.Key.MatPri_Precio,
                             Categoria = ASG.Key.CatMP_Nombre                             
                         });

            var tinta = (from tnt in _context.Set<Tinta>()
                         from asmp in _context.Set<Asignacion_MatPrima>()
                         from dastnt in _context.Set<DetalleAsignacion_Tinta>()
                         from ent_tinta in _context.Set<Entrada_Tintas>()
                         from detent_tinta in _context.Set<Detalles_EntradaTintas>()
                         where tnt.Tinta_Id == Id
                         && dastnt.Tinta_Id == tnt.Tinta_Id
                         && dastnt.AsigMp_Id == asmp.AsigMp_Id
                         && ent_tinta.EntTinta_Id == detent_tinta.EntTinta_Id
                         && detent_tinta.Tinta_Id == tnt.Tinta_Id
                         && ent_tinta.entTinta_FechaEntrada == date
                         group dastnt by new
                         {
                             tnt.Tinta_Id,
                             tnt.Tinta_Nombre,
                             tnt.Tinta_InvInicial,
                             tnt.Tinta_Stock,
                             tnt.UndMed_Id,
                             tnt.Tinta_Precio,
                             tnt.CatMP.CatMP_Nombre,
                         } into ASG
                         select new
                         {
                             ID = ASG.Key.Tinta_Id,
                             Nombre = ASG.Key.Tinta_Nombre,
                             Stock_Inicial = ASG.Key.Tinta_InvInicial,
                             Cantidad_Asignada = ASG.Sum(dta => dta.DtAsigTinta_Cantidad),
                             Stock = ASG.Key.Tinta_Stock,
                             Medida = ASG.Key.UndMed_Id,
                             Precio = ASG.Key.Tinta_Precio,
                             Subtotal = ASG.Key.Tinta_Stock * ASG.Key.Tinta_Precio,
                             Categoria = ASG.Key.CatMP_Nombre,
                             
                         });

            var BOPP = (from bopp in _context.Set<BOPP>()
                        from asb in _context.Set<Asignacion_BOPP>()
                        from dasb in _context.Set<DetalleAsignacion_BOPP>()
                        where bopp.BOPP_Id == Id
                        && dasb.BOPP_Id == bopp.BOPP_Id
                        && dasb.AsigBOPP_Id == asb.AsigBOPP_Id
                        && asb.AsigBOPP_FechaEntrega == date
                        group dasb by new
                        {
                            bopp.BOPP_Id,
                            bopp.BOPP_Nombre,
                            bopp.BOPP_CantidadInicialKg,
                            bopp.BOPP_Stock,
                            bopp.UndMed_Id,
                            bopp.BOPP_Precio,
                            bopp.CatMP.CatMP_Nombre,
                        } into ASG
                        select new
                        {
                            ID = ASG.Key.BOPP_Id,
                            Nombre = ASG.Key.BOPP_Nombre,
                            Stock_Inicial = ASG.Key.BOPP_CantidadInicialKg,
                            Cantidad_Asignada = ASG.Sum(dta => dta.DtAsigBOPP_Cantidad),
                            Stock = ASG.Key.BOPP_Stock,
                            Medida = ASG.Key.UndMed_Id,
                            Precio = ASG.Key.BOPP_Precio,
                            Subtotal = ASG.Key.BOPP_Stock * ASG.Key.BOPP_Precio,
                            Categoria = ASG.Key.CatMP_Nombre,
                        });

            var Query = matPrima.Concat(tinta).Concat(BOPP);

            return Ok(Query);
        }

        // GET: api/Materia_Prima/5
        [HttpGet("categoria/{CatMP_Id}")]
        public ActionResult<Materia_Prima> Getcaegoria(long CatMP_Id)
        {
            var materia_Prima = _context.Materias_Primas.Where(mp => mp.CatMP_Id == CatMP_Id).ToList();

            if (materia_Prima == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(materia_Prima);
            }
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
