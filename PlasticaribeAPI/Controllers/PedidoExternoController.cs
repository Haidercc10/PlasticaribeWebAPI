using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController, Authorize]
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
        [HttpGet("{id}")]
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
                    pp.SedeCli_Id,
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
                    pp.SedeCli_Id,
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
                    pp.Key.SedeCli_Id,
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

        [HttpGet("PedidosSinOT/")]
        public ActionResult<PedidoExterno> GetPedidosSinOT()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var pedidos = from ped in _context.Set<PedidoProducto>()
                          where ped.PedidoExt.Estado_Id != 5
                                && ped.PedExtProd_CantidadFacturada < ped.PedExtProd_Cantidad
                          group ped by new
                          {
                              ped.PedExt_Id,
                              ped.PedidoExt.SedeCli.Cli_Id,
                              ped.PedidoExt.SedeCli.Cli.Cli_Nombre,
                              ped.PedidoExt.PedExt_FechaEntrega
                          } into ped
                          select new
                          {
                              Id_Pedido = ped.Key.PedExt_Id,
                              Id_Cliente = ped.Key.Cli_Id,
                              Nombre_Cliente = ped.Key.Cli_Nombre,
                              Fecha_Entrega = ped.Key.PedExt_FechaEntrega,
                              Cantidad_Productos = ped.Count(),
                              Zeus = false,
                          };
            if (pedidos == null)
            {
                return NotFound();
            }
            return Ok(pedidos);
        }

        // funcion que consultará la información de un pedido
        [HttpGet("getInfoPedido/{pedido}")]
        public ActionResult getInfoPedido(int pedido)
        {
            var con = from ped in _context.Set<PedidoProducto>()
                      where ped.PedExt_Id == pedido
                            && ped.PedidoExt.Estado_Id != 5
                            && ped.PedExtProd_CantidadFacturada < ped.PedExtProd_Cantidad
                      select new
                      {
                          Id_Vendedor = ped.PedidoExt.Usua_Id,
                          Vendedor = ped.PedidoExt.Usua.Usua_Nombre,
                          Id_Sede_Cliente = ped.PedidoExt.SedeCli_Id,
                          Id_Cliente = ped.PedidoExt.SedeCli.Cli_Id,
                          Cliente = ped.PedidoExt.SedeCli.Cli.Cli_Nombre,
                          Ciudad = ped.PedidoExt.SedeCli.SedeCliente_Ciudad,
                          Direccion = ped.PedidoExt.SedeCli.SedeCliente_Direccion,
                          Estado = ped.PedidoExt.Estado_Id,
                          Observacion = ped.PedidoExt.PedExt_Observacion,
                          Id_Producto = ped.Prod_Id,
                          Producto = ped.Product.Prod_Nombre,
                          Ancho_Producto = ped.Product.Prod_Ancho,
                          Calibre_Producto = ped.Product.Prod_Calibre,
                          Fuelle_Producto = ped.Product.Prod_Fuelle,
                          Largo_Producto = ped.Product.Prod_Largo,
                          Und_ACFL = ped.Product.UndMedACF,
                          Peso_Producto = ped.Product.Prod_Peso,
                          Peso_Millar = ped.Product.Prod_Peso_Millar,
                          Tipo_Producto = ped.Product.TpProd.TpProd_Nombre,
                          Material_Producto = ped.Product.MaterialMP.Material_Nombre,
                          Pigmento_Producto = ped.Product.Pigmt.Pigmt_Nombre,
                          Cant_Paquete = ped.Product.Prod_CantBolsasPaquete,
                          Cant_Bulto = ped.Product.Prod_CantBolsasBulto,
                          Cantidad_Pedida = ped.PedExtProd_Cantidad,
                          Cantidad_Restante = ped.PedExtProd_Cantidad - ped.PedExtProd_CantidadFacturada,
                          Und_Pedido = ped.UndMed_Id,
                          Tipo_Sellado = ped.Product.TiposSellados.TpSellados_Nombre,
                          Precio_Producto = ped.PedExtProd_PrecioUnitario,
                          SubTotal_Producto = (ped.PedExtProd_PrecioUnitario * (ped.PedExtProd_Cantidad - ped.PedExtProd_CantidadFacturada)),
                          Fecha_Entrega = ped.PedExtProd_FechaEntrega,
                      };
            return Ok(con);
        }

        //funcion que llevará la información para crear el pdf del ultimo pedido ingresado
        [HttpGet("getCrearPdfUltPedido/{pedido}")]
        public ActionResult getCrearPdfUltPedido(int pedido)
        {
            var con = from ped in _context.Set<PedidoProducto>()
                      from Emp in _context.Set<Empresa>()
                      where ped.PedExt_Id == pedido
                            && Emp.Empresa_Id == 800188732
                      select new
                      {
                          Id_Pedido = ped.PedExt_Id,
                          Consecutivo = ped.PedidoExt.PedExt_Codigo,
                          FechaCreacion = ped.PedidoExt.PedExt_FechaCreacion,
                          //FechaEntrega = ped.PedidoExt.PedExt_FechaEntrega,
                          Hora = ped.PedidoExt.PedExt_HoraCreacion,
                          Creador_Id = ped.PedidoExt.Creador_Id,
                          Creador = ped.PedidoExt.Creador.Usua_Nombre,

                          Estado_Id = ped.PedidoExt.Estado_Id,
                          Estado = ped.PedidoExt.Estado.Estado_Nombre,
                          Observacion = ped.PedidoExt.PedExt_Observacion,
                          Vendedor_Id = ped.PedidoExt.Usua_Id,
                          Vendedor = ped.PedidoExt.Usua.Usua_Nombre,
                          Descuento = ped.PedidoExt.PedExt_Descuento,
                          Iva = ped.PedidoExt.PedExt_Iva,
                          Precio_Total = ped.PedidoExt.PedExt_PrecioTotal,
                          Precio_Final = ped.PedidoExt.PedExt_PrecioTotalFinal,
                          Cliente_Id = ped.PedidoExt.SedeCli.Cli_Id,
                          Tipo_Id = ped.PedidoExt.SedeCli.Cli.TipoIdentificacion_Id,
                          Cliente = ped.PedidoExt.SedeCli.Cli.Cli_Nombre,
                          Tipo_Cliente = ped.PedidoExt.SedeCli.Cli.TPCli.TPCli_Nombre,
                          Telefono_Cliente = ped.PedidoExt.SedeCli.Cli.Cli_Telefono,
                          Ciudad_Cliente = ped.PedidoExt.SedeCli.SedeCliente_Ciudad,
                          Correo_Cliente = ped.PedidoExt.SedeCli.Cli.Cli_Email,
                          Direccion_Cliente = ped.PedidoExt.SedeCli.SedeCliente_Direccion,
                          CodPostal_Cliente = ped.PedidoExt.SedeCli.SedeCli_CodPostal,

                          Producto_Id = ped.Prod_Id,
                          Producto = ped.Product.Prod_Nombre,
                          Cantidad = ped.PedExtProd_Cantidad,
                          Presentacion = ped.UndMed_Id,
                          Precio_Unitario = ped.PedExtProd_PrecioUnitario,
                          SubTotal_Producto = (ped.PedExtProd_Cantidad * ped.PedExtProd_PrecioUnitario),
                          Fecha_Entrega = ped.PedExtProd_FechaEntrega,

                          Empresa_Id = Emp.Empresa_Id,
                          Empresa_Ciudad = Emp.Empresa_Ciudad,
                          Empresa_Codigo = Emp.Empresa_COdigoPostal,
                          Empresa_Correo = Emp.Empresa_Correo,
                          Empresa_Direccion = Emp.Empresa_Direccion,
                          Empresa_Telefono = Emp.Empresa_Telefono,
                          Empresa = Emp.Empresa_Nombre,
                      };
            return Ok(con);
        }

        //Funcion que traerá la informacion completa de un pedido, esta consulta se usará inicialmente para la edicion de pedidos
        [HttpGet("getInfoEditarPedido/{pedido}")]
        public ActionResult GetInfoEditarPedido(int pedido)
        {
            var con = from ped in _context.Set<PedidoExterno>()
                      from pedProd in _context.Set<PedidoProducto>()
                      where ped.PedExt_Id == pedido
                            && ped.PedExt_Id == pedProd.PedExt_Id
                            && ped.Estado_Id == 11
                      select new
                      {
                          Consecutivo = ped.PedExt_Id,
                          Id_SedeCliente = ped.SedeCli_Id,
                          Id_Cliente = ped.SedeCli.Cli_Id,
                          Cliente = ped.SedeCli.Cli.Cli_Nombre,
                          Ciudad = ped.SedeCli.SedeCliente_Ciudad,
                          Direccion = ped.SedeCli.SedeCliente_Direccion,
                          Id_Vendedor = ped.Usua_Id,
                          Vendedor = ped.Usua.Usua_Nombre,
                          Observacion = ped.PedExt_Observacion,
                          Descuento = ped.PedExt_Descuento,
                          Iva = ped.PedExt_Iva,
                          Id_Creador = ped.Creador_Id,
                          Creador = ped.Creador.Usua_Nombre,
                          Valor_Total = ped.PedExt_PrecioTotal,

                          Id_Producto = pedProd.Prod_Id,
                          Producto = pedProd.Product.Prod_Nombre,
                          Cantidad_Pedida = pedProd.PedExtProd_Cantidad,
                          Presentacion = pedProd.UndMed_Id,
                          Precio_Unitario = pedProd.PedExtProd_PrecioUnitario,
                          Fecha_Entrega = pedProd.PedExtProd_FechaEntrega,
                      };
            return Ok(con);

        }

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
