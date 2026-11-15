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
    /// Eventos del calendario. El usuario y el rol se toman siempre del token (no de la URL).
    /// Visibilidad (EventoCal_Visibilidad) en formato "|token|token|":
    /// "|Solo Yo|" = solo el creador; "|Todos|" = todos los roles, incluso los que se creen después;
    /// "|5|12|" = los roles indicados. Los eventos antiguos "para todos" guardan la lista completa de roles y siguen funcionando.
    /// Solo el creador o un administrador (rol 1) puede editar o eliminar un evento.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class EventosCalendariosController : ControllerBase
    {
        private const string RolAdministrador = "1";
        private const string VisibilidadSoloYo = "Solo Yo";
        private const string VisibilidadTodos = "Todos";
        private const int MaxCaracteresNombre = 200;
        private const int MaxCaracteresDescripcion = 1000;
        private const int MaxDiasConsulta = 400;

        private readonly dataContext _context;

        /// <summary>Inicializa el controlador.</summary>
        public EventosCalendariosController(dataContext context)
        {
            _context = context;
        }

        /// <summary>Datos que envía el front para crear o editar un evento.</summary>
        public class EventoCalendarioPeticion
        {
            public string Nombre { get; set; } = "";
            public string? Descripcion { get; set; }
            public DateTime FechaInicial { get; set; }
            public string HoraInicial { get; set; } = "00:00:00";
            public DateTime FechaFinal { get; set; }
            public string HoraFinal { get; set; } = "23:59:59";
            /// <summary>Formato "|token|token|" (ver la descripción del controlador).</summary>
            public string Visibilidad { get; set; } = "";
        }

        /// <summary>Evento tal como lo consume el front.</summary>
        public record EventoCalendarioDto(
            long Id, string Nombre, string Descripcion,
            string FechaInicial, string HoraInicial, string FechaFinal, string HoraFinal,
            string Visibilidad, long UsuarioId, string Creador, bool EsPropio, bool PuedeEditar);

        /// <summary>
        /// Eventos visibles para el usuario que se cruzan con el rango [fechaInicio, fechaFin] (días completos).
        /// Responde una lista vacía si no hay eventos.
        /// </summary>
        [HttpGet("rango")]
        public async Task<ActionResult<IEnumerable<EventoCalendarioDto>>> GetRango([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();
            if (!RangoValido(fechaInicio, fechaFin, out var error)) return BadRequest(error);
            return Ok(await ConsultarAsync(usuarioId, rol, fechaInicio.Date, fechaFin.Date));
        }

        /// <summary>Eventos que ocurren hoy, incluidos los de varios días que empezaron antes.</summary>
        [HttpGet("hoy")]
        public async Task<ActionResult<IEnumerable<EventoCalendarioDto>>> GetHoy()
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();
            return Ok(await ConsultarAsync(usuarioId, rol, DateTime.Today, DateTime.Today));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<EventoCalendarioDto>> GetEvento(long id)
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();
            var evento = (await Proyectar(Visibles(usuarioId, rol).Where(ev => ev.EventoCal_Id == id), usuarioId, rol).ToListAsync()).FirstOrDefault();
            return evento == null ? NotFound() : Ok(evento);
        }

        [HttpPost]
        public async Task<ActionResult<EventoCalendarioDto>> PostEvento(EventoCalendarioPeticion peticion)
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();
            if (!Validar(peticion, rol, out var datos, out var error)) return BadRequest(error);

            var evento = new EventosCalendario
            {
                Usua_Id = usuarioId, // siempre el usuario del token
                EventoCal_FechaCreacion = DateTime.Today,
                EventoCal_HoraCreacion = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
            };
            Aplicar(evento, datos);
            _context.EventosCalendario.Add(evento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvento), new { id = evento.EventoCal_Id }, new { id = evento.EventoCal_Id });
        }

        /// <summary>Edita un evento propio (o cualquiera, si es administrador). Conserva el creador y la fecha de creación.</summary>
        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutEvento(long id, EventoCalendarioPeticion peticion)
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();
            var evento = await _context.EventosCalendario.FirstOrDefaultAsync(ev => ev.EventoCal_Id == id);
            // 404 y no 403: el interceptor del front cierra la sesión con 403
            if (evento == null || !PuedeEditar(evento.Usua_Id, usuarioId, rol)) return NotFound();
            if (!Validar(peticion, rol, out var datos, out var error)) return BadRequest(error);

            Aplicar(evento, datos);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteEvento(long id)
        {
            if (!TryObtenerUsuario(out var usuarioId, out var rol)) return Unauthorized();
            var evento = await _context.EventosCalendario.FirstOrDefaultAsync(ev => ev.EventoCal_Id == id);
            if (evento == null || !PuedeEditar(evento.Usua_Id, usuarioId, rol)) return NotFound();

            _context.EventosCalendario.Remove(evento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ---------------------------------------------------------------- Auxiliares

        private bool TryObtenerUsuario(out long usuarioId, out string rol)
        {
            rol = User.FindFirstValue(ClaimTypes.Role) ?? "";
            return long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out usuarioId) && rol != "";
        }

        private static bool PuedeEditar(long creadorId, long usuarioId, string rol) => creadorId == usuarioId || rol == RolAdministrador;

        private static bool RangoValido(DateTime inicio, DateTime fin, out string? error)
        {
            error = inicio == default || fin == default ? "Debe indicar la fecha inicial y la final."
                : inicio.Date > fin.Date ? "La fecha inicial no puede ser mayor que la final."
                : (fin.Date - inicio.Date).TotalDays > MaxDiasConsulta ? $"El rango no puede superar {MaxDiasConsulta} días."
                : null;
            return error == null;
        }

        /// <summary>Eventos que el usuario puede ver: los suyos, los de su rol y los de todos. Opcionalmente, los que se cruzan con un rango.</summary>
        private IQueryable<EventosCalendario> Visibles(long usuarioId, string rol, DateTime? desde = null, DateTime? hasta = null)
        {
            var patronRol = $"|{rol}|";
            var patronTodos = $"|{VisibilidadTodos}|";
            var consulta = _context.EventosCalendario.AsNoTracking()
                .Where(ev => ev.Usua_Id == usuarioId || ev.EventoCal_Visibilidad.Contains(patronRol) || ev.EventoCal_Visibilidad.Contains(patronTodos));
            if (desde.HasValue && hasta.HasValue)
                consulta = consulta.Where(ev => ev.EventoCal_FechaInicial <= hasta.Value && ev.EventoCal_FechaFinal >= desde.Value);
            return consulta;
        }

        private static IQueryable<EventoCalendarioDto> Proyectar(IQueryable<EventosCalendario> consulta, long usuarioId, string rol) =>
            consulta.Select(ev => new EventoCalendarioDto(
                ev.EventoCal_Id, ev.EventoCal_Nombre, ev.EventoCal_Descripcion,
                ev.EventoCal_FechaInicial.ToString("yyyy-MM-dd"), ev.EventoCal_HoraInicial,
                ev.EventoCal_FechaFinal.ToString("yyyy-MM-dd"), ev.EventoCal_HoraFinal,
                ev.EventoCal_Visibilidad, ev.Usua_Id,
                ev.Usuario != null ? ev.Usuario.Usua_Nombre : "",
                ev.Usua_Id == usuarioId,
                ev.Usua_Id == usuarioId || rol == RolAdministrador));

        private async Task<List<EventoCalendarioDto>> ConsultarAsync(long usuarioId, string rol, DateTime desde, DateTime hasta)
        {
            var eventos = await Proyectar(Visibles(usuarioId, rol, desde, hasta), usuarioId, rol).ToListAsync();
            return eventos.OrderBy(e => e.FechaInicial).ThenBy(e => NormalizarHora(e.HoraInicial)).ToList();
        }

        /// <summary>Las horas antiguas se guardaron como H:mm:ss; se normalizan a HH:mm:ss.</summary>
        private static string NormalizarHora(string hora) =>
            TimeSpan.TryParse(hora, CultureInfo.InvariantCulture, out var t) ? t.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture) : hora;

        private record DatosEvento(string Nombre, string Descripcion, DateTime FechaInicial, string HoraInicial, DateTime FechaFinal, string HoraFinal, string Visibilidad);

        private static bool Validar(EventoCalendarioPeticion p, string rol, out DatosEvento datos, out string? error)
        {
            datos = null!;
            var nombre = (p.Nombre ?? "").Trim();
            var descripcion = (p.Descripcion ?? "").Trim();
            if (nombre == "") { error = "El nombre del evento es obligatorio."; return false; }
            if (nombre.Length > MaxCaracteresNombre) { error = $"El nombre no puede superar {MaxCaracteresNombre} caracteres."; return false; }
            if (descripcion.Length > MaxCaracteresDescripcion) { error = $"La descripción no puede superar {MaxCaracteresDescripcion} caracteres."; return false; }
            if (p.FechaInicial == default || p.FechaFinal == default) { error = "Debe indicar la fecha inicial y la final."; return false; }
            if (!TimeSpan.TryParse(p.HoraInicial, CultureInfo.InvariantCulture, out var horaInicial) || horaInicial.TotalDays >= 1
                || !TimeSpan.TryParse(p.HoraFinal, CultureInfo.InvariantCulture, out var horaFinal) || horaFinal.TotalDays >= 1)
            { error = "Las horas no tienen un formato válido."; return false; }
            if (p.FechaFinal.Date + horaFinal < p.FechaInicial.Date + horaInicial) { error = "La fecha final no puede ser anterior a la inicial."; return false; }
            if (!ValidarVisibilidad(p.Visibilidad, rol, out var visibilidad, out error)) return false;

            datos = new DatosEvento(nombre, descripcion,
                p.FechaInicial.Date, horaInicial.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture),
                p.FechaFinal.Date, horaFinal.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture),
                visibilidad);
            error = null;
            return true;
        }

        /// <summary>
        /// Acepta "|Solo Yo|", "|Todos|" o una lista de ids de rol. Quien no es administrador solo puede
        /// elegir "Solo Yo" o su propio rol (igual que las opciones que le ofrece el front).
        /// </summary>
        private static bool ValidarVisibilidad(string? valor, string rol, out string visibilidad, out string? error)
        {
            visibilidad = "";
            var partes = (valor ?? "").Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Distinct().ToList();
            var esAdmin = rol == RolAdministrador;

            if (partes.Count == 1 && partes[0] == VisibilidadSoloYo) visibilidad = $"|{VisibilidadSoloYo}|";
            else if (partes.Count == 1 && partes[0] == VisibilidadTodos && esAdmin) visibilidad = $"|{VisibilidadTodos}|";
            else if (partes.Count > 0 && partes.All(x => int.TryParse(x, out _)) && (esAdmin || (partes.Count == 1 && partes[0] == rol)))
                visibilidad = $"|{string.Join("|", partes)}|";

            error = visibilidad == "" ? "La visibilidad seleccionada no es válida para su rol." : null;
            return error == null;
        }

        private static void Aplicar(EventosCalendario evento, DatosEvento datos)
        {
            evento.EventoCal_Nombre = datos.Nombre;
            evento.EventoCal_Descripcion = datos.Descripcion;
            evento.EventoCal_FechaInicial = datos.FechaInicial;
            evento.EventoCal_HoraInicial = datos.HoraInicial;
            evento.EventoCal_FechaFinal = datos.FechaFinal;
            evento.EventoCal_HoraFinal = datos.HoraFinal;
            evento.EventoCal_Visibilidad = datos.Visibilidad;
        }
    }
}
#pragma warning restore CS1591
