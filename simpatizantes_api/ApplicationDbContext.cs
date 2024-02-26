﻿using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace simpatizantes_api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<ActaEscrutinio> ActasEscrutinios { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<TipoEleccion> TiposElecciones { get; set; }
        public DbSet<TipoAgrupacionPolitica> TiposAgrupacionesPoliticas { get; set; }
        public DbSet<Combinacion> Combinaciones { get; set; }
        public DbSet<DistribucionCandidatura> DistribucionesCandidaturas { get; set; }
        public DbSet<DistribucionOrdenada> DistribucionesOrdenadas { get; set; }
        public DbSet<ResultadoCandidatura> ResultadosCandidaturas { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Simpatizante> Simpatizantes { get; set; }
        public DbSet<Genero> Generos { get; set; }
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
        public DbSet<Promotor> Promotores { get; set; }
        public DbSet<OperadorSeccion> OperadoresSecciones { get; set; }
        public DbSet<PromotorOperador> PromotoresOperadores { get; set; }

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