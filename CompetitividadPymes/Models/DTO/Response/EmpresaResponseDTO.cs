namespace CompetitividadPymes.Models.DTO.Response
{
    public class EmpresaResponseDTO
    {
        public int IdEmpresa { get; set; }

        public string Nombre { get; set; } = null!;

        public string Sector { get; set; } = null!;

        public string Clasificacion { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime? FechaRegistro { get; set; }
    }
}
