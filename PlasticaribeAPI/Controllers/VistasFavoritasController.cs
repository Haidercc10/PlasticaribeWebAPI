using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Accesos rápidos (vistas favoritas) de la barra inferior.
    /// El usuario y el rol se toman siempre del token: un usuario solo puede ver y cambiar sus propios favoritos,
    /// y solo puede elegir vistas activas permitidas para su rol.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class VistasFavoritasController : ControllerBase
    {
        /// <summary>Cantidad máxima de favoritos por usuario (columnas VistaFav_Num1..VistaFav_Num7).</summary>
        public const int LimiteFavoritos = 7;

        private readonly dataContext _context;

        public VistasFavoritasController(dataContext context)
        {
            _context = context;
        }

        public record VistaFavoritaDto(int Id, string Nombre, string Icono, string Categoria, string Ruta);

        public record MisFavoritosDto(int Limite, IReadOnlyList<VistaFavoritaDto> Favoritos);

        // GET: api/VistasFavoritas/mias
        /// <summary>Favoritos del usuario del token, en el orden elegido, sin las vistas inactivas o ya no permitidas para su rol.</summary>
        [HttpGet("mias")]
        public async Task<ActionResult<MisFavoritosDto>> GetMias()
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();

            var fila = await _context.VistasFavoritas.AsNoTracking().FirstOrDefaultAsync(vf => vf.Usua_Id == usuarioId);
            var ids = fila == null ? new List<int>() : IdsDeFila(fila);

            return new MisFavoritosDto(LimiteFavoritos, await VistasEnOrden(ids, rol));
        }

        // GET: api/VistasFavoritas/disponibles
        /// <summary>Vistas activas que el rol del token puede elegir como favoritas.</summary>
        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<VistaFavoritaDto>>> GetDisponibles()
        {
            if (!TryObtenerUsuario(out _, out var rol)) return Unauthorized();

            return await VistasPermitidas(rol)
                .OrderBy(vp => vp.Vp_Categoria).ThenBy(vp => vp.Vp_Nombre)
                .Select(vp => new VistaFavoritaDto(vp.Vp_Id, vp.Vp_Nombre, vp.Vp_Icono_Dock, vp.Vp_Categoria, vp.Vp_Ruta))
                .ToListAsync();
        }

        // PUT: api/VistasFavoritas/mias
        /// <summary>Reemplaza los favoritos del usuario del token por la lista recibida (en orden). Crea la fila si no existe.</summary>
        [HttpPut("mias")]
        public async Task<ActionResult<MisFavoritosDto>> PutMias([FromBody] List<int> vistas)
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();

            vistas ??= new List<int>();
            if (vistas.Count > LimiteFavoritos)
                return BadRequest($"Solo puede elegir hasta {LimiteFavoritos} favoritos.");
            if (vistas.Any(id => id <= 0) || vistas.Distinct().Count() != vistas.Count)
                return BadRequest("La lista de favoritos tiene vistas repetidas o no válidas.");

            var permitidas = await VistasPermitidas(rol).Where(vp => vistas.Contains(vp.Vp_Id)).Select(vp => vp.Vp_Id).ToListAsync();
            if (permitidas.Count != vistas.Count)
                return BadRequest("Una o más vistas no existen, están inactivas o no están permitidas para su rol.");

            var fila = await _context.VistasFavoritas.FirstOrDefaultAsync(vf => vf.Usua_Id == usuarioId);
            if (fila == null)
            {
                fila = new VistasFavoritas { Usua_Id = usuarioId };
                _context.VistasFavoritas.Add(fila);
            }

            AsignarIds(fila, vistas);
            var ahora = DateTime.Now;
            fila.VistaFav_Fecha = ahora.Date;
            fila.VistaFav_Hora = ahora.ToString("H:mm:ss", CultureInfo.InvariantCulture);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) when (_context.Entry(fila).State == EntityState.Added)
            {
                // Otra petición del mismo usuario creó la fila al mismo tiempo (índice único en Usua_Id): se actualiza esa.
                _context.Entry(fila).State = EntityState.Detached;
                var existente = await _context.VistasFavoritas.FirstAsync(vf => vf.Usua_Id == usuarioId);
                AsignarIds(existente, vistas);
                existente.VistaFav_Fecha = fila.VistaFav_Fecha;
                existente.VistaFav_Hora = fila.VistaFav_Hora;
                await _context.SaveChangesAsync();
            }

            return new MisFavoritosDto(LimiteFavoritos, await VistasEnOrden(vistas, rol));
        }

        #region Endpoints anteriores (front viejo). Quitar cuando todos los clientes usen "mias".

        [Obsolete("Usar GET api/VistasFavoritas/mias")]
        [HttpGet("getVistasFavUsuario/{usuario}")]
        public ActionResult getVistasFavUsuario(long usuario)
        {
            if (!TryObtenerUsuario(out var usuarioId, out _)) return Unauthorized();
            if (usuario != usuarioId) return Forbid();

            return Ok(_context.VistasFavoritas.AsNoTracking().Where(vf => vf.Usua_Id == usuarioId).ToList());
        }

        [Obsolete("Usar PUT api/VistasFavoritas/mias")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVistasFavoritas(int id, VistasFavoritas vistasFavoritas)
        {
            if (!TryObtenerUsuario(out var usuarioId, out _)) return Unauthorized();
            if (id != vistasFavoritas.VistasFav_Id) return BadRequest();

            var fila = await _context.VistasFavoritas.FindAsync(id);
            if (fila == null) return NotFound();
            if (fila.Usua_Id != usuarioId) return Forbid();

            // El front viejo solo maneja 5 posiciones: se conservan la 6 y la 7.
            fila.VistaFav_Num1 = vistasFavoritas.VistaFav_Num1;
            fila.VistaFav_Num2 = vistasFavoritas.VistaFav_Num2;
            fila.VistaFav_Num3 = vistasFavoritas.VistaFav_Num3;
            fila.VistaFav_Num4 = vistasFavoritas.VistaFav_Num4;
            fila.VistaFav_Num5 = vistasFavoritas.VistaFav_Num5;
            fila.VistaFav_Fecha = vistasFavoritas.VistaFav_Fecha;
            fila.VistaFav_Hora = vistasFavoritas.VistaFav_Hora;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Obsolete("Usar PUT api/VistasFavoritas/mias")]
        [HttpPost]
        public async Task<ActionResult<VistasFavoritas>> PostVistasFavoritas(VistasFavoritas vistasFavoritas)
        {
            if (!TryObtenerUsuario(out var usuarioId, out _)) return Unauthorized();
            if (vistasFavoritas.Usua_Id != usuarioId) return Forbid();
            if (await _context.VistasFavoritas.AnyAsync(vf => vf.Usua_Id == usuarioId))
                return Conflict("El usuario ya tiene favoritos registrados.");

            vistasFavoritas.VistasFav_Id = 0;
            _context.VistasFavoritas.Add(vistasFavoritas);
            await _context.SaveChangesAsync();

            return Ok(vistasFavoritas);
        }

        #endregion

        private bool TryObtenerUsuario(out long usuarioId, out string rol)
        {
            rol = User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;
            return long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out usuarioId) && rol != string.Empty;
        }

        /// <summary>Vistas activas permitidas para el rol (mismo criterio que Vistas_Permisos/get_By_Rol).</summary>
        private IQueryable<Vistas_Permisos> VistasPermitidas(string rol)
        {
            var patron = $"|{rol}|";
            return _context.Vistas_Permisos.AsNoTracking().Where(vp => vp.Vp_Estado == 1 && vp.Vp_Id_Roles.Contains(patron));
        }

        /// <summary>Devuelve las vistas de <paramref name="ids"/> en ese mismo orden, omitiendo las no permitidas.</summary>
        private async Task<List<VistaFavoritaDto>> VistasEnOrden(List<int> ids, string rol)
        {
            if (ids.Count == 0) return new List<VistaFavoritaDto>();

            var vistas = await VistasPermitidas(rol)
                .Where(vp => ids.Contains(vp.Vp_Id))
                .Select(vp => new VistaFavoritaDto(vp.Vp_Id, vp.Vp_Nombre, vp.Vp_Icono_Dock, vp.Vp_Categoria, vp.Vp_Ruta))
                .ToDictionaryAsync(v => v.Id);

            return ids.Where(vistas.ContainsKey).Select(id => vistas[id]).ToList();
        }

        private static List<int> IdsDeFila(VistasFavoritas f) =>
            new[] { f.VistaFav_Num1, f.VistaFav_Num2, f.VistaFav_Num3, f.VistaFav_Num4, f.VistaFav_Num5, f.VistaFav_Num6, f.VistaFav_Num7 }
                .Where(id => id > 0).Distinct().ToList();

        private static void AsignarIds(VistasFavoritas f, List<int> ids)
        {
            int Id(int i) => i < ids.Count ? ids[i] : 0;
            f.VistaFav_Num1 = Id(0);
            f.VistaFav_Num2 = Id(1);
            f.VistaFav_Num3 = Id(2);
            f.VistaFav_Num4 = Id(3);
            f.VistaFav_Num5 = Id(4);
            f.VistaFav_Num6 = Id(5);
            f.VistaFav_Num7 = Id(6);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
