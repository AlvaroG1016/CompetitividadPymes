namespace CompetitividadPymes.Models.DTO.Response
{
    public class ResultadoFactorDTO
    {
        public int IdFactor { get; set; }
        public string NombreFactor { get; set; } = string.Empty;
        public decimal PuntajeObtenido { get; set; }
        public decimal PuntajeMaximo { get; set; }
        public decimal PorcentajeFactor { get; set; }
        public decimal PesoFactor { get; set; }
        public decimal ContribucionFinal { get; set; }
        public int CantidadVariables { get; set; }
        public List<ResultadoVariableDTO> Variables { get; set; } = new();
    }
}
