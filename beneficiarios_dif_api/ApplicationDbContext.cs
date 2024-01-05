using beneficiarios_dif_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Votante> Votantes { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Indicador> Indicadores { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<Claim> Claims { get; set; }

    }
}