using beneficiarios_dif_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AreaAdscripcion> AreasAdscripcion { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<ProgramaSocial> ProgramasSociales { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Indicador> Indicadores { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<Claim> Claims { get; set; }

    }
}
