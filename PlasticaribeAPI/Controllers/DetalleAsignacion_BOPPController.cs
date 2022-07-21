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
    public class DetalleAsignacion_BOPPController : ControllerBase
    {
        private readonly dataContext _context;

        public DetalleAsignacion_BOPPController(dataContext context)
        {
            _context = context;
        }

        // GET: api/DetalleAsignacion_BOPP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleAsignacion_BOPP>>> GetDetallesAsignaciones_BOPP()
        {
          if (_context.DetallesAsignaciones_BOPP == null)
          {
              return NotFound();
          }
            return await _context.DetallesAsignaciones_BOPP.ToListAsync();
        }

        // GET: api/DetalleAsignacion_BOPP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleAsignacion_BOPP>> GetDetalleAsignacion_BOPP(long id)
        {
          if (_context.DetallesAsignaciones_BOPP == null)
          {
              return NotFound();
          }
            var detalleAsignacion_BOPP = await _context.DetallesAsignaciones_BOPP.FindAsync(id);

            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            return detalleAsignacion_BOPP;
        }

        // PUT: api/DetalleAsignacion_BOPP/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleAsignacion_BOPP(long id, DetalleAsignacion_BOPP detalleAsignacion_BOPP)
        {
            if (id != detalleAsignacion_BOPP.AsigBOPP_Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleAsignacion_BOPP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleAsignacion_BOPPExists(id))
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

        // POST: api/DetalleAsignacion_BOPP
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleAsignacion_BOPP>> PostDetalleAsignacion_BOPP(DetalleAsignacion_BOPP detalleAsignacion_BOPP)
        {
          if (_context.DetallesAsignaciones_BOPP == null)
          {
              return Problem("Entity set 'dataContext.DetallesAsignaciones_BOPP'  is null.");
          }
            _context.DetallesAsignaciones_BOPP.Add(detalleAsignacion_BOPP);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleAsignacion_BOPPExists(detalleAsignacion_BOPP.AsigBOPP_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleAsignacion_BOPP", new { id = detalleAsignacion_BOPP.AsigBOPP_Id }, detalleAsignacion_BOPP);
        }

        // DELETE: api/DetalleAsignacion_BOPP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleAsignacion_BOPP(long id)
        {
            if (_context.DetallesAsignaciones_BOPP == null)
            {
                return NotFound();
            }
            var detalleAsignacion_BOPP = await _context.DetallesAsignaciones_BOPP.FindAsync(id);
            if (detalleAsignacion_BOPP == null)
            {
                return NotFound();
            }

            _context.DetallesAsignaciones_BOPP.Remove(detalleAsignacion_BOPP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleAsignacion_BOPPExists(long id)
        {
            return (_context.DetallesAsignaciones_BOPP?.Any(e => e.AsigBOPP_Id == id)).GetValueOrDefault();
        }
    }
}