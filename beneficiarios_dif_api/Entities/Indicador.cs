using System.ComponentModel.DataAnnotations;

namespace beneficiarios_dif_api.Entities
{
    public class Indicador
    {        
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
    }
}
