﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Orden_MaquilaController : ControllerBase
    {
        private readonly dataContext _context;

        public Orden_MaquilaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Orden_Maquila
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden_Maquila>>> GetOrden_Maquila()
        {
            if (_context.Orden_Maquila == null)
            {
                return NotFound();
            }
            return await _context.Orden_Maquila.ToListAsync();
        }

        // GET: api/Orden_Maquila/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden_Maquila>> GetOrden_Maquila(long id)
        {
            if (_context.Orden_Maquila == null)
            {
                return NotFound();
            }
            var orden_Maquila = await _context.Orden_Maquila.FindAsync(id);

            if (orden_Maquila == null)
            {
                return NotFound();
            }

            return orden_Maquila;
        }

        //FUNCION QUE CONSULTARÁ Y RETORNARÁ EL ULTIMO ID CREADO 
        [HttpGet("GetUltimoId")]
        public ActionResult GetUltimoId()
        {
            var con = _context.Orden_Maquila.OrderBy(x => x.OM_Id).Select(x => x.OM_Id).Last();
            return Ok(con);
        }

        [HttpGet("getConsultaDocumentos/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetConsultaDocumento(DateTime fechaInicial, DateTime fechaFinal, string? doc = "", string? estado = "")
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var ordenes = from om in _context.Set<Orden_Maquila>()
                          where om.OM_Fecha >= fechaInicial
                                && om.OM_Fecha <= fechaFinal
                                && Convert.ToString(om.OM_Id).Contains(doc)
                                && Convert.ToString(om.Estado_Id).Contains(estado)
                          select new
                          {
                              Id = om.OM_Id,
                              Codigo = Convert.ToString(om.OM_Id),
                              Usuario = om.Usua.Usua_Nombre,
                              Fecha = om.OM_Fecha + " " + om.OM_Hora,
                              Tipo = om.TipoDoc.TpDoc_Nombre,
                              Tipo_Id = om.TpDoc_Id,
                              Estado = om.Estado.Estado_Nombre,
                          };

            var facturacion = from fac in _context.Set<Facturacion_OrdenMaquila>()
                              where fac.FacOM_Fecha >= fechaInicial
                                    && fac.FacOM_Fecha <= fechaFinal
                                    && Convert.ToString(fac.FacOM_Codigo).Contains(doc)
                                    && Convert.ToString(fac.Estado_Id).Contains(estado)
                              select new
                              {
                                  Id = fac.FacOM_Id,
                                  Codigo = fac.FacOM_Codigo,
                                  Usuario = fac.Usua.Usua_Nombre,
                                  Fecha = fac.FacOM_Fecha + " " + fac.FacOM_Hora,
                                  Tipo = fac.TipoDoc.TpDoc_Nombre,
                                  Tipo_Id = fac.TpDoc_Id,
                                  Estado = fac.Estado.Estado_Nombre,
                              };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            return Ok(ordenes.Concat(facturacion));
        }

        [HttpGet("getConsultaConsolidado/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetConsultaConsolidado(DateTime fechaInicial, DateTime fechaFinal, string? doc = "", string? estado = "", string? tercero = "")
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var facturacion = from fac in _context.Set<DetalleFacturacion_OrdenMaquila>()
                              where fac.FacOM.FacOM_Fecha.Year >= fechaInicial.Year
                                    && fac.FacOM.FacOM_Fecha.Year <= fechaFinal.Year
                                    && fac.FacOM.FacOM_Fecha.Month >= fechaInicial.Month
                                    && fac.FacOM.FacOM_Fecha.Month <= fechaFinal.Month
                                    && Convert.ToString(fac.FacOM.FacOM_Codigo).Contains(doc)
                                    && Convert.ToString(fac.FacOM.Estado_Id).Contains(estado)
                                    && Convert.ToString(fac.FacOM.Tercero_Id).Contains(tercero)
                              group fac by new
                              {
                                  Anio = fac.FacOM.FacOM_Fecha.Year,
                                  Mes = fac.FacOM.FacOM_Fecha.Month,
                                  Tipo = fac.FacOM.TipoDoc.TpDoc_Nombre,
                                  Tercero = fac.FacOM.Tercero.Tercero_Nombre,
                                  MateriaPrima_Id = fac.MatPri_Id,
                                  MateriaPrima = fac.MatPrima.MatPri_Nombre,
                                  Tinta_Id = fac.Tinta_Id,
                                  Tinta = fac.Tinta.Tinta_Nombre,
                                  Bopp_Id = fac.Bopp_Id,
                                  Bopp = fac.BOPP.BOPP_Nombre,
                                  Presentacion = fac.UndMed_Id,
                                  Precio = fac.DtFacOM_ValorUnitario,
                              } into fac
                              select new
                              {
                                  fac.Key.Anio,
                                  fac.Key.Mes,
                                  fac.Key.Tipo,
                                  fac.Key.Tercero,
                                  fac.Key.MateriaPrima_Id,
                                  fac.Key.MateriaPrima,
                                  fac.Key.Tinta_Id,
                                  fac.Key.Tinta,
                                  fac.Key.Bopp_Id,
                                  fac.Key.Bopp,
                                  fac.Key.Presentacion,
                                  fac.Key.Precio,
                                  Cantidad = fac.Sum(x => x.DtFacOM_Cantidad),
                                  SubTotal = fac.Sum(x => x.DtFacOM_Cantidad) * fac.Key.Precio,
                              };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            return Ok(facturacion);
        }

        // PUT: api/Orden_Maquila/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrden_Maquila(long id, Orden_Maquila orden_Maquila)
        {
            if (id != orden_Maquila.OM_Id)
            {
                return BadRequest();
            }

            _context.Entry(orden_Maquila).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Orden_MaquilaExists(id))
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

        // POST: api/Orden_Maquila
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orden_Maquila>> PostOrden_Maquila(Orden_Maquila orden_Maquila)
        {
            if (_context.Orden_Maquila == null)
            {
                return Problem("Entity set 'dataContext.Orden_Maquila'  is null.");
            }
            _context.Orden_Maquila.Add(orden_Maquila);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrden_Maquila", new { id = orden_Maquila.OM_Id }, orden_Maquila);
        }

        // DELETE: api/Orden_Maquila/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden_Maquila(long id)
        {
            if (_context.Orden_Maquila == null)
            {
                return NotFound();
            }
            var orden_Maquila = await _context.Orden_Maquila.FindAsync(id);
            if (orden_Maquila == null)
            {
                return NotFound();
            }

            _context.Orden_Maquila.Remove(orden_Maquila);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Orden_MaquilaExists(long id)
        {
            return (_context.Orden_Maquila?.Any(e => e.OM_Id == id)).GetValueOrDefault();
        }
    }
}
