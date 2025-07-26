namespace CompetitividadPymes.Models.DTO.Response
{
    public class RespuestaPorFactorDTO
    {
        public string IdPregunta { get; set; }
        public int ValorRespuesta { get; set; }
        public string? DescripcionPregunta { get; set; }
        public string? NombreVariable { get; set; }
        public int IdVariable { get; set; }
        public int IdFactor { get; set; }
    }
}
