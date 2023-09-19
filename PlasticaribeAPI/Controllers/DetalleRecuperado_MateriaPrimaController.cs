using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DetalleRecuperado_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleRecuperado_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleRecuperado_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRecuperado_MateriaPrima>>> GetDetallesRecuperados_MateriasPrimas()
        {
            if (_context.DetallesRecuperados_MateriasPrimas == null)
            {
                return NotFound();
            }
            return await _context.DetallesRecuperados_MateriasPrimas.ToListAsync();
        }

        // GET: api/DetalleRecuperado_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleRecuperado_MateriaPrima>> GetDetalleRecuperado_MateriaPrima(long id)
        {
            if (_context.DetallesRecuperados_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleRecuperado_MateriaPrima = await _context.DetallesRecuperados_MateriasPrimas.FindAsync(id);

            if (detalleRecuperado_MateriaPrima == null)
            {
                return NotFound();
            }

            return detalleRecuperado_MateriaPrima;
        }

        /** Mostrar Consultas para reporte de Recuperado MP*/
        /** Fecha Inicial */
        [HttpGet("MostrarMPRecuperada/{FechaEntregaInicial}/{FechaEntregaFinal}/{Operario}/{Turno}/{IDPeletizado}")]
        public ActionResult GetMPRecuperada(DateTime FechaEntregaInicial, DateTime FechaEntregaFinal, long Operario = 0, string Turno = "NE", long IDPeletizado = 0)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            DateTime Hoy = DateTime.Today;

#pragma warning disable CS8073 // El resultado de la expresión siempre es el mismo ya que un valor de este tipo siempre es igual a "null"
            if (FechaEntregaInicial != null && FechaEntregaFinal != null && Operario != 0 && Turno != "NE" && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal &&
                    rec.RecMp.Usua_Operador == Operario &&
                    rec.MatPri_Id == IDPeletizado &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && FechaEntregaFinal != null && Turno != "NE" && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal &&
                    rec.MatPri_Id == IDPeletizado &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && FechaEntregaFinal != null && Operario != 0 && Turno != "NE")
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal &&
                    rec.RecMp.Usua_Operador == Operario &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && FechaEntregaFinal != null && Operario != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal &&
                    rec.RecMp.Usua_Operador == Operario
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && FechaEntregaFinal != null && Turno != "NE")
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && FechaEntregaFinal != null && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega == FechaEntregaFinal &&
                    rec.MatPri_Id == IDPeletizado
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && FechaEntregaFinal != null && Turno == "NE")
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);
            }
            else if (FechaEntregaInicial != null)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);
            }

            else if (FechaEntregaInicial != null && Operario != 0 && Turno != "NE" && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial &&
                    rec.RecMp.Usua_Operador == Operario &&
                    rec.MatPri_Id == IDPeletizado &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && Operario != 0 && Turno != "NE")
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                   rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial &&
                   rec.RecMp.Usua_Operador == Operario &&
                   rec.RecMp.Turno_Id == Turno
                   )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);
            }
            else if (FechaEntregaInicial != null && Operario != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                   rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial &&
                   rec.RecMp.Usua_Operador == Operario
                   )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);
            }

            else if (FechaEntregaInicial != null && Turno != "NE" && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial &&
                    rec.MatPri_Id == IDPeletizado &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial
                                                  && x.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (FechaEntregaInicial != null && Turno != "NE")
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                   rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial &&
                   rec.RecMp.Turno_Id == Turno
                   )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);
            }
            else if (FechaEntregaInicial != null && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                   rec => rec.RecMp.RecMp_FechaEntrega == FechaEntregaInicial &&
                   rec.MatPri_Id == IDPeletizado
                   )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == FechaEntregaInicial)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);
            }

            else if (Operario != 0 && Turno != "NE" && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.Usua_Operador == Operario &&
                    rec.MatPri_Id == IDPeletizado &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (Operario != 0 && Turno != "NE")
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.Usua_Operador == Operario &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (Operario != 0 && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.Usua_Operador == Operario &&
                    rec.MatPri_Id == IDPeletizado
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (Operario != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.Usua_Operador == Operario
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.Usua_Operador == Operario)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }

            else if (Turno != "NE" && IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.MatPri_Id == IDPeletizado &&
                    rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }
            else if (Turno != "NE")
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.Turno_Id == Turno
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }

            else if (IDPeletizado != 0)
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.MatPri_Id == IDPeletizado
                    )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
                .ToList();

                return Ok(con);

            }

            else
            {
                var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                  rec => rec.RecMp.RecMp_FechaEntrega == Hoy
                  )
                    .Include(rec => rec.RecMp)
                    .Include(recu => recu.RecMp.UsuaOperador)
                    .GroupBy(agr => new
                    {
                        agr.MatPri_Id,
                        agr.MatPri.MatPri_Nombre,
                        agr.UndMed_Id
                    })
                    .Select(rec => new
                    {
                        rec.Key.MatPri_Id,
                        rec.Key.MatPri_Nombre,
                        SumaCantidad = rec.Sum(da => da.RecMatPri_Cantidad),
                        sumaDia = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "DIA"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == Hoy)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        sumaNoche = _context.DetallesRecuperados_MateriasPrimas
                                           .Where(x => x.RecMp.Turno_Id == "NOCHE"
                                                  && x.MatPri_Id == rec.Key.MatPri_Id
                                                  && x.RecMp.RecMp_FechaEntrega == Hoy)
                                           .GroupBy(x => x.MatPri_Id)
                                           .Select(x => new
                                           {
                                               Suma = x.Sum(y => y.RecMatPri_Cantidad)
                                           }).ToList(),
                        rec.Key.UndMed_Id
                    })
              .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

                return Ok(con);
            }
