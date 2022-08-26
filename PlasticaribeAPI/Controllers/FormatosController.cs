﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;
using PlasticaribeAPI.Models;

namespace PlasticaribeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatosController : ControllerBase
    {
        private readonly dataContext _context;

        public FormatosController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Formatos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formato>>> GetFormato()
        {
          if (_context.Formato == null)
          {
              return NotFound();
          }
            return await _context.Formato.ToListAsync();
        }

        // GET: api/Formatos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Formato>> GetFormato(long id)
        {
          if (_context.Formato == null)
          {
              return NotFound();
          }
            var formato = await _context.Formato.FindAsync(id);

            if (formato == null)
            {
                return NotFound();
            }

            return formato;
        }

        // PUT: api/Formatos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormato(long id, Formato formato)
        {
            if (id != formato.Formato_Id)
            {
                return BadRequest();
            }

            _context.Entry(formato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormatoExists(id))
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

        // POST: api/Formatos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Formato>> PostFormato(Formato formato)
        {
          if (_context.Formato == null)
          {
              return Problem("Entity set 'dataContext.Formato'  is null.");
          }
            _context.Formato.Add(formato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormato", new { id = formato.Formato_Id }, formato);
        }

        // DELETE: api/Formatos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormato(long id)
        {
            if (_context.Formato == null)
            {
                return NotFound();
            }
            var formato = await _context.Formato.FindAsync(id);
            if (formato == null)
            {
                return NotFound();
            }

            _context.Formato.Remove(formato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormatoExists(long id)
        {
            return (_context.Formato?.Any(e => e.Formato_Id == id)).GetValueOrDefault();
        }
    }
}
