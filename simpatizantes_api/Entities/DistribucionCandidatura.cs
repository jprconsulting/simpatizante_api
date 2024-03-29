﻿namespace simpatizantes_api.Entities
{
    public class DistribucionCandidatura
    {
        public int Id { get; set; }
        public TipoEleccion TipoEleccion { get; set; }
        public int? EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public int? DistritoId { get; set; }
        public Distrito? Distrito { get; set; }
        public int? MunicipioId { get; set; }
        public Municipio? Municipio { get; set; }
        public int? ComunidadId { get; set; }
        public Comunidad? Comunidad { get; set; }
        public string Partidos { get; set; }
        public string Coalicion { get; set; }
        public string Comun { get; set; }
        public string Independiente { get; set; }

        public List<DistribucionOrdenada> DistribucionesOrdenadas { get; set; }
        public List<ResultadoCandidatura> ResultadosCandidaturas { get; set; }

    }
}
