using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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

        [HttpGet("getPdfOTInsertada/{Ot_Id}")]
        public ActionResult GetPdfOTInsertada(long Ot_Id)
        {
            var con = from ot in _context.Set<Orden_Trabajo>()
                      from otExt in _context.Set<OT_Extrusion>()
                      from otImp in _context.Set<OT_Impresion>()
                      from otLam in _context.Set<OT_Laminado>()
                      from otSelCor in _context.Set<OT_Sellado_Corte>()
                      from ped in _context.Set<PedidoProducto>()
                      where ot.Ot_Id == Ot_Id
                            && otExt.Ot_Id == Ot_Id
                            && otImp.Ot_Id == Ot_Id
                            && otLam.OT_Id == Ot_Id
                            && otSelCor.Ot_Id == Ot_Id
                            && ped.Prod_Id == ot.Prod_Id
                            && ped.UndMed_Id == ot.UndMed_Id
                            && ped.PedExt_Id == ot.PedExt_Id
                      select new
                      {
                          // Información de la OT
                          Numero_Orden = ot.Ot_Id,
                          Id_SedeCliente = ot.SedeCli_Id,
                          Id_Cliente = ot.SedeCli.Cli_Id,
                          Cliente = ot.SedeCli.Cli.Cli_Nombre,
                          Ciudad = ot.SedeCli.SedeCliente_Ciudad,
                          Direccion = ot.SedeCli.SedeCliente_Direccion,
                          Id_Producto = ot.Prod_Id,
                          Producto = ot.Prod.Prod_Nombre,
                          Id_Presentacion = ot.UndMed_Id,
                          Presentacion = ot.Unidad_Medida.UndMed_Nombre,
                          Margen = ot.Ot_MargenAdicional,
                          Fecha_Creacion = ot.Ot_FechaCreacion,
                          Fecha_Entrega = ot.PedidoExterno.PedExt_FechaEntrega,
                          Estado_Orden = ot.Estado_Id,
                          Estado = ot.Estado.Estado_Nombre,
                          Id_Pedido = ot.PedExt_Id,
                          Id_Vendedor = ot.PedidoExterno.Usua_Id,
                          Vendedor = ot.PedidoExterno.Usua.Usua_Nombre,
                          Observacion = ot.Ot_Observacion,
                          Cyrel = ot.Ot_Cyrel,
                          Extrusion = ot.Extrusion,
                          Impresion = ot.Impresion,
                          Rotograbado = ot.Rotograbado,
                          Laminado = ot.Laminado,
                          Corte = ot.Ot_Corte,
                          Sellado = ot.Sellado,
                          Cantidad_Pedida = ot.Ot_CantidadPedida,
                          Peso_Neto = ot.Ot_PesoNetoKg,
                          Precio_Producto = ped.PedExtProd_PrecioUnitario,

                          // Información de Extrusión
                          Id_Material = otExt.Material_Id,
                          Material = otExt.Material_MatPrima.Material_Nombre,
                          Id_Pigmento_Extrusion = otExt.Pigmt_Id,
                          Pigmento_Extrusion = otExt.Pigmento.Pigmt_Nombre,
                          Id_Formato_Extrusion = otExt.Formato_Id,
                          Formato_Extrusin = otExt.Formato.Formato_Nombre,
                          Id_Tratado = otExt.Tratado_Id,
                          Tratado = otExt.Tratado.Tratado_Nombre,
                          Calibre_Extrusion = otExt.Extrusion_Calibre,
                          Und_Extrusion = otExt.UndMed_Id,
                          Ancho1_Extrusion = otExt.Extrusion_Ancho1,
                          Ancho2_Extrusion = otExt.Extrusion_Ancho2,
                          Ancho3_Extrusion = otExt.Extrusion_Ancho3,
                          Peso_Extrusion = otExt.Extrusion_Peso,

                          // Información de Impresión
                          Id_Tipo_Imptesion = otImp.TpImpresion_Id,
                          Tipo_Impresion = otImp.Tipos_Impresion.TpImpresion_Nombre,
                          Rodillo = otImp.Rodillo_Id,
                          Pista = otImp.Pista_Id,
                          Id_Tinta1 = otImp.Tinta1_Id,
                          Tinta1 = otImp.Tinta1.Tinta_Nombre,
                          Id_Tinta2 = otImp.Tinta2_Id,
                          Tinta2 = otImp.Tinta2.Tinta_Nombre,
                          Id_Tinta3 = otImp.Tinta3_Id,
                          Tinta3 = otImp.Tinta3.Tinta_Nombre,
                          Id_Tinta4 = otImp.Tinta4_Id,
                          Tinta4 = otImp.Tinta4.Tinta_Nombre,
                          Id_Tinta5 = otImp.Tinta5_Id,
                          Tinta5 = otImp.Tinta5.Tinta_Nombre,
                          Id_Tinta6 = otImp.Tinta6_Id,
                          Tinta6 = otImp.Tinta6.Tinta_Nombre,
                          Id_Tinta7 = otImp.Tinta7_Id,
                          Tinta7 = otImp.Tinta7.Tinta_Nombre,
                          Id_Tinta8 = otImp.Tinta8_Id,
                          Tinta8 = otImp.Tinta8.Tinta_Nombre,

                          // Información de Laminado
                          Id_Capa1 = otLam.Capa_Id1,
                          Laminado_Capa1 = otLam.Laminado_Capa.LamCapa_Nombre,
                          Calibre_Laminado_Capa1 = otLam.LamCapa_Calibre1,
                          Cantidad_Laminado_Capa1 = otLam.LamCapa_Cantidad1,
                          Id_Capa2 = otLam.Capa_Id2,
                          Laminado_Capa2 = otLam.Laminado_Capa2.LamCapa_Nombre,
                          Calibre_Laminado_Capa2 = otLam.LamCapa_Calibre2,
                          Cantidad_Laminado_Capa2 = otLam.LamCapa_Cantidad2,
                          Id_Capa3 = otLam.Capa_Id3,
                          Laminado_Capa3 = otLam.Laminado_Capa3.LamCapa_Nombre,
                          Calibre_Laminado_Capa3 = otLam.LamCapa_Calibre3,
                          Cantidad_Laminado_Capa3 = otLam.LamCapa_Cantidad3,

                          // Información de Sellado
                          Id_Formato_Producto = otSelCor.Formato_Id,
                          Formato_Producto = otSelCor.Formato.TpProd_Nombre,
                          otSelCor.SelladoCorte_Ancho,
                          otSelCor.SelladoCorte_Largo,
                          otSelCor.SelladoCorte_Fuelle,
                          otSelCor.TpSellado_Id,
                          otSelCor.TipoSellado.TpSellados_Nombre,
                          otSelCor.SelladoCorte_PesoMillar,
                          otSelCor.SelladoCorte_PesoBulto,
                          otSelCor.SelladoCorte_CantBolsasBulto,
                          otSelCor.SelladoCorte_CantBolsasPaquete,
                          otSelCor.SelladoCorte_PrecioSelladoDia,
                          otSelCor.SelladoCorte_PrecioSelladoNoche,
                          otSelCor.SelladoCorte_PesoPaquete,
                          otSelCor.SelladoCorte_PesoProducto,

                          // Información de Mezclas
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

            if (con == null) return NotFound();
            return Ok(con);
        }

        [HttpGet("getOrdenTrabajo/{orden}")]
        public ActionResult GetOrdenTrabajo(long orden)
        {
            var con = from ot in _context.Set<Orden_Trabajo>()
                      join otExt in _context.Set<OT_Extrusion>() on ot.Ot_Id equals otExt.Ot_Id
                      join otImp in _context.Set<OT_Impresion>() on ot.Ot_Id equals otImp.Ot_Id
                      join otLam in _context.Set<OT_Laminado>() on ot.Ot_Id equals otLam.OT_Id
                      join otSelCor in _context.Set<OT_Sellado_Corte>() on ot.Ot_Id equals otSelCor.Ot_Id
                      join exis in _context.Set<Existencia_Productos>() on ot.Prod_Id equals exis.Prod_Id
                      where ot.Ot_Id == orden &&
                            exis.UndMed_Id == ot.UndMed_Id
                      select new
                      {
                          // Información de la OT
                          Numero_Orden = ot.Ot_Id,
                          Id_SedeCliente = ot.SedeCli.SedeCli_CodBagPro,
                          Id_Cliente = ot.SedeCli.Cli_Id,
                          Cliente = ot.SedeCli.Cli.Cli_Nombre,
                          Ciudad = ot.SedeCli.SedeCliente_Ciudad,
                          Direccion = ot.SedeCli.SedeCliente_Direccion,
                          Id_Producto = ot.Prod_Id,
                          Producto = ot.Prod.Prod_Nombre,
                          Id_Presentacion = ot.UndMed_Id,
                          Presentacion = ot.Unidad_Medida.UndMed_Nombre,
                          Margen = ot.Ot_MargenAdicional,
                          Fecha_Creacion = ot.Ot_FechaCreacion,
                          Fecha_Entrega = ot.PedidoExterno.PedExt_FechaEntrega,
                          Estado_Orden = ot.Estado_Id,
                          Estado = ot.Estado.Estado_Nombre,
                          Id_Pedido = ot.PedExt_Id,
                          Id_Vendedor = ot.PedidoExterno.Usua_Id,
                          Vendedor = ot.PedidoExterno.Usua.Usua_Nombre,
                          Observacion = ot.Ot_Observacion,
                          Cyrel = ot.Ot_Cyrel,
                          Extrusion = ot.Extrusion,
                          Impresion = ot.Impresion,
                          Rotograbado = ot.Rotograbado,
                          Laminado = ot.Laminado,
                          Corte = ot.Ot_Corte,
                          Sellado = ot.Sellado,
                          Cantidad_Pedida = ot.Ot_CantidadPedida,
                          Peso_Neto = ot.Ot_PesoNetoKg,
                          Precio_Producto = exis.ExProd_PrecioVenta,
                          ValorKg = ot.Ot_ValorKg,
                          ValorUnidad = ot.Ot_ValorUnidad,

                          // Información de Extrusión
                          Id_Material = otExt.Material_Id,
                          Material = otExt.Material_MatPrima.Material_Nombre,
                          Id_Pigmento_Extrusion = otExt.Pigmt_Id,
                          Pigmento_Extrusion = otExt.Pigmento.Pigmt_Nombre,
                          Id_Formato_Extrusion = otExt.Formato_Id,
                          Formato_Extrusin = otExt.Formato.Formato_Nombre,
                          Id_Tratado = otExt.Tratado_Id,
                          Tratado = otExt.Tratado.Tratado_Nombre,
                          Calibre_Extrusion = otExt.Extrusion_Calibre,
                          Und_Extrusion = otExt.UndMed_Id,
                          Ancho1_Extrusion = otExt.Extrusion_Ancho1,
                          Ancho2_Extrusion = otExt.Extrusion_Ancho2,
                          Ancho3_Extrusion = otExt.Extrusion_Ancho3,
                          Peso_Extrusion = otExt.Extrusion_Peso,

                          // Información de Impresión
                          Id_Tipo_Imptesion = otImp.TpImpresion_Id,
                          Tipo_Impresion = otImp.Tipos_Impresion.TpImpresion_Nombre,
                          Rodillo = otImp.Rodillo_Id,
                          Pista = otImp.Pista_Id,
                          Id_Tinta1 = otImp.Tinta1_Id,
                          Tinta1 = otImp.Tinta1.Tinta_Nombre,
                          Id_Tinta2 = otImp.Tinta2_Id,
                          Tinta2 = otImp.Tinta2.Tinta_Nombre,
                          Id_Tinta3 = otImp.Tinta3_Id,
                          Tinta3 = otImp.Tinta3.Tinta_Nombre,
                          Id_Tinta4 = otImp.Tinta4_Id,
                          Tinta4 = otImp.Tinta4.Tinta_Nombre,
                          Id_Tinta5 = otImp.Tinta5_Id,
                          Tinta5 = otImp.Tinta5.Tinta_Nombre,
                          Id_Tinta6 = otImp.Tinta6_Id,
                          Tinta6 = otImp.Tinta6.Tinta_Nombre,
                          Id_Tinta7 = otImp.Tinta7_Id,
                          Tinta7 = otImp.Tinta7.Tinta_Nombre,
                          Id_Tinta8 = otImp.Tinta8_Id,
                          Tinta8 = otImp.Tinta8.Tinta_Nombre,

                          // Información de Laminado
                          Id_Capa1 = otLam.Capa_Id1,
                          Laminado_Capa1 = otLam.Laminado_Capa.LamCapa_Nombre,
                          Calibre_Laminado_Capa1 = otLam.LamCapa_Calibre1,
                          Cantidad_Laminado_Capa1 = otLam.LamCapa_Cantidad1,
                          Id_Capa2 = otLam.Capa_Id2,
                          Laminado_Capa2 = otLam.Laminado_Capa2.LamCapa_Nombre,
                          Calibre_Laminado_Capa2 = otLam.LamCapa_Calibre2,
                          Cantidad_Laminado_Capa2 = otLam.LamCapa_Cantidad2,
                          Id_Capa3 = otLam.Capa_Id3,
                          Laminado_Capa3 = otLam.Laminado_Capa3.LamCapa_Nombre,
                          Calibre_Laminado_Capa3 = otLam.LamCapa_Calibre3,
                          Cantidad_Laminado_Capa3 = otLam.LamCapa_Cantidad3,

                          // Información de Sellado
                          Id_Formato_Producto = otSelCor.Formato_Id,
                          Formato_Producto = otSelCor.Formato.TpProd_Nombre,
                          otSelCor.SelladoCorte_Ancho,
                          otSelCor.SelladoCorte_Largo,
                          otSelCor.SelladoCorte_Fuelle,
                          otSelCor.TpSellado_Id,
                          otSelCor.TipoSellado.TpSellados_Nombre,
                          otSelCor.SelladoCorte_PesoMillar,
                          otSelCor.SelladoCorte_PesoBulto,
                          otSelCor.SelladoCorte_CantBolsasBulto,
                          otSelCor.SelladoCorte_CantBolsasPaquete,
                          otSelCor.SelladoCorte_PrecioSelladoDia,
                          otSelCor.SelladoCorte_PrecioSelladoNoche,
                          otSelCor.SelladoCorte_PesoPaquete,
                          otSelCor.SelladoCorte_PesoProducto,

                          // Información de Mezclas
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

            return con.Any() ? Ok(con) : BadRequest($"¡No se encontró una Orden de Trabajo con el consecutivo {orden}!");
        }

        //Funcion que consultará la información de la ultima orden de trabajo que se hizo de un producto con una presentación en especifico
        [HttpGet("getInfoUltOT/{producto}/{presentacion}")]
        public ActionResult GetInfoUltOT(int producto, string presentacion)
        {
            var con = (from ot in _context.Set<Orden_Trabajo>()
                       join otExt in _context.Set<OT_Extrusion>() on ot.Ot_Id equals otExt.Ot_Id
                       join otImp in _context.Set<OT_Impresion>() on ot.Ot_Id equals otImp.Ot_Id
                       join otLam in _context.Set<OT_Laminado>() on ot.Ot_Id equals otLam.OT_Id
                       join otSelCor in _context.Set<OT_Sellado_Corte>() on ot.Ot_Id equals otSelCor.Ot_Id
                       where ot.Prod_Id == producto
                             && ot.UndMed_Id == presentacion
                       orderby ot.Ot_Id descending
                       select new
                       {
                           // Información de la OT
                           Numero_Orden = ot.Ot_Id,
                           Id_SedeCliente = ot.SedeCli_Id,
                           Id_Cliente = ot.SedeCli.Cli_Id,
                           Cliente = ot.SedeCli.Cli.Cli_Nombre,
                           Ciudad = ot.SedeCli.SedeCliente_Ciudad,
                           Direccion = ot.SedeCli.SedeCliente_Direccion,
                           Id_Producto = ot.Prod_Id,
                           Producto = ot.Prod.Prod_Nombre,
                           Id_Presentacion = ot.UndMed_Id,
                           Presentacion = ot.Unidad_Medida.UndMed_Nombre,
                           Margen = ot.Ot_MargenAdicional,
                           Fecha_Creacion = ot.Ot_FechaCreacion,
                           Fecha_Entrega = ot.PedidoExterno.PedExt_FechaEntrega,
                           Estado_Orden = ot.Estado_Id,
                           Estado = ot.Estado.Estado_Nombre,
                           Id_Pedido = ot.PedExt_Id,
                           Id_Vendedor = ot.PedidoExterno.Usua_Id,
                           Vendedor = ot.PedidoExterno.Usua.Usua_Nombre,
                           Observacion = ot.Ot_Observacion,
                           Cyrel = ot.Ot_Cyrel,
                           Extrusion = ot.Extrusion,
                           Impresion = ot.Impresion,
                           Rotograbado = ot.Rotograbado,
                           Laminado = ot.Laminado,
                           Corte = ot.Ot_Corte,
                           Sellado = ot.Sellado,
                           Cantidad_Pedida = ot.Ot_CantidadPedida,
                           PrecioUnidad = ot.Ot_ValorUnidad,
                           PrecioKilo = ot.Ot_ValorKg,
                           Peso_Neto = ot.Ot_PesoNetoKg,

                           // Información de Extrusión
                           Id_Material = otExt.Material_Id,
                           Material = otExt.Material_MatPrima.Material_Nombre,
                           Id_Pigmento_Extrusion = otExt.Pigmt_Id,
                           Pigmento_Extrusion = otExt.Pigmento.Pigmt_Nombre,
                           Id_Formato_Extrusion = otExt.Formato_Id,
                           Formato_Extrusin = otExt.Formato.Formato_Nombre,
                           Id_Tratado = otExt.Tratado_Id,
                           Tratado = otExt.Tratado.Tratado_Nombre,
                           Calibre_Extrusion = otExt.Extrusion_Calibre,
                           Und_Extrusion = otExt.UndMed_Id,
                           Ancho1_Extrusion = otExt.Extrusion_Ancho1,
                           Ancho2_Extrusion = otExt.Extrusion_Ancho2,
                           Ancho3_Extrusion = otExt.Extrusion_Ancho3,
                           Peso_Extrusion = otExt.Extrusion_Peso,

                           // Información de Impresión
                           Id_Tipo_Imptesion = otImp.TpImpresion_Id,
                           Tipo_Impresion = otImp.Tipos_Impresion.TpImpresion_Nombre,
                           Rodillo = otImp.Rodillo_Id,
                           Pista = otImp.Pista_Id,
                           Id_Tinta1 = otImp.Tinta1_Id,
                           Tinta1 = otImp.Tinta1.Tinta_Nombre,
                           Id_Tinta2 = otImp.Tinta2_Id,
                           Tinta2 = otImp.Tinta2.Tinta_Nombre,
                           Id_Tinta3 = otImp.Tinta3_Id,
                           Tinta3 = otImp.Tinta3.Tinta_Nombre,
                           Id_Tinta4 = otImp.Tinta4_Id,
                           Tinta4 = otImp.Tinta4.Tinta_Nombre,
                           Id_Tinta5 = otImp.Tinta5_Id,
                           Tinta5 = otImp.Tinta5.Tinta_Nombre,
                           Id_Tinta6 = otImp.Tinta6_Id,
                           Tinta6 = otImp.Tinta6.Tinta_Nombre,
                           Id_Tinta7 = otImp.Tinta7_Id,
                           Tinta7 = otImp.Tinta7.Tinta_Nombre,
                           Id_Tinta8 = otImp.Tinta8_Id,
                           Tinta8 = otImp.Tinta8.Tinta_Nombre,

                           // Información de Laminado
                           Id_Capa1 = otLam.Capa_Id1,
                           Laminado_Capa1 = otLam.Laminado_Capa.LamCapa_Nombre,
                           Calibre_Laminado_Capa1 = otLam.LamCapa_Calibre1,
                           Cantidad_Laminado_Capa1 = otLam.LamCapa_Cantidad1,
                           Id_Capa2 = otLam.Capa_Id2,
                           Laminado_Capa2 = otLam.Laminado_Capa2.LamCapa_Nombre,
                           Calibre_Laminado_Capa2 = otLam.LamCapa_Calibre2,
                           Cantidad_Laminado_Capa2 = otLam.LamCapa_Cantidad2,
                           Id_Capa3 = otLam.Capa_Id3,
                           Laminado_Capa3 = otLam.Laminado_Capa3.LamCapa_Nombre,
                           Calibre_Laminado_Capa3 = otLam.LamCapa_Calibre3,
                           Cantidad_Laminado_Capa3 = otLam.LamCapa_Cantidad3,

                           // Información de Sellado
                           Id_Formato_Producto = otSelCor.Formato_Id,
                           Formato_Producto = otSelCor.Formato.TpProd_Nombre,
                           otSelCor.SelladoCorte_Ancho,
                           otSelCor.SelladoCorte_Largo,
                           otSelCor.SelladoCorte_Fuelle,
                           otSelCor.TpSellado_Id,
                           otSelCor.TipoSellado.TpSellados_Nombre,
                           otSelCor.SelladoCorte_PesoMillar,
                           otSelCor.SelladoCorte_PesoBulto,
                           otSelCor.SelladoCorte_CantBolsasBulto,
                           otSelCor.SelladoCorte_CantBolsasPaquete,
                           otSelCor.SelladoCorte_PrecioSelladoDia,
                           otSelCor.SelladoCorte_PrecioSelladoNoche,
                           ot.Prod.Prod_Peso_Millar,

                           // Información de Mezclas
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
                       }).Take(1);

            return con != null ? Ok(con) : NotFound();
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
