using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoExternoController : ControllerBase
    {
        private readonly dataContext _context;

        public PedidoExternoController(dataContext context)
        {
            _context = context;
        }
        
        // GET: api/PedidoExterno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoExterno>>> GetPedidos_Externos()
        {
          if (_context.Pedidos_Externos == null)
          {
              return NotFound();
          }
            return await _context.Pedidos_Externos.ToListAsync();
        }

        // GET: api/PedidoExterno/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<PedidoExterno>> GetPedidoExterno(long id)
        {
          if (_context.Pedidos_Externos == null)
          {
              return NotFound();
          }
            var pedidoExterno = await _context.Pedidos_Externos.FindAsync(id);

            if (pedidoExterno == null)
            {
                return NotFound();
            }

            return pedidoExterno;
        }

        [HttpGet("idPedidoLlenarPDF/{PedExt_Id}")]
        public ActionResult<PedidoExterno> GetLlenadoPDF(int PedExt_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_Id == PedExt_Id)
                .Select(pp => new
                {
                    pp.PedExt_Id,
                    pp.PedExt_FechaCreacion,
                    pp.PedExt_FechaEntrega,
                    pp.SedeCli.Cli.Cli_Id,
                    pp.SedeCli.Cli.Cli_Nombre,
                    pp.SedeCli.Cli.Usua.Usua_Nombre,
                    pp.Usua_Id,
                    pp.SedeCli.Cli.Usua.RolUsu_Id,
                    pp.PedExt_PrecioTotal,
                    pp.Estado.Estado_Nombre,
                    pp.SedeCli.Cli.Usua.TipoIdentificacion_Id,
                    pp.SedeCli.Cli.TPCli.TPCli_Nombre,
                    pp.SedeCli.Cli.Cli_Telefono,
                    pp.SedeCli.SedeCliente_Ciudad,
                    pp.SedeCli.SedeCliente_Direccion,
                    pp.SedeCli.SedeCli_CodPostal,
                    pp.SedeCli.Cli.Cli_Email,
                    pp.PedExt_Observacion,
                    pp.PedExt_Descuento,
                    pp.PedExt_Iva,
                    pp.PedExt_PrecioTotalFinal
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("UltimoPedido/")]
        public ActionResult<PedidoExterno> GetIdUltimoPedido()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos.OrderByDescending(p => p.PedExt_Id).First();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("PedidoExterno/")]
        public ActionResult<PedidoExterno> Get()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
                .GroupBy(pp => new
                {
                    pp.PedExt_Id,
                    pp.PedExt_FechaCreacion,
                    pp.PedExt_FechaEntrega,
                    pp.SedeCli.Cli.Cli_Id,
                    pp.SedeCli.Cli.Cli_Nombre,
                    pp.Usua.Usua_Nombre,
                    pp.Usua_Id,
                    pp.Usua.RolUsu_Id,
                    pp.PedExt_PrecioTotal,
                    pp.Estado.Estado_Nombre
                })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("FechaCreacion/{PedExt_FechaCreacion}")]
        public ActionResult<PedidoExterno> Get(DateTime PedExt_FechaCreacion)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaCreacion == PedExt_FechaCreacion)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
                .GroupBy(pp => new
                {
                    pp.PedExt_Id,
                    pp.PedExt_FechaCreacion,
                    pp.PedExt_FechaEntrega,
                    pp.SedeCli.Cli.Cli_Id,
                    pp.SedeCli.Cli.Cli_Nombre,
                    pp.Usua.Usua_Nombre,
                    pp.Usua_Id,
                    pp.Usua.RolUsu_Id,
                    pp.PedExt_PrecioTotal,
                    pp.Estado.Estado_Nombre
                })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("FechaEntrega/{PedExt_FechaEntrega}")]
        public ActionResult<PedidoExterno> GetFechaEntrga(DateTime PedExt_FechaEntrega)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaEntrega == PedExt_FechaEntrega)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
                .GroupBy(pp => new
                {
                    pp.PedExt_Id,
                    pp.PedExt_FechaCreacion,
                    pp.PedExt_FechaEntrega,
                    pp.SedeCli.Cli.Cli_Id,
                    pp.SedeCli.Cli.Cli_Nombre,
                    pp.Usua.Usua_Nombre,
                    pp.Usua_Id,
                    pp.Usua.RolUsu_Id,
                    pp.PedExt_PrecioTotal,
                    pp.Estado.Estado_Nombre
                })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("nombreCliente/{Cli_Nombre}")]
        public ActionResult<PedidoExterno> Get(string Cli_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.SedeCli.Cli.Cli_Nombre == Cli_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("idCliente/{Cli_Id}")]
        public ActionResult<PedidoExterno> Get(long Cli_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.SedeCli.Cli.Cli_Id == Cli_Id)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("nomberVendeder/{Usua_Nombre}")]
        public ActionResult<PedidoExterno> GetNombreVendeor(string Usua_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.Usua.Usua_Nombre == Usua_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("idPedido/{PedExt_Id}")]
        public ActionResult<PedidoExterno> Get(int PedExt_Id)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_Id == PedExt_Id)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
                .GroupBy(pp => new
                {
                    pp.PedExt_Id,
                    pp.PedExt_FechaCreacion,
                    pp.PedExt_FechaEntrega,
                    pp.SedeCli.Cli.Cli_Id,
                    pp.SedeCli.Cli.Cli_Nombre,
                    pp.Usua.Usua_Nombre,
                    pp.Usua_Id,
                    pp.Usua.RolUsu_Id,
                    pp.PedExt_PrecioTotal,
                    pp.Estado.Estado_Nombre
                })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("estadoNombre/{Estado_Nombre}")]
        public ActionResult<PedidoExterno> GetEstado(string Estado_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.Estado.Estado_Nombre == Estado_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechacreacion/{PedExt_FechaCreacion}/fechaEntrega{PedExt_FechaEntrega}")]
        public ActionResult<PedidoExterno> GetFechas(DateTime PedExt_FechaCreacion, DateTime PedExt_FechaEntrega)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaCreacion == PedExt_FechaCreacion && pp.PedExt_FechaEntrega == PedExt_FechaEntrega)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("estadoUsuario/{Estado_Nombre}/{Usua_Nombre}")]
        public ActionResult<PedidoExterno> Get(string Estado_Nombre, string Usua_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.Estado.Estado_Nombre == Estado_Nombre && pp.Usua.Usua_Nombre == Usua_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechaEntregaUsuario/{PedExt_FechaEntrega}/{Usua_Nombre}")]
        public ActionResult<PedidoExterno> Get(DateTime PedExt_FechaEntrega, string Usua_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaEntrega == PedExt_FechaEntrega && pp.Usua.Usua_Nombre == Usua_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechaEntregaEstado/{PedExt_FechaEntrega}/{Estado_Nombre}")]
        public ActionResult<PedidoExterno> GetFEE(DateTime PedExt_FechaEntrega, string Estado_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaEntrega == PedExt_FechaEntrega && pp.Estado.Estado_Nombre == Estado_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechaCreacionUsuario/{PedExt_FechaCreacion}/{Usua_Nombre}")]
        public ActionResult<PedidoExterno> GetfechaCreacion(DateTime PedExt_FechaCreacion, string Usua_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaCreacion == PedExt_FechaCreacion && pp.Usua.Usua_Nombre == Usua_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechaCreacionEstado/{PedExt_FechaCreacion}/{Estado_Nombre}")]
        public ActionResult<PedidoExterno> GetfechaCreacionEstado(DateTime PedExt_FechaCreacion, string Estado_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaCreacion == PedExt_FechaCreacion && pp.Estado.Estado_Nombre == Estado_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechaEntregaEstadoVendedor/{PedExt_FechaEntrega}/{Estado_Nombre}/{Usua_Nombre}")]
        public ActionResult<PedidoExterno> Get(DateTime PedExt_FechaEntrega, string Estado_Nombre, string Usua_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaEntrega == PedExt_FechaEntrega && pp.Estado.Estado_Nombre == Estado_Nombre && pp.Usua.Usua_Nombre == Usua_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechaCreacionEstadoVendedor/{PedExt_FechaCreacion}/{Estado_Nombre}/{Usua_Nombre}")]
        public ActionResult<PedidoExterno> Geto(DateTime PedExt_FechaCreacion, string Estado_Nombre, string Usua_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaCreacion == PedExt_FechaCreacion && pp.Estado.Estado_Nombre == Estado_Nombre && pp.Usua.Usua_Nombre == Usua_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechasEstadoVendedor/{PedExt_FechaCreacion}/{PedExt_FechaEntrega}/{Estado_Nombre}/{Usua_Nombre}")]
        public ActionResult<PedidoExterno> Get(DateTime PedExt_FechaCreacion, DateTime PedExt_FechaEntrega, string Estado_Nombre, string Usua_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaCreacion == PedExt_FechaCreacion 
                        && pp.PedExt_FechaEntrega == PedExt_FechaEntrega 
                        && pp.Estado.Estado_Nombre == Estado_Nombre 
                        && pp.Usua.Usua_Nombre == Usua_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        [HttpGet("fechasEstado/{PedExt_FechaCreacion}/{PedExt_FechaEntrega}/{Estado_Nombre}")]
        public ActionResult<PedidoExterno> Get(DateTime PedExt_FechaCreacion, DateTime PedExt_FechaEntrega, string Estado_Nombre)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var pedido_Producto = _context.Pedidos_Externos
                .Where(pp => pp.PedExt_FechaCreacion == PedExt_FechaCreacion
                        && pp.PedExt_FechaEntrega == PedExt_FechaEntrega
                        && pp.Estado.Estado_Nombre == Estado_Nombre)
                .Include(pp => pp.SedeCli.Cli)
                .Include(pp => pp.Estado)
                .Include(pp => pp.Usua)
               .GroupBy(pp => new
               {
                   pp.PedExt_Id,
                   pp.PedExt_FechaCreacion,
                   pp.PedExt_FechaEntrega,
                   pp.SedeCli.Cli.Cli_Id,
                   pp.SedeCli.Cli.Cli_Nombre,
                   pp.Usua.Usua_Nombre,
                   pp.Usua_Id,
                   pp.Usua.RolUsu_Id,
                   pp.PedExt_PrecioTotal,
                   pp.Estado.Estado_Nombre
               })
                .Select(pp => new
                {
                    pp.Key.PedExt_Id,
                    pp.Key.PedExt_FechaCreacion,
                    pp.Key.PedExt_FechaEntrega,
                    pp.Key.Cli_Id,
                    pp.Key.Cli_Nombre,
                    pp.Key.Usua_Nombre,
                    pp.Key.Usua_Id,
                    pp.Key.RolUsu_Id,
                    pp.Key.PedExt_PrecioTotal,
                    pp.Key.Estado_Nombre
                }).ToList();

            if (pedido_Producto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pedido_Producto);
            }
        }

        /*[HttpGet("PedidosSinOT/")]
        public ActionResult<PedidoExterno> GetPedidosSinOT()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL. 
            var orden_Trabajo = _context.Pedidos_Externos
                .Where(ot => ot.PedExt_Id != ot.orden_Trabajo.PedExt_Id)
                .Select(ot => new
                {
                    ot.PedExt_Id
                })
                .ToList();

            if (orden_Trabajo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(orden_Trabajo);
            }
        }*/


            // PUT: api/PedidoExterno/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidoExterno(long id, PedidoExterno pedidoExterno)
        {
            if (id != pedidoExterno.PedExt_Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidoExterno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExternoExists(id))
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

        // POST: api/PedidoExterno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PedidoExterno>> PostPedidoExterno(PedidoExterno pedidoExterno)
        {
          if (_context.Pedidos_Externos == null)
          {
              return Problem("Entity set 'dataContext.Pedidos_Externos'  is null.");
          }
            _context.Pedidos_Externos.Add(pedidoExterno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoExterno", new { id = pedidoExterno.PedExt_Id }, pedidoExterno);
        }

        // DELETE: api/PedidoExterno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoExterno(long id)
        {
            if (_context.Pedidos_Externos == null)
            {
                return NotFound();
            }
            var pedidoExterno = await _context.Pedidos_Externos.FindAsync(id);
            if (pedidoExterno == null)
            {
                return NotFound();
            }

            _context.Pedidos_Externos.Remove(pedidoExterno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExternoExists(long id)
        {
            return (_context.Pedidos_Externos?.Any(e => e.PedExt_Id == id)).GetValueOrDefault();
        }
    }
}
