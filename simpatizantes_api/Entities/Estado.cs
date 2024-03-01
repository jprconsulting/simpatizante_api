﻿namespace simpatizantes_api.Entities
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Pais Pais { get; set; } 
        public List <Distrito> Distritos { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
    }
}
