namespace simpatizantes_api.Entities
{
    public class Simpatizante
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public string CURP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Estatus { get; set; }
        public string ClaveElector { get; set; }
        public Genero Genero { get; set; }
        public ProgramaSocial ProgramaSocial { get; set; }
        public Seccion Seccion { get; set; }
        public Municipio Municipio { get; set; }
        public Estado Estado { get; set; }
        public Operador Operador { get; set; }
        public List<Visita> Visitas { get; set; }
        public List<Voto> Votos { get; set; }

    }
}