﻿namespace simpatizantes_api.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Sexo { get; set; }
        public string Sobrenombre { get; set; }
        public string Foto { get; set; }
        public string Emblema { get; set; }
        public bool Estatus { get; set; }
        public Cargo Cargo { get; set; }
        public List <Operador> Operador { get; set; }
        public Usuario? Usuario { get; set; }


    }
}