namespace beneficiarios_dif_api.Entities
{
    public class Votante
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public int Sexo { get; set; }
        public string CURP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Estatus { get; set; }
        public string Folio { get; set; }
        public ProgramaSocial? ProgramaSocial { get; set; }
        public Seccion Seccion { get; set; }
        public Municipio Municipio { get; set; }
        public Estado Estado { get; set; }

    }
}