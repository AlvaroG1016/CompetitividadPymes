namespace CompetitividadPymes.Models.DTO.Response
{
    public class CaracterizacionEmpresaResponseDTO
    {
        public string NombreEmpresa { get; set; } = null!;

        public string Ciudad { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string TiempoMercado { get; set; } = null!;

        public string ClasificacionEmpresa { get; set; } = null!;

        public bool Caracterizado { get; set;}
    }
}
