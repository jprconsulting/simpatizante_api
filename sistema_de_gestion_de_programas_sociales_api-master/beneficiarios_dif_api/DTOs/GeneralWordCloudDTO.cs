namespace beneficiarios_dif_api.DTOs
{
    public class GeneralWordCloudDTO
    {
        public List<WordCloudDTO> GeneralWordCloud { get; set; }
        public List<MunicipioWordCloudDTO> WordCloudPorMunicipios { get; set; }
    }
}
