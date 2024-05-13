﻿using backENDCliente.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backENDCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodosPagosController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public MetodosPagosController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/getMetodosPagos")]
        public async Task<IActionResult> GetMetodosPago(int id)
        {
            try
            {
                // Busca el cliente por su ID
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                // Obtiene todos los métodos de pago asociados al cliente
                var metodospagos = await _context.MetodosPagos
                    .Where(c => c.idCliente == id)
                    .ToListAsync();
                return Ok(metodospagos);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{id}/MetodosPagos")]
        public async Task<IActionResult> PostMetodoPagoCliente(int id, MetodosPagos metodoPago)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                metodoPago.idCliente = id; // Asigna el idCliente al método de pago
                _context.Add(metodoPago);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetMetodosPago", new { id = id }, metodoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{clienteId}/MetodosPagos/{metodoPagoId}")]
        public async Task<IActionResult> PutMetodoPago(int clienteId, int metodoPagoId, MetodosPagos metodoPago)
        {
            if (clienteId != metodoPago.idCliente || metodoPagoId != metodoPago.idMetodoPago)
            {
                return BadRequest("Los IDs de cliente y método de pago no coinciden con el cuerpo de la solicitud.");
            }

            try
            {
                _context.Entry(metodoPago).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodoPagoExists(metodoPagoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool MetodoPagoExists(int metodoPagoId)
        {
            return _context.MetodosPagos.Any(c => c.idMetodoPago == metodoPagoId);
        }

        [HttpDelete("{clienteId}/MetodosPagos/{metodoPagoId}")]
        public async Task<IActionResult> DeleteMetodoPago(int clienteId, int metodoPagoId)
        {
            try
            {
                var metodoPago = await _context.MetodosPagos.FindAsync(metodoPagoId);
                if (metodoPago == null)
                {
                    return NotFound();
                }

                _context.MetodosPagos.Remove(metodoPago);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //soy un comentario
    }
}
