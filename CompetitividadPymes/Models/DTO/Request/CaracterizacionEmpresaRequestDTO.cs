namespace CompetitividadPymes.Models.DTO.Request
{
    public class CaracterizacionEmpresaRequestDTO
    {
        public string NombreEmpresa { get; set; } = null!;

        public string Ciudad { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string? Telefono { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string TiempoMercado { get; set; } = null!;

        public string ClasificacionEmpresa { get; set; } = null!;

    }
}
