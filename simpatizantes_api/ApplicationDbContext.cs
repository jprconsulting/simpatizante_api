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
        public DbSet <ActiveToken> activetokens { get; set; }
        public DbSet<ResultadoPreEliminar> resultadospreeliminares { get; set; }
        public DbSet<PropagandaElectoral> propagandaselectorales { get; set; } 
        public DbSet<Comunidad> comunidades { get; set; } 
        public DbSet<Distrito> distritos { get; set; }
        public DbSet<ActaEscrutinio> actasescrutinios { get; set; }
        public DbSet<Candidatura> candidaturas { get; set; }
        public DbSet<TipoEleccion> tiposelecciones { get; set; }
        public DbSet<TipoAgrupacionPolitica> tiposagrupacionespoliticas { get; set; }
        public DbSet<Combinacion> combinaciones { get; set; }
        public DbSet<DistribucionCandidatura> distribucionescandidaturas { get; set; }
        public DbSet<DistribucionOrdenada> distribucionesordenadas { get; set; }
        public DbSet<ResultadoCandidatura> resultadoscandidaturas { get; set; }
        public DbSet<Simpatizante> simpatizantes { get; set; }
        public DbSet<Genero> generos { get; set; } 
        public DbSet<Operador> operadores { get; set; }
        public DbSet<Candidato> candidatos { get; set; }
        public DbSet<Municipio> municipios { get; set; }
        public DbSet<Estado> estados { get; set; }
        public DbSet<ProgramaSocial> programassociales { get; set; }
        public DbSet<Seccion> secciones { get; set; }
        public DbSet<Incidencia> incidencias { get; set; }
        public DbSet<TipoIncidencia> tiposincidencias { get; set; }
        public DbSet<Rol> rols { get; set; }
        public DbSet<Casilla> casillas { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Visita> visitas { get; set; }
        public DbSet<Claim> claims { get; set; }
        public DbSet<Cargo> cargos { get; set; }
        public DbSet<Voto> votos { get; set; }
        public DbSet<Promotor> promotores { get; set; }
        public DbSet<OperadorSeccion> operadoressecciones { get; set; }
        public DbSet<PromotorOperador> promotoresoperadores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operador>()
                .HasMany(o => o.OperadorSecciones)
                .WithOne(os => os.Operador)
                .HasForeignKey(os => os.OperadorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Promotor>()
                .HasMany(p => p.PromotorOperadores)
                .WithOne(po => po.Promotor)
                .HasForeignKey(os => os.PromotorId)
                .OnDelete(DeleteBehavior.Cascade);
        }



    }
}