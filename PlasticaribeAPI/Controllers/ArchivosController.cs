using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly dataContext _context;

        public ArchivosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Archivos
        [HttpGet]
        public ActionResult get()
        {
            try
            {
                return Ok(_context.Archivos.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Archivos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostArchivo([FromForm] List<IFormFile> archivo, DateTime Fecha, int categoria_Id, long usua_Id, string? carpeta = "")
        {
            List<Archivos> archivos = new List<Archivos>();
            try
            {
                var filePath = "C:\\ArchivosPlasticaribe\\" + carpeta;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (archivo.Count > 0)
                {
                    foreach (var item in archivo)
                    {
                        
                        if (carpeta == "")
                        {
                            var crearArchivo = filePath + item.FileName;

                            using (var stream = System.IO.File.Create(crearArchivo))
                            {
                                item.CopyToAsync(stream);
                            }
                            Archivos archivo2 = new Archivos();
                            archivo2.Nombre = item.FileName;
                            archivo2.Ubicacion = crearArchivo;
                            archivo2.Fecha = Fecha;
                            archivo2.Categoria_Id = categoria_Id;
                            archivo2.Usua_Id = usua_Id;
                            archivos.Add(archivo2);
                        }
                        else
                        {
                            var crearArchivo = filePath + "\\" + item.FileName;

                            using (var stream = System.IO.File.Create(crearArchivo))
                            {
                                item.CopyToAsync(stream);
                            }
                            Archivos archivo2 = new Archivos();
                            archivo2.Nombre = item.FileName;
                            archivo2.Ubicacion = crearArchivo;
                            archivo2.Fecha = Fecha;
                            archivo2.Categoria_Id = categoria_Id;
                            archivo2.Usua_Id = usua_Id;
                            archivos.Add(archivo2);
                        }
                    }
                    _context.Archivos.AddRange(archivos);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(archivo);
        }

        [HttpGet]
        [Route("/download/")]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            var filePath = "C:\\Users\\SANDRA\\Documents\\Base de datos PlastiCaribe\\Archivos\\" + file;
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), file);
            
        }

        private string GetContentType (string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType)){
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
