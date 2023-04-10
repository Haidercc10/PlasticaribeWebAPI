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


        // GET: api/Materia_Prima/5
        [HttpGet("getMaximoIdMatPrima")]
        public  ActionResult GetMaxMateria_Prima()
        {           
            var materia_Prima =  _context.Materias_Primas.Select(m => m.MatPri_Id).Max();
            return Ok(materia_Prima);
        }

        //
        [HttpGet("ConsultaInventario1/{fecha1}/{fecha2}/{id}/{categoria}")]
        public ActionResult Get(DateTime fecha1, DateTime fecha2, long id, long categoria)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            //Asignaciones de Materia Prima
            var conAsg = _context.DetallesAsignaciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.AsigMp.AsigMp_FechaEntrega >= fecha1
                       && asg.AsigMp.AsigMp_FechaEntrega <= fecha2
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.DtAsigMp_Cantidad);

            //Asignacion de Materia Prima para la creacion de Tintas
            var conAsgMPCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.MatPri_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

            //Devoluciones de Materia Prima
            var conDevoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.DevMatPri.DevMatPri_Fecha >= fecha1
                       && asg.DevMatPri.DevMatPri_Fecha <= fecha2
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.DtDevMatPri_CantidadDevuelta);

            //Facturas de Materia Prima
            var conFacturas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Facco.Facco_FechaFactura >= fecha1
                       && asg.Facco.Facco_FechaFactura <= fecha2
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Materia Prima
            var conRemisiones = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Rem.Rem_Fecha >= fecha1
                       && asg.Rem.Rem_Fecha <= fecha2
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.RemiMatPri_Cantidad);

            //Recuperado de Materia Prima
            var conRecuperado = _context.DetallesRecuperados_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.RecMp.RecMp_FechaIngreso >= fecha1
                       && asg.RecMp.RecMp_FechaIngreso <= fecha2
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.RecMatPri_Cantidad);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2
                       && asg.BOPP.CatMP_Id == categoria).Sum(asg => asg.DtAsigBOPP_Cantidad);

            //Entrada de BOPP
            var conEntradaBOPP = _context.BOPP
                .Where(x => x.BOPP_FechaIngreso >= fecha1
                       && x.BOPP_FechaIngreso <= fecha2
                       && x.BOPP_Serial == id
                       && x.CatMP_Id == categoria).Sum(x => x.BOPP_CantidadInicialKg);

            //Asignacion de Tinta
            var conAsgTinta = _context.DetalleAsignaciones_Tintas
                .Where(asg => asg.Tinta_Id == id
                        && asg.AsigMp.AsigMp_FechaEntrega >= fecha1
                        && asg.AsigMp.AsigMp_FechaEntrega <= fecha2
                        && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.DtAsigTinta_Cantidad);

            //Asignacion de Tintas para la creacion de Tintas
            var conAsgTintaCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega >= fecha1
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega <= fecha2
                       && asg.TintasDAMPxT.CatMP_Id == categoria).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

            //Facturas de Tintas
            var conFacturasTintas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Facco.Facco_FechaFactura >= fecha1
                       && asg.Facco.Facco_FechaFactura <= fecha2
                       && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Tintas
            var conRemisionesTintas = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Rem.Rem_Fecha >= fecha1
                       && asg.Rem.Rem_Fecha <= fecha2
                       && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.RemiMatPri_Cantidad);

            //Creacion de Tintas
            var conCreacionTintas = _context.Asignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta_FechaEntrega >= fecha1
                       && asg.AsigMPxTinta_FechaEntrega <= fecha2
                       && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.AsigMPxTinta_Cantidad);

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
                           mp.CatMP.CatMP_Nombre
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Ancho = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = salidaMateriaPrima,
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
                                 && (conAsgBopp > 0 || bopp.BOPP_Stock > 0)
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(conEntradaBOPP),
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
                                Entrada = Convert.ToDouble(entradaTintas),
                                Salida = salidaTintas,
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

            //Asignacion de Materia Prima para la creacion de Tintas
            var conAsgMPCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.MatPri_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega == fecha1).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

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

            //Entrada de BOPP
            var conEntradaBOPP = _context.BOPP
                .Where(x => x.BOPP_FechaIngreso == fecha1
                       && x.BOPP_Serial == id).Sum(x => x.BOPP_CantidadInicialKg);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega == fecha1).Sum(asg => asg.DtAsigBOPP_Cantidad);

            //Asignacion de Tinta
            var conAsgTinta = _context.DetalleAsignaciones_Tintas
                .Where(asg => asg.Tinta_Id == id
                        && asg.AsigMp.AsigMp_FechaEntrega == fecha1).Sum(asg => asg.DtAsigTinta_Cantidad);

            //Asignacion de Tintas para la creacion de Tintas
            var conAsgTintaCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega == fecha1).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

            //Facturas de Tintas
            var conFacturasTintas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Facco.Facco_FechaFactura == fecha1).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Tintas
            var conRemisionesTintas = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Rem.Rem_Fecha == fecha1).Sum(asg => asg.RemiMatPri_Cantidad);

            //Creacion de Tintas
            var conCreacionTintas = _context.Asignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta_FechaEntrega == fecha1).Sum(asg => asg.AsigMPxTinta_Cantidad);

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
                           mp.CatMP.CatMP_Nombre
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Ancho = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = salidaMateriaPrima,
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
                                 && (conAsgBopp > 0 || bopp.BOPP_Stock > 0)
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(conEntradaBOPP),
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
                                Entrada = Convert.ToDouble(entradaTintas),
                                Salida = salidaTintas,
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
                       && asg.AsigMp.AsigMp_FechaEntrega == fecha1
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.DtAsigMp_Cantidad);

            //Asignacion de Materia Prima para la creacion de Tintas
            var conAsgMPCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.MatPri_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega == fecha1
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

            //Devoluciones de Materia Prima
            var conDevoluciones = _context.DetallesDevoluciones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.DevMatPri.DevMatPri_Fecha == fecha1
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.DtDevMatPri_CantidadDevuelta);

            //Facturas de Materia Prima
            var conFacturas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Facco.Facco_FechaFactura == fecha1
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Materia Prima
            var conRemisiones = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.Rem.Rem_Fecha == fecha1
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.RemiMatPri_Cantidad);

            //Recuperado de Materia Prima
            var conRecuperado = _context.DetallesRecuperados_MateriasPrimas
                .Where(asg => asg.MatPri_Id == id
                       && asg.RecMp.RecMp_FechaIngreso == fecha1
                       && asg.MatPri.CatMP_Id == categoria).Sum(asg => asg.RecMatPri_Cantidad);

            //Entrada de BOPP
            var conEntradaBOPP = _context.BOPP
                .Where(x => x.BOPP_FechaIngreso == fecha1
                       && x.CatMP_Id == categoria
                       && x.BOPP_Serial == id).Sum(x => x.BOPP_CantidadInicialKg);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega == fecha1
                       && asg.BOPP.CatMP_Id == categoria).Sum(asg => asg.DtAsigBOPP_Cantidad);

            //Asignacion de Tinta
            var conAsgTinta = _context.DetalleAsignaciones_Tintas
                .Where(asg => asg.Tinta_Id == id
                        && asg.AsigMp.AsigMp_FechaEntrega == fecha1
                        && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.DtAsigTinta_Cantidad);

            //Asignacion de Tintas para la creacion de Tintas
            var conAsgTintaCreacionTintas = _context.DetallesAsignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta.AsigMPxTinta_FechaEntrega == fecha1
                       && asg.TintasDAMPxT.CatMP_Id == categoria).Sum(asg => asg.DetAsigMPxTinta_Cantidad);

            //Facturas de Tintas
            var conFacturasTintas = _context.FacturasCompras_MateriaPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Facco.Facco_FechaFactura == fecha1
                       && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.FaccoMatPri_Cantidad);

            //Remisiones de Tintas
            var conRemisionesTintas = _context.Remisiones_MateriasPrimas
                .Where(asg => asg.Tinta_Id == id
                       && asg.Rem.Rem_Fecha == fecha1
                       && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.RemiMatPri_Cantidad);

            //Creacion de Tintas
            var conCreacionTintas = _context.Asignaciones_MatPrimasXTintas
                .Where(asg => asg.Tinta_Id == id
                       && asg.AsigMPxTinta_FechaEntrega == fecha1
                       && asg.Tinta.CatMP_Id == categoria).Sum(asg => asg.AsigMPxTinta_Cantidad);

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
                           mp.CatMP.CatMP_Nombre
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Ancho = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = salidaMateriaPrima,
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
                                 && (conAsgBopp > 0 || bopp.BOPP_Stock > 0)
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(conEntradaBOPP),
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
                                Entrada = Convert.ToDouble(entradaTintas),
                                Salida = salidaTintas,
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
        [HttpGet("GetConsultaMateriaPrimaFI/{fecha1}/{fecha2}/{id}")]
        public ActionResult GetConsultaMateriaPrimaFIC(DateTime fecha1, DateTime fecha2, long id)
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

            //Entrada de BOPP
            var conEntradaBOPP = _context.BOPP
                .Where(x => x.BOPP_FechaIngreso >= fecha1
                       && x.BOPP_FechaIngreso <= fecha2
                       && x.BOPP_Serial == id).Sum(x => x.BOPP_CantidadInicialKg);

            //Asignaciones de BOPP
            var conAsgBopp = _context.DetallesAsignaciones_BOPP
                .Where(asg => asg.BOPP.BOPP_Serial == id
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega >= fecha1
                       && asg.AsigBOPP.AsigBOPP_FechaEntrega <= fecha2).Sum(asg => asg.DtAsigBOPP_Cantidad);

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
                           mp.CatMP.CatMP_Nombre
                       } into x
                       select new
                       {
                           Id = x.Key.MatPri_Id,
                           Nombre = x.Key.MatPri_Nombre,
                           Ancho = Convert.ToDouble(0.00),
                           Inicial = x.Key.InvInicial_Stock,
                           Entrada = Convert.ToDouble(entrada),
                           Salida = salidaMateriaPrima,
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
                                 && (conAsgBopp > 0 || bopp.BOPP_Stock > 0)
                           select new
                           {
                               Id = bopp.BOPP_Serial,
                               Nombre = bopp.BOPP_Nombre,
                               Ancho = Convert.ToDouble(bopp.BOPP_Ancho),
                               Inicial = bopp.BOPP_CantidadInicialKg,
                               Entrada = Convert.ToDouble(conEntradaBOPP),
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
                                Entrada = Convert.ToDouble(entradaTintas),
                                Salida = salidaTintas,
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
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var asig_MatPrima = (_context.DetallesAsignaciones_MateriasPrimas.
                                Where(mp => mp.MatPri_Id == Id
                                && mp.AsigMp.AsigMp_FechaEntrega == date));
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            /** var Recu_MatPrima = (_context..
                                 Where(mp => mp.MatPri_Id == Id
                                 && mp.AsigMp.AsigMp_FechaEntrega == date)); */


#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
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
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
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
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
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
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

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

            var bopp = from bp in _context.Set<BOPP>()
                       where bp.BOPP_Serial == id
                       select new
                       {
                           Id = bp.BOPP_Serial,
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

            return Ok(materiaPrima.Concat(tinta).Concat(bopp));
        }

        [HttpGet("getMatPrimasYTintas")]
        public ActionResult getMpTinta()
        {
            var materiaPrima = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id != 84
                               select new
                               {
                                   Id = mp.MatPri_Id,
                                   Nombre = mp.MatPri_Nombre,
                                   Medida = mp.UndMed_Id

                               };

            var tinta = from tt in _context.Set<Tinta>()
                        where tt.Tinta_Id != 2001 
                        select new
                        {
                            Id = tt.Tinta_Id,
                            Nombre = tt.Tinta_Nombre,
                            Medida = tt.UndMed_Id
                        };

            return Ok(materiaPrima.Concat(tinta));
        }

        [HttpGet("getMatPrimasYTintasxId/{Ident}")]
        public ActionResult getMpTintaxId(long Ident)
        {
            var materiaPrima = from mp in _context.Set<Materia_Prima>()
                               where mp.MatPri_Id != 84 && mp.MatPri_Id == Ident
                               select new
                               {
                                   Id = mp.MatPri_Id,
                                   Nombre = mp.MatPri_Nombre,
                                   Medida = mp.UndMed_Id
                               };

            var tinta = from tt in _context.Set<Tinta>()
                        where tt.Tinta_Id != 2001 && tt.Tinta_Id == Ident
                        select new
                        {
                            Id = tt.Tinta_Id,
                            Nombre = tt.Tinta_Nombre,
                            Medida = tt.UndMed_Id
                        };

            return Ok(materiaPrima.Concat(tinta));
        }

        [HttpGet("GetMateriaPrima_LikeNombre/{nombre}")]
        public ActionResult GetMateriaPrima_LikeNombre(string nombre)
        {
            var mp = from matPri in _context.Set<Materia_Prima>()
                     where matPri.MatPri_Nombre.StartsWith(nombre)
                           || matPri.MatPri_Nombre.EndsWith(nombre)
                     select new
                     {
                         Id = matPri.MatPri_Id, 
                         Nombre = matPri.MatPri_Nombre,
                     };
            var tinta = from t in _context.Set<Tinta>()
                        where t.Tinta_Nombre.StartsWith(nombre)
                              || t.Tinta_Nombre.EndsWith(nombre)
                        select new
                        {
                            Id = t.Tinta_Id,
                            Nombre = t.Tinta_Nombre,
                        };
            var bopp = from bp in _context.Set<BOPP>()
                       where bp.BOPP_Nombre.StartsWith(nombre)
                             || bp.BOPP_Nombre.EndsWith(nombre)
                       select new
                       {
                           Id = bp.BOPP_Id,
                           Nombre = bp.BOPP_Nombre,
                       };

            return Ok(mp.Concat(tinta).Concat(bopp));
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
