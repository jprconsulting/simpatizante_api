﻿namespace simpatizantes_api.Entities
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Candidato> Candidatos { get; set; }
    }
}