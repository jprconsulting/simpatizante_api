﻿using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class CandidaturaDTO
    {
        public int? Id { get; set; }
        public TipoAgrupacionPoliticaDTO TipoAgrupacionPolitica { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string ImagenBase64 { get; set; }
        public string Acronimo { get; set; }
        public bool Estatus { get; set; }
        public List<CandidaturaDTO> Partidos { get; set; }
        public int Orden { get; set; }
    }
}
