using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;

namespace beneficiarios_dif_api.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // source, destination
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<Cargo, CargoDTO>();
            CreateMap<CargoDTO, Cargo>();

            CreateMap<Incidencia, IncidenciaDTO>()
                .ForMember(dest => dest.TipoIncidencia, opt => opt.MapFrom(src => src.TipoIncidencia))
                .ForMember(dest => dest.Casilla, opt => opt.MapFrom(src => src.Casilla));

            CreateMap<IncidenciaDTO, Incidencia>();

            CreateMap<Candidato, CandidatoDTO>()
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
                .ForMember(dest => dest.StrFechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"));

            CreateMap<CandidatoDTO, Candidato>();

            CreateMap<Operador, OperadorDTO>()
                .ForMember(dest => dest.Seccion, opt => opt.MapFrom(src => src.Seccion))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
                .ForMember(dest => dest.StrFechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"));

            CreateMap<OperadorDTO, Operador>();

            CreateMap<Casilla, CasillaDTO>();
            CreateMap<CasillaDTO, Casilla>();

            CreateMap<ProgramaSocial, ProgramaSocialDTO>();
            CreateMap<ProgramaSocialDTO, ProgramaSocial>();

            CreateMap<Municipio, MunicipioDTO>();
            CreateMap<MunicipioDTO, Municipio>();

            CreateMap<Estado, EstadoDTO>();
            CreateMap<EstadoDTO, Estado>();

            CreateMap<TipoIncidencia, IndicadorDTO>();
            CreateMap<IndicadorDTO, TipoIncidencia>();

            CreateMap<Seccion, SeccionDTO>();
            CreateMap<SeccionDTO, Seccion>();

            CreateMap<VotoDTO, Voto>();
            CreateMap<Voto, VotoDTO>()
            .ForMember(dest => dest.Votante, opt => opt.MapFrom(src => src.Votante));

            CreateMap<VotanteDTO, Votante>();
            CreateMap<Votante, VotanteDTO>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
            .ForMember(dest => dest.ProgramaSocial, opt => opt.MapFrom(src => src.ProgramaSocial))
            .ForMember(dest => dest.Seccion, opt => opt.MapFrom(src => src.Seccion))
            .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"));


            CreateMap<VisitaDTO, Visita>();
            CreateMap<Visita, VisitaDTO>()
                  .ForMember(dest => dest.StrFechaHoraVisita, opt => opt.MapFrom(src => $"{src.FechaHoraVisita:dd/MM/yyyy}"))
                  .ForMember(dest => dest.Votante, opt => opt.MapFrom(src => src.Votante));

            CreateMap<Rol, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id));


        }
    }
}
