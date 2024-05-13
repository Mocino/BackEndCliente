using backENDCliente.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backENDCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposContacto : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public TiposContacto (AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("obtenerTiposContacto")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listTypeContact = await _context.TypeContact.ToListAsync();
                return Ok(listTypeContact);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
