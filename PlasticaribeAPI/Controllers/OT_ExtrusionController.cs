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
    public class OT_ExtrusionController : ControllerBase
    {
        private readonly dataContext _context;

        public OT_ExtrusionController(dataContext context)
        {
            _context = context;
        }

        // GET: api/OT_Extrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OT_Extrusion>>> GetOT_Extrusion()
        {
          if (_context.OT_Extrusion == null)
          {
              return NotFound();
          }
            return await _context.OT_Extrusion.ToListAsync();
        }

        // GET: api/OT_Extrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OT_Extrusion>> GetOT_Extrusion(int id)
        {
          if (_context.OT_Extrusion == null)
          {
              return NotFound();
          }
            var oT_Extrusion = await _context.OT_Extrusion.FindAsync(id);

            if (oT_Extrusion == null)
            {
                return NotFound();
            }

            return oT_Extrusion;
        }

        // Funcion que consultará los datos en el proceso de extrusion de una orden de trabajo
        [HttpGet("getOT_Extrusion/{ot}")]
        public ActionResult getOt_Extrusion (long ot)
        {
            var con = from ext in _context.Set<OT_Extrusion>()
                      where ext.Ot_Id == ot
                      select ext;
            return Ok(con);
        }

        // PUT: api/OT_Extrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOT_Extrusion(int id, OT_Extrusion oT_Extrusion)
        {
            if (id != oT_Extrusion.Extrusion_Id)
            {
                return BadRequest();
            }

            _context.Entry(oT_Extrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OT_ExtrusionExists(id))
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

        // POST: api/OT_Extrusion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OT_Extrusion>> PostOT_Extrusion(OT_Extrusion oT_Extrusion)
        {
          if (_context.OT_Extrusion == null)
          {
              return Problem("Entity set 'dataContext.OT_Extrusion'  is null.");
          }
            _context.OT_Extrusion.Add(oT_Extrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOT_Extrusion", new { id = oT_Extrusion.Extrusion_Id }, oT_Extrusion);
        }

        // DELETE: api/OT_Extrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOT_Extrusion(int id)
        {
            if (_context.OT_Extrusion == null)
            {
                return NotFound();
            }
            var oT_Extrusion = await _context.OT_Extrusion.FindAsync(id);
            if (oT_Extrusion == null)
            {
                return NotFound();
            }

            _context.OT_Extrusion.Remove(oT_Extrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OT_ExtrusionExists(int id)
        {
            return (_context.OT_Extrusion?.Any(e => e.Extrusion_Id == id)).GetValueOrDefault();
        }
    }
}
