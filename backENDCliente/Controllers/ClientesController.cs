using backENDCliente.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backENDCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        //Inicializa el controlador con el contexto de la base de datos
        public ClientesController(AplicationDbContext context)
        {
            _context = context;
        }

        //Obtiene todos los clientes de la base de datos.
        [HttpGet("obtenerClientes")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listCliente = await _context.Cliente.Where(c => c.Estatus == "A").ToListAsync();
                return Ok(listCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Guarda un nuevo cliente en la base de datos.
        [HttpPost("guardarClientes")]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            try
            {
                var existingCliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.DPI == cliente.DPI);

                if (existingCliente != null)
                {
                    return BadRequest("DPI REPETIDO");
                }

                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new { id = cliente.idCliente }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para verificar si un DPI existe
        [HttpGet("verificarDPI/{dpi}")]
        public async Task<IActionResult> VerificarDPI(string dpi)
        {
            try
            {
                // Verificar si el DPI existe
                var existingCliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.DPI == dpi);

                if (existingCliente != null)
                {
                    return Ok(new { exists = true, message = "El DPI ya existe." });
                }

                return Ok(new { exists = false, message = "El DPI no existe." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        //Edita un cliente existente en la base de datos, se debe ingresar el id del cliente.
        [HttpPut("editarCliente/{id}")]
        public async Task<IActionResult> Put(int id, Cliente cliente)
        {
            try
            {
                if (id != cliente.idCliente)
                {
                    return BadRequest();
                }

                var clienteItem = await _context.Cliente.FindAsync(id);

                if (clienteItem == null)
                {
                    return NotFound();
                }

                clienteItem.Nombres = cliente.Nombres;
                clienteItem.Nombres = cliente.Nombres;
                clienteItem.Apellidos = cliente.Apellidos;
                clienteItem.Direccion = cliente.Direccion;
                clienteItem.FechaNacimiento = cliente.FechaNacimiento;
                clienteItem.DPI = cliente.DPI;
                clienteItem.NIT = cliente.NIT;
                clienteItem.Empresa = cliente.Empresa;


                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Elimina un cliente de la base de datos, se debe ingresar el id del cliente.
        [HttpDelete("eliminarCliente/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);

                if (cliente == null)
                {
                    return NotFound();
                }

                cliente.Estatus = "I";
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
