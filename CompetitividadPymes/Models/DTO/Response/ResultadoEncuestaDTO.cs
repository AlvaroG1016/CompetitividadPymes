namespace CompetitividadPymes.Models.DTO.Response
{
    public class ResultadoEncuestaDTO
    {
        public int IdEncuesta { get; set; }
        public List<ResultadoFactorDTO> ResultadosPorFactor { get; set; } = new();
        public List<ResultadoVariableDTO> ResultadosPorVariable { get; set; } = new();
        public decimal PuntajeFinalTotal { get; set; }
        public decimal PorcentajeFinalTotal { get; set; }
        public DateTime FechaCalculo { get; set; }

        // Estadísticas adicionales
        public int TotalFactores { get; set; }
        public int TotalVariables { get; set; }
        public int TotalPreguntas { get; set; }
    }
}
