namespace simpatizantes_api.Entities
{
    public class Operador
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public List<OperadorSeccion> OperadorSecciones { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
