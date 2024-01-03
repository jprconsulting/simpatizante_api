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
                .ForMember(dest => dest.AreaAdscripcion, opt => opt.MapFrom(src => src.AreaAdscripcion))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.ApellidoPaterno} {src.ApellidoMaterno}"));      

            CreateMap<AreaAdscripcion, AreaAdscripcionDTO>();
            CreateMap<AreaAdscripcionDTO, AreaAdscripcion>();

            CreateMap<Rol, RolDTO>();
            CreateMap<RolDTO, Rol>();

            CreateMap<BeneficiarioDTO, Beneficiario>();
            CreateMap<Beneficiario, BeneficiarioDTO>()
               .ForMember(dest => dest.ProgramaSocial, opt => opt.MapFrom(src => src.ProgramaSocial))
               .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio))
               .ForMember(dest => dest.StrFechaNacimiento, opt => opt.MapFrom(src => $"{src.FechaNacimiento:dd/MM/yyyy}"))
               .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"));


            CreateMap<ProgramaSocial, ProgramaSocialDTO>();
            CreateMap<ProgramaSocialDTO, ProgramaSocial>();

            CreateMap<Municipio, MunicipioDTO>();
            CreateMap<MunicipioDTO, Municipio>();

            CreateMap<Indicador, IndicadorDTO>();
            CreateMap<IndicadorDTO, Indicador>();

            CreateMap<VisitaDTO, Visita>();
            CreateMap<Visita, VisitaDTO>()
                  .ForMember(dest => dest.StrFechaHoraVisita, opt => opt.MapFrom(src => $"{src.FechaHoraVisita:dd/MM/yyyy H:mm}"));

            CreateMap<Claim, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Rol.Id))
                .IncludeMembers(src => src.Rol);

            CreateMap<Rol, ClaimDTO>()
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.Id));


        }
    }
}
