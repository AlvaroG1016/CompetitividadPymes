namespace CompetitividadPymes.Models.DTO.Response
{
    public class CaracterizacionStatusResponseDTO
    {
        public bool Completed { get; set; }
        public int Percentage { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string Estado { get; set; }
        public string Mensaje { get; set; }
    }
}
