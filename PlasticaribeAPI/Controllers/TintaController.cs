using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TintaController : ControllerBase
    {
        private readonly dataContext _context;

        public TintaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tinta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tinta>>> GetTintas()
        {
            if (_context.Tintas == null)
            {
                return NotFound();
            }
            return await _context.Tintas.ToListAsync();
        }

        // GET: api/Tinta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tinta>> GetTinta(long id)
        {
            if (_context.Tintas == null)
            {
                return NotFound();
            }
            var tinta = await _context.Tintas.FindAsync(id);

            if (tinta == null)
            {
                return NotFound();
            }

            return tinta;
        }

        [HttpGet("TintasColores")]
        public ActionResult GetTintaColor()
        {
            if (_context.Tintas == null)
            {
                return NotFound();
            }
            var tinta = _context.Tintas.ToList();

            if (tinta == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tinta);
            }

            //return tinta;
        }

        //Consulta que traerá las categorias de materia prima de la tabla Tintas
        [HttpGet("getCategoriasTintas")]
        public ActionResult GetCategoriasTintas()
        {
            var con = from tt in _context.Set<Tinta>()
                      group tt by new
                      {
                          tt.CatMP_Id
                      } into tt
                      select tt.Key.CatMP_Id;
            return Ok(con);
        }

        [HttpGet("consultaImpresion/{Tinta_Nombre1}/{Tinta_Nombre2}/{Tinta_Nombre3}/{Tinta_Nombre4}/{Tinta_Nombre5}/{Tinta_Nombre6}/{Tinta_Nombre7}/{Tinta_Nombre8}")]
        public ActionResult GetConsultaImpresion(string Tinta_Nombre1, string Tinta_Nombre2, string Tinta_Nombre3, string Tinta_Nombre4, string Tinta_Nombre5, string Tinta_Nombre6, string Tinta_Nombre7, string Tinta_Nombre8)
        {
            var con = from t1 in _context.Set<Tinta>()
                      from t2 in _context.Set<Tinta>()
                      from t3 in _context.Set<Tinta>()
                      from t4 in _context.Set<Tinta>()
                      from t5 in _context.Set<Tinta>()
                      from t6 in _context.Set<Tinta>()
                      from t7 in _context.Set<Tinta>()
                      from t8 in _context.Set<Tinta>()
                      where t1.Tinta_Nombre == Tinta_Nombre1
                            && t2.Tinta_Nombre == Tinta_Nombre2
                            && t3.Tinta_Nombre == Tinta_Nombre3
                            && t4.Tinta_Nombre == Tinta_Nombre4
                            && t5.Tinta_Nombre == Tinta_Nombre5
                            && t6.Tinta_Nombre == Tinta_Nombre6
                            && t7.Tinta_Nombre == Tinta_Nombre7
                            && t8.Tinta_Nombre == Tinta_Nombre8
                      select new
                      {
                          Tinta_Id1 = t1.Tinta_Id,
                          Tinta_Nombre1 = t1.Tinta_Nombre,
                          Tinta_Id2 = t2.Tinta_Id,
                          Tinta_Nombre2 = t2.Tinta_Nombre,
                          Tinta_Id3 = t3.Tinta_Id,
                          Tinta_Nombre3 = t3.Tinta_Nombre,
                          Tinta_Id4 = t4.Tinta_Id,
                          Tinta_Nombre4 = t4.Tinta_Nombre,
                          Tinta_Id5 = t5.Tinta_Id,
                          Tinta_Nombre5 = t5.Tinta_Nombre,
                          Tinta_Id6 = t6.Tinta_Id,
                          Tinta_Nombre6 = t6.Tinta_Nombre,
                          Tinta_Id7 = t7.Tinta_Id,
                          Tinta_Nombre7 = t7.Tinta_Nombre,
                          Tinta_Id8 = t8.Tinta_Id,
                          Tinta_Nombre8 = t8.Tinta_Nombre,
                      };

            return Ok(con);
        }

        // PUT: api/Tinta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTinta(long id, Tinta tinta)
        {
            if (id != tinta.Tinta_Id)
            {
                return BadRequest();
            }

            _context.Entry(tinta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TintaExists(id))
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

        // POST: api/Tinta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tinta>> PostTinta(Tinta tinta)
        {
            if (_context.Tintas == null)
            {
                return Problem("Entity set 'dataContext.Tintas'  is null.");
            }
            _context.Tintas.Add(tinta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTinta", new { id = tinta.Tinta_Id }, tinta);
        }

        /// <summary>
        /// Crea una tinta, chip o solvente validando los datos (a diferencia del POST genérico, que guarda lo que reciba).
        /// Responde 400 si falta un dato o no es válido y 409 si ya existe una tinta con el mismo nombre.
        /// </summary>
        [HttpPost("crear")]
        public async Task<ActionResult> CrearTinta(CrearTintaRequest request)
        {
            var nombre = (request.Nombre ?? string.Empty).Trim().ToUpper();
            var descripcion = string.IsNullOrWhiteSpace(request.Descripcion) ? nombre : request.Descripcion.Trim().ToUpper();
            var codigo = (request.CodigoHexadecimal ?? string.Empty).Trim().TrimStart('#').ToUpper();
            var unidad = (request.UndMed_Id ?? string.Empty).Trim();
            if (nombre.Length == 0) return BadRequest("El nombre de la tinta es obligatorio.");
            if (request.Precio < 0) return BadRequest("El precio no puede ser negativo.");
            if (codigo.Length > 0 && !System.Text.RegularExpressions.Regex.IsMatch(codigo, "^([0-9A-F]{3}|[0-9A-F]{6}|[0-9A-F]{8})$"))
                return BadRequest("El código hexadecimal no es válido (3, 6 u 8 dígitos entre 0-9 y A-F).");
            if (!Materia_PrimaController.CategoriasTintas.Contains(request.CatMP_Id)) return BadRequest("La categoría debe ser de tintas, chips o solventes.");
            if (unidad.Length == 0 || !await _context.Unidades_Medidas.AnyAsync(u => u.UndMed_Id == unidad)) return BadRequest("La unidad de medida no es válida.");
            if (request.SubCatMP_Id == null) return BadRequest("La subcategoría es obligatoria.");
            if (!await _context.Subcategorias_MatPrima.AnyAsync(s => s.SubCatMP_Id == request.SubCatMP_Id && s.CatMP_Id == request.CatMP_Id))
                return BadRequest("La subcategoría no pertenece a la categoría elegida.");

            var existente = await _context.Tintas.AsNoTracking()
                .Where(t => t.Tinta_Nombre.Trim().ToUpper() == nombre)
                .Select(t => new { t.Tinta_Id })
                .FirstOrDefaultAsync();
            if (existente != null) return Conflict($"Ya existe una tinta con el nombre {nombre} (Id {existente.Tinta_Id}).");

            var ahora = DateTime.Now;
            var tinta = new Tinta
            {
                Tinta_Nombre = nombre,
                Tinta_Descripcion = descripcion,
                Tinta_CodigoHexadecimal = codigo,
                Tinta_Stock = 0,
                UndMed_Id = unidad,
                Tinta_Precio = request.Precio,
                CatMP_Id = request.CatMP_Id,
                TpBod_Id = 5,
                Tinta_InvInicial = 0,
                Tinta_FechaIngreso = ahora.Date,
                Tinta_Hora = ahora.ToString("H:mm:ss"),
                SubCatMP_Id = request.SubCatMP_Id,
            };
            _context.Tintas.Add(tinta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTinta", new { id = tinta.Tinta_Id }, tinta);
        }

        // DELETE: api/Tinta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTinta(long id)
        {
            if (_context.Tintas == null)
            {
                return NotFound();
            }
            var tinta = await _context.Tintas.FindAsync(id);
            if (tinta == null)
            {
                return NotFound();
            }

            _context.Tintas.Remove(tinta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TintaExists(long id)
        {
            return (_context.Tintas?.Any(e => e.Tinta_Id == id)).GetValueOrDefault();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
