﻿using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;

namespace simpatizantes_api.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // source, destination
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Candidato, opt => opt.MapFrom(src => src.Candidato))
                .ForMember(dest => dest.Operador, opt => opt.MapFrom(src => src.Operador))
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<Cargo, CargoDTO>();
            CreateMap<CargoDTO, Cargo>();

            CreateMap<Genero, GeneroDTO>();
            CreateMap<GeneroDTO, Genero>();

            CreateMap<Incidencia, IncidenciaDTO>()
                .ForMember(dest => dest.TipoIncidencia, opt => opt.MapFrom(src => src.TipoIncidencia))
                .ForMember(dest => dest.Casilla, opt => opt.MapFrom(src => src.Casilla));

            CreateMap<IncidenciaDTO, Incidencia>();

            CreateMap<Candidato, CandidatoDTO>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
                .ForMember(dest => dest.StrFechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"));

            CreateMap<CandidatoDTO, Candidato>();

            CreateMap<Operador, OperadorDTO>()
                .ForMember(dest => dest.Candidato, opt => opt.MapFrom(src => src.Candidato))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
                .ForMember(dest => dest.StrFechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"));
            CreateMap<OperadorDTO, Operador>();

            CreateMap<OperadorSeccion, OperadorSeccionDTO>()
                .ForMember(dest => dest.Seccion, opt => opt.MapFrom(src => src.Seccion));
            CreateMap<OperadorSeccionDTO, OperadorSeccion>();

            CreateMap<Casilla, CasillaDTO>();
            CreateMap<CasillaDTO, Casilla>();

            CreateMap<ProgramaSocial, ProgramaSocialDTO>();
            CreateMap<ProgramaSocialDTO, ProgramaSocial>();

            CreateMap<Municipio, MunicipioDTO>();
            CreateMap<MunicipioDTO, Municipio>();

            CreateMap<Estado, EstadoDTO>();
            CreateMap<EstadoDTO, Estado>();

            CreateMap<TipoIncidencia, TipoIncidenciaDTO>();
            CreateMap<TipoIncidenciaDTO, TipoIncidencia>();

            CreateMap<Seccion, SeccionDTO>();
            CreateMap<SeccionDTO, Seccion>();

            CreateMap<VotoDTO, Voto>();
            CreateMap<Voto, VotoDTO>()
            .ForMember(dest => dest.Simpatizante, opt => opt.MapFrom(src => src.Simpatizante));

            CreateMap<SimpatizanteDTO, Simpatizante>();
            CreateMap<Simpatizante, SimpatizanteDTO>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
                .ForMember(dest => dest.ProgramaSocial, opt => opt.MapFrom(src => src.ProgramaSocial))
                .ForMember(dest => dest.Seccion, opt => opt.MapFrom(src => src.Seccion))
                .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.StrFechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"));


            CreateMap<VisitaDTO, Visita>();
            CreateMap<Visita, VisitaDTO>()
                  .ForMember(dest => dest.StrFechaHoraVisita, opt => opt.MapFrom(src => $"{src.FechaHoraVisita:dd/MM/yyyy}"))
                  .ForMember(dest => dest.Simpatizante, opt => opt.MapFrom(src => src.Simpatizante));

            CreateMap<Claim, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Rol.Id))
                .IncludeMembers(src => src.Rol);

            CreateMap<Rol, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id));


        }
    }
}
