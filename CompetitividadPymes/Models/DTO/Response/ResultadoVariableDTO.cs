namespace CompetitividadPymes.Models.DTO.Response
{
    public class ResultadoVariableDTO
    {
        public int IdVariable { get; set; }
        public string NombreVariable { get; set; } = string.Empty;
        public int IdFactor { get; set; }
        public decimal PuntajeObtenido { get; set; }
        public decimal PuntajeMaximo { get; set; }
        public decimal PorcentajeVariable { get; set; }
        public decimal PesoVariable { get; set; }
        public decimal ContribucionFinal { get; set; }
        public int CantidadPreguntas { get; set; }
    }
}
