using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;

namespace simpatizantes_api.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        private string[] SplitPartidos(string partidos)
        {
            if (!string.IsNullOrEmpty(partidos))
            {
                return partidos.Split(',');
            }
            else
            {
                return new string[0]; 
            }
        }

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

            CreateMap<Promotor, PromotorDTO>();
            CreateMap<PromotorDTO, Promotor>();

            CreateMap<Cargo, CargoDTO>();
            CreateMap<CargoDTO, Cargo>();

            CreateMap<Pais, PaisDTO>();
            CreateMap<PaisDTO, Pais>();

            CreateMap<Comunidad, ComunidadDTO>();
            CreateMap<ComunidadDTO, Comunidad>();

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

            CreateMap<Distrito, DistritoDTO>();
            CreateMap<DistritoDTO, Distrito>();

            CreateMap<TipoEleccion, TipoEleccionDTO>();
            CreateMap<TipoEleccionDTO, TipoEleccion>();

            CreateMap<TipoAgrupacionPolitica, TipoAgrupacionPoliticaDTO>();
            CreateMap<TipoAgrupacionPoliticaDTO, TipoAgrupacionPolitica>();

            CreateMap<Candidatura, CandidaturaDTO>()
                .ForMember(dest => dest.TipoAgrupacionPolitica, opt => opt.MapFrom(src => src.TipoAgrupacionPolitica))
                .ForMember(dest => dest.Partidos, opt => opt.MapFrom(src => SplitPartidos(src.Partidos)));
            CreateMap<CandidaturaDTO, Candidatura>()
                .ForMember(dest => dest.Partidos, opt => opt.MapFrom(src => string.Join(",", src.Partidos)));

            CreateMap<CombinacionDTO, Combinacion>();
            CreateMap<Combinacion, CombinacionDTO>()
            .ForMember(dest => dest.Candidatura, opt => opt.MapFrom(src => src.Candidatura));

            CreateMap<ResultadoCandidaturaDTO, ResultadoCandidatura>();
            CreateMap<ResultadoCandidatura, ResultadoCandidaturaDTO>()
            .ForMember(dest => dest.ActaEscrutinio, opt => opt.MapFrom(src => src.ActaEscrutinio))
            .ForMember(dest => dest.DistribucionCandidatura, opt => opt.MapFrom(src => src.DistribucionCandidatura))
            .ForMember(dest => dest.Candidatura, opt => opt.MapFrom(src => src.Candidatura))
            .ForMember(dest => dest.Combinacion, opt => opt.MapFrom(src => src.Combinacion));

            CreateMap<DistribucionOrdenadaDTO, DistribucionOrdenada>();
            CreateMap<DistribucionOrdenada, DistribucionOrdenadaDTO>()
            .ForMember(dest => dest.DistribucionCandidatura, opt => opt.MapFrom(src => src.DistribucionCandidatura))
            .ForMember(dest => dest.TipoAgrupacionPolitica, opt => opt.MapFrom(src => src.TipoAgrupacionPolitica))
            .ForMember(dest => dest.Candidatura, opt => opt.MapFrom(src => src.Candidatura))
            .ForMember(dest => dest.Combinacion, opt => opt.MapFrom(src => src.Combinacion));

            CreateMap<DistribucionCandidaturaDTO, DistribucionCandidatura>();
            CreateMap<DistribucionCandidatura, DistribucionCandidaturaDTO>()
            .ForMember(dest => dest.TipoEleccion, opt => opt.MapFrom(src => src.TipoEleccion))
            .ForMember(dest => dest.Candidatura, opt => opt.MapFrom(src => src.Candidatura))
            .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
            .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio))
            .ForMember(dest => dest.Comunidad, opt => opt.MapFrom(src => src.Comunidad));

            CreateMap<ActaEscrutinioDTO, ActaEscrutinio>();
            CreateMap<ActaEscrutinio, ActaEscrutinioDTO>()
            .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
            .ForMember(dest => dest.Municipio, opt => opt.MapFrom(src => src.Municipio))
            .ForMember(dest => dest.Seccion, opt => opt.MapFrom(src => src.Seccion))
            .ForMember(dest => dest.Casilla, opt => opt.MapFrom(src => src.Casilla))
            .ForMember(dest => dest.TipoEleccion, opt => opt.MapFrom(src => src.TipoEleccion));

            CreateMap<VotoDTO, Voto>();
            CreateMap<Voto, VotoDTO>()
            .ForMember(dest => dest.Simpatizante, opt => opt.MapFrom(src => src.Simpatizante));

            CreateMap<SimpatizanteDTO, Simpatizante>();
            CreateMap<Simpatizante, SimpatizanteDTO>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombres} {src.ApellidoPaterno} {src.ApellidoMaterno}"))
                .ForMember(dest => dest.ProgramaSocial, opt => opt.MapFrom(src => src.ProgramaSocial))
                .ForMember(dest => dest.Promotor, opt => opt.MapFrom(src => src.Promotor))
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