#pragma warning restore CS8073 // El resultado de la expresión siempre es el mismo ya que un valor de este tipo siempre es igual a "null"
        }

        [HttpGet("MostrarRecuperadoModal/{FechaEntregaInicial}/{FechaEntregaFinal}/{Turno}/{IDPeletizado}")]
        public ActionResult GetMPRecuperada(DateTime FechaEntregaInicial, DateTime FechaEntregaFinal, string Turno, long IDPeletizado)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = _context.DetallesRecuperados_MateriasPrimas.Where(
                    rec => rec.RecMp.RecMp_FechaEntrega >= FechaEntregaInicial &&
                    rec.RecMp.RecMp_FechaEntrega <= FechaEntregaFinal &&
                    rec.MatPri_Id == IDPeletizado &&
                    rec.RecMp.Turno_Id == Turno)
                .GroupBy(rec => new
                {
                    rec.RecMp.RecMp_FechaEntrega,
                    rec.RecMp.Usua_Id,
                    rec.RecMp.UsuaOperador.Usua_Nombre,
                    rec.MatPri_Id,
                    rec.MatPri.MatPri_Nombre,
                    rec.RecMp.Turno_Id,
                    rec.RecMp.TurnoRecMP.Turno_Nombre,
                    rec.RecMatPri_Cantidad,
                    rec.UndMed_Id,
                }).Select(rec => new
                {
                    rec.Key.RecMp_FechaEntrega,
                    rec.Key.Usua_Id,
                    rec.Key.Usua_Nombre,
                    rec.Key.MatPri_Id,
                    rec.Key.MatPri_Nombre,
                    rec.Key.Turno_Id,
                    rec.Key.Turno_Nombre,
                    rec.Key.RecMatPri_Cantidad,
                    rec.Key.UndMed_Id,
                }).ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // PUT: api/DetalleRecuperado_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleRecuperado_MateriaPrima(long id, DetalleRecuperado_MateriaPrima detalleRecuperado_MateriaPrima)
        {
            if (id != detalleRecuperado_MateriaPrima.RecMp_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleRecuperado_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleRecuperado_MateriaPrimaExists(id))
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

        // POST: api/DetalleRecuperado_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleRecuperado_MateriaPrima>> PostDetalleRecuperado_MateriaPrima(DetalleRecuperado_MateriaPrima detalleRecuperado_MateriaPrima)
        {
            if (_context.DetallesRecuperados_MateriasPrimas == null)
            {
                return Problem("Entity set 'dataContext.DetallesRecuperados_MateriasPrimas'  is null.");
            }
            _context.DetallesRecuperados_MateriasPrimas.Add(detalleRecuperado_MateriaPrima);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleRecuperado_MateriaPrimaExists(detalleRecuperado_MateriaPrima.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleRecuperado_MateriaPrima", new { id = detalleRecuperado_MateriaPrima.Codigo }, detalleRecuperado_MateriaPrima);
        }

        // DELETE: api/DetalleRecuperado_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleRecuperado_MateriaPrima(long id)
        {
            if (_context.DetallesRecuperados_MateriasPrimas == null)
            {
                return NotFound();
            }
            var detalleRecuperado_MateriaPrima = await _context.DetallesRecuperados_MateriasPrimas.FindAsync(id);
            if (detalleRecuperado_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.DetallesRecuperados_MateriasPrimas.Remove(detalleRecuperado_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleRecuperado_MateriaPrimaExists(long id)
        {
            return (_context.DetallesRecuperados_MateriasPrimas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
