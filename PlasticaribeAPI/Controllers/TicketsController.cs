using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Imaging;
using Aspose.Imaging.ImageOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly dataContext _context;

        public TicketsController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tickets>>> GetTickets()
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tickets>> GetTickets(long id)
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            var tickets = await _context.Tickets.FindAsync(id);

            if (tickets == null)
            {
                return NotFound();
            }

            return tickets;
        }

        // Consulta que va a devolver el codigo del siguiente ticket
        [HttpGet("get_IdSigTicket")]
        public ActionResult Get_IdSigTicket()
        {
            var con = (from tk in _context.Set<Tickets>()
                      orderby tk.Ticket_Id descending
                      select tk.Ticket_Id + 1).FirstOrDefault();
            return Ok(con);
        }

        // Consulta que va a devolver la información con la que se llenará el pdf que se enviará despues de crear cada ticket
        [HttpGet("get_InfoTicket_PDF/{codigo}")]
        public ActionResult Get_InfoTicket_PDF(long codigo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from tk in _context.Set<Tickets>()
                      from emp in _context.Set<Empresa>()
                      where tk.Ticket_Id == codigo
                            && emp.Empresa_Id == 800188732
                      select new
                      {
                          Codigo = tk.Ticket_Id,
                          Descripcion = tk.Ticket_Descripcion,
                          Fecha = tk.Ticket_Fecha,
                          Hora = tk.Ticket_Hora,
                          Usuario = tk.Usuario.Usua_Nombre,
                          Estado = tk.Estado.Estado_Nombre,

                          Id_Empresa = emp.Empresa_Id,
                          Nombre_Empresa = emp.Empresa_Nombre,
                          Ciudad_Empresa = emp.Empresa_Ciudad,
                          Direccion_Empresa = emp.Empresa_Direccion,
                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            return Ok(con);
        }

        // Consulta que devolverá la informacion de los tickets que se encuentran abiertos y en revision
        [HttpGet("get_Tickets_AbiertosEnRevision")]
        public ActionResult Get_CantTickets()
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var tickets = from tk in _context.Set<Tickets>()
                           where tk.Estado_Id == 28 
                                 || tk.Estado_Id == 29
                           select new
                           {
                               Codigo = tk.Ticket_Id,
                               Fecha = tk.Ticket_Fecha + " - " + tk.Ticket_Hora,
                               Estado = tk.Estado.Estado_Nombre,
                               Descripcion = tk.Ticket_Descripcion
                           };
            if (tickets.Count() > 0) return Ok(tickets);
            else return BadRequest("No hay tickets por resolver");
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        // Consulta que devolverá la cantidad de los tickets que estan abiertos, en revision y los resuletos en el mes
        [HttpGet("get_CantidadTickets")]
        public ActionResult Get_CantidadTickets() 
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var fecha = DateTime.Today;
            var abiertos = from tk in _context.Set<Tickets>()
                           where tk.Estado_Id == 28
                                 && tk.Ticket_Fecha.Month == fecha.Month
                                 && tk.Ticket_Fecha.Year == fecha.Year
                           group tk by new
                           {
                               tk.Estado.Estado_Nombre,
                               tk.Estado_Id,
                           } into tk
                           select new
                           {
                               Estado = tk.Key.Estado_Nombre,
                               Cantidad = tk.Count(),
                           };

            var EnRevision = from tk in _context.Set<Tickets>()
                           where tk.Estado_Id == 29
                                 && tk.Ticket_Fecha.Month == fecha.Month
                                 && tk.Ticket_Fecha.Year == fecha.Year
                             group tk by new
                           {
                               tk.Estado.Estado_Nombre,
                               tk.Estado_Id,
                           } into tk
                           select new
                           {
                               Estado = tk.Key.Estado_Nombre,
                               Cantidad = tk.Count(),
                           };

            var Resueltos = from tk in _context.Set<Tickets>()
                             where tk.Estado_Id == 30
                                 && tk.Ticket_Fecha.Month == fecha.Month
                                 && tk.Ticket_Fecha.Year == fecha.Year
                            group tk by new
                             {
                                 tk.Estado.Estado_Nombre,
                                 tk.Estado_Id,
                             } into tk
                             select new
                             {
                                 Estado = tk.Key.Estado_Nombre,
                                 Cantidad = tk.Count(),
                             };
            return Ok(abiertos.Concat(EnRevision).Concat(Resueltos));
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        // Consulta que va a devolver la informacion de las imagenes que se adjuntaron al ticket
        [HttpGet("get_ImagenesTicket")]
        public async Task<IActionResult> Get_ImagenesTicket(string file, string filePath)
        {
            filePath = Path.Combine(filePath, file);
            byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return Ok(base64ImageRepresentation);
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTickets(long id, Tickets tickets)
        {
            if (id != tickets.Ticket_Id)
            {
                return BadRequest();
            }

            _context.Entry(tickets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketsExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tickets>> PostTickets(Tickets tickets)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'dataContext.Tickets'  is null.");
          }
            _context.Tickets.Add(tickets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTickets", new { id = tickets.Ticket_Id }, tickets);
        }

        //Creacion de Archivos en Carpeta
        [HttpPost("SubirArchivo")]
        public ActionResult PostArchivo([FromForm] List<IFormFile> archivo) {
            string filePath = "C:\\Users\\SANDRA\\Desktop\\Plasticaribe";
            
            if (filePath != null) {
                try {
                    if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                    if (archivo != null) {
                        if (archivo.Count > 0) {
                            using var stream = System.IO.File.Create(filePath + "\\" + archivo[0].FileName, 100000, FileOptions.Asynchronous);
                            archivo[0].CopyToAsync(stream);

                            using Image image = Image.Load(stream);
                            PngOptions options = new() {
                                CompressionLevel = 9
                            };
                            image.Save(filePath + "\\Tickets\\" + archivo[0].FileName, options);

                        } else return BadRequest("No hay archivos por crear");
                    } else return Ok("Se ha creado el archivo satisfactoriamente");
                } catch (Exception ex) {
                    return BadRequest(ex.Message);
                }
                return Ok(archivo);
            } else return BadRequest("La ruta suministrada para almacenar los archivos no es valida");
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTickets(long id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(tickets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketsExists(long id)
        {
            return (_context.Tickets?.Any(e => e.Ticket_Id == id)).GetValueOrDefault();
        }

        private static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();

#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            if (!provider.TryGetContentType(path, out string contentType))
            {
                contentType = "application/octet-stream";
            }
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            return contentType;
        }
    }
}
