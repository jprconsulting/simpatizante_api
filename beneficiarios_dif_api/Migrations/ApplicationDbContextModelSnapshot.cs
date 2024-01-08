﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using beneficiarios_dif_api;

#nullable disable

namespace beneficiariosdifapi.Migrations
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

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Candidato", b =>
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

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Foto")
                        .HasColumnType("longtext");

                    b.Property<int?>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .HasColumnType("longtext");

                    b.Property<int?>("SeccionId")
                        .HasColumnType("int");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Sobrenombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("SeccionId");

                    b.ToTable("Candidatos");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Claim", b =>
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

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Indicador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Indicadores");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.ProgramaSocial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ProgramasSociales");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreRol")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Rols");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Seccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Secciones");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("longtext");

                    b.Property<string>("Correo")
                        .HasColumnType("longtext");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Visita", b =>
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

                    b.Property<int?>("VotanteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VotanteId");

                    b.ToTable("Visitas");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Votante", b =>
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

                    b.Property<string>("Domicilio")
                        .HasColumnType("longtext");

                    b.Property<int?>("EstadoId")
                        .HasColumnType("int");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Folio")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Latitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Longitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .HasColumnType("longtext");

                    b.Property<int?>("SeccionId")
                        .HasColumnType("int");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("SeccionId");

                    b.ToTable("Votantes");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Candidato", b =>
                {
                    b.HasOne("beneficiarios_dif_api.Entities.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId");

                    b.HasOne("beneficiarios_dif_api.Entities.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId");

                    b.HasOne("beneficiarios_dif_api.Entities.Seccion", "Seccion")
                        .WithMany()
                        .HasForeignKey("SeccionId");

                    b.Navigation("Cargo");

                    b.Navigation("Municipio");

                    b.Navigation("Seccion");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Claim", b =>
                {
                    b.HasOne("beneficiarios_dif_api.Entities.Rol", "Rol")
                        .WithMany("Claims")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Usuario", b =>
                {
                    b.HasOne("beneficiarios_dif_api.Entities.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Visita", b =>
                {
                    b.HasOne("beneficiarios_dif_api.Entities.Votante", "Votante")
                        .WithMany()
                        .HasForeignKey("VotanteId");

                    b.Navigation("Votante");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Votante", b =>
                {
                    b.HasOne("beneficiarios_dif_api.Entities.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");

                    b.HasOne("beneficiarios_dif_api.Entities.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId");

                    b.HasOne("beneficiarios_dif_api.Entities.Seccion", "Seccion")
                        .WithMany()
                        .HasForeignKey("SeccionId");

                    b.Navigation("Estado");

                    b.Navigation("Municipio");

                    b.Navigation("Seccion");
                });

            modelBuilder.Entity("beneficiarios_dif_api.Entities.Rol", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
