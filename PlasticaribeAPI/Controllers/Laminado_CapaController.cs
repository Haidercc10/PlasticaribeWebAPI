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
    public class Laminado_CapaController : ControllerBase
    {
        private readonly dataContext _context;

        public Laminado_CapaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Laminado_Capa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laminado_Capa>>> GetLaminado_Capa()
        {
          if (_context.Laminado_Capa == null)
          {
              return NotFound();
          }
            return await _context.Laminado_Capa.ToListAsync();
        }

        // GET: api/Laminado_Capa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laminado_Capa>> GetLaminado_Capa(int id)
        {
          if (_context.Laminado_Capa == null)
          {
              return NotFound();
          }
            var laminado_Capa = await _context.Laminado_Capa.FindAsync(id);

            if (laminado_Capa == null)
            {
                return NotFound();
            }

            return laminado_Capa;
        }

        // PUT: api/Laminado_Capa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaminado_Capa(int id, Laminado_Capa laminado_Capa)
        {
            if (id != laminado_Capa.LamCapa_Id)
            {
                return BadRequest();
            }

            _context.Entry(laminado_Capa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Laminado_CapaExists(id))
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

        // POST: api/Laminado_Capa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laminado_Capa>> PostLaminado_Capa(Laminado_Capa laminado_Capa)
        {
          if (_context.Laminado_Capa == null)
          {
              return Problem("Entity set 'dataContext.Laminado_Capa'  is null.");
          }
            _context.Laminado_Capa.Add(laminado_Capa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaminado_Capa", new { id = laminado_Capa.LamCapa_Id }, laminado_Capa);
        }

        // DELETE: api/Laminado_Capa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaminado_Capa(int id)
        {
            if (_context.Laminado_Capa == null)
            {
                return NotFound();
            }
            var laminado_Capa = await _context.Laminado_Capa.FindAsync(id);
            if (laminado_Capa == null)
            {
                return NotFound();
            }

            _context.Laminado_Capa.Remove(laminado_Capa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Laminado_CapaExists(int id)
        {
            return (_context.Laminado_Capa?.Any(e => e.LamCapa_Id == id)).GetValueOrDefault();
        }
    }
}
