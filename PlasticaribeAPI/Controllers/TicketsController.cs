using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    /// <summary>
    /// Gestión de tickets: creación por parte de los trabajadores, consulta de sus propios tickets
    /// y atención por parte de soporte (roles con permiso a la vista "Gestionar Tickets").
    /// </summary>
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TicketsController : ControllerBase
    {
        private const string VistaGestionTickets = "Gestionar Tickets";
        /// <summary>Estado_Id de un usuario activo en la tabla Usuarios.</summary>
        private const int UsuarioActivo = 1;        /// <summary>Carpeta por defecto, relativa a la raíz de la API, si "Tickets:RutaAdjuntos" no está configurada.</summary>
        private const string RutaAdjuntosPorDefecto = "Archivos\\Tickets_Adjuntos";
        private const int MaxImagenes = 5;
        private const long MaxBytesImagen = 5 * 1024 * 1024;
        /// <summary>Máximo de caracteres de la descripción y de cada comentario (igual al maxlength del front).</summary>
        private const int MaxCaracteresTexto = 2000;
        private static readonly string[] ExtensionesLegado = { ".png", ".jpg", ".jpeg" };

        private readonly dataContext _context;
        private readonly ILogger<TicketsController> _logger;
        private readonly string _rutaAdjuntos;

        /// <summary>
        /// Inicializa el controlador.
        /// </summary>
        /// <param name="context">Contexto de datos.</param>
        /// <param name="configuration">Configuración; usa la clave "Tickets:RutaAdjuntos" para la carpeta de imágenes (absoluta o relativa a la raíz de la API).</param>
        /// <param name="entorno">Entorno de la aplicación; da la raíz para rutas relativas.</param>
        /// <param name="logger">Registro de errores.</param>
        public TicketsController(dataContext context, IConfiguration configuration, IWebHostEnvironment entorno, ILogger<TicketsController> logger)
        {
            _context = context;
            _logger = logger;
            var ruta = configuration["Tickets:RutaAdjuntos"];
            if (string.IsNullOrWhiteSpace(ruta)) ruta = RutaAdjuntosPorDefecto;
            _rutaAdjuntos = Path.GetFullPath(Path.IsPathRooted(ruta) ? ruta : Path.Combine(entorno.ContentRootPath, ruta));
        }

        /// <summary>
        /// Tipos de ticket activos para el dropdown del formulario.
        /// </summary>
        [HttpGet("tipos")]
        public async Task<ActionResult> GetTipos()
        {
            var tipos = await _context.Tickets_Tipos
                .AsNoTracking()
                .Where(t => t.TipoTicket_Activo)
                .OrderBy(t => t.TipoTicket_Id)
                .Select(t => new { Id = t.TipoTicket_Id, Nombre = t.TipoTicket_Nombre })
                .ToListAsync();
            return Ok(tipos);
        }

        /// <summary>
        /// Indica si el usuario autenticado pertenece a soporte (puede gestionar tickets).
        /// </summary>
        [HttpGet("permisos")]
        public async Task<ActionResult> GetPermisos() => Ok(new { EsSoporte = await EsSoporteAsync() });

        /// <summary>
        /// Usuarios activos a cuyo nombre soporte puede registrar un ticket, en orden alfabético.
        /// Solo expone id, nombre y área (la tabla Usuarios tiene datos sensibles).
        /// </summary>
        [HttpGet("reportantes")]
        public async Task<ActionResult> GetReportantes()
        {
            if (!await EsSoporteAsync()) return Forbid();
            var usuarios = await _context.Usuarios
                .AsNoTracking()
                .Where(u => u.Estado_Id == UsuarioActivo)
                .OrderBy(u => u.Usua_Nombre)
                .Select(u => new
                {
                    Id = u.Usua_Id,
                    Nombre = u.Usua_Nombre,
                    Area = u.Area != null ? u.Area.Area_Nombre : "",
                })
                .ToListAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Crea un ticket con imágenes .png/.jpg opcionales. Queda a nombre del usuario autenticado, salvo que
        /// soporte indique un <c>ReportanteId</c>: entonces el ticket es del reportante y se registra quién lo digitó.
        /// </summary>
        /// <returns>201 con el código del ticket; 400 si hay datos o archivos inválidos.</returns>
        [HttpPost]
        [RequestSizeLimit(MaxImagenes * MaxBytesImagen + 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxImagenes * MaxBytesImagen + 1024 * 1024)]
        public async Task<ActionResult> CrearTicket([FromForm] CrearTicketDto datos)
        {
            if (!TryObtenerUsuarioId(out var usuarioId)) return Unauthorized();

            var asunto = datos.Asunto?.Trim() ?? string.Empty;
            var descripcion = datos.Descripcion?.Trim() ?? string.Empty;
            if (asunto.Length == 0) return BadRequest("El asunto es obligatorio.");
            if (asunto.Length > 150) return BadRequest("El asunto no puede superar 150 caracteres.");
            if (descripcion.Length == 0) return BadRequest("La descripción es obligatoria.");
            if (descripcion.Length > MaxCaracteresTexto) return BadRequest($"La descripción no puede superar {MaxCaracteresTexto} caracteres.");
            if (!await _context.Tickets_Tipos.AnyAsync(t => t.TipoTicket_Id == datos.TipoTicketId && t.TipoTicket_Activo))
                return BadRequest("El tipo de ticket no es válido.");

            // Ticket a nombre de otra persona: solo soporte, y el reportante debe estar activo
            long reportanteId = usuarioId;
            string? nombreReportante = null;
            if (datos.ReportanteId.HasValue && datos.ReportanteId.Value != usuarioId)
            {
                if (!await EsSoporteAsync())
                    return BadRequest("Solo soporte puede crear tickets a nombre de otra persona.");
                nombreReportante = await _context.Usuarios
                    .Where(u => u.Usua_Id == datos.ReportanteId.Value && u.Estado_Id == UsuarioActivo)
                    .Select(u => u.Usua_Nombre)
                    .FirstOrDefaultAsync();
                if (nombreReportante == null) return BadRequest("El usuario que reporta no existe o no está activo.");
                reportanteId = datos.ReportanteId.Value;
            }

            var imagenes = datos.Imagenes ?? new List<IFormFile>();
            if (imagenes.Count > MaxImagenes) return BadRequest($"Solo se permiten {MaxImagenes} imágenes por ticket.");

            // Validar todas las imágenes antes de escribir cualquier archivo
            var imagenesValidas = new List<(IFormFile Archivo, string Extension, string TipoContenido)>();
            foreach (var img in imagenes)
            {
                if (img.Length == 0) return BadRequest($"La imagen '{img.FileName}' está vacía.");
                if (img.Length > MaxBytesImagen) return BadRequest($"La imagen '{img.FileName}' supera los 5 MB.");
                var tipo = await DetectarTipoImagenAsync(img);
                if (tipo == null) return BadRequest($"El archivo '{img.FileName}' no es una imagen .png o .jpg válida.");
                imagenesValidas.Add((img, tipo.Value.Extension, tipo.Value.TipoContenido));
            }

            var ahora = DateTime.Now;
            var ticket = new Tickets
            {
                Ticket_Fecha = ahora.Date,
                Ticket_Hora = ahora.ToString("HH:mm:ss"),
                Ticket_FechaCreacion = ahora,
                Usua_Id = reportanteId,
                UsuaRegistra_Id = reportanteId != usuarioId ? usuarioId : null,
                Estado_Id = (int)EstadoTicket.ABIERTO,
                Ticket_Asunto = asunto,
                TipoTicket_Id = datos.TipoTicketId,
                Ticket_Descripcion = descripcion,
            };
            _context.Tickets.Add(ticket);

            // Así el reportante entiende por qué tiene un ticket que no creó
            if (nombreReportante != null)
            {
                _context.Tickets_Revisados.Add(new Tickets_Revisados
                {
                    Tickets = ticket,
                    Usua_Id = usuarioId,
                    TicketRev_Descripcion = $"Ticket registrado por soporte a nombre de {nombreReportante}.",
                    EstadoNuevo_Id = null,
                    TicketRev_Fecha = ahora.Date,
                    TicketRev_Hora = ahora.ToString("HH:mm:ss"),
                });
            }

            // Guardar archivos en disco; si falla la BD se eliminan
            var archivosEscritos = new List<string>();
            try
            {
                foreach (var (archivo, extension, tipoContenido) in imagenesValidas)
                {
                    var rutaRelativa = Path.Combine(ahora.ToString("yyyy"), ahora.ToString("MM"), $"{Guid.NewGuid():N}{extension}");
                    var rutaCompleta = Path.Combine(_rutaAdjuntos, rutaRelativa);
                    Directory.CreateDirectory(Path.GetDirectoryName(rutaCompleta)!);
                    await using (var destino = System.IO.File.Create(rutaCompleta))
                    {
                        await archivo.CopyToAsync(destino);
                    }
                    archivosEscritos.Add(rutaCompleta);

                    _context.Tickets_Adjuntos.Add(new Tickets_Adjuntos
                    {
                        Ticket = ticket,
                        Adj_NombreOriginal = Truncar(Path.GetFileName(archivo.FileName), 255),
                        Adj_RutaRelativa = rutaRelativa,
                        Adj_TipoContenido = tipoContenido,
                        Adj_TamanoBytes = archivo.Length,
                        Adj_Fecha = ahora,
                        Usua_Id = usuarioId,
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                foreach (var ruta in archivosEscritos)
                {
                    try { System.IO.File.Delete(ruta); } catch { /* el archivo huérfano no bloquea la respuesta */ }
                }
                if (ex is UnauthorizedAccessException or IOException)
                {
                    _logger.LogError(ex, "No se pudieron guardar las imágenes del ticket en {Ruta}", _rutaAdjuntos);
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "No se pudieron guardar las imágenes en el servidor. Informe a soporte que revise la carpeta configurada en Tickets:RutaAdjuntos.");
                }
                throw;
            }

            return CreatedAtAction(nameof(GetDetalle), new { id = ticket.Ticket_Id }, new { Codigo = ticket.Ticket_Id, Reportante = nombreReportante });
        }

        /// <summary>
        /// Tickets del usuario autenticado (como reportante) y los que registró a nombre de otros, del más reciente al más antiguo.
        /// </summary>
        [HttpGet("misTickets")]
        public async Task<ActionResult> GetMisTickets()
        {
            if (!TryObtenerUsuarioId(out var usuarioId)) return Unauthorized();
            var tickets = await ProyectarListado(_context.Tickets.AsNoTracking()
                .Where(t => t.Usua_Id == usuarioId || t.UsuaRegistra_Id == usuarioId)).ToListAsync();
            return Ok(tickets);
        }

        /// <summary>
        /// Tickets para soporte. Por defecto solo abiertos y en revisión.
        /// </summary>
        /// <param name="incluirFinalizados">Si es true también devuelve resueltos y cancelados.</param>
        [HttpGet("gestion")]
        public async Task<ActionResult> GetGestion(bool incluirFinalizados = false)
        {
            if (!await EsSoporteAsync()) return Forbid();
            var consulta = _context.Tickets.AsNoTracking();
            if (!incluirFinalizados)
                consulta = consulta.Where(t => t.Estado_Id == (int)EstadoTicket.ABIERTO || t.Estado_Id == (int)EstadoTicket.EN_REVISION);
            return Ok(await ProyectarListado(consulta).ToListAsync());
        }

        /// <summary>
        /// Cantidades para las tarjetas de soporte: abiertos, en revisión y resueltos en el mes actual.
        /// </summary>
        [HttpGet("cantidades")]
        public async Task<ActionResult> GetCantidades()
        {
            if (!await EsSoporteAsync()) return Forbid();
            var hoy = DateTime.Now;
            var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
            var inicioMesSiguiente = inicioMes.AddMonths(1);

            var abiertos = await _context.Tickets.CountAsync(t => t.Estado_Id == (int)EstadoTicket.ABIERTO);
            var enRevision = await _context.Tickets.CountAsync(t => t.Estado_Id == (int)EstadoTicket.EN_REVISION);
            var resueltosMes = await _context.Tickets.CountAsync(t => t.Estado_Id == (int)EstadoTicket.RESUELTO
                && t.Ticket_FechaResolucion >= inicioMes && t.Ticket_FechaResolucion < inicioMesSiguiente);

            return Ok(new { Abiertos = abiertos, EnRevision = enRevision, ResueltosMes = resueltosMes });
        }

        /// <summary>
        /// Detalle de un ticket con sus adjuntos e historial. Solo para el creador o soporte.
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<ActionResult> GetDetalle(long id)
        {
            var (ticket, error) = await ObtenerTicketConAccesoAsync(id);
            if (ticket == null) return error!;

            var info = await ProyectarListado(_context.Tickets.AsNoTracking().Where(t => t.Ticket_Id == id)).FirstAsync();

            var adjuntos = await _context.Tickets_Adjuntos
                .AsNoTracking()
                .Where(a => a.Ticket_Id == id)
                .OrderBy(a => a.Adj_Id)
                .Select(a => new { Nombre = a.Adj_NombreOriginal, Url = $"Tickets/adjuntos/{a.Adj_Id}" })
                .ToListAsync();

            // Tickets anteriores al cambio guardaban las imágenes como "nombre1|nombre2"
            var adjuntosAnteriores = NombresImagenesAnteriores(ticket)
                .Select((nombre, indice) => new { Nombre = nombre, Url = $"Tickets/{id}/adjuntosAnteriores/{indice}" });

            var historial = await _context.Tickets_Revisados
                .AsNoTracking()
                .Where(r => r.Ticket_Id == id)
                .OrderBy(r => r.TicketRev_Id)
                .Select(r => new
                {
                    Id = r.TicketRev_Id,
                    Fecha = r.TicketRev_Fecha,
                    Hora = r.TicketRev_Hora,
                    Usuario = r.Usuario != null ? r.Usuario.Usua_Nombre : "",
                    EstadoId = r.EstadoNuevo_Id,
                    Estado = r.EstadoNuevo != null ? r.EstadoNuevo.Estado_Nombre : null,
                    Comentario = r.TicketRev_Descripcion,
                })
                .ToListAsync();

            return Ok(new
            {
                Ticket = info,
                Descripcion = ticket.Ticket_Descripcion,
                Adjuntos = adjuntos.Concat(adjuntosAnteriores),
                Historial = historial,
            });
        }

        /// <summary>
        /// Devuelve la imagen de un adjunto. Solo para el creador del ticket o soporte.
        /// </summary>
        [HttpGet("adjuntos/{adjuntoId:long}")]
        public async Task<ActionResult> GetAdjunto(long adjuntoId)
        {
            var adjunto = await _context.Tickets_Adjuntos.AsNoTracking().FirstOrDefaultAsync(a => a.Adj_Id == adjuntoId);
            if (adjunto == null) return NotFound();

            var (ticket, error) = await ObtenerTicketConAccesoAsync(adjunto.Ticket_Id);
            if (ticket == null) return error!;

            var rutaCompleta = Path.GetFullPath(Path.Combine(_rutaAdjuntos, adjunto.Adj_RutaRelativa));
            if (!rutaCompleta.StartsWith(_rutaAdjuntos + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)
                || !System.IO.File.Exists(rutaCompleta))
                return NotFound("No se encontró el archivo de la imagen.");

            return PhysicalFile(rutaCompleta, adjunto.Adj_TipoContenido);
        }

        /// <summary>
        /// Devuelve una imagen de un ticket creado antes de la tabla Tickets_Adjuntos.
        /// </summary>
        /// <param name="id">Código del ticket.</param>
        /// <param name="indice">Posición de la imagen dentro de Ticket_NombreImagen.</param>
        [HttpGet("{id:long}/adjuntosAnteriores/{indice:int}")]
        public async Task<ActionResult> GetAdjuntoAnterior(long id, int indice)
        {
            var (ticket, error) = await ObtenerTicketConAccesoAsync(id);
            if (ticket == null) return error!;

            var nombres = NombresImagenesAnteriores(ticket);
            if (indice < 0 || indice >= nombres.Count || string.IsNullOrWhiteSpace(ticket.Ticket_RutaImagen)) return NotFound();

            var nombre = nombres[indice];
            var carpeta = Path.GetFullPath(ticket.Ticket_RutaImagen);
            var rutaCompleta = Path.GetFullPath(Path.Combine(carpeta, nombre));
            var extension = Path.GetExtension(nombre).ToLowerInvariant();
            if (Path.GetDirectoryName(rutaCompleta) != carpeta.TrimEnd(Path.DirectorySeparatorChar)
                || !ExtensionesLegado.Contains(extension)
                || !System.IO.File.Exists(rutaCompleta))
                return NotFound("No se encontró el archivo de la imagen.");

            return PhysicalFile(rutaCompleta, extension == ".png" ? "image/png" : "image/jpeg");
        }

        /// <summary>
        /// Cambia el estado de un ticket y deja el registro en el historial.
        /// Soporte: Abierto → En revisión; Abierto/En revisión → Resuelto o Cancelado; Resuelto/Cancelado → En revisión (reabrir).
        /// Creador: puede cancelar su ticket mientras siga abierto.
        /// </summary>
        [HttpPost("{id:long}/estado")]
        public async Task<ActionResult> CambiarEstado(long id, CambiarEstadoTicketDto datos)
        {
            if (!TryObtenerUsuarioId(out var usuarioId)) return Unauthorized();
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Ticket_Id == id);
            if (ticket == null) return NotFound();

            var esSoporte = await EsSoporteAsync();
            var esCreador = ticket.Usua_Id == usuarioId;
            if (!esSoporte && !esCreador) return NotFound();

            var actual = (EstadoTicket)ticket.Estado_Id;
            var nuevo = (EstadoTicket)datos.EstadoId;
            var comentario = datos.Comentario?.Trim() ?? string.Empty;
            if (comentario.Length > MaxCaracteresTexto) return BadRequest($"El comentario no puede superar {MaxCaracteresTexto} caracteres.");

            bool permitido;
            bool requiereComentario;
            if (esSoporte)
            {
                (permitido, requiereComentario) = (actual, nuevo) switch
                {
                    (EstadoTicket.ABIERTO, EstadoTicket.EN_REVISION) => (true, false),
                    (EstadoTicket.ABIERTO or EstadoTicket.EN_REVISION, EstadoTicket.RESUELTO) => (true, true),
                    (EstadoTicket.ABIERTO or EstadoTicket.EN_REVISION, EstadoTicket.CANCELADO) => (true, true),
                    (EstadoTicket.RESUELTO or EstadoTicket.CANCELADO, EstadoTicket.EN_REVISION) => (true, true),
                    _ => (false, false)
                };
            }
            else
            {
                permitido = actual == EstadoTicket.ABIERTO && nuevo == EstadoTicket.CANCELADO;
                requiereComentario = false;
                if (comentario.Length == 0) comentario = "Cancelado por el usuario que lo creó.";
            }

            if (!permitido)
                return Conflict($"No se puede pasar el ticket #{id} de {NombreEstado(actual)} a {NombreEstado(nuevo)}.");
            if (requiereComentario && comentario.Length == 0)
                return BadRequest("Debe escribir un comentario para este cambio de estado.");

            var ahora = DateTime.Now;
            ticket.Estado_Id = (int)nuevo;
            ticket.Ticket_FechaResolucion = nuevo == EstadoTicket.RESUELTO ? ahora : null;
            _context.Tickets_Revisados.Add(NuevoHistorial(ticket.Ticket_Id, usuarioId, comentario, (int)nuevo, ahora));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Agrega un comentario al historial sin cambiar el estado. Creador o soporte.
        /// </summary>
        [HttpPost("{id:long}/comentarios")]
        public async Task<ActionResult> Comentar(long id, ComentarTicketDto datos)
        {
            if (!TryObtenerUsuarioId(out var usuarioId)) return Unauthorized();
            var (ticket, error) = await ObtenerTicketConAccesoAsync(id);
            if (ticket == null) return error!;

            var comentario = datos.Comentario?.Trim() ?? string.Empty;
            if (comentario.Length > MaxCaracteresTexto) return BadRequest($"El comentario no puede superar {MaxCaracteresTexto} caracteres.");
            if (comentario.Length == 0) return BadRequest("El comentario no puede estar vacío.");

            _context.Tickets_Revisados.Add(NuevoHistorial(id, usuarioId, comentario, null, DateTime.Now));
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ---------------------------------------------------------------- utilidades

        private static IQueryable<object> ProyectarListado(IQueryable<Tickets> consulta) =>
            consulta
                .OrderByDescending(t => t.Ticket_Id)
                .Select(t => new
                {
                    Codigo = t.Ticket_Id,
                    Asunto = t.Ticket_Asunto,
                    Tipo = t.TipoTicket != null ? t.TipoTicket.TipoTicket_Nombre : "",
                    EstadoId = t.Estado_Id,
                    Estado = t.Estado != null ? t.Estado.Estado_Nombre : "",
                    FechaCreacion = t.Ticket_FechaCreacion,
                    FechaResolucion = t.Ticket_FechaResolucion,
                    UsuarioId = t.Usua_Id,
                    Usuario = t.Usuario != null ? t.Usuario.Usua_Nombre : "",
                    Area = t.Usuario != null && t.Usuario.Area != null ? t.Usuario.Area.Area_Nombre : "",
                    RegistradoPor = t.UsuarioRegistra != null ? t.UsuarioRegistra.Usua_Nombre : null,
                });

        private bool TryObtenerUsuarioId(out long usuarioId) =>
            long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out usuarioId);

        /// <summary>Soporte = el rol del token tiene permiso a la vista "Gestionar Tickets" (mismo criterio que el guard del front).</summary>
        private async Task<bool> EsSoporteAsync()
        {
            var rol = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrWhiteSpace(rol)) return false;
            var patron = $"|{rol}|";
            return await _context.Vistas_Permisos.AnyAsync(vp => vp.Vp_Nombre == VistaGestionTickets
                && vp.Vp_Estado == 1
                && vp.Vp_Id_Roles.Contains(patron));
        }

        /// <summary>Carga el ticket si el usuario es su creador o es de soporte; de lo contrario devuelve el error HTTP.</summary>
        private async Task<(Tickets? Ticket, ActionResult? Error)> ObtenerTicketConAccesoAsync(long id)
        {
            if (!TryObtenerUsuarioId(out var usuarioId)) return (null, Unauthorized());
            var ticket = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Ticket_Id == id);
            if (ticket == null) return (null, NotFound());
            if (ticket.Usua_Id != usuarioId && ticket.UsuaRegistra_Id != usuarioId && !await EsSoporteAsync()) return (null, NotFound()); // 404 y no 403: el interceptor del front cierra sesión con 403
            return (ticket, null);
        }

        private static List<string> NombresImagenesAnteriores(Tickets ticket) =>
            string.IsNullOrWhiteSpace(ticket.Ticket_NombreImagen)
                ? new List<string>()
                : ticket.Ticket_NombreImagen.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

        private static Tickets_Revisados NuevoHistorial(long ticketId, long usuarioId, string comentario, int? estadoNuevo, DateTime fecha) => new()
        {
            Ticket_Id = ticketId,
            Usua_Id = usuarioId,
            TicketRev_Descripcion = comentario,
            EstadoNuevo_Id = estadoNuevo,
            TicketRev_Fecha = fecha.Date,
            TicketRev_Hora = fecha.ToString("HH:mm:ss"),
        };

        /// <summary>Identifica PNG o JPEG por su firma binaria, no por la extensión del nombre.</summary>
        private static async Task<(string Extension, string TipoContenido)?> DetectarTipoImagenAsync(IFormFile archivo)
        {
            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
            if (extension is not (".png" or ".jpg" or ".jpeg")) return null;

            var cabecera = new byte[8];
            await using var stream = archivo.OpenReadStream();
            var leidos = await stream.ReadAsync(cabecera.AsMemory(0, cabecera.Length));

            byte[] firmaPng = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            if (leidos == 8 && cabecera.SequenceEqual(firmaPng)) return (".png", "image/png");
            if (leidos >= 3 && cabecera[0] == 0xFF && cabecera[1] == 0xD8 && cabecera[2] == 0xFF) return (".jpg", "image/jpeg");
            return null;
        }

        private static string NombreEstado(EstadoTicket estado) => estado switch
        {
            EstadoTicket.ABIERTO => "ABIERTO",
            EstadoTicket.EN_REVISION => "EN REVISIÓN",
            EstadoTicket.RESUELTO => "RESUELTO",
            EstadoTicket.CANCELADO => "CANCELADO",
            _ => "un estado desconocido"
        };

        private static string Truncar(string texto, int max) => texto.Length <= max ? texto : texto[..max];
    }
}
