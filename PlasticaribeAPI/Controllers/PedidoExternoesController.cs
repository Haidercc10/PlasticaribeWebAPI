﻿#nullable disable
using System;
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
    public class PedidoExternoesController : ControllerBase
    {
        private readonly dataContext _context;

        public PedidoExternoesController(dataContext context)
        {
            _context = context;
        }

        // GET: api/PedidoExternoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoExterno>>> GetPedidos_Externos()
        {
            return await _context.Pedidos_Externos.ToListAsync();
        }

        // GET: api/PedidoExternoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoExterno>> GetPedidoExterno(long id)
        {
            var pedidoExterno = await _context.Pedidos_Externos.FindAsync(id);

            if (pedidoExterno == null)
            {
                return NotFound();
            }

            return pedidoExterno;
        }

        // PUT: api/PedidoExternoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidoExterno(long id, PedidoExterno pedidoExterno)
        {
            if (id != pedidoExterno.PedExt_Id)
            {
                return BadRequest();
            }

            _context.Entry(pedidoExterno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExternoExists(id))
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

        // POST: api/PedidoExternoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PedidoExterno>> PostPedidoExterno(PedidoExterno pedidoExterno)
        {
            _context.Pedidos_Externos.Add(pedidoExterno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoExterno", new { id = pedidoExterno.PedExt_Id }, pedidoExterno);
        }

        // DELETE: api/PedidoExternoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoExterno(long id)
        {
            var pedidoExterno = await _context.Pedidos_Externos.FindAsync(id);
            if (pedidoExterno == null)
            {
                return NotFound();
            }

            _context.Pedidos_Externos.Remove(pedidoExterno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExternoExists(long id)
        {
            return _context.Pedidos_Externos.Any(e => e.PedExt_Id == id);
        }
    }
}
