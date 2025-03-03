namespace CompetitividadPymes.Models.DTO.Response
{
    public class UsuarioResponseDTO
    {
        public int IdUsuario { get; set; }

        public int? IdEmpresa { get; set; }

        public int IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime? FechaRegistro { get; set; }

        public DateTime? UltimoAcceso { get; set; }
    }
}
