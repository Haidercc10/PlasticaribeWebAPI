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
    public class Solicitud_MateriaPrimaController : ControllerBase
    {
        private readonly dataContext _context;

        public Solicitud_MateriaPrimaController(dataContext context)
        {
            _context = context;
        }

        // GET: api/Solicitud_MateriaPrima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud_MateriaPrima>>> GetSolicitud_MateriaPrima()
        {
          if (_context.Solicitud_MateriaPrima == null)
          {
              return NotFound();
          }
            return await _context.Solicitud_MateriaPrima.ToListAsync();
        }

        // GET: api/Solicitud_MateriaPrima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud_MateriaPrima>> GetSolicitud_MateriaPrima(long id)
        {
          if (_context.Solicitud_MateriaPrima == null)
          {
              return NotFound();
          }
            var solicitud_MateriaPrima = await _context.Solicitud_MateriaPrima.FindAsync(id);

            if (solicitud_MateriaPrima == null)
            {
                return NotFound();
            }

            return solicitud_MateriaPrima;
        }

        // PUT: api/Solicitud_MateriaPrima/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitud_MateriaPrima(long id, Solicitud_MateriaPrima solicitud_MateriaPrima)
        {
            if (id != solicitud_MateriaPrima.Solicitud_Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitud_MateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Solicitud_MateriaPrimaExists(id))
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

        // POST: api/Solicitud_MateriaPrima
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitud_MateriaPrima>> PostSolicitud_MateriaPrima(Solicitud_MateriaPrima solicitud_MateriaPrima)
        {
          if (_context.Solicitud_MateriaPrima == null)
          {
              return Problem("Entity set 'dataContext.Solicitud_MateriaPrima'  is null.");
          }
            _context.Solicitud_MateriaPrima.Add(solicitud_MateriaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitud_MateriaPrima", new { id = solicitud_MateriaPrima.Solicitud_Id }, solicitud_MateriaPrima);
        }

        // DELETE: api/Solicitud_MateriaPrima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud_MateriaPrima(long id)
        {
            if (_context.Solicitud_MateriaPrima == null)
            {
                return NotFound();
            }
            var solicitud_MateriaPrima = await _context.Solicitud_MateriaPrima.FindAsync(id);
            if (solicitud_MateriaPrima == null)
            {
                return NotFound();
            }

            _context.Solicitud_MateriaPrima.Remove(solicitud_MateriaPrima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Solicitud_MateriaPrimaExists(long id)
        {
            return (_context.Solicitud_MateriaPrima?.Any(e => e.Solicitud_Id == id)).GetValueOrDefault();
        }
    }
}
