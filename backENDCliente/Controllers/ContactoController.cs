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

        // Obtiene todos los contactos de un cliente específico.
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

                var contactos = await _context.Contacto.Where(c => c.idCliente == id && c.Estatus == "A" ).ToListAsync();
                return Ok(contactos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Crea un nuevo contacto para un cliente específico.
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

        // Elimina un contacto de un cliente específico.
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

                contacto.Estatus = "I";
                _context.Contacto.Update(contacto);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Actualiza un contacto existente de un cliente específico.
        [HttpPut("{clienteId}/contactos/{contactoId}")]
        public async Task<IActionResult> PutContacto(int clienteId, int contactoId, Contacto contacto)
        {
            // Verifica si los IDs de cliente y contacto coinciden con el cuerpo de la solicitud
            if (clienteId != contacto.idCliente || contactoId != contacto.IdContacto)
            {
                return BadRequest("Los IDs de cliente y contacto no coinciden con el cuerpo de la solicitud.");
            }

            try
            {
                // Actualiza el contacto en la base de datos
                _context.Entry(contacto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verifica si el contacto existe
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

        // Verifica si un contacto existe en la base de datos.
        private bool ContactoExists(int contactoId)
        {
            return _context.Contacto.Any(c => c.IdContacto == contactoId);
        }


        // Método para verificar si un DPI existe
        [HttpGet("verificarEmail/{email}/{idcliente}")]
        public async Task<IActionResult> VerificarDPI(string email, int idcliente)
        {
            try
            {
                // Verificar si un email ya existe
                var existingEmail = await _context.Contacto
                    .FirstOrDefaultAsync(c => c.ValorContacto == email && c.idCliente == idcliente);

                if (existingEmail != null)
                {
                    return Ok(new { exists = true, message = "El email ya existe." });
                }

                return Ok(new { exists = false, message = "El email no existe." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("verificarEmail/{email}")]
        public async Task<IActionResult> VerificarEmail(string email)
        {
            try
            {
                // Verificar si un email ya existe en la base de datos
                var existingEmail = await _context.Contacto
                    .FirstOrDefaultAsync(c => c.ValorContacto == email);

                if (existingEmail != null)
                {
                    return Ok(new { exists = true, message = "El email ya existe." });
                }

                return Ok(new { exists = false, message = "El email no existe." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
