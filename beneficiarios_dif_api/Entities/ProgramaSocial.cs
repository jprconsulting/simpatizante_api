﻿namespace beneficiarios_dif_api.Entities
{
    public class ProgramaSocial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Votante> Votantes { get; set; }
    }
}