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
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.ApellidoPaterno} {src.ApellidoMaterno}"));      

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<Incidencia, IncidenciaDTO>();
            CreateMap<IncidenciaDTO, Incidencia>();

            CreateMap<Casilla, CasillaDTO>();
            CreateMap<CasillaDTO, Casilla>();

            CreateMap<ProgramaSocial, ProgramaSocialDTO>();
            CreateMap<ProgramaSocialDTO, ProgramaSocial>();

            CreateMap<Municipio, MunicipioDTO>();
            CreateMap<MunicipioDTO, Municipio>();

            CreateMap<Estado, EstadoDTO>();
            CreateMap<EstadoDTO, Estado>();

            CreateMap<Indicador, IndicadorDTO>();
            CreateMap<IndicadorDTO, Indicador>();

            CreateMap<Seccion, SeccionDTO>();
            CreateMap<SeccionDTO, Seccion>();

            CreateMap<VotanteDTO, Votante>();
            CreateMap<Votante, VotanteDTO>();           

            CreateMap<VisitaDTO, Visita>();
            CreateMap<Visita, VisitaDTO>()
                  .ForMember(dest => dest.StrFechaHoraVisita, opt => opt.MapFrom(src => $"{src.FechaHoraVisita:dd/MM/yyyy H:mm}"));

            CreateMap<Rol, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id));


        }
    }
}
