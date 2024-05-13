using Microsoft.EntityFrameworkCore;

namespace backENDCliente.Modelo
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<MetodosPagos> MetodosPagos { get; set; }
        public DbSet<TypeContact> TypeContact { get; set; }

    }
}
