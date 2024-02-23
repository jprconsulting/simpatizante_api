﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using simpatizantes_api;

#nullable disable

namespace simpatizantesapi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("simpatizantes_api.Entities.Candidato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("longtext");

                    b.Property<int?>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Emblema")
                        .HasColumnType("longtext");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaHoraCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaHoraEdicion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Foto")
                        .HasColumnType("longtext");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .HasColumnType("longtext");

                    b.Property<string>("Sobrenombre")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioCreacionNombre")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioEdicionNombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("GeneroId");

                    b.ToTable("Candidatos");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Casilla", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Casillas");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<bool>("ClaimValue")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Incidencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CasillaId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("longtext");

                    b.Property<string>("Foto")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Latitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Longitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Retroalimentacion")
                        .HasColumnType("longtext");

                    b.Property<int?>("TipoIncidenciaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CasillaId");

                    b.HasIndex("TipoIncidenciaId");

                    b.ToTable("Incidencias");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Operador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("longtext");

                    b.Property<int?>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaHoraCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaHoraEdicion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombres")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioCreacionNombre")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioEdicionNombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CandidatoId");

                    b.ToTable("Operadores");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.OperadorSeccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("OperadorId")
                        .HasColumnType("int");

                    b.Property<int>("SeccionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperadorId");

                    b.HasIndex("SeccionId");

                    b.ToTable("OperadoresSecciones");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.ProgramaSocial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ProgramasSociales");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Promotor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaHoraCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaHoraEdicion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombres")
                        .HasColumnType("longtext");

                    b.Property<string>("Telefono")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioCreacionNombre")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioEdicionNombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Promotores");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.PromotorOperador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("OperadorId")
                        .HasColumnType("int");

                    b.Property<int>("PromotorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperadorId");

                    b.HasIndex("PromotorId");

                    b.ToTable("PromotoresOperadores");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreRol")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Rols");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Seccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Clave")
                        .HasColumnType("longtext");

                    b.Property<int?>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.ToTable("Secciones");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Simpatizante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("CURP")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaveElector")
                        .HasColumnType("longtext");

                    b.Property<string>("Domicilio")
                        .HasColumnType("longtext");

                    b.Property<int?>("EstadoId")
                        .HasColumnType("int");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaHoraCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaHoraEdicion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("int");

                    b.Property<decimal>("Latitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Longitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .HasColumnType("longtext");

                    b.Property<string>("Numerotel")
                        .HasColumnType("longtext");

                    b.Property<int>("OperadorId")
                        .HasColumnType("int");

                    b.Property<int?>("ProgramaSocialId")
                        .HasColumnType("int");

                    b.Property<int?>("PromotorId")
                        .HasColumnType("int");

                    b.Property<int?>("SeccionId")
                        .HasColumnType("int");

                    b.Property<string>("TercerNivelContacto")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioCreacionNombre")
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioEdicionNombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("GeneroId");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("OperadorId");

                    b.HasIndex("ProgramaSocialId");

                    b.HasIndex("PromotorId");

                    b.HasIndex("SeccionId");

                    b.ToTable("Simpatizantes");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.TipoIncidencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("longtext");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TiposIncidencias");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.UserSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LastAccessTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("longtext");

                    b.Property<int?>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .HasColumnType("longtext");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int?>("OperadorId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int?>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidatoId")
                        .IsUnique();

                    b.HasIndex("OperadorId")
                        .IsUnique();

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Visita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaHoraVisita")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Foto")
                        .HasColumnType("longtext");

                    b.Property<string>("Servicio")
                        .HasColumnType("longtext");

                    b.Property<bool>("Simpatiza")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("SimpatizanteId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SimpatizanteId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Visitas");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Voto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("EstatusVoto")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaHoraVot")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Foto")
                        .HasColumnType("longtext");

                    b.Property<int?>("SimpatizanteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SimpatizanteId");

                    b.ToTable("Votos");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Candidato", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Cargo", "Cargo")
                        .WithMany("Candidatos")
                        .HasForeignKey("CargoId");

                    b.HasOne("simpatizantes_api.Entities.Genero", "Genero")
                        .WithMany("Candidato")
                        .HasForeignKey("GeneroId");

                    b.Navigation("Cargo");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Claim", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Rol", "Rol")
                        .WithMany("Claims")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Incidencia", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Casilla", "Casilla")
                        .WithMany("Incidencias")
                        .HasForeignKey("CasillaId");

                    b.HasOne("simpatizantes_api.Entities.TipoIncidencia", "TipoIncidencia")
                        .WithMany("Incidencias")
                        .HasForeignKey("TipoIncidenciaId");

                    b.Navigation("Casilla");

                    b.Navigation("TipoIncidencia");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Municipio", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Estado", "Estado")
                        .WithMany("Municipios")
                        .HasForeignKey("EstadoId");

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Operador", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Candidato", "Candidato")
                        .WithMany("Operador")
                        .HasForeignKey("CandidatoId");

                    b.Navigation("Candidato");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.OperadorSeccion", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Operador", "Operador")
                        .WithMany("OperadorSecciones")
                        .HasForeignKey("OperadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("simpatizantes_api.Entities.Seccion", "Seccion")
                        .WithMany("OperadorSecciones")
                        .HasForeignKey("SeccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operador");

                    b.Navigation("Seccion");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.PromotorOperador", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Operador", "Operador")
                        .WithMany("PromotorOperadores")
                        .HasForeignKey("OperadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("simpatizantes_api.Entities.Promotor", "Promotor")
                        .WithMany("PromotorOperadores")
                        .HasForeignKey("PromotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operador");

                    b.Navigation("Promotor");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Seccion", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Municipio", "Municipio")
                        .WithMany("Secciones")
                        .HasForeignKey("MunicipioId");

                    b.Navigation("Municipio");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Simpatizante", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Estado", "Estado")
                        .WithMany("Simpatizantes")
                        .HasForeignKey("EstadoId");

                    b.HasOne("simpatizantes_api.Entities.Genero", "Genero")
                        .WithMany("Simpatizante")
                        .HasForeignKey("GeneroId");

                    b.HasOne("simpatizantes_api.Entities.Municipio", "Municipio")
                        .WithMany("Simpatizantes")
                        .HasForeignKey("MunicipioId");

                    b.HasOne("simpatizantes_api.Entities.Operador", "Operador")
                        .WithMany("Simpatizantes")
                        .HasForeignKey("OperadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("simpatizantes_api.Entities.ProgramaSocial", "ProgramaSocial")
                        .WithMany("Simpatizantes")
                        .HasForeignKey("ProgramaSocialId");

                    b.HasOne("simpatizantes_api.Entities.Promotor", "Promotor")
                        .WithMany("Simpatizantes")
                        .HasForeignKey("PromotorId");

                    b.HasOne("simpatizantes_api.Entities.Seccion", "Seccion")
                        .WithMany("Simpatizantes")
                        .HasForeignKey("SeccionId");

                    b.Navigation("Estado");

                    b.Navigation("Genero");

                    b.Navigation("Municipio");

                    b.Navigation("Operador");

                    b.Navigation("ProgramaSocial");

                    b.Navigation("Promotor");

                    b.Navigation("Seccion");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.UserSession", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Usuario", null)
                        .WithMany("Sessions")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Usuario", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Candidato", "Candidato")
                        .WithOne("Usuario")
                        .HasForeignKey("simpatizantes_api.Entities.Usuario", "CandidatoId");

                    b.HasOne("simpatizantes_api.Entities.Operador", "Operador")
                        .WithOne("Usuario")
                        .HasForeignKey("simpatizantes_api.Entities.Usuario", "OperadorId");

                    b.HasOne("simpatizantes_api.Entities.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId");

                    b.Navigation("Candidato");

                    b.Navigation("Operador");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Visita", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Simpatizante", "Simpatizante")
                        .WithMany("Visitas")
                        .HasForeignKey("SimpatizanteId");

                    b.HasOne("simpatizantes_api.Entities.Usuario", "Usuario")
                        .WithMany("Visitas")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Simpatizante");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Voto", b =>
                {
                    b.HasOne("simpatizantes_api.Entities.Simpatizante", "Simpatizante")
                        .WithMany("Votos")
                        .HasForeignKey("SimpatizanteId");

                    b.Navigation("Simpatizante");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Candidato", b =>
                {
                    b.Navigation("Operador");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Cargo", b =>
                {
                    b.Navigation("Candidatos");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Casilla", b =>
                {
                    b.Navigation("Incidencias");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Estado", b =>
                {
                    b.Navigation("Municipios");

                    b.Navigation("Simpatizantes");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Genero", b =>
                {
                    b.Navigation("Candidato");

                    b.Navigation("Simpatizante");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Municipio", b =>
                {
                    b.Navigation("Secciones");

                    b.Navigation("Simpatizantes");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Operador", b =>
                {
                    b.Navigation("OperadorSecciones");

                    b.Navigation("PromotorOperadores");

                    b.Navigation("Simpatizantes");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.ProgramaSocial", b =>
                {
                    b.Navigation("Simpatizantes");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Promotor", b =>
                {
                    b.Navigation("PromotorOperadores");

                    b.Navigation("Simpatizantes");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Rol", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Seccion", b =>
                {
                    b.Navigation("OperadorSecciones");

                    b.Navigation("Simpatizantes");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Simpatizante", b =>
                {
                    b.Navigation("Visitas");

                    b.Navigation("Votos");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.TipoIncidencia", b =>
                {
                    b.Navigation("Incidencias");
                });

            modelBuilder.Entity("simpatizantes_api.Entities.Usuario", b =>
                {
                    b.Navigation("Sessions");

                    b.Navigation("Visitas");
                });
#pragma warning restore 612, 618
        }
    }
}
