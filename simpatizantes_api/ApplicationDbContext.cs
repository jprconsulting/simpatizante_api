using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace simpatizantes_api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Simpatizante> Simpatizantes { get; set; }
        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<ProgramaSocial> ProgramasSociales { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<TipoIncidencia> TiposIncidencias { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Casilla> Casillas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Voto> Votos { get; set; }
        public DbSet<OperadorSeccion> OperadoresSecciones { get; set; }

      

    }
}