using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class Bopp_GenericoController : ControllerBase
    {
        private readonly dataContext _context;

        public Bopp_GenericoController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Bopp_Generico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bopp_Generico>>> GetBopp_Generico()
        {
            return await _context.Bopp_Generico.ToListAsync();
        }

        // GET: api/Bopp_Generico/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bopp_Generico>> GetBopp_Generico(long id)
        {
            var bopp_Generico = await _context.Bopp_Generico.FindAsync(id);

            if (bopp_Generico == null)
            {
                return NotFound();
            }

            return bopp_Generico;
        }

        // PUT: api/Bopp_Generico/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBopp_Generico(long id, Bopp_Generico bopp_Generico)
        {
            if (id != bopp_Generico.BoppGen_Id)
            {
                return BadRequest();
            }

            _context.Entry(bopp_Generico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Bopp_GenericoExists(id))
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

        [HttpPut("putPrecioEstandar/{id}/{precioEstandar}")]
        public async Task<IActionResult> PutPrecioEstandar(long id, decimal precioEstandar)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var bopp = (from bp in _context.Set<Bopp_Generico>() where bp.BoppGen_Id == id select bp).FirstOrDefault();
            bopp.BoppGen_PrecioEstandar = precioEstandar;

            _context.Entry(bopp).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!Bopp_GenericoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // POST: api/Bopp_Generico
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bopp_Generico>> PostBopp_Generico(Bopp_Generico bopp_Generico)
        {
            _context.Bopp_Generico.Add(bopp_Generico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBopp_Generico", new { id = bopp_Generico.BoppGen_Id }, bopp_Generico);
        }

        // DELETE: api/Bopp_Generico/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBopp_Generico(long id)
        {
            var bopp_Generico = await _context.Bopp_Generico.FindAsync(id);
            if (bopp_Generico == null)
            {
                return NotFound();
            }

            _context.Bopp_Generico.Remove(bopp_Generico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Bopp_GenericoExists(long id)
        {
            return _context.Bopp_Generico.Any(e => e.BoppGen_Id == id);
        }
    }
}
