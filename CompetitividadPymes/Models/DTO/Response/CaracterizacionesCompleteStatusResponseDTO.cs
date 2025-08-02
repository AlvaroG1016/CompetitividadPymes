namespace CompetitividadPymes.Models.DTO.Response
{
    public class CaracterizacionesCompleteStatusResponseDTO
    {
        public bool TodasCompletas { get; set; }
        public CaracterizacionStatusResponseDTO CaracterizacionEmpresa { get; set; }
        public CaracterizacionStatusResponseDTO CaracterizacionUsuario { get; set; }
        public bool PuedenEmpezarFactores { get; set; }
        public string Mensaje { get; set; }
    }
}
