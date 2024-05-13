using backENDCliente.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backENDCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public ContactoController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/contactos")]
        public async Task<IActionResult> GetContactosCliente(int id)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                var contactos = await _context.Contacto.Where(c => c.idCliente == id).ToListAsync();
                return Ok(contactos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/contactos")]
        public async Task<IActionResult> PostContactoCliente(int id, Contacto contacto)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                contacto.idCliente = id; // Asigna el idCliente al contacto
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetContactosCliente", new { id = id }, contacto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{clienteId}/contactos/{contactoId}")]
        public async Task<IActionResult> DeleteContacto(int clienteId, int contactoId)
        {
            try
            {
                var contacto = await _context.Contacto.FindAsync(contactoId);
                if (contacto == null)
                {
                    return NotFound();
                }

                _context.Contacto.Remove(contacto);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{clienteId}/contactos/{contactoId}")]
        public async Task<IActionResult> PutContacto(int clienteId, int contactoId, Contacto contacto)
        {
            if (clienteId != contacto.idCliente || contactoId != contacto.IdContacto)
            {
                return BadRequest("Los IDs de cliente y contacto no coinciden con el cuerpo de la solicitud.");
            }

            try
            {
                _context.Entry(contacto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(contactoId))
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

        private bool ContactoExists(int contactoId)
        {
            return _context.Contacto.Any(c => c.IdContacto == contactoId);
        }

    }
}
