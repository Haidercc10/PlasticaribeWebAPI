using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.DTOs;
using PlasticaribeAPI.Models;
using PlasticaribeAPI.Service;
using System.Security.Claims;

namespace PlasticaribeAPI.Controllers
{
    /// <summary>
    /// Ficha complementaria del item: datos que no existen en BagPro (embobinado, código de barras, repeticiones,
    /// guías y largo continuo), una por producto. La captura "Crear Item" y la lee la Orden de Trabajo.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Ficha_Complementaria_ItemController : ControllerBase
    {
        private readonly dataContext _context;

        /// <summary>
        /// Crea el controlador con el contexto de Plasticaribe.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public Ficha_Complementaria_ItemController(dataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ficha complementaria de un item.
        /// </summary>
        /// <param name="prodId">Código del item (Prod_Id).</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>200 con la ficha; 404 si el item no tiene ficha.</returns>
        [HttpGet("{prodId:int}")]
        public async Task<ActionResult<FichaComplementariaItemDto>> GetFicha(int prodId, CancellationToken cancellationToken)
        {
            var ficha = await _context.Ficha_Complementaria_Item.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Prod_Id == prodId, cancellationToken);
            if (ficha == null)
                return Problem($"El item {prodId} no tiene ficha complementaria.", statusCode: StatusCodes.Status404NotFound);
            return Ok(FichaComplementariaItemReglas.ADto(ficha));
        }

        /// <summary>
        /// Crea o actualiza (upsert) la ficha complementaria de un item. Al crear registra el usuario del token y la fecha;
        /// al actualizar conserva al creador y registra al usuario del token como último modificador. Solo roles 1 y 98.
        /// </summary>
        /// <param name="prodId">Código del item (Prod_Id).</param>
        /// <param name="dto">Datos de la ficha. Prod_Id y Usua_Id son opcionales; si se envían deben coincidir con la ruta y el token.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>200 con la ficha guardada; 400 (ValidationProblem) si los datos no son válidos; 401 sin usuario en el token;
        /// 403 si el rol no está autorizado; 404 si el producto no existe; 409 si otro usuario la creó al mismo tiempo.</returns>
        [HttpPut("{prodId:int}")]
        public async Task<ActionResult<FichaComplementariaItemDto>> PutFicha(int prodId, FichaComplementariaItemDto dto, CancellationToken cancellationToken)
        {
            if (!long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var usuarioId))
                return Unauthorized();
            if (!ProductoController.RolesCreanArticuloZeus.Contains(User.FindFirstValue(ClaimTypes.Role) ?? string.Empty))
                return Forbid();

            FichaComplementariaItemReglas.Normalizar(dto);
            var errores = FichaComplementariaItemReglas.Validar(dto, prodId, usuarioId);
            if (errores.Count > 0)
            {
                foreach (var error in errores)
                    foreach (var mensaje in error.Value)
                        ModelState.AddModelError(error.Key, mensaje);
                return ValidationProblem(ModelState);
            }

            if (!await _context.Productos.AnyAsync(p => p.Prod_Id == prodId, cancellationToken))
                return Problem($"El producto {prodId} no existe en Plasticaribe.", statusCode: StatusCodes.Status404NotFound);

            var ficha = await _context.Ficha_Complementaria_Item.FirstOrDefaultAsync(f => f.Prod_Id == prodId, cancellationToken);
            var ahora = DateTime.Now;
            if (ficha == null)
            {
                ficha = new Ficha_Complementaria_Item { Prod_Id = prodId, Usua_Id = usuarioId, Fecha_Creacion = ahora };
                _context.Ficha_Complementaria_Item.Add(ficha);
            }
            else
            {
                ficha.Usua_Modifica = usuarioId;
                ficha.Fecha_Modifica = ahora;
            }
            FichaComplementariaItemReglas.Aplicar(dto, ficha);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException) when (_context.Entry(ficha).State == EntityState.Added)
            {
                // Otro usuario creó la ficha del mismo item entre la consulta y el guardado (choque de PK); otro error se propaga
                if (!await _context.Ficha_Complementaria_Item.AsNoTracking().AnyAsync(f => f.Prod_Id == prodId, cancellationToken))
                    throw;
                return Problem($"La ficha complementaria del item {prodId} fue creada por otro usuario al mismo tiempo. Recargue e intente de nuevo.",
                    statusCode: StatusCodes.Status409Conflict);
            }

            return Ok(FichaComplementariaItemReglas.ADto(ficha));
        }
    }
}
