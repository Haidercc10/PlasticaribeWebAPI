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
    public class Orden_TrabajoController : ControllerBase
    {
        private readonly dataContext _context;

        public Orden_TrabajoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Orden_Trabajo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden_Trabajo>>> GetOrden_Trabajo()
        {
          if (_context.Orden_Trabajo == null)
          {
              return NotFound();
          }
            return await _context.Orden_Trabajo.ToListAsync();
        }

        // GET: api/Orden_Trabajo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden_Trabajo>> GetOrden_Trabajo(long id)
        {
          if (_context.Orden_Trabajo == null)
          {
              return NotFound();
          }
            var orden_Trabajo = await _context.Orden_Trabajo.FindAsync(id);

            if (orden_Trabajo == null)
            {
                return NotFound();
            }

            return orden_Trabajo;
        }

        [HttpGet("NumeroOt/{Ot_Id}")]
        public ActionResult<Orden_Trabajo> GetNumeroOt(long Ot_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var orden_trabajo = _context.Orden_Trabajo
                .Where(ot => ot.Ot_Id == Ot_Id)
                .Include(ot => ot.PedidoExterno)
                .Include(ot => ot.SedeCli)
                .Include(ot => ot.SedeCli.Cli)
                .Include(ot => ot.Estado)
                .Select(ot => new
                {
                    ot.SedeCli.Cli_Id,
                    ot.SedeCli.Cli.Cli_Nombre,
                    ot.PedExt_Id,
                    ot.PedidoExterno.Usua.Usua_Nombre,
                    ot.Ot_FechaCreacion,
                    ot.PedidoExterno.PedExt_FechaEntrega,
                    ot.SedeCli.SedeCliente_Ciudad,
                    ot.SedeCli.SedeCliente_Direccion,
                    ot.Estado.Estado_Nombre,
                    ot.Ot_Observacion,
                    ot.Ot_MargenAdicional,
                    ot.Ot_Cyrel,
                    ot.Ot_Corte,
                    ot.Prod_Id,
                    ot.Ot_CantidadKilos
                })
                .ToList();
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            return Ok(orden_trabajo);
        }

        [HttpGet("NumeroPedido/{PedExt_Id}")]
        public ActionResult<Orden_Trabajo> GetNumeroPedido(long PedExt_Id)
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var orden_trabajo = _context.Orden_Trabajo
                .Where(ot => ot.PedExt_Id == PedExt_Id)
                .ToList();
#pragma warning restore CS8604 // Posible argumento de referencia nulo

            return Ok(orden_trabajo);
        }

        [HttpGet("PdfOTInsertada/{Ot_Id}")]
        public ActionResult GetOTInsertada(long Ot_Id)
        {
            var con = from ot in _context.Set<Orden_Trabajo>()
                      from otExt in _context.Set<OT_Extrusion>()
                      from otImp in _context.Set<OT_Impresion>()
                      from otLam in _context.Set<OT_Laminado>()
                      where ot.Ot_Id == Ot_Id
                            && otExt.Ot_Id == Ot_Id
                            && otImp.Ot_Id == Ot_Id
                            && otLam.OT_Id == Ot_Id
                      select new
                      {
                          //Informacion General de la OT
                          ot.Ot_Id,
                          ot.SedeCli_Id,
                          ot.SedeCli.Cli_Id,
                          ot.SedeCli.Cli.Cli_Nombre,
                          ot.Prod_Id,
                          ot.Prod.Prod_Nombre,
                          Presentacion_Id = ot.UndMed_Id,
                          Presentacion_Nombre = ot.Unidad_Medida.UndMed_Nombre,
                          ot.Ot_CantidadKilos,
                          ot.Ot_CantidadUnidades,
                          ot.Ot_MargenAdicional,
                          ot.Ot_CantidadKilos_Margen,
                          ot.Ot_CantidadUnidades_Margen,
                          ot.Ot_FechaCreacion,
                          ot.Estado_Id,
                          ot.Estado.Estado_Nombre,
                          ot.PedExt_Id,
                          ot.PedidoExterno.PedExt_FechaEntrega,
                          ot.Ot_Observacion,

                          //Informacion de extrusion de la OT
                          otExt.Material_Id,
                          otExt.Material_MatPrima.Material_Nombre,
                          otExt.Pigmt_Id,
                          otExt.Pigmento.Pigmt_Nombre,
                          otExt.Formato_Id,
                          otExt.Formato.Formato_Nombre,
                          otExt.Tratado_Id,
                          otExt.Tratado.Tratado_Nombre,
                          otExt.Extrusion_Calibre,
                          otExt.UndMed_Id,
                          otExt.Extrusion_Ancho1,
                          otExt.Extrusion_Ancho2,
                          otExt.Extrusion_Ancho3,

                          //Informacion de impresion de la OT
                          otImp.TpImpresion_Id,
                          otImp.Tipos_Impresion.TpImpresion_Nombre,
                          otImp.Rodillo_Id,
                          otImp.Pista_Id,
                          otImp.Tinta1_Id,
                          Tinta1_Nombre = otImp.Tinta1.Tinta_Nombre,
                          otImp.Tinta2_Id,
                          Tinta2_Nombre = otImp.Tinta2.Tinta_Nombre,
                          otImp.Tinta3_Id,
                          Tinta3_Nombre = otImp.Tinta3.Tinta_Nombre,
                          otImp.Tinta4_Id,
                          Tinta4_Nombre = otImp.Tinta4.Tinta_Nombre,
                          otImp.Tinta5_Id,
                          Tinta5_Nombre = otImp.Tinta5.Tinta_Nombre,
                          otImp.Tinta6_Id,
                          Tinta6_Nombre = otImp.Tinta6.Tinta_Nombre,
                          otImp.Tinta7_Id,
                          Tinta7_Nombre = otImp.Tinta7.Tinta_Nombre,
                          otImp.Tinta8_Id,
                          Tinta8_Nombre = otImp.Tinta8.Tinta_Nombre,

                          //Informacion de Laminado de la OT
                          otLam.Capa_Id1,
                          lamCapa1_Nombre = otLam.Laminado_Capa.LamCapa_Nombre,
                          otLam.LamCapa_Calibre1,
                          otLam.LamCapa_Cantidad1,
                          otLam.Capa_Id2,
                          lamCapa2_Nombre = otLam.Laminado_Capa2.LamCapa_Nombre,
                          otLam.LamCapa_Calibre2,
                          otLam.LamCapa_Cantidad2,
                          otLam.Capa_Id3,
                          lamCapa3_Nombre = otLam.Laminado_Capa3.LamCapa_Nombre,
                          otLam.LamCapa_Calibre3,
                          otLam.LamCapa_Cantidad3,

                          //Informacion detallada del Producto
                          ot.Prod.TpProd_Id,
                          ot.Prod.TpProd.TpProd_Nombre,
                          ot.Prod.Prod_Ancho,
                          ot.Prod.Prod_Largo,
                          ot.Prod.Prod_Fuelle,
                          ot.Prod.UndMedACF,
                          ot.Prod.TpSellado_Id,
                          ot.Prod.TiposSellados.TpSellados_Nombre,
                          ot.Prod.Prod_Peso_Millar,
                          ot.Prod.Prod_CantBolsasPaquete,
                          ot.Prod.Prod_CantBolsasBulto,

                          //Informacion de las mezclas de la OT
                          ot.Mezcla_Id,
                          ot.Mezcla.Mezcla_Nombre,
                          ot.Mezcla.Mezcla_NroCapas,
                            // Informacion Capa 1
                          ot.Mezcla.Mezcla_PorcentajeCapa1,
                          ot.Mezcla.Mezcla_PorcentajeMaterial1_Capa1,
                          ot.Mezcla.MezMaterial_Id1xCapa1,
                          M1C1_nombre = ot.Mezcla.MezMaterial_MP1C1.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial2_Capa1,
                          ot.Mezcla.MezMaterial_Id2xCapa1,
                          M2C1_nombre = ot.Mezcla.MezMaterial_MP2C1.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial3_Capa1,
                          ot.Mezcla.MezMaterial_Id3xCapa1,
                          M3C1_nombre = ot.Mezcla.MezMaterial_MP3C1.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial4_Capa1,
                          ot.Mezcla.MezMaterial_Id4xCapa1,
                          M4C1_nombre = ot.Mezcla.MezMaterial_MP4C1.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajePigmto1_Capa1,
                          ot.Mezcla.MezPigmto_Id1xCapa1,
                          P1C1_Nombre = ot.Mezcla.MezPigmento1C1.MezPigmto_Nombre,

                          ot.Mezcla.Mezcla_PorcentajePigmto2_Capa1,
                          ot.Mezcla.MezPigmto_Id2xCapa1,
                          P2C1_Nombre = ot.Mezcla.MezPigmento2C1.MezPigmto_Nombre,
                            //Informacion Capa 2
                          ot.Mezcla.Mezcla_PorcentajeCapa2,
                          ot.Mezcla.Mezcla_PorcentajeMaterial1_Capa2,
                          ot.Mezcla.MezMaterial_Id1xCapa2,
                          M1C2_nombre = ot.Mezcla.MezMaterial_MP1C2.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial2_Capa2,
                          ot.Mezcla.MezMaterial_Id2xCapa2,
                          M2C2_nombre = ot.Mezcla.MezMaterial_MP2C2.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial3_Capa2,
                          ot.Mezcla.MezMaterial_Id3xCapa2,
                          M3C2_nombre = ot.Mezcla.MezMaterial_MP3C2.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial4_Capa2,
                          ot.Mezcla.MezMaterial_Id4xCapa2,
                          M4C2_nombre = ot.Mezcla.MezMaterial_MP4C2.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajePigmto1_Capa2,
                          ot.Mezcla.MezPigmto_Id1xCapa2,
                          P1C2_Nombre = ot.Mezcla.MezPigmento1C2.MezPigmto_Nombre,

                          ot.Mezcla.Mezcla_PorcentajePigmto2_Capa2,
                          ot.Mezcla.MezPigmto_Id2xCapa2,
                          P2C2_Nombre = ot.Mezcla.MezPigmento2C2.MezPigmto_Nombre,
                            //Informacion Capa 3
                          ot.Mezcla.Mezcla_PorcentajeCapa3,
                          ot.Mezcla.Mezcla_PorcentajeMaterial1_Capa3,
                          ot.Mezcla.MezMaterial_Id1xCapa3,
                          M1C3_nombre = ot.Mezcla.MezMaterial_MP1C3.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial2_Capa3,
                          ot.Mezcla.MezMaterial_Id2xCapa3,
                          M2C3_nombre = ot.Mezcla.MezMaterial_MP2C3.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial3_Capa3,
                          ot.Mezcla.MezMaterial_Id3xCapa3,
                          M3C3_nombre = ot.Mezcla.MezMaterial_MP3C3.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajeMaterial4_Capa3,
                          ot.Mezcla.MezMaterial_Id4xCapa3,
                          M4C3_nombre = ot.Mezcla.MezMaterial_MP4C3.MezMaterial_Nombre,

                          ot.Mezcla.Mezcla_PorcentajePigmto1_Capa3,
                          ot.Mezcla.MezPigmto_Id1xCapa3,
                          P1C3_Nombre = ot.Mezcla.MezPigmento1C3.MezPigmto_Nombre,

                          ot.Mezcla.Mezcla_PorcentajePigmto2_Capa3,
                          ot.Mezcla.MezPigmto_Id2xCapa3,
                          P2C3_Nombre = ot.Mezcla.MezPigmento2C3.MezPigmto_Nombre,


                      };

            return Ok(con);
        }

        // PUT: api/Orden_Trabajo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrden_Trabajo(long id, Orden_Trabajo orden_Trabajo)
        {
            if (id != orden_Trabajo.Ot_Id)
            {
                return BadRequest();
            }

            _context.Entry(orden_Trabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Orden_TrabajoExists(id))
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

        // POST: api/Orden_Trabajo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orden_Trabajo>> PostOrden_Trabajo(Orden_Trabajo orden_Trabajo)
        {
          if (_context.Orden_Trabajo == null)
          {
              return Problem("Entity set 'dataContext.Orden_Trabajo'  is null.");
          }
            _context.Orden_Trabajo.Add(orden_Trabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrden_Trabajo", new { id = orden_Trabajo.Ot_Id }, orden_Trabajo);
        }

        // DELETE: api/Orden_Trabajo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden_Trabajo(long id)
        {
            if (_context.Orden_Trabajo == null)
            {
                return NotFound();
            }
            var orden_Trabajo = await _context.Orden_Trabajo.FindAsync(id);
            if (orden_Trabajo == null)
            {
                return NotFound();
            }

            _context.Orden_Trabajo.Remove(orden_Trabajo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Orden_TrabajoExists(long id)
        {
            return (_context.Orden_Trabajo?.Any(e => e.Ot_Id == id)).GetValueOrDefault();
        }
    }
}
